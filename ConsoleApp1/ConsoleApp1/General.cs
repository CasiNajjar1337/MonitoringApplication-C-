using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

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
            }
            catch (Exception )
            {
                return false;
            }
        }

        public static bool FindElement(By locator)
        {
            try
            {
                driver.FindElement(locator);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool ClickElement(By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                element.Click();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static string GetText(By locator)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                string txt = element.Text;
                return txt;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static Int32 GetElementCount(By locator)
        {
            try
            {
                Int32 count = General.driver.FindElements(locator).Count;
                return count;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        
        public static bool SendText(By locator, string txt)
        {
            try
            {
                IWebElement element = driver.FindElement(locator);
                element.SendKeys(txt);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool WaitForElement(By locator, int timeout)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)).Until(ExpectedConditions.ElementExists(locator));
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Element not appeared in given time");
                return false;
            }

        }

        public static bool PresenceOfElement(By locator)
        {
            try
            {
               bool status= FindElement(locator);
                if (status)
                    return true;
                else
                    return false;
            }
            catch (Exception )
            {
                return false;
            }

        }

        public static bool PresenceOfElement(string locator)
        {
            try
            {
                driver.FindElement(By.XPath(locator));
                return true;
            }
            catch (Exception )
            {
                return false;
            }

        }

        public static bool PresenceOfElement(By locator, int timeOut)
        {
            try
            {
                bool status = WaitForElement(locator, timeOut);
                if (status)
                {
                    Console.WriteLine("Element Found");
                    return true;
                }
                else
                    return false;
            }
            catch (Exception )
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
