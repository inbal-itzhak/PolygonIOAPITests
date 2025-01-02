using PolygonTests.APITests;
using PolygonTests.SeleniumTests.POM;
using Allure.NUnit;
using System.Text.Json;
using PolygonTests.APITests.AlphaVantageAPI;
using PolygonTests.APITests.PolygonAPI;
using System.Globalization;
//using Newtonsoft.Json;

namespace PolygonTests.Tests
{
    [AllureNUnit]
    public class CheckStockRatesTests : BaseTestAPI
    {
       
        private AlphaVantageRequests _alphaVantageRequests;
        private PolygonIORequests _polygonIORequests;
        private PolygonSteps _polygonSteps;
        private AlphaVantageSteps _alphaVantageSteps;
        private static string _lastTestName = null;
        string alphaApiKey = config["ApiKeys:AlphaVantageApiKey"];
        string polygonApiKey = config["ApiKeys:PolygonIoApiKey"];
        string date;
        [SetUp]
        public async Task Setup()
        {
            _alphaVantageRequests = new AlphaVantageRequests(AlphaVantageClient, alphaApiKey);
            _polygonIORequests = new PolygonIORequests(PolygonClient, polygonApiKey);
            _polygonSteps = new PolygonSteps(PolygonClient);
            _alphaVantageSteps = new AlphaVantageSteps(AlphaVantageClient);
            date = _polygonSteps.GetPreviousBusinessDay().ToString("yyyy-MM-dd");
            string currentTestName = TestContext.CurrentContext.Test.Name;
            // since the PolygonIO free API allowns only 5 calls per minute I added a minute delay between each test and put only 5 stocks in my test data.
            if (_lastTestName != currentTestName)
            {
                _lastTestName = currentTestName;
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
           
        }

        [Test, Description("Get Stock Open Rate for 2024-11-29 compare to AlphaVantage quote"),
          TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task GetOpenRate(string ticker)
        {
        
            var polygonResponseData = await _polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonOpenRate = polygonResponseData.Open;

            AlphaVantageOpenCloseResponseData alphaResponseData = await _alphaVantageSteps.GetStockOpenCloseDataByDateAlphaVantage(ticker);

            var alphaMetaDataResponseData = await _alphaVantageSteps.GetAlphaVantageOpenCloseMetaData(alphaResponseData);
          
            var alphaMetrics = alphaResponseData.TimeSeriesDaily[date];
            string alphaSymbol = alphaMetaDataResponseData.Symbol;
            double alphaOpenRate = alphaMetrics.Open;

            Assert.That(polygonSymbol, Is.EqualTo(alphaSymbol), $"ticker should be {ticker} in PolygonIO it's {polygonSymbol} and in AlphaVantage it's {alphaSymbol}");
            Assert.That(polygonOpenRate, Is.EqualTo(alphaOpenRate), $"Expected open Rate is the AlphaVantage rate {alphaOpenRate}, actual in PolygonIO {polygonOpenRate}");
        }


        [Test, Description("Get Stock Closing Rate for 2024-11-29compare to AlphaVantage quote"),
          TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task GetClosingRate(string ticker)
        {
            var polygonResponseData = await _polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonCloseRate = polygonResponseData.Close;

            AlphaVantageOpenCloseResponseData alphaResponseData = await _alphaVantageSteps.GetStockOpenCloseDataByDateAlphaVantage(ticker);

            var alphaMetaDataResponseData = await _alphaVantageSteps.GetAlphaVantageOpenCloseMetaData(alphaResponseData);

            var alphaMetrics = alphaResponseData.TimeSeriesDaily[date];
            string alphaSymbol = alphaMetaDataResponseData.Symbol;
            double alphaCloseRate = alphaMetrics.Close;

            Assert.That(polygonSymbol, Is.EqualTo(alphaSymbol), $"ticker should be {ticker} in PolygonIO it's {polygonSymbol} and in AlphaVantage it's {alphaSymbol}");
            Assert.That(polygonCloseRate, Is.EqualTo(alphaCloseRate), $"Expected open Rate is the AlphaVantage rate {alphaCloseRate}, actual in PolygonIO {polygonCloseRate}");
            
        }

        [Test, Description("Get Stock High Rate for 2024-11-29compare to AlphaVantage quote"),
         TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task GetDayHighRate(string ticker)
        {
           
            var polygonResponseData = await _polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonDayHighRate = polygonResponseData.High;

            AlphaVantageOpenCloseResponseData alphaResponseData = await _alphaVantageSteps.GetStockOpenCloseDataByDateAlphaVantage(ticker);

            var alphaMetaDataResponseData = await _alphaVantageSteps.GetAlphaVantageOpenCloseMetaData(alphaResponseData);

            var alphaMetrics = alphaResponseData.TimeSeriesDaily[date];
            string alphaSymbol = alphaMetaDataResponseData.Symbol;
            double alphaDayHighRate = alphaMetrics.High;

            //Assert
            Assert.That(polygonSymbol, Is.EqualTo(alphaSymbol), $"ticker should be {ticker} in PolygonIO it's {polygonSymbol} and in AlphaVantage it's {alphaSymbol}");
            Assert.That(polygonDayHighRate, Is.EqualTo(alphaDayHighRate), $"Expected open Rate is the AlphaVantage rate {alphaDayHighRate}, actual in PolygonIO {polygonDayHighRate}");

        }

        [Test, Description("Get Stock Low Rate for 2024-11-29compare to AlphaVantage quote"),
      TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task GetDayLowRate(string ticker)
        {
            var polygonResponseData = await _polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonDayLowRate = polygonResponseData.Low;

            AlphaVantageOpenCloseResponseData alphaResponseData = await _alphaVantageSteps.GetStockOpenCloseDataByDateAlphaVantage(ticker);

            var alphaMetaDataResponseData = await _alphaVantageSteps.GetAlphaVantageOpenCloseMetaData(alphaResponseData);

            var alphaMetrics = alphaResponseData.TimeSeriesDaily[date];
            string alphaSymbol = alphaMetaDataResponseData.Symbol;
            double alphaDayLowRate = alphaMetrics.Low;
            //Assert
            Assert.That(polygonSymbol, Is.EqualTo(alphaSymbol), $"ticker should be {ticker} in PolygonIO it's {polygonSymbol} and in AlphaVantage it's {alphaSymbol}");
            Assert.That(polygonDayLowRate, Is.EqualTo(alphaDayLowRate), $"Expected open Rate is the AlphaVantage rate {alphaDayLowRate}, actual in PolygonIO {polygonDayLowRate}");
        }


        [Test, Description("Get Stock trade volume for 2024-11-29compare to AlphaVantage quote"),
      TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task GetTradeVolume(string ticker)
        {

            var polygonResponseData = await _polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, date);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonVolume = polygonResponseData.Volume;

            AlphaVantageOpenCloseResponseData alphaResponseData = await _alphaVantageSteps.GetStockOpenCloseDataByDateAlphaVantage(ticker);

            var alphaMetaDataResponseData = await _alphaVantageSteps.GetAlphaVantageOpenCloseMetaData(alphaResponseData);

            var alphaMetrics = alphaResponseData.TimeSeriesDaily[date];
            string alphaSymbol = alphaMetaDataResponseData.Symbol;
            double alphaVolume = alphaMetrics.Volume;

            //Assert
            Assert.That(polygonSymbol, Is.EqualTo(alphaSymbol), $"ticker should be {ticker} in PolygonIO it's {polygonSymbol} and in AlphaVantage it's {alphaSymbol}");
            Assert.That(polygonVolume, Is.EqualTo(alphaVolume), $"Expected open Rate is the AlphaVantage rate {alphaVolume}, actual in PolygonIO {polygonVolume}");
        }

    }



}

