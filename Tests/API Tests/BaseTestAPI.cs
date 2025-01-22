using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpTracer;
using HttpTracer.Logger;
using System.Net.Mime;
using PolygonAPITests.APITests;
using Microsoft.Extensions.Configuration;


namespace PolygonAPITests
{
    public abstract class BaseTestAPI
    {
        public RestClient PolygonClient;
        public RestClient AlphaVantageClient;
        public string alphaApiKey;
        public string polygonApiKey;
        public string baseURL;
        //private readonly RestClient _client;
      

        public static readonly IConfigurationRoot config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile(path: "appsettings.json")
          .Build();

        [OneTimeSetUp]
        public void Setup()
        {
            var polygonOptions = new RestClientOptions(ApplicationType.PolygonIO)
            {
                ConfigureMessageHandler = handler => new HttpTracerHandler(handler, new ConsoleLogger(), HttpMessageParts.All)
            };
            PolygonClient = (RestClient)new RestClient(polygonOptions).AddDefaultHeader(KnownHeaders.Accept, MediaTypeNames.Application.Json);

            if (PolygonClient == null)
            {
                throw new InvalidOperationException("PolygonClient was not initialized.");
            }
            else 
            {
                Console.WriteLine("PolygonClient is initialized");
            }

            var alphaOptions = new RestClientOptions(ApplicationType.AlaphaVentage)
            {
                ConfigureMessageHandler = handler => new HttpTracerHandler(handler, new ConsoleLogger(), HttpMessageParts.ResponseBody)

            };
            AlphaVantageClient = (RestClient)new RestClient(alphaOptions).AddDefaultHeader(KnownHeaders.Accept, MediaTypeNames.Application.Json);

            if (AlphaVantageClient == null)
            {
                throw new InvalidOperationException("AlphaVantageClient was not initialized.");
            }
            alphaApiKey = config["ApiKeys:AlphaVantageApiKey"];
            polygonApiKey = config["ApiKeys:PolygonIoApiKey"];
           
        }

      
    }
}
