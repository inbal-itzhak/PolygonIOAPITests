﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Allure.NUnit.Attributes;
using RestSharp;


namespace PolygonTests.APITests.AlphaVantageAPI
{
    public class AlphaVantageSteps : BaseTestAPI
    {
        private AlphaVantageRequests _alphaVentageRequests;
        public RestClient _client;
      
        public async Task<RestResponse>GetAlphaResponse(string ticker)
        {
            var alphaResponse = await _alphaVentageRequests.GetOpenCloseDataByDate(ticker);
            return alphaResponse;
        }

      
        public AlphaVantageSteps(RestClient client)
        {
            _client = client;
            _alphaVentageRequests = new AlphaVantageRequests(_client, config["ApiKeys:AlphaVantageApiKey"]);
            
        }

        [AllureStep("send Get Open Close Data By Date for AlphaVantage")]
        public async Task<AlphaVantageOpenCloseResponseData> GetStockOpenCloseDataByDateAlphaVantage(string ticker)
        {
            var alphaResponse = await _alphaVentageRequests.GetOpenCloseDataByDate(ticker);
            Assert.IsNotNull(alphaResponse);
            Assert.That((int)alphaResponse.StatusCode, Is.EqualTo(200), $"Response status code is {alphaResponse.StatusCode}, should be 200");
            var alphaResponseData = JsonSerializer.Deserialize<AlphaVantageOpenCloseResponseData>(alphaResponse.Content);
            return alphaResponseData;
        }

        [AllureStep("Get AlphaVantage Open Close MetaData")]
        public async Task<AlaphaVantageOpenCloseMetaData> GetAlphaVantageOpenCloseMetaData(AlphaVantageOpenCloseResponseData alphaResponseData)
        {
           // var alphaResponse = await _alphaVentageRequests.GetOpenCloseDataByDate(ticker, alphaApiKey);
          //  var alphaResponseData = JsonSerializer.Deserialize<AlphaVantageOpenCloseResponseData>(response.Content);
            //Get the MetaData
            var alphaMetaDataResponseData = alphaResponseData.MetaData;
            Assert.IsNotNull(alphaMetaDataResponseData);
            return alphaMetaDataResponseData;
        }

        [AllureStep("Get stock data for specific date")]
        public async Task<AlphaVantageOpenCloseDataDailyMetrics> GetStockDataForDate(AlphaVantageOpenCloseResponseData response, string date)
        {
           // var alphaResponseData = await GetStockOpenCloseDataByDateAlphaVantage(ticker);
            var alphaMetrics = response.TimeSeriesDaily[date];
            return alphaMetrics;
        }
    }
}
