using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Exchange.Common.Currency;

namespace ExchangeCurrency.UnitTest
{
    public class FakeCyrrency : CurrencyTemplate
    {
        public override IEnumerable<CurrencyModel> Select()
        {
            return new List<CurrencyModel>()
            {
                 new CurrencyModel("Test","tst"),
                new CurrencyModel("Test2","tst2"),
                new CurrencyModel("Test3","tst3"),
            };
        }
    }

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
            var currency = new FakeCyrrency().IsSymbolExist(wrongSymbol);

            //Assert
            currency.Should().BeFalse();
        }
        [Fact]
        public void IsSymbolExist_WithExistedSymbol_ReturnTrue()
        {
            //Arrange
            string existedSymbol = "tst";

            //Act
            var currency = new FakeCyrrency().IsSymbolExist(existedSymbol);

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
            var currency = new FakeCyrrency().GetAll();

            //Assert
            currency.Should().BeEquivalentTo(expected);

        }
    }
}
