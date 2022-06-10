using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using Exchange.Common.Currency;

namespace ExchangeCurrency.UnitTest
{
    public class CurrencyTests
    {
        private readonly List<CurrencyModel> fakeCurrencies = new List<CurrencyModel>()
            {
                new CurrencyModel("Test","tst"),
                new CurrencyModel("Test2","tst2"),
                new CurrencyModel("Test3","tst3"),
            };
        [Fact]
        public void IsSymbolExist_WithWrongSymbol_ReturnFalse()
        {
            //Arrange
            string wrongSymbol = "ts";

            //Act
            var currency = new Currency(fakeCurrencies).IsSymbolExist(wrongSymbol);

            //Assert
            currency.Should().BeFalse();
        }
        [Fact]
        public void IsSymbolExist_WithExistedSymbol_ReturnTrue()
        {
            //Arrange
            string existedSymbol = "tst";

            //Act
            var currency = new Currency(fakeCurrencies).IsSymbolExist(existedSymbol);

            //Assert
            currency.Should().BeTrue();
        }
        [Fact]
        public void GetAll_ReturnTheExactListOfInjectedCurrencyModel()
        {
            //Arrange
            List<CurrencyModel> expected = new List<CurrencyModel>()
            {
                new CurrencyModel("Test","tst"),
                new CurrencyModel("Test2","tst2"),
                new CurrencyModel("Test3","tst3"),
            };

            //Act
            var currency = new Currency(fakeCurrencies).getAll();

            //Assert
            currency.Should().BeEquivalentTo(expected);

        }
    }
}
