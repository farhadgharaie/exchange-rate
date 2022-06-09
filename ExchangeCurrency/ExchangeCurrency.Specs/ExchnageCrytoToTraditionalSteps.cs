using System;
using TechTalk.SpecFlow;

namespace ExchangeCurrency.Specs
{
    [Binding]
    public class ExchnageCrytoToTraditionalSteps
    {
        [Given(@"the user entered BTC cryptocurrency symbol")]
        public void GivenTheUserEnteredBTCCryptocurrencySymbol()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"the user press submit")]
        public void WhenTheUserPressSubmit()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be (.*) in screen")]
        public void ThenTheResultShouldBeInScreen(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
