using Exchange.Service;
using FluentAssertions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ExchangeCurrency.UnitTest
{
    public class ConvertCryptoToCurrencyTests
    {
        [Fact]
        public void ConvertCryptocurrencyToTraditionalCurrency_WithSpecificCrypto_ReturenListOfCurrencies()
        {
            //Arrange
            var cryptoCurrency = "BTC";
            var expectedConverts = new Dictionary<string, double>()
            {
                { "USD",1.1},
                { "EUR",1.2},
                { "BEL",1.3},
                { "GBP",1.4},
                { "AUD",1.5}
            };

            //Act
            var actual = new ConvertService().ConvertCryptoToTraditional(cryptoCurrency);

            //Assert
            actual.Should().BeEquivalentTo(expectedConverts);

        }
    }
}

