using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.SeleniumTests.POM
{
    public class StockPage : BasePage
    {
        public StockPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetExchangeData()
        {
            string exchangeData = Driver.FindElement(By.CssSelector(".exchange.yf-wk4yba span")).Text;
            return exchangeData;
        }

        public string GetStockName()
        {
            string stockName = Driver.FindElement(By.CssSelector("h1.yf-xxbei9")).Text;
            return stockName;
        }

        public double GetstockPrice()
        {
            string price = Driver.FindElement(By.CssSelector("[data-testid='qsp-price']")).Text;
            double stockPrice = double.Parse(price);
            return stockPrice;
        }

        public string GetStockPriceChange()
        {
            string priceChange = Driver.FindElement(By.CssSelector("[data-testid='qsp-price-change']")).Text;
            return priceChange;
        }

        public string GetStockPriceChangePercent()
        {
            string priceChangePercent = Driver.FindElement(By.CssSelector("[data-testid='qsp-price-change-percent']")).Text;
            return priceChangePercent;
        }

        public double GetPostMarketPrice()
        {
            string postPrice = Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price']")).Text;
            double postClosePrice = double.Parse(postPrice);
            return postClosePrice;
        }

        public string GetPostPriceChange()
        {
            string postPriceChange = Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price-change']")).Text;
            return postPriceChange;
        }

        public string GettPostPriceChangePercent()
        {
            string priceChangePercent = Driver.FindElement(By.CssSelector("[data-testid='qsp-post-price-change-percent']")).Text;
            return priceChangePercent;
        }

        public void ShowOneDayGraph()
        {
            var oneDayTab = Driver.FindElement(By.CssSelector("#tab-1d-qsp"));
            oneDayTab.Click();
        }

        public void ShowFiveDayGraph()
        {
            var fiveDayTab = Driver.FindElement(By.CssSelector("#tab-5d-qsp"));
            fiveDayTab.Click();
        }
        public void ShowOneMonthGraph()
        {
            var oneMonthTab = Driver.FindElement(By.CssSelector("#tab-1m"));
            oneMonthTab.Click();
        }

        public void ShowSixMonthGraph()
        {
            var sixMonthTab = Driver.FindElement(By.CssSelector("#tab-6m"));
            sixMonthTab.Click();
        }

        public void ShowOneYearGraph()
        {
            var oneYearTab = Driver.FindElement(By.CssSelector("#tab-YTD"));
            oneYearTab.Click();
        }

        public void ShowFiveYearGraph()
        {
            var fiveYearTab = Driver.FindElement(By.CssSelector("#tab-5y"));
            fiveYearTab.Click();
        }

        public void ShowMaxTinmGraph()
        {
            var maxTimeTab = Driver.FindElement(By.CssSelector("#tab-Max"));
            maxTimeTab.Click();
        }

        public string GetChartDescription()
        {
            var chartContainerElement = Driver.FindElement(By.CssSelector(".chartContainer.stx-crosshair-cursor-on"));
            string ariaLabel = string.Empty;
            List<IWebElement> canvasElements = chartContainerElement.FindElements(By.CssSelector("canvas")).ToList();
            foreach (var canvasElement in canvasElements)
            {
                if(canvasElement.GetAttribute("aria-label") != null)
                {
                     ariaLabel = canvasElement.GetAttribute("aria-label").ToString();
                }
            }
           return ariaLabel;
        }
    }
}
