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
    public class EntryMessage : BasePage
    {
        public EntryMessage(IWebDriver driver) :base(driver)
        {
        }
        

        public void ClickOnGoToBottomBtn()
        {
            Driver.FindElement(By.CssSelector("#scroll-down-btn")).Click();
        }

        public void ClickOnAcceptAllCookies()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("[name='agree']"))).Click();

        }

        public void ClickOnRejectAllCookies()
        {
            Driver.FindElement(By.CssSelector(".btn.secondary.reject-all")).Click();
        }
    }
}
