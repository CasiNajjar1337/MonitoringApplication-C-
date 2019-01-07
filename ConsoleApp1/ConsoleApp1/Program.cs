using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            
            General.OpenBrowser(Variables.uRL);
            HomePage.SearchGroup(Variables.groupName);
            //Setting up the data base
            SQLLiteConn.SetConnection(Variables.dataBase);
            //Creating the table
            SQLLiteConn.CreateTable(Variables.tableName, Variables.columnName1, Variables.columnName2, Variables.columnName3, Variables.columnName4);
            ChatWindow.ScrollToTop();
            ChatWindow.StoreInitialTexts();
            ChatWindow.MonitorForNewMessages();
           
            Console.WriteLine("Enter Key to Quit");
            Console.ReadKey();

            SQLLiteConn.CloseConn();
            General.CloseDriver();
        }


    }
    
}
