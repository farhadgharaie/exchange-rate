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
                new CurrencyModel("testXCV","xcv")
            };
        public FakeInputCurreny() : base(currencies)
        {

        }
    }
    public class FakeOutputCurrency : Currency
    {
        private static List<CurrencyModel> currencies = new List<CurrencyModel>
            {
                new CurrencyModel("test1","tt1"),
                new CurrencyModel("test2","tt2")
            };
        public FakeOutputCurrency() : base(currencies)
        {

        }
    }
    public class ExchangeCryptoToCurrencyTests
    {
        private readonly Mock<ICurrency> convertToStub = new Mock<ICurrency>();
        private readonly Mock<ICurrency> inputStub = new Mock<ICurrency>();
        private readonly Mock<ICryptoExchange> cryptoExchangeMoq = new Mock<ICryptoExchange>();
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


            cryptoExchangeMoq.Setup(a => a.ExchangeToTraditional(It.IsAny<string>(), convertToStub.Object))
                .Returns(new Dictionary<string, double>()
                                        {
                                            { "tt2",1},
                                            { "tt1",1}
                                        });

            //Act
            var actual = new CryptoExchangeService(inputStub.Object, cryptoExchangeMoq.Object)
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

            cryptoExchangeMoq.Setup(a => a.ExchangeToTraditional(cryptoCurrency, convertToStub.Object))
                .Returns(It.IsAny<Dictionary<string,double>>());

            //Act
            Action act = () => new CryptoExchangeService(inputStub.Object, cryptoExchangeMoq.Object)
                                .ToTraditional(cryptoCurrency, convertToStub.Object);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Symbol not found");
        }
    }
}

