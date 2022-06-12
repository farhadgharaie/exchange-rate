using CryptoExchange.ACL.ExchangeRates.ExchangeRatesModel;
using Exchange.Common.Config;
using Exchange.Common.CustomException;
using Exchange.Common.interfaces;
using Polly;
using Polly.Retry;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoExchange.ACL.ExchangeRates
{
   
    public class ExchangeRatesAPI: IExchangeBaseOnUSD
    {
        private readonly RestClient _client;
        private int _maxRetries ;
        private readonly AsyncRetryPolicy<RestResponse> _retryPolicy;
        private readonly ThirdPartAPIConfig _config;

        public ExchangeRatesAPI(ThirdPartAPIConfig config)
        {
            _config = config;
            _client = new RestClient(_config.URL);
            _maxRetries = _config.MaximumRetries;
            _retryPolicy = Policy
                .HandleResult<RestResponse>(a => a.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                   retryCount: _maxRetries,
                   sleepDurationProvider: _ => TimeSpan.FromSeconds(1),
                   onRetry: (exception, sleepDuration, attemptNumber, context) =>
                   {
                       //Log($"Too many requests. Retrying in {sleepDuration}. {attemptNumber} / {MAX_RETRIES}");
                   });
        }
        public async Task<Dictionary<string, double>> ExchangeBaseOnUSD(string[] exchangeTo)
        {
            var request = new RestRequest("exchangerates_data/latest", Method.Get);
            request.AddQueryParameter("apikey", _config.ApiKey);
            request.AddQueryParameter("base", "USD");
            request.AddQueryParameter("symbols", string.Join(",",exchangeTo));

            var respone = await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _client.ExecuteGetAsync(request);
                if (!res.IsSuccessful)
                {
                    throw new ThirdPartyAPIServiceUnavailableException();
                }
                return res;
            });
            var result = new Dictionary<string, double>();
            var data = JsonSerializer.Deserialize<ExchangeRateAPIResponse>(respone.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (data.success)
            {
                var rates=data.rates;
                foreach (var rate in rates)
                {
                    result.Add(rate.Key, rate.Value);
                }
            }

            return result;
        }
    }
}

