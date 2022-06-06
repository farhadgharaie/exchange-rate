using Exchange.Service;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ExchangeCurrency.UnitTest
{
    public class FakeInputCurreny : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
            {
                new CurrencyModel
                {
                    Name="testXCV",
                    Symbol="xcv"
                }
            };
        public FakeInputCurreny(): base(currencies)
        {

        }
    }
    public class FakeOutputCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
            {
                new CurrencyModel
                {
                    Name="test1",
                    Symbol="tt1"
                },
                new CurrencyModel
                {
                    Name="test2",
                    Symbol="tt2"
                }
            };
        public FakeOutputCurrency() : base(currencies)
        {
            
        }
    }
    public class ExchangeCryptoToCurrencyTests
    {
        private readonly Mock<ICurrency> convertToStub = new Mock<ICurrency>();
        private readonly Mock<ICurrency> inputStub = new Mock<ICurrency>();
        [Fact]
        public void ExchangeCryptoToTraditional_WithSpecificCrypto_ReturenListOfCurrencies()
        {
            //Arrange
            var expectedConverts = new Dictionary<string, double>()
            {
                { "tt2",1},
                { "tt1",1}
            };

            inputStub.Setup(a => a.IsSymbolExist(It.IsAny<string>()))
               .Returns(true);

            convertToStub.Setup(a => a.getAll())
               .Returns(new FakeOutputCurrency().getAll());

            //Act
            var actual = new CryptoExchangeService(inputStub.Object)
                                .ToTraditional(It.IsAny<string>(), convertToStub.Object);

            //Assert
            actual.Should().BeEquivalentTo(expectedConverts);

        }
        [Fact]
        public void ExchangeCryptoToTraditional_WithWrongCrypto_ReturenBaseNotFoundException()
        {
            //Arrange
            var cryptoCurrency = "wrongCoin";

            convertToStub.Setup(a => a.getAll())
                .Returns(It.IsAny<IEnumerable<CurrencyModel>>());

            inputStub.Setup(a => a.IsSymbolExist(It.IsAny<string>()))
               .Returns(false);

            //Act
            Action act = () => new CryptoExchangeService(inputStub.Object)
                                .ToTraditional(cryptoCurrency, convertToStub.Object);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Symbol not found");
        }
    }
}

