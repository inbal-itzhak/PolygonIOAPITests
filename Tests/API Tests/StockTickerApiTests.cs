using PolygonAPITests.APITests;
using Allure.NUnit;
using System.Text.Json;
using RestSharp;
using System.Net.Http.Json;
using PolygonAPITests.APITests.PolygonAPI;
using PolygonAPITests.APITests.AlphaVantageAPI;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;

namespace PolygonAPITests.Tests
{
    [AllureNUnit]
    [AllureEpic("Polygom.IO API Boundary and Error Handling")]
    public class StockTickerApiTests : BaseTestAPI
    {
        private AlphaVantageRequests _alphaVentageRequests;
        private PolygonIORequests _polygonIORequests;
        //string alphaApiKey = config["ApiKeys:AlphaVantageApiKey"];
        //string polygonApiKey = config["ApiKeys:PolygonIoApiKey"];

        [SetUp]
        public async Task Setup()
        {

            _alphaVentageRequests = new AlphaVantageRequests(AlphaVantageClient, alphaApiKey);
            _polygonIORequests = new PolygonIORequests(PolygonClient, polygonApiKey);
            // added a 10 seconds delay between each test so that the limit of 5 calls per minute will not pass.
            await Task.Delay(TimeSpan.FromSeconds(10));
        }


