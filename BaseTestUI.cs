using HttpTracer.Logger;
using HttpTracer;
using OpenQA.Selenium;
using PolygonTests.APITests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using PolygonTests.SeleniumTests.POM;
using OpenQA.Selenium.DevTools.V129.DOM;
using PolygonTests.APITests.PolygonAPI;


namespace PolygonTests
{
    public abstract class BaseTestUI 
    {
        public IWebDriver driver;
        public EntryMessage entryMessage;
        public QuoteLookup quoteLookup;
        public StockHistoricalData stockData;
        public StockDataMenu stockDataMenu;
        public RestClient PolygonClient;
        public StockPage stockPage;
        public StockChart stockChart;
        public string polygonApiKey;
        public BasePage basePage;
        public string baseUrl;
        public PolygonSteps polygonSteps;
        DateTime previousBusinessDay;



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

            polygonApiKey = config["ApiKeys:PolygonIoApiKey"];

            baseUrl = config["BaseURL:baseUrl"];
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = baseUrl;
            entryMessage = new EntryMessage(driver);
            quoteLookup = new QuoteLookup(driver);
            stockData = new StockHistoricalData(driver);
            stockPage = new StockPage(driver);
            stockChart = new StockChart(driver);
            stockDataMenu = new StockDataMenu(driver);
            entryMessage.ClickOnAcceptAllCookies();
            basePage = new BasePage(driver);
            polygonSteps = new PolygonSteps(PolygonClient);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
           driver.Dispose();
        }
    }
}
