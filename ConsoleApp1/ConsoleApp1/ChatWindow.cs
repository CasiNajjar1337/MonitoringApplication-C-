using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class ChatWindow
    {
        public static bool ScrollToTop()
        {
            try
            {
                General.driver.FindElement(By.XPath("//div[@class='_2nmDZ']")).Click();

                for (Int32 i = 1; ; i++)
                {
                    Actions actions = new Actions(General.driver);
                    actions.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();

                    //  Thread.Sleep(2000);
                    //  Console.WriteLine(i);
                    if (General.PresenceOfElement("//div[@class='copyable-area']//span[text()='You created this group']"))
                    {
                        actions.KeyDown(Keys.Control).SendKeys(Keys.Home).Perform();
                        break;
                    }
                }
                //Thread.Sleep(3000);
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
                    if(!General.PresenceOfElement("//div[@class='copyable-area']//span[text()='You created this group']", 60)||General.PresenceOfElement("//div[@role='button']//span[@class='OUeyt']",60))
                    {
                        General.driver.FindElement(By.XPath("//div[@role='button']//span[@data-icon='down']")).Click();
                        if (General.PresenceOfElement("//div[contains(@class,'1mq8g')]/span[contains(text(),'unread message')]"))
                        {
                            Int32 count = General.driver.FindElements(By.XPath("//div[contains(@class,'1mq8g')]/following-sibling::div[contains(@class,'vW7d1')]")).Count;
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
                                    SQLLiteConn.CreateAndInsertToTable(tableName, "FirstNameOrPhoneNo", "LastName", "Message", "TimeStamp", str[2], str[3], message, timeStamp);
                                    ScrollToTop();
                                }
                                else
                                {
                                    SQLLiteConn.CreateAndInsertToTable(tableName, "FirstNameOrPhoneNo", "LastName", "Message", "TimeStamp", str[2], "", message, timeStamp);
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
                Int32 numberOfEntries = General.driver.FindElements(By.XPath("//div[contains(@class,'vW7d1')]")).Count;
                Int32 numberOfMessages = General.driver.FindElements(By.XPath("//div[contains(@class,'vW7d1')]/div[contains(@class,'message')]")).Count;
                Console.WriteLine(numberOfMessages);
                //Int32 numberOfActualTexts = numberOfMessages + 4;
                Int32 countOfEntries = 0;
                //Console.WriteLine("Total Number of Texts in the chat Window are :: " + numberOfActualTexts);
                for (Int32 i = 1; i <= numberOfEntries; i++)
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
                            SQLLiteConn.CreateAndInsertToTable(tableName, "FirstNameOrPhoneNo", "LastName", "Message", "TimeStamp", str[2], str[3], message, timeStamp);
                            countOfEntries++;
                        }
                        else
                        {
                            SQLLiteConn.CreateAndInsertToTable(tableName, "FirstNameOrPhoneNo", "LastName", "Message", "TimeStamp", str[2], "", message, timeStamp);
                            countOfEntries++;
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
