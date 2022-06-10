using Exchange.Common.Config;
using Exchange.Common.interfaces;
using ExchangeCurrency.Specs.Fakes;
using ExchangeCurrency.Specs.Setup;
using ExchangeCurrency.Web;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace ExchangeCurrency.Specs
{
   
    public class FakeConfig
    {
        public string URL { get; set; } = "test";
        public string ApiKey { get; set; } = "test";
        public int MaximumRetries { get; set; } = 3;
    }
    [Binding]
    public class ExchnageCrytoToTraditionalSteps : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private HttpClient _client { get; set; }
        private string content;
        private string symbol;


        public ExchnageCrytoToTraditionalSteps(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.WithWebHostBuilder(builder=> {
                builder.ConfigureTestServices(service =>
                {
                    service.AddScoped<ICryptoToUSD, FakeCryptotoUSD>();
                    service.AddScoped<IExchangeBaseOnUSD, FakeExchangeBaseOnUSD>();
                }
                );
            }). CreateClient();
        }
        [Given(@"the user entered BTC cryptocurrency symbol")]
        public void GivenTheUserEnteredBTCCryptocurrencySymbol()
        {
            symbol = "BTC";
        }

        [When(@"the user press submit")]
        public async Task WhenTheUserPressSubmit()
        {
            string url = ("/api/exchange/crypto?symbol="+symbol);
            var response = await _client.GetAsync(url);
            content=response.Content.ReadAsStringAsync().Result;
        }

        [Then(@"the result should be (.*) in screen")]
        public void  ThenTheResultShouldBeInScreen(string currency)
        {
            var data = JsonSerializer.Deserialize<Dictionary<string,double>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var actual = data.ContainsKey(currency);
            actual.Should().BeTrue();
        }
    }
}
