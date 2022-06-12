using Exchange.Common.Authentication.interfaces;
using ExchangeCurrency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExchangeCurrency.Web.Controllers
{
    public class HomeController : Controller
    {
        private  string baseUrl ;
        private  string key;
        public HomeController(IAuthentication clientAuthentication)
        {
            baseUrl = clientAuthentication.GetURL();
            key = clientAuthentication.GetAPIKey();
        }
        public async Task<IActionResult> Index([FromQuery] string cryptoSymbol)
        {
            Dictionary<string, double> CurrencyQuotes= new Dictionary<string, double>();
            ExchangeCurrencyModel exchangeCurrencyModel = new ExchangeCurrencyModel();
            if (string.IsNullOrWhiteSpace(cryptoSymbol))
            { return View(); }
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("XApiKey", key);
                client.BaseAddress = new Uri(baseUrl);
                var result = await client.GetAsync("api/Exchange/Crypto?symbol="+cryptoSymbol);

                if (result.IsSuccessStatusCode)
                {

                    var readTask = await result.Content.ReadAsStringAsync();
                    CurrencyQuotes = JsonSerializer.Deserialize<Dictionary<string,double>>(readTask);
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
