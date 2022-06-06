using Exchange.Service;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ExchangeCurrency.UnitTest
{
    public class FakeOutputCurrency
    {
        public List<Currency> testOutput()
        {
            return new List<Currency>
            {
                new Currency
                {
                    Name="test1",
                    Symbol="tt1"
                }
            };
        }
        public List<Currency> testInput()
        {
            return new List<Currency>
            {
                new Currency
                {
                    Name="testXCV",
                    Symbol="xcv"
                }
            };
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
            var cryptoCurrency = "xcv";

           
            convertToStub.Setup(a => a.getAll())
                .Returns(new FakeOutputCurrency().testOutput());
            inputStub.Setup(a => a.getAll())
               .Returns(new FakeOutputCurrency().testInput());

            var expectedConverts = new Dictionary<string, double>()
            {
                { "tt1",1}
            };

            //Act
            var actual = new CryptoExchangeService(inputStub.Object,convertToStub.Object)
                                .ToTraditional(cryptoCurrency);

            //Assert
            actual.Should().BeEquivalentTo(expectedConverts);

        }
        [Fact]
        public void ExchangeCryptoToTraditional_WithWrongCrypto_ReturenBaseNotFoundException()
        {
            //Arrange
            var cryptoCurrency = "wrongCoin";
            convertToStub.Setup(a => a.getAll())
                .Returns(It.IsAny<IEnumerable<Currency>>());

            inputStub.Setup(a => a.getAll())
               .Returns(new FakeOutputCurrency().testInput());

            //Act
            Action act = () => new CryptoExchangeService(inputStub.Object,convertToStub.Object)
                                .ToTraditional(cryptoCurrency);

            //Assert
            act.Should().Throw<Exception>().WithMessage("Symbol not found");
        }
    }
}

