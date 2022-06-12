using CryptoExchange.ACL.CoinMarketCapModel;
using Exchange.Common.Authentication.interfaces;
using Exchange.Common.Config;
using Exchange.Common.CustomException;
using Exchange.Common.interfaces;
using Polly;
using Polly.Retry;
using RestSharp;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CryptoExchange.ACL.CoinMarketCap
{
    public class CoinMarketCapAPI : ICryptoToUSD
    {
        private readonly RestClient _client;
        private int _maxRetries ;
        private readonly AsyncRetryPolicy<RestResponse> _retryPolicy;
        private readonly IThirdPartyConfiguration _config;

        public CoinMarketCapAPI(IThirdPartyConfiguration config)
        {
            _config = config;
            _maxRetries = _config.GetMaximumRetry();
            _client = new RestClient(_config.GetURL());
            _client.AddDefaultHeader("X-CMC_PRO_API_KEY", _config.GetAPIKey());
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
        public async Task<double> GetUSDQuoteAsync(string cryptoSymbol)
        {
            var request = new RestRequest("v2/tools/price-conversion", Method.Get);
            request.AddQueryParameter("amount", 1);
            request.AddQueryParameter("symbol", cryptoSymbol);
            request.AddQueryParameter("convert", "USD");
            
            var respone= await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _client.ExecuteGetAsync(request);
                if (!res.IsSuccessful)
                {
                    throw new ThirdPartyAPIServiceUnavailableException();
                }
                return res;
            });
            var priceConversionData = JsonSerializer.Deserialize<PriceConversionDto>(respone.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return priceConversionData.data[0].quote.USD.price;
        }
    }
}
