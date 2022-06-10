using Exchange.Common.interfaces;
using ExchangeCurrency.Specs.Fakes;
using ExchangeCurrency.Specs.Setup;
using ExchangeCurrency.Web;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace ExchangeCurrency.Specs
{
   
    
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
            content=response.Content.ReadAsStringAsync().Result.ToLower();
        }

        [Then(@"the result should be (.*) in screen")]
        public void  ThenTheResultShouldBeInScreen(string p0)
        {
            var actual = content.ToLower().Contains(p0.ToLower());
            actual.Should().BeTrue();
        }
    }
}
