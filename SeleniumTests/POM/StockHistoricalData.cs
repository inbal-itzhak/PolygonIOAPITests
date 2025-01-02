using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PolygonTests.APITests.PolygonAPI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.SeleniumTests.POM
{
    public class StockHistoricalData :BasePage
    {
        public StockHistoricalData(IWebDriver driver) :base(driver) { }

        public void NanigateToHistoricalData()
        {
            Driver.FindElement(By.CssSelector("[title='Historical Data']")).Click();
        }

        public HistoricalData GetStockDataByDate(DateTime date)
        {
            try
         {   string siteDate = date.ToString("MMM d, yyyy");
            List<IWebElement> dataRows = Driver.FindElements(By.CssSelector(".table.yf-j5d1ld.noDl tbody>tr")).ToList();
            List<HistoricalData> historicalData = new List<HistoricalData>();
                // checking first 5 rows, since I am only looking for last business
            for (int i = 0; i < 5; i++)
                {
                    List<string> dataCells = new List<string>();
                    List<IWebElement> cells = dataRows[i].FindElements(By.TagName("td")).ToList();
                    foreach(IWebElement cell in cells)
                    {
                        dataCells.Add(cell.Text);
                    }
                    string[] stockDataRow = dataCells.ToArray();
                    Console.WriteLine($"DataCellsListCount = {dataCells.Count}, stockDataLenght = {stockDataRow.Length}, stockDataArray = {stockDataRow.ToString()}");
                    historicalData.Add(new HistoricalData
                    {
                        Date = dataCells[0],
                        Open = dataCells[1],
                        High = dataCells[2],
                        Low = dataCells[3],
                        Close = dataCells[4],
                        AdjClose = dataCells[5],
                        Volume = dataCells[6]
                    });
                }
           
            foreach(HistoricalData data in historicalData)
            {
                if (data.Date == siteDate)
                {
                    Console.WriteLine($"HistoricalData returned");
                    return data;
                }
            }
            Console.WriteLine($"HistoricalData not found");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        public string CompanyName()
        {
            string companyName = Driver.FindElement(By.CssSelector("h1.yf-xxbei9")).Text;
            return companyName;
        }

        public string GetStockSymbol()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            IWebElement tickerSymbol = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("h1.yf-xxbei9")));
            if(tickerSymbol != null ) 
            {
                string stockNameSymbol = tickerSymbol.Text;
                string stockSymbol = stockNameSymbol.Split('(')[1].Split(')')[0];
                return stockSymbol;
            }
            else
            {
                return "element not found";
            }

        }
        public List<IWebElement> StockDataList()
        { 
            List<IWebElement> stockData = Driver.FindElements(By.CssSelector(".value.yf-swtyw6")).ToList(); 
            return stockData;
        }

        public string PreviousClose()
        {
                var previousCloseElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketPreviousClose'].yf-dudngy"));
                if (previousCloseElement != null)
                {
                    return previousCloseElement.Text;
                }
           return string.Empty;
        }

        public string OpenRate()
        {
                var openRateElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketOpen'].yf-dudngy"));
                if (openRateElement != null)
                {
                    return openRateElement.Text;
                }
            return string.Empty;
        }

        public string Volume()
        {
                var openRateElement = Driver.FindElement(By.CssSelector("[data-field='regularMarketVolume'].yf-dudngy"));
                if (openRateElement != null)
                {
                    return openRateElement.Text;
                }
            return string.Empty;
        }


     
    }
    
}
