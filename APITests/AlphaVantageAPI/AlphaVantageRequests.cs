using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.APITests
{
    public class AlphaVantageRequests
    {
        private readonly RestClient _client;
        private string _apiKey;
        string query = "query?function=";

        public AlphaVantageRequests(RestClient client, string apiKey)
        {
            _apiKey = apiKey;
            _client = client;
        }

     

        public async Task<RestResponse> GetOpenCloseDataByDate(string ticker)
        {
        //https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=IBM&apikey=ZMILG5AIDNBFAKO0
        var request = new RestRequest($"{query}TIME_SERIES_DAILY&symbol={ticker}&apikey={_apiKey}",Method.Get);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetLastDayOpenClose(string  ticker)
        {
            //https://www.alphavantage.co/query?function=GLOBAL_QUOTE&symbol=IBM&apikey=ZMILG5AIDNBFAKO0
            var request = new RestRequest($"{query}GLOBAL_QUOTE&symbol={ticker}&apikey={_apiKey}", Method.Get);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetComapnyOverview(string ticker)
        {
            //https://www.alphavantage.co/query?function=OVERVIEW&symbol=IBM&apikey=demo
            var request = new RestRequest($"{query}OVERVIEW&symbol={ticker}&apikey={_apiKey}", Method.Get);
            return await _client.ExecuteAsync(request);
        }
    }
}
