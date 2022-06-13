using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Exchange.Common.Currency;
using System.Threading.Tasks;
using Exchange.Common.CustomException;
using System;

namespace ExchangeCurrency.UnitTest
{
    public class FakeCurrency : CurrencyTemplate
    {
        public override Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency)
        {
            throw new NotImplementedException();
        }

        public override Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency)
        {
            throw new NotImplementedException();
        }

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
        [Fact]
        public void IsSymbolExist_WithWrongSymbol_ReturnFalse()
        {
            //Arrange
            string wrongSymbol = "ts";

            //Act
            var currency = new FakeCurrency().IsSymbolExist(wrongSymbol);

            //Assert
            currency.Should().BeFalse();
        }
        [Fact]
        public void IsSymbolExist_WithExistedSymbol_ReturnTrue()
        {
            //Arrange
            string existedSymbol = "tst";

            //Act
            var currency = new FakeCurrency().IsSymbolExist(existedSymbol);

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
            var currency = new FakeCurrency().GetAll();

            //Assert
            currency.Should().BeEquivalentTo(expected);

        }
        [Fact]
        public void Get_WithInListSymbol_ReturnTheCurrencyModel()
        {
            //Arrange
            CurrencyModel expected = new CurrencyModel("Test", "tst");
            string symbol = "tst";
            //Act
            var currency = new FakeCurrency().Get(symbol);

            //Assert
            currency.Should().BeEquivalentTo(expected);

        }
        [Fact]
        public void Get_WithNotInListSymbol_ReturenSymbolNotFoundException()
        {
            ///Arrange
            string notExistedSymbol = "notExistedSymbol";

            //Act
            Action act = () => new FakeCurrency().Get(notExistedSymbol);

            //Assert
            act.Should().Throw<SymbolNotFoundException>();
        }
        [Fact]
        public void Get_WithNullSymbol_ReturenymbolNotProvidedException()
        {
            //Arrange
            string nullInput = null;

            //Act
            Action act = () => new FakeCurrency().Get(nullInput);

            //Assert
            act.Should().Throw<SymbolNotProvidedException>();
        }
    }
}
