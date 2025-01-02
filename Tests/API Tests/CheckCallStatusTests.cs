using PolygonTests.APITests;
using PolygonTests.SeleniumTests.POM;
using Allure.NUnit;
using PolygonTests.APITests.AlphaVantageAPI;

namespace PolygonTests.Tests
{
    public class CheckCallStatusTests : BaseTestAPI
    {
        private AlphaVantageRequests _alphaVentageRequests;
        private PolygonIORequests _polygonIORequests;
        string alphaApiKey = config["ApiKeys:AlphaVaentageApiKey"];
        string polygonApiKey = config["ApiKeys:PolygonIoApiKey"];
        [SetUp]
        public void Setup()
        {
            
            _alphaVentageRequests = new AlphaVantageRequests(AlphaVantageClient, alphaApiKey);
            _polygonIORequests = new PolygonIORequests(PolygonClient, polygonApiKey);
        }

        [Test,Description("Verify status code for PolygonGetTickerInfoAsync is 200")]
        public async Task PolygonGetTickerInfoAsync()
        {
            var response = await _polygonIORequests.GetTickerInfoAsync("IBM");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }

        [Test, Description("Verify status code for PolygonGetOpenCloseData is 200")]
        public async Task PolygonGetOpenCloseData()
        {
            var response = await _polygonIORequests.GetOpenCloseData("IBM","2024-11-29");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }

        [Test, Description("Verify status code for PolygonGetTickerDetails is 200")]
        public async Task PolygonGetTickerDetails()
        {
            var response = await _polygonIORequests.GetTickerDetails("IBM");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }

       
        [Test, Description("Verify status code for AlphaGetOpenCloseData is 200")]
        public async Task AlphaGetOpenCloseData()
        {
            var response = await _alphaVentageRequests.GetOpenCloseDataByDate("IBM");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }

        [Test, Description("Verify status code for AlphaGetLastDayOpenClose is 200")]
        public async Task AlphaGetLastDayOpenClose()
        {
            var response = await _alphaVentageRequests.GetLastDayOpenClose("IBM");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }

        [Test, Description("Verify status code for AlphaGetComapnyOverview is 200")]
        public async Task AlphaGetComapnyOverview()
        {
            var response = await _alphaVentageRequests.GetComapnyOverview("IBM");
            Assert.IsNotNull(response);
            Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Response status code is {response.StatusCode}, should be 200");
        }



    }
}