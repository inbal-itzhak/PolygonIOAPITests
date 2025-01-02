using OpenQA.Selenium;
using PolygonTests.APITests.PolygonAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.SeleniumTests.POM
{
    public class StockChart : BasePage
    {
        public StockChart(IWebDriver driver) : base(driver)
        {
        }

        public string GetChartInfo()
        {
            List<IWebElement> canvasElements = Driver.FindElements(By.CssSelector("cq-context canvas")).ToList();
            string chartInfo = "";
            foreach (IWebElement canvasElement in canvasElements)
            {
                if (canvasElement.GetAttribute("aria-label") != null)
                {
                    chartInfo = canvasElement.GetAttribute("aria-label");
                }
            }
            return chartInfo;
        }

        public void SelectOneDayChart()
        {
            Driver.FindElement(By.CssSelector("#tab-1d-qsp")).Click();
        }

        public void SelectFiveDayChart()
        {
            Driver.FindElement(By.CssSelector("#tab-5d-qsp")).Click();
        }

        public void SelectOneMonthChart()
        {
            Driver.FindElement(By.CssSelector("#tab-1m")).Click();
        }

        public void SelectSixMonthChart()
        {
            Driver.FindElement(By.CssSelector("#tab-6m")).Click();
        }

        public void SelectOneYearChart()
        {
            Driver.FindElement(By.CssSelector("#tab-YTD")).Click();
        }

        public void SelecFiveYearChart()
        {
            Driver.FindElement(By.CssSelector("#tab-5y")).Click();
        }

        public enum TimePeriod
        {
            OneDay,
            FiveDays,
            OneMonth,
            SixMonths,
            OneYear,
            FiveYears,
        }

      

        public string GetExpectedChartTimesDescription(string ticker, TimePeriod timePeriod, DateTime lastTradeTime)
        {
            string expctedDescription;
            TimeSpan timeSpan;
            
            //This 1 minute mountain chart displays price over time for symbol NFLX between 12/16/2024, 5:00:00 PM and 12/17/2024, 8:21:00 AM.  The chart also displays Volume Underlay study.
            switch (timePeriod)
            {
                case TimePeriod.OneDay:
                    timeSpan = TimeSpan.FromDays(1);
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {lastTradeTime.Add(-timeSpan).ToString("MM/dd/yyyy, h:mm tt")} and {lastTradeTime.ToString("MM/dd/yyyy, h:mm tt")}.  The chart also displays Volume Underlay study.";
                    break;

                case TimePeriod.FiveDays:
                    timeSpan = TimeSpan.FromDays(5);
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {lastTradeTime.Add(-timeSpan).ToString("MM/dd/yyyy, h:mm tt")} and {lastTradeTime.ToString("MM/dd/yyyy, h:mm tt")}.  The chart also displays Volume Underlay study.";
                    break;

                case TimePeriod.OneMonth:
                    TimeSpan daysInMonth = DateTime.Today - DateTime.Now.AddMonths(-1);
                    timeSpan = daysInMonth;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("MM/dd/yyyy")} and {DateTime.Now.ToString("MM/dd/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.SixMonths:
                    TimeSpan daysInSixMonths = DateTime.Today - DateTime.Now.AddMonths(-6);
                    timeSpan = daysInSixMonths;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("MM/dd/yyyy")} and {DateTime.Now.ToString("MM/dd/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.OneYear:
                    TimeSpan daysInOneYear = DateTime.Today - DateTime.Now.AddYears(-1);
                    timeSpan = daysInOneYear;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("MM/dd/yyyy")} and {DateTime.Now.ToString("MM/dd/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;
                case TimePeriod.FiveYears:
                    TimeSpan daysInFiveYears = DateTime.Today - DateTime.Now.AddYears(-5);
                    timeSpan = daysInFiveYears;
                    expctedDescription = $"This 1 minute mountain chart displays price over time for symbol {ticker.ToUpper()} between {DateTime.Now.Add(-timeSpan).ToString("MM/dd/yyyy")} and {DateTime.Now.ToString("MM/dd/yyyy")}.  The chart also displays Volume Underlay study.";
                    break;

                default:
                    expctedDescription = string.Empty;
                    break;
            }
            return expctedDescription;
        }


    }
}
