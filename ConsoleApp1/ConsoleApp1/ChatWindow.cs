using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class ChatWindow
    {
        public static By chatArea = By.XPath("//div[@class='_2nmDZ']");
        private static By creationMessage = By.XPath("//div[@class='copyable-area']//span[contains(text(),'created group']");
        private static By newMessageIndicator = By.XPath("//div[@role='button']//span[@class='OUeyt']");
        private static By downButton = By.XPath("//div[@role='button']//span[@data-icon='down']");
        private static By unreadMessageBar = By.XPath("//div[contains(@class,'1mq8g')]/span[contains(text(),'unread message')]");
        private static By countOfNewMessages = By.XPath("//div[contains(@class,'1mq8g')]/following-sibling::div[contains(@class,'vW7d1')]");
        private static By numberOfEntries = By.XPath("//div[contains(@class,'vW7d1')]");
        private static By numberOfMessages = By.XPath("//div[contains(@class,'vW7d1')]/div[contains(@class,'message')]");


        public static bool ScrollToTop()
        {
            try
            {
                General.ClickElement(chatArea);
                for (Int32 i = 1; ; i++)
                {
                    Actions actions = new Actions(General.driver);
                    actions.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();

                    if (General.PresenceOfElement(creationMessage))
                    {
                        actions.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();
                        break;
                    }
                }
                Console.WriteLine("***At the start of the Chat***");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool MonitorForNewMessages(string tableName)
        {
            try
            {
                for(; ; )
                {
                    if(!General.PresenceOfElement(creationMessage, 60)||General.PresenceOfElement(newMessageIndicator,60))
                    {
                        General.ClickElement(downButton);
                        if (General.PresenceOfElement(unreadMessageBar))
                        {
                            Int32 count = General.GetElementCount(countOfNewMessages);
                            Console.WriteLine("Found " + count + " unread Messages");
                            for (Int32 i = 1; i<= count; i++)
                            {
                                string tmp = General.driver.FindElement(By.XPath("//div[contains(@class,'1mq8g')]/following-sibling::div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]")).GetAttribute("data-pre-plain-text");
                                string[] str = tmp.Replace("[", "").Replace("]", "").Replace(",", "").Replace(": ", "").Split(' ');
                                string message = General.driver.FindElement(By.XPath("//div[contains(@class,'1mq8g')]/following-sibling::div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]//span")).Text;
                                message = message.Replace("'", "");
                                string timeStamp = str[0] + " " + str[1];
                                if (str.Count() == 4)
                                {
                                    SQLLiteConn.CreateAndInsertToTable(Variables.tableName, Variables.columnName1, Variables.columnName2, Variables.columnName3, Variables.columnName4, str[2], str[3], message, timeStamp);
                                    ScrollToTop();
                                }
                                else
                                {
                                    SQLLiteConn.CreateAndInsertToTable(Variables.tableName, Variables.columnName1, Variables.columnName2, Variables.columnName3, Variables.columnName4, str[2], "", message, timeStamp);
                                    ScrollToTop();
                                }

                            }
                        }
                    }
                }
                return true;
            }catch(Exception e)
            {
                return false;
            }
            
        }

        public static bool StoreInitialTexts(string tableName)
        {
            try
            {
                Int32 countOfEntries = General.GetElementCount(numberOfEntries);
                Int32 countOfMessages = General.driver.FindElements(numberOfMessages).Count;
                Console.WriteLine(numberOfMessages);
                //Int32 numberOfActualTexts = numberOfMessages + 4;
                Int32 countOfEntredRecords = 0;
                //Console.WriteLine("Total Number of Texts in the chat Window are :: " + numberOfActualTexts);
                for (Int32 i = 1; i <= countOfEntries; i++)
                {
                    if (General.PresenceOfElement("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]"))
                    {
                        Console.WriteLine(i);
                        string tmp = General.driver.FindElement(By.XPath("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]")).GetAttribute("data-pre-plain-text");
                        string[] str = tmp.Replace("[", "").Replace("]", "").Replace(",", "").Replace(": ", "").Split(' ');
                        string message = General.driver.FindElement(By.XPath("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]//span")).Text;
                        message = message.Replace("'", "");
                        string timeStamp = str[0] + " " + str[1];
                        if (str.Count() == 4)
                        {
                            SQLLiteConn.CreateAndInsertToTable(Variables.tableName, Variables.columnName1, Variables.columnName2, Variables.columnName3, Variables.columnName4, str[2], str[3], message, timeStamp);
                            countOfEntredRecords++;
                        }
                        else
                        {
                            SQLLiteConn.CreateAndInsertToTable(Variables.tableName, Variables.columnName1, Variables.columnName2, Variables.columnName3, Variables.columnName4, str[2], "", message, timeStamp);
                            countOfEntredRecords++;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                Console.WriteLine("Total Number of Records entered in DB are " + countOfEntries);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
