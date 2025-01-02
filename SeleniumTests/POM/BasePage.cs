using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V129.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonTests.SeleniumTests.POM
{
    public class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;

        }

        public IWebDriver Driver { get; set; }

        public void NavigateTo(string url) 
        {
            Driver.Navigate().GoToUrl(url);
        }

     

       
    }
}
