using CryptoExchange.ACL.CoinMarketCapModel;
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
        const string APIKey = @"bd820c50-a4a4-4b76-8b23-a78d07a2ed85";
        const int MAX_RETRIES = 3;
        private readonly AsyncRetryPolicy<RestResponse> _retryPolicy;
    
        public CoinMarketCapAPI()
        {
            _client = new RestClient(@"https://pro-api.coinmarketcap.com/");
            _client.AddDefaultHeader("X-CMC_PRO_API_KEY", APIKey);
            _retryPolicy = Policy
                .HandleResult<RestResponse>(a => a.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                   retryCount: MAX_RETRIES,
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
