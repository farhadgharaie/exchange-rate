using Exchange.Common.interfaces;
using Exchange.Common.Currency;
using Exchange.Service;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading.Tasks;
using Exchange.Common.CustomException;

namespace ExchangeCurrency.UnitTest
{
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
    public class CryptoExchangeServiceTests
    {
        private readonly Mock<ITraditionalCurrency> convertToStub = new Mock<ITraditionalCurrency>();
        private readonly Mock<ICryptoCurrency> inputStub = new Mock<ICryptoCurrency>();
        private readonly Mock<ICryptoToUSD> cryptoToUSDMoq = new Mock<ICryptoToUSD>();
        private readonly Mock<IExchangeBaseOnUSD> exchangeBaseOnUSDMoq = new Mock<IExchangeBaseOnUSD>();
        [Fact]
        public async Task ExchangeCryptoToTraditional_WithSpecificCrypto_ReturenListOfCurrencies()
        {
            //Arrange
            double outPutUSDRate = 1;
            var expectedConverts = new Dictionary<string, double>()
            {
                { "tt2",outPutUSDRate},
                { "tt1",outPutUSDRate}
            };


            inputStub.Setup(a => a.IsSymbolExist(It.IsAny<string>()))
               .Returns(true);

            convertToStub.Setup(a => a.getAll())
              .Returns(new FakeOutputCurrency().getAll());

            cryptoToUSDMoq.Setup(a => a.GetUSDQuoteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((double)outPutUSDRate));

            exchangeBaseOnUSDMoq.Setup(a => a.ExchangeBaseOnUSD(It.IsAny<string[]>()))
                .Returns(Task.FromResult(new Dictionary<string, double>() { { "tt2",outPutUSDRate},
                { "tt1",outPutUSDRate} }));

            //Act
            var actual = await new CryptoExchangeService(inputStub.Object,
                                                         cryptoToUSDMoq.Object,
                                                         exchangeBaseOnUSDMoq.Object)
                                .ToTraditional(It.IsAny<string>(), convertToStub.Object);

            //Assert
            actual.Should().BeEquivalentTo(expectedConverts);

        }
        [Fact]
        public async Task ExchangeCryptoToTraditional_WithZeroUSDRate_ReturenException()
        {
            //Arrange
            double outPutUSDRate = 0;
            var cryptoCurrency = It.IsAny<string>();

            inputStub.Setup(a => a.IsSymbolExist(It.IsAny<string>()))
               .Returns(true);

            convertToStub.Setup(a => a.getAll())
              .Returns(new FakeOutputCurrency().getAll());

            cryptoToUSDMoq.Setup(a => a.GetUSDQuoteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult((double)outPutUSDRate));

            exchangeBaseOnUSDMoq.Setup(a => a.ExchangeBaseOnUSD(It.IsAny<string[]>()))
                .Returns(Task.FromResult(new Dictionary<string, double>()));

            //Act
            Func<Task> act = async () => await new CryptoExchangeService(inputStub.Object, cryptoToUSDMoq.Object, exchangeBaseOnUSDMoq.Object)
                                .ToTraditional(cryptoCurrency, convertToStub.Object);

            //Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("No USD base quote provided");
        }
        [Fact]
        public async Task ExchangeCryptoToTraditional_WithWrongCrypto_ReturenBaseNotFoundException()
        {
            //Arrange
            var cryptoCurrency = "wrongCoin";

            convertToStub.Setup(a => a.getAll())
                .Returns(It.IsAny<IEnumerable<CurrencyModel>>());

            inputStub.Setup(a => a.IsSymbolExist(It.IsAny<string>()))
               .Returns(false);

            cryptoToUSDMoq.Setup(a => a.GetUSDQuoteAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(It.IsAny<double>()));

            exchangeBaseOnUSDMoq.Setup(a => a.ExchangeBaseOnUSD(It.IsAny<string[]>()))
                .Returns(Task.FromResult(new Dictionary<string, double>() { { "tt2",1},
                { "tt1",1} }));

            //Act
            Func<Task> act = async () => await new CryptoExchangeService(inputStub.Object, cryptoToUSDMoq.Object, exchangeBaseOnUSDMoq.Object)
                                .ToTraditional(cryptoCurrency, convertToStub.Object);

            //Assert
            await act.Should().ThrowAsync<SymbolNotFoundException>();
        }
    }
}

