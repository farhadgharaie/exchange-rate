using CryptoExchange.ACL.ExchangeRates.ExchangeRatesModel;
using Exchange.Common.Authentication.interfaces;
using Exchange.Common.Config;
using Exchange.Common.Currency;
using Exchange.Common.CustomException;
using Exchange.Common.interfaces;
using Polly;
using Polly.Retry;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoExchange.ACL.ExchangeRates
{
   
    public class ExchangeRatesAPI: IConvertAll
    {
        private readonly RestClient _client;
        private int _maxRetries ;
        private readonly AsyncRetryPolicy<RestResponse> _retryPolicy;
        private readonly IThirdPartyConfiguration _config;

        public ExchangeRatesAPI(IThirdPartyConfiguration config)
        {
            _config = config;
            _client = new RestClient(_config.GetURL());
            _maxRetries = _config.GetMaximumRetry();
            _retryPolicy = Policy
                .HandleResult<RestResponse>(a => a.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                   retryCount: _maxRetries,
                   sleepDurationProvider: _ => TimeSpan.FromSeconds(1),
                   onRetry: (exception, sleepDuration, attemptNumber, context) =>
                   {
                       throw new ThirdPartyAPIServiceUnavailableException();
                   });
        }

        public async Task<Dictionary<string, double>> ConvertTo(CurrencyModel fromCurrency, IEnumerable<CurrencyModel> convertTo)
        {
            var exchangeTo = convertTo.ToList().Select(a => a.Symbol).ToArray();
            var request = new RestRequest("exchangerates_data/latest", Method.Get);
            request.AddQueryParameter("apikey", _config.GetAPIKey());
            request.AddQueryParameter("base", fromCurrency.Symbol);
            request.AddQueryParameter("symbols", string.Join(",", exchangeTo));

            var respone = await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _client.ExecuteGetAsync(request);
                if (!res.IsSuccessful)
                {
                    throw new NoResponseThirdPartyAPIServiceException();
                }
                return res;
            });
            var result = new Dictionary<string, double>();
            var data = JsonSerializer.Deserialize<ExchangeRateAPIResponse>(respone.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (data.success)
            {
                var rates = data.rates;
                foreach (var rate in rates)
                {
                    result.Add(rate.Key, rate.Value);
                }
            }

            return result;
        }
    }
}

