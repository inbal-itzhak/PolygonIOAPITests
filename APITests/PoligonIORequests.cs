using OpenQA.Selenium.DevTools.V129.CSS;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.APITests
{
    public class PolygonIORequests
    {
        private readonly RestClient _client;
        private string _apiKey;

        public PolygonIORequests(RestClient client, string apiKey)
        {
            _apiKey = apiKey;
            Console.WriteLine($"PolygonClient in PolygonIORequests: {client}");
            _client = client;
        }

        public async Task<RestResponse> GetTickerInfoAsync(string ticker)
        {
            var request = new RestRequest($"/v3/reference/tickers/{ticker}?apiKey={_apiKey}", Method.Get);
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse>GetOpenCloseData(string ticker,string date)
        {
           
            var request = new RestRequest($"v1/open-close/{ticker}/{date}?adjusted=true&apiKey={_apiKey}", Method.Get);
            string requestbody = request.ToString();
            return await _client.ExecuteAsync(request);
        }

        public async Task<RestResponse> GetTickerDetails(string ticker)
        {
            //https://api.polygon.io/v3/reference/tickers/IBM?apiKey=w07f4tOqikWuF24vjpZNQYfdWzxLIY7p
            var request = new RestRequest($"v3/reference/tickers/{ticker}?apiKey={_apiKey}", Method.Get);
            return await _client.ExecuteAsync(request);
        }
    }
}
