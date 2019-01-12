namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            General.OpenBrowser(Variables.uRL);
            HomePage.SearchGroup(Variables.groupName);
            SQLLiteConn.SetNewConnection();
            SQLLiteConn.CreateTable();
            ChatWindow.ScrollToTop();
            ChatWindow.StoreInitialTexts(Variables.tableName);
            SQLLiteConn.CloseConn();
            ChatWindow.MonitorForNewMessages();
        }
    }
}
