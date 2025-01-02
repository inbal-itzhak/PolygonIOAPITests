using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace PolygonTests.SeleniumTests.POM
{
    public class StockDataMenu : BasePage
    {
        public StockDataMenu(IWebDriver driver) :base(driver) 
        {
        }

      
        public void CLickOnMenuItem(string menuItemName)
        {
            List<IWebElement> menuItems = Driver.FindElements(By.CssSelector(".ellipsis.yf-1e6z5da")).ToList();
            foreach (IWebElement menuItem in menuItems) 
            { 
                if(menuItem.Text.Contains(menuItemName))
                {
                    menuItem.Click();
                }
            }
        }
    }
}
