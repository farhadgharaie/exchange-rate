using Exchange.Common.Currency;
using Exchange.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeCurrency.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly CryptoExchangeService _cryptoExchangeService;
        public Dictionary<string, double> CurrencyQuotes;
        public IndexModel(ILogger<IndexModel> logger,
                          CryptoExchangeService cryptoExchangeService)
        {
            _logger = logger;
            _cryptoExchangeService = cryptoExchangeService;
        }
        public string Error { get; private set; }
        [BindProperty(SupportsGet =true)]
        public string CryptoSymbol { get; set; }
        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(CryptoSymbol)) {
                try
                {
                    CurrencyQuotes = await _cryptoExchangeService.ToTraditional(CryptoSymbol, new TraditionalCurrency());
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }               
            }
        }
        public IActionResult OnPost()
        {
            if(string.IsNullOrEmpty(CryptoSymbol))
            {
                ModelState.AddModelError("CryptoSymbol", "Symbol is required.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("/index", new { CryptoSymbol });
        }
    }
}
