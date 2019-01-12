using Finisar.SQLite;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class ChatWindow
    {
        public static By chatArea = By.XPath("//div[@class='_2nmDZ']");
        private static By creationMessage = By.XPath("//div[@class='copyable-area']//span[contains(text(),'created') and contains(text(), 'group')]");
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
                        Console.WriteLine("***At the start of the Chat***");
                        break;
                    }
                }
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool MonitorForNewMessages()
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
                                Variables.message = General.driver.FindElement(By.XPath("//div[contains(@class,'1mq8g')]/following-sibling::div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]//span")).Text;
                                Variables.message = Variables.message.Replace("'", "");

                                if (str[1].ToLower().Equals("am") || str[1].ToLower().Equals("pm"))
                                    Variables.timeFormat = "12 Hours Format";
                                else
                                    Variables.timeFormat = "24 Hours Format";

                                if (Variables.timeFormat.Equals("24 Hours Format"))
                                {
                                    Variables.timeStamp = str[0] + " " + str[1];
                                    if (str.Count() == 4)
                                    {
                                        SQLLiteConn.SetConnectionToExistingDB();
                                        SQLLiteConn.InsertRecords(str[2], str[3], Variables.message, Variables.timeStamp);
                                        SQLLiteConn.CloseConn();
                                        ScrollToTop();
                                    }
                                    else if (str.Count() == 3)
                                    {
                                        SQLLiteConn.SetConnectionToExistingDB();
                                        SQLLiteConn.InsertRecords(str[2], "", Variables.message, Variables.timeStamp);
                                        SQLLiteConn.CloseConn();
                                        ScrollToTop();
                                    }
                                }
                                else if (Variables.timeFormat.Equals("12 Hours Format"))
                                {
                                    Variables.timeStamp = str[0] + " " + str[1] + " " + str[2];
                                    if (str.Count() == 5)
                                    {
                                        SQLLiteConn.SetConnectionToExistingDB();
                                        SQLLiteConn.InsertRecords(str[3], str[4], Variables.message, Variables.timeStamp);
                                        SQLLiteConn.CloseConn();
                                        ScrollToTop();
                                    }
                                    else if (str.Count() == 4)
                                    {
                                        SQLLiteConn.SetConnectionToExistingDB();
                                        SQLLiteConn.InsertRecords(str[3], "", Variables.message, Variables.timeStamp);
                                        SQLLiteConn.CloseConn();
                                        ScrollToTop();
                                    }
                                }
                            }
                        }
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine("Exception Raised while waiting for new message " + e.GetBaseException());
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
                Int32 countOfEntredRecords = 0;
                for (Int32 i = 1; i <= countOfEntries; i++)
                {
                    if (General.PresenceOfElement("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]"))
                    {
                        Console.WriteLine(i);
                        string tmp = General.driver.FindElement(By.XPath("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]")).GetAttribute("data-pre-plain-text");
                        string[] str = tmp.Replace("[", "").Replace("]", "").Replace(",", "").Replace(": ", "").Split(' ');
                        Variables.message = General.driver.FindElement(By.XPath("//div[contains(@class,'vW7d1')][" + i + "]//div[contains(@class,'copyable-text')]//span")).Text;
                        Variables.message = Variables.message.Replace("'", "");

                        if (str[1].ToLower().Equals("am") || str[1].ToLower().Equals("pm"))
                            Variables.timeFormat = "12 Hours Format";
                        else
                            Variables.timeFormat = "24 Hours Format";
                      
                        if (Variables.timeFormat.Equals("24 Hours Format"))
                        {
                            Variables.timeStamp = str[0] + " " + str[1];
                            if (str.Count() == 4)
                            {
                                SQLLiteConn.InsertRecords(str[2], str[3], Variables.message, Variables.timeStamp);
                                countOfEntredRecords++;
                            }
                            else if (str.Count() == 3)
                            {
                                SQLLiteConn.InsertRecords(str[2], "", Variables.message, Variables.timeStamp);
                                countOfEntredRecords++;
                            }
                        }
                        else if (Variables.timeFormat.Equals("12 Hours Format"))
                        {
                            Variables.timeStamp = str[0] + " " + str[1] + " " + str[2];
                            if (str.Count() == 5)
                            {
                                SQLLiteConn.InsertRecords(str[3], str[4], Variables.message, Variables.timeStamp);
                                countOfEntredRecords++;
                            }
                            else if (str.Count() == 4)
                            {
                                SQLLiteConn.InsertRecords(str[3], "", Variables.message, Variables.timeStamp);
                                countOfEntredRecords++;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
                Console.WriteLine("Total Number of Records entered in DB are " + countOfEntredRecords);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
