using Exchange.Common.Currency;
using Exchange.Common.CustomException;
using Exchange.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeCurrency.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CryptoExchangeService _cryptoExchangeService;

        public ExchangeController(ILogger<HomeController> logger,
                              CryptoExchangeService cryptoExchangeService)
        {
            _logger = logger;
            _cryptoExchangeService = cryptoExchangeService;

        }
        [Route("{symbol}")]
        [HttpGet]
        public async Task<ActionResult<Dictionary<string, double>>> Crypto([FromQuery] string symbol)
        {
            try
            {
                Dictionary<string, double> CurrencyQuotes;
                CurrencyQuotes = await _cryptoExchangeService.ToTraditional(symbol, new TraditionalCurrency());
                return Ok(CurrencyQuotes);
            }
            catch(SymbolNotFoundException ex)
            {
                return BadRequestWithCustomError(ex.Message);
            }
            catch(ServiceUnavailableException)
            {
                return BadRequestWithCustomError("Service is unavailable");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                return new ContentResult() { Content = "Server error", StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
        private ActionResult BadRequestWithCustomError(string error)
        {
            _logger.LogError(error);
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = error;
            return BadRequest();
        }

    }
}
