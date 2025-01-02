using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Allure.NUnit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium;
using PolygonTests.APITests;
using PolygonTests.APITests.PolygonAPI;
using PolygonTests.SeleniumTests.POM;
using OpenQA.Selenium.Support.UI;
using System.Linq.Expressions;


namespace PolygonTests.Tests.Selenium_Tests
{
    [AllureNUnit]
    public class YahooFinanaceUITests :BaseTestUI
    {



        [SetUp]
        public void Setup()
        {
            basePage.NavigateTo(baseUrl);
        }
      


        [Test, Description("Quote lookup functionality"),
         TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public void LookupQuote(string ticker)
        {
            try
            {

                quoteLookup.LookupQuote(ticker);
                
                string stockSymbol = stockData.GetStockSymbol();
                Assert.That(stockSymbol, Is.EqualTo(ticker.ToUpper()));
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.ToString());
            }
        }


        [Test, Description("Quote lookup functionality"),
        TestCaseSource(typeof(DataProvider), nameof(DataProvider.TickersToTest))]
        public async Task QuoteDataValidation(string ticker)
        { 
            try
          {  //Arrange
           
            string polygonDate = polygonSteps.GetPreviousBusinessDay().ToString("yyyy-MM-dd");
            DateTime yahooDate = polygonSteps.GetPreviousBusinessDay();
            var polygonResponseData =  await polygonSteps.GetStockOpenCloseDataByDatePolygonIO(ticker, polygonDate);
            Assert.That(polygonResponseData, Is.Not.Null);
            string polygonSymbol = polygonResponseData.Symbol;
            double polygonPreviousClose = polygonResponseData.Close;
            double polygonOpenRate = polygonResponseData.Open;
            double polygonVolume = polygonResponseData.Volume;
            Console.WriteLine($"Polygon Response for stock {ticker}: polygonSymbol: {polygonSymbol}, polygonPreviousClose: {polygonPreviousClose}," +
                    $"polygonOpenRate: {polygonOpenRate},polygonVolume:{polygonVolume}");
            //act
             quoteLookup.LookupQuote(ticker);
                
            string stockSymbol = stockData.GetStockSymbol();
                Console.WriteLine($"stockSymbol: {stockSymbol}");
                stockData.NanigateToHistoricalData();
                HistoricalData stockHistoricalData =  stockData.GetStockDataByDate(yahooDate);
                double stockClose = double.Parse(stockHistoricalData.Close);
                Console.WriteLine($"stockPreviousClose: {stockClose}");
                double stockOpenRate = double.Parse(stockHistoricalData.Open);
                Console.WriteLine($"stockOpenRate: {stockOpenRate}");
                double stockVolume = double.Parse(stockHistoricalData.Volume);
                //Assert
                Assert.That(polygonSymbol, Is.EqualTo(stockSymbol),$"Expected symbol {polygonSymbol}, Actual symbol {stockSymbol}");
                Assert.That(polygonPreviousClose, Is.EqualTo(stockClose), $"Expecred Previous close rate {polygonPreviousClose}, Actual previous close rate {stockClose}");
                Assert.That(polygonOpenRate, Is.EqualTo(stockOpenRate), $"Expected Open rate {polygonOpenRate}, Actual open rate {stockOpenRate}");
                Assert.That(polygonVolume, Is.EqualTo(stockVolume), $"Expected stock volume {polygonVolume}, Actual stock volume {stockVolume}"); 
            }
            catch(Exception ex) 
            {
                Console.WriteLine (ex.StackTrace);
            }

        }
        [Test,Description("Verify Real time Stock data has values")]
        public void StockDataTypeValidation()
        {
            quoteLookup.LookupQuote("NFLX");
            Assert.That(stockPage.GetExchangeData(), Is.TypeOf<string>(), $"Stock is in exchange {stockPage.GetExchangeData()}");
            Assert.That(stockPage.GetStockName(), Is.EqualTo("Netflix, Inc. (NFLX)"), $"Stock name is {stockPage.GetStockName()}");
            Assert.IsNotNull(stockPage.GetstockPrice(), $"stock price is {stockPage.GetstockPrice()}");
            Assert.IsNotNull(stockPage.GetStockPriceChange(), $"stock price change is {stockPage.GetStockPriceChange()}");
            Assert.IsNotNull(stockPage.GetStockPriceChangePercent(), $"Stock price change in percent is {stockPage.GetStockPriceChangePercent()}");
            Assert.IsNotNull(stockPage.GetPostMarketPrice(), $"stock post market price is {stockPage.GetPostMarketPrice()}");
            Assert.IsNotNull(stockPage.GetPostPriceChange(), $"stock post market price change is {stockPage.GetPostPriceChange()}");
            Assert.IsNotNull(stockPage.GettPostPriceChangePercent(), $"stock post market price percent change is {stockPage.GettPostPriceChangePercent()}");
        }

        [Test,Description("Verify Chart Data")]
        public async Task VerifyChartData()
        {
            string ticker = "NFLX";
            quoteLookup.LookupQuote(ticker);
            stockChart.SelectOneDayChart();
            DateTime lastTradeDate = polygonSteps.GetLastTradeDateTime();
            var expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.OneDay, lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info one day is: {stockChart.GetChartInfo()}");
            stockChart.SelectFiveDayChart();
            expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.FiveDays,lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info 5 days is: {stockChart.GetChartInfo()}");
            stockChart.SelectOneMonthChart();
            expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.OneMonth,lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info1 month is: {stockChart.GetChartInfo()}");
            stockChart.SelectSixMonthChart();
            expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.SixMonths, lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info 6 months is: {stockChart.GetChartInfo()}");
            stockChart.SelectOneYearChart();
            expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.OneYear, lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info 1 year is: {stockChart.GetChartInfo()}");
            stockChart.SelecFiveYearChart();
            expectedChartInfo = stockChart.GetExpectedChartTimesDescription(ticker, StockChart.TimePeriod.FiveYears, lastTradeDate);
            Assert.That(stockChart.GetChartInfo(), Is.EqualTo(expectedChartInfo), $"actual chart info 5 years is: {stockChart.GetChartInfo()}");
        }
       
    }
}
