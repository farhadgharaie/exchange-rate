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
    public class Root
    {
        public bool success { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }
        public string date { get; set; }
        public string rates { get; set; }
    }
    public class ExchangeRatesAPI: IExchangeBaseOnUSD
    {
        private readonly RestClient _client;
        const string APIKey = @"dh6Px535GT55YKup86G5MYRCJbb2X5RN";
        const int MAX_RETRIES = 3;
        private readonly AsyncRetryPolicy<RestResponse> _retryPolicy;

        public ExchangeRatesAPI()
        {
            _client = new RestClient(@"https://api.exchangeratesapi.io/");
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
        public async Task<Dictionary<string, double>> ExchangeBaseOnUSD(string[] exchangeTo)
        {
            var request = new RestRequest("v1/latest", Method.Get);
            request.AddQueryParameter("access_key ", APIKey);
            request.AddQueryParameter("base", "USD");
            request.AddQueryParameter("symbols", string.Join(",",exchangeTo));

            var respone = await _retryPolicy.ExecuteAsync(async () =>
            {
                var res = await _client.ExecuteGetAsync(request);
                if (!res.IsSuccessful)
                {
                    throw new Exception("Service is unavailable.");
                }
                return res;
            });
            var result = new Dictionary<string, double>();
            var data = JsonSerializer.Deserialize<Root>(respone.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (data.success)
            {
                var rates=data.rates.Split(',');
                foreach (var rate in rates)
                {
                    var rateArray = rate.Split(':');
                    double.TryParse(rateArray[1], out double number);
                    result.Add(rateArray[0], number);
                }
            }

            return result;
        }
    }
}

