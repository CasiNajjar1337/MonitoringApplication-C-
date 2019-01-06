using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class General
    {
        public static IWebDriver driver;

        public static bool OpenBrowser(string URL)
        {
            try
            {
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl(URL);
                driver.Manage().Window.Maximize();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }
        public static bool PresenceOfElement(String locator)
        {
            try
            {
                //new WebDriverWait(General.driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                driver.FindElement(By.XPath(locator));
                Console.WriteLine("Element Found");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Element not found " + locator);
                return false;
            }

        }
        public static bool PresenceOfElement(String locator, int timeOut)
        {
            try
            {
                new WebDriverWait(General.driver, TimeSpan.FromSeconds(timeOut)).Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                //driver.FindElement(By.XPath(locator));
                Console.WriteLine("Element Found");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public static bool CloseDriver()
        {
            try
            {
                driver.Quit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