        [Test, Description("Verify rate limit error when exceeding max API calls per minute")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Error Handling")]
        [AllureStory("Verify rate limit error when exceeding max API calls per minute")]
        public async Task XRateLimitErrorTest()
        {
            int maxCallsPerMinute = 5;
            for (int i = 1; i <= maxCallsPerMinute; i++)
            {
                var response = await _polygonIORequests.GetTickerInfoAsync("IBM");
                Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Call {i} should be successful");
            }

            var rateLimitResponse = await _polygonIORequests.GetTickerInfoAsync("IBM");
            Assert.That((int)rateLimitResponse.StatusCode, Is.EqualTo(429), "The 6th call should return a 429 Too Many Requests error");

            // Step 3: Wait for 1 minute to simulate recovery from the rate limit
            await Task.Delay(TimeSpan.FromSeconds(60));

            // Step 4: Make another API call after 1 minute and ensure it is successful
            var recoveryResponse = await _polygonIORequests.GetTickerInfoAsync("IBM");
            Assert.That((int)recoveryResponse.StatusCode, Is.EqualTo(200), "The 7th call should be successful after waiting 1 minute");
        }

        [Test, Description("Validating the response schema for a stock ticker query")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Response Schema Validation")]
        [AllureStory("Ensure open/close response schema matches expected format")]
        public async Task ValidateOpenCloseResponseSchemaTest()
        {
            string ticker = "NFLX";
            string date = "2024-12-02";
           
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker, date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(200), $"Response status code is {polygonResponse.StatusCode}, should be 200");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonOpenCloseData>(polygonResponse.Content);

            var alphaResponse = await _alphaVentageRequests.GetLastDayOpenClose("NFLX");
            Assert.IsNotNull(alphaResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(200));
            var alphaResponseData = JsonSerializer.Deserialize<GlobalQuoteData>(alphaResponse.Content);
            Assert.That(polygonResponseData.From, Is.EqualTo(date), $"Expected 'From' value {date}, actual {polygonResponseData.From}");
            Assert.That(polygonResponseData.Symbol, Is.EqualTo(ticker), $"Expected 'Symbol' value {ticker}, actual {polygonResponseData.Symbol}");
            Assert.That(polygonResponseData.Open, Is.TypeOf<double>(), $"Expected 'Open' value  {typeof(double)}, actual {polygonResponseData.Open.GetType()}");
            Assert.That(polygonResponseData.High, Is.TypeOf<double>(), $"Expected 'High' value type to be {typeof(double)}, actual {polygonResponseData.High.GetType()}");
            Assert.That(polygonResponseData.Low, Is.TypeOf<double>(), $"Expected 'Low' value {typeof(double)}, actual {polygonResponseData.Low.GetType()}");
            Assert.That(polygonResponseData.Close, Is.TypeOf<double>(), $"Expected 'Close' value  {typeof(double)}, actual {polygonResponseData.Close.GetType()}");
            Assert.That(polygonResponseData.Volume, Is.TypeOf<double>(), $"Expected 'Volume' value  {typeof(double)}, actual {polygonResponseData.Volume.GetType()}");


        }
        [Test, Description("send request with empty ticker string")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Field Validation")]
        [AllureStory("Test API with empty ticker string")]
        public async Task EmptyTickerString()
        {
            string ticker = "";
            string date = "2024-12-02";
           
            string expectedError = "Ticker was incorrectly formatted";
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker, date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(400), $"Response status code is {polygonResponse.StatusCode}, should be 400");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonBadRequest>(polygonResponse.Content);
            Assert.That(polygonResponseData.Error, Is.EqualTo(expectedError), $"Error message is {polygonResponseData.Error}, should be {expectedError}");
        }

        [Test,Description("send request with ticker name with special characters")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Field Validation")]
        [AllureStory("Test API with special character ticker")]
        public async Task TestTickerSpecialChars()
        {
            string ticker = "NFL$";
            string date = "2024-12-02";
            
            string expectedError = "Data not found.";
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker, date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(404), $"Response status code is {polygonResponse.StatusCode}, should be 400");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonNotFoundRequest>(polygonResponse.Content);
            Assert.That(polygonResponseData.Message, Is.EqualTo(expectedError), $"Error message is {polygonResponseData.Message}, should be {expectedError}");
        }

        [Test, Description("send request with ticker name with the Max chars allowed in nasdaq stocks:  5 chars")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Field Validation")]
        [AllureStory("Test API with maximum allowed ticker length")]
        public async Task TestTickerMaxCharsAllowedInput()
        {

            string ticker = "GOOGL";
            string date = "2024-12-02";
         
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker,date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(200), $"Response status code is {polygonResponse.StatusCode}, should be 200");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonOpenCloseData>(polygonResponse.Content);
            Assert.That(polygonResponseData.Symbol, Is.EqualTo(ticker), $"Expected 'Symbol' value {ticker}, actual {polygonResponseData.Symbol}");
        }

        [Test, Description("send request with ticker longer name then the Max chars allowed in nasdaq stocks:  5 chars, this also tests non existing stock")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Field Validation")]
        [AllureStory("Test API with longer than allowed ticker name")]
        public async Task TestTickerLongerTheMaxCharsAllowedInput()
        {
            string ticker = "GOOGLE";
            string date = "2024-12-02";
            
            string expectedError = "Data not found.";
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker,date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(404), $"Response status code is {polygonResponse.StatusCode}, should be 400");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonNotFoundRequest>(polygonResponse.Content);
            Assert.That(polygonResponseData.Message, Is.EqualTo(expectedError), $"Error message is {polygonResponseData.Message}, should be {expectedError}");
        }

        [Test, Description("send request with ticker name 4 white spaces")]
        [AllureSeverity(SeverityLevel.normal)]
        [AllureFeature("API Field Validation")]
        [AllureStory("Test API with whitespace in ticker")]
        public async Task TestTickerWhiteSpaces()
        {
            string ticker = "    ";
            string date = "2024-12-02";
            
            string expectedError = "Data not found.";
            var polygonResponse = await _polygonIORequests.GetOpenCloseData(ticker,date);
            Assert.IsNotNull(polygonResponse);
            Assert.That((int)polygonResponse.StatusCode, Is.EqualTo(404), $"Response status code is {polygonResponse.StatusCode}, should be 400");
            var polygonResponseData = JsonSerializer.Deserialize<PolygonNotFoundRequest>(polygonResponse.Content);
            Assert.That(polygonResponseData.Message, Is.EqualTo(expectedError), $"Error message is {polygonResponseData.Message}, should be {expectedError}");
        }
    }

}
