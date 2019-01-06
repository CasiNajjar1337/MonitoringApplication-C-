using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HomePage 
    {
        public static bool SearchGroup(string groupName)
        {
            try { 
            new WebDriverWait(General.driver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementExists(By.XPath("//h1[text()='Keep your phone connected']")));
            //Thread.Sleep(3000);

            bool isElementDisplayed = General.PresenceOfElement("//span[@title='" + groupName + "']",5);
            if (isElementDisplayed)
            {
               // Console.WriteLine("If Line");
                General.driver.FindElement(By.XPath("//span[@title='"+groupName+"']")).Click();
            }
            else
            {
                //Console.WriteLine("Else Line");
                General.driver.FindElement(By.XPath("//input[@title='Search or start new chat']")).SendKeys(groupName);
                new WebDriverWait(General.driver, TimeSpan.FromSeconds(120)).Until(ExpectedConditions.ElementExists(By.XPath("//span[@dir='auto']/span")));
                General.driver.FindElement(By.XPath("//span[@dir='auto']/span")).Click();
            }
               bool status = General.PresenceOfElement("//header//span[text()='" + groupName + "']",15);
                if (status) { 
                return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
