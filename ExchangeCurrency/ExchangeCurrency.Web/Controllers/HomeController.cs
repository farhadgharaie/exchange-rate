using Exchange.Common.Currency;
using Exchange.Service;
using ExchangeCurrency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeCurrency.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string BaseUrl = "https://localhost:44357/";

        public HomeController()
        {
           
        }
        public async Task<IActionResult> Index([FromQuery] string cryptoSymbol)
        {
            Dictionary<string, double> CurrencyQuotes= new Dictionary<string, double>();
            ExchangeCurrencyModel exchangeCurrencyModel = new ExchangeCurrencyModel();
            if (string.IsNullOrWhiteSpace(cryptoSymbol))
            { return View(); }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var responseTask = client.GetAsync("api/Exchange/Crypto?symbol="+cryptoSymbol);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readTask = await result.Content.ReadAsStringAsync();
                    //readTask.Wait();
                    CurrencyQuotes = JsonSerializer.Deserialize<Dictionary<string, double>>(readTask);
                    exchangeCurrencyModel.Symbol = cryptoSymbol.ToUpper();
                    exchangeCurrencyModel.LastUpdateDate = DateTime.Now;
                    exchangeCurrencyModel.Exchange = CurrencyQuotes;
                }
                else if(result.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Error= result.ReasonPhrase;
                    return View();
                }
                else
                {
                    ViewBag.Error = "Oops, there is a server error. please try later.";
                    return View();
                }
            }
            return View(exchangeCurrencyModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
