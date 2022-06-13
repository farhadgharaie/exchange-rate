using Exchange.Common.interfaces;
using Exchange.Common.Currency;
using Exchange.Service;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Exchange.Common.CustomException;

namespace ExchangeCurrency.UnitTest
{

    public class FakeOutputCurrency : CurrencyTemplate
    {
        public override Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency)
        {
            throw new NotImplementedException();
        }

        public override async Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency)
        {
           var result= new Dictionary<string, double>()
            {
                { "tt2",1},
                { "tt1",1}
            };
            return result;
        }

        public override IEnumerable<CurrencyModel> Select()
        {
            return new List<CurrencyModel>() {
                new CurrencyModel("test1","tt1"),
                new CurrencyModel("test2","tt2")
            };
        }
    }
    public class FakeInputCurrency : CurrencyTemplate
    {
        public override async Task<double> ConvertTo(CurrencyModel fromCurrency, CurrencyModel toCurrency)
        {
            return 1.0;
        }

        public override async Task<Dictionary<string, double>> ConvertToAll(CurrencyModel fromCurrency)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<CurrencyModel> Select()
        {
            return new List<CurrencyModel>() {
                new CurrencyModel("test1","tt1"),
                new CurrencyModel("test2","tt2")
            };
        }
    }

    public class CryptoExchangeServiceTests
    {
        private readonly Mock<IFiatCurrency> convertToStub = new Mock<IFiatCurrency>();
        private readonly Mock<ICryptoCurrency> inputStub = new Mock<ICryptoCurrency>();
       
        [Fact]
        public async Task ExchangeCryptoToFiat_WithSpecificCrypto_ReturenListOfCurrencies()
        {
            //Arrange
            string inputCryptoSymbol = "cryptoSymbol";
            double outPutUSDRate = 1;
            var expectedConverts = new Dictionary<string, double>()
            {
                { "tt2",outPutUSDRate},
                { "tt1",outPutUSDRate}
            };

            inputStub.Setup(a => a.ConvertTo(It.IsAny<CurrencyModel>(), It.IsAny<CurrencyModel>()))
               .Returns(new FakeInputCurrency().ConvertTo(It.IsAny<CurrencyModel>(), It.IsAny<CurrencyModel>()));

            convertToStub.Setup(a => a.ConvertToAll(It.IsAny<CurrencyModel>()))
              .Returns(new FakeOutputCurrency().ConvertToAll(It.IsAny<CurrencyModel>()));

            //Act
            var actual = await new CryptoExchangeService(inputStub.Object)
                                  .ToFiat(inputCryptoSymbol, convertToStub.Object);

            //Assert
            actual.Should().BeEquivalentTo(expectedConverts);

        }
    }
}

