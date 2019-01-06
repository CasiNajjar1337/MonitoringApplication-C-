using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            General.OpenBrowser("https://web.whatsapp.com/");
            HomePage.SearchGroup("TestDemo");
            //Setting up the data base and creating a table
            SQLLiteConn.SetConnection("Test");
            SQLLiteConn.CreateTable("Sample", "FirstNameOrPhoneNo", "LastName", "Message", "TimeStamp");
            ChatWindow.ScrollToTop();
            ChatWindow.StoreInitialTexts("Sample");
            ChatWindow.MonitorForNewMessages("Sample");
           // SQLLiteConn.GetData("SELECT COUNT (Message) FROM Sample");


            Console.WriteLine("Enter Key to Quit");
            Console.ReadKey();

            SQLLiteConn.CloseConn();
            General.CloseDriver();// driver.Quit();
        }


    }
    
}
