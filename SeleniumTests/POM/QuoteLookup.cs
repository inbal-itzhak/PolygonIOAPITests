using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PolygonTests.SeleniumTests.POM
{
    public class QuoteLookup: BasePage
    {
        public QuoteLookup(IWebDriver driver):base(driver)
        {
        }
        public void LookupQuote(string ticker)
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

            IWebElement formElement = wait.Until(ExpectedConditions.ElementExists(By.CssSelector("form>[aria-label='Quote Lookup']")));
            formElement.SendKeys(ticker);
            formElement.SendKeys(Keys.Enter);
            var ddb =  Driver.FindElements(By.CssSelector("[role='listbox']"));
            foreach (var linsting in ddb)
            {
                var symbol = Driver.FindElement(By.CssSelector(".modules-module_quoteSymbol__BGsyF"));
                if(symbol.Text == ticker.ToUpper())
                {
                    symbol.Click();
                }
            }
        }
    }
}
