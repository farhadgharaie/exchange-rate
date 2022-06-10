using ExchangeCurrency.Specs.Hooks;
using ExchangeCurrency.Web;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace ExchangeCurrency.Specs
{
    [Binding]
    public class ExchnageCrytoToTraditionalSteps
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private HttpClient _client { get; set; }
        private string content;
        private string symbol;


        public ExchnageCrytoToTraditionalSteps(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
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
