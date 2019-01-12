using Finisar.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class SQLLiteConn
    {
        public static SQLiteConnection sqlite_conn;
        public static SQLiteCommand sqlite_cmd;
        
       public static bool SetNewConnection()
        {
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=" + Variables.dataBase + ".db;Version=3;New=True;Compress=True;");
                sqlite_conn.Open();
                if (sqlite_conn.State == System.Data.ConnectionState.Open)
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    Console.WriteLine("Connection to the DB " + Variables.dataBase + " is created successfully");
                    return true;
                }
                else
                    Console.WriteLine("Connection to the DB is not Opened");
                return false;
            }
            catch (Exception)
            {
                Console.WriteLine("Creating the connection failed");
                return false;
            }
        }
        public static bool SetConnectionToExistingDB()
        {
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=" + Variables.dataBase + ".db;Version=3;New=False;Compress=True;");
                sqlite_conn.Open();
                if (sqlite_conn.State == System.Data.ConnectionState.Open)
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    Console.WriteLine("Connection to the Existing DB " + Variables.dataBase + " is created successfully");
                    return true;
                }
                else
                    Console.WriteLine("Connection to the existing DB is not Opened");
                    return false;
            }
            catch (Exception)
            {
                Console.WriteLine("Opening the connection failed");
                return false;
            }
        }
        public static bool CreateTable()
        {
            try
            {
                sqlite_cmd.CommandText = "CREATE TABLE " + Variables.tableName + " (" + Variables.columnName1 + " varchar(10) , " + Variables.columnName2 + " varchar(10) , " + Variables.columnName3 + " varchar(250) , " + Variables.columnName4 + " varchar(10));";
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Table creation of " + Variables.tableName + " is success");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Table creation of " + Variables.tableName + " failed " + e.GetBaseException());
                return false;
            }
        }

        public static bool InsertRecords(string fNameValue, string lNameValue, string messageValue, string timeValue)
        {
            try
            {
                sqlite_cmd.CommandText = "INSERT INTO " + Variables.tableName + " ("+ Variables.columnName1+ ","+ Variables.columnName2+ ","+Variables.columnName3+","+ Variables.columnName4+ ") VALUES ('" + fNameValue + "', '" + lNameValue + "', '" + messageValue + "', '" + timeValue + "');";
                sqlite_cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Writing Message " + messageValue + " is failed ::"+e.GetBaseException());
                return false;
            }
        }

        public static bool CloseConn()
        {
            try
            {
                sqlite_conn.Close();
                sqlite_conn.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
    }
}
