using OpenQA.Selenium;
using System;

namespace ConsoleApp1
{
    class HomePage
    {

        public static By logInMessage = By.XPath("//h1[text()='Keep your phone connected']");
        public static By group = By.XPath("//span[@title='" + Variables.groupName + "']");
        public static By searchBox = By.XPath("//input[@title='Search or start new chat']");
        public static By searchResult = By.XPath("//span[@dir='auto']/span");
        public static By groupHeader = By.XPath("//header//span[text()='" + Variables.groupName + "']");


        public static bool SearchGroup(string groupName)
        {
            try
            {
                General.WaitForElement(logInMessage, 120);
                bool isElementDisplayed = General.PresenceOfElement(group, 5);
                if (isElementDisplayed)
                {
                    General.ClickElement(group);
                }
                else
                {
                    General.SendText(searchBox, Variables.groupName);
                    General.WaitForElement(searchResult, 60);
                    General.ClickElement(searchResult);
                }
                bool status = General.PresenceOfElement(groupHeader, 15);
                if (status)
                    return true;
                else
                   return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
