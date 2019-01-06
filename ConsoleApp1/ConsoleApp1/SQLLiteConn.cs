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
        // We use these three SQLite objects:
        static SQLiteConnection sqlite_conn;
        public static SQLiteCommand sqlite_cmd;
        public static SQLiteDataReader sqlite_datareader;

       public static bool SetConnection(string DBName)
        {
            try
            {
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=" + DBName + ".db;Version=3;New=True;Compress=True;");

                // open the connection:
                sqlite_conn.Open();
                Console.WriteLine("Connection to the DB " + DBName + " is created successfully");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Creating the connection failed");
                return false;
            }
        }

        public static bool CreateTable(string TableName, string col1, string col2, string col3, string col4)
        {
            try
            {
                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();
                // Let the SQLiteCommand object know our SQL-Query:
                sqlite_cmd.CommandText = "CREATE TABLE " + TableName + " (" + col1 + " varchar(10) , " + col2 + " varchar(10) , " + col3 + " varchar(250) , " + col4 + " varchar(5));";
                // Now lets execute the SQL ;D
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Table creation of " + TableName + " is success");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Table creation of " + TableName + " failed");
                return false;
            }
        }
        public static bool CreateAndInsertToTable(string TableName, string col1, string col2, string col3, string col4, string fNameValue, string lNameValue, string messageValue, string timeValue)
        {
            try
            {
                // Lets insert something into our new table:
                sqlite_cmd.CommandText = "INSERT INTO " + TableName + " ("+col1+","+col2+","+col3+","+col4+") VALUES ('" + fNameValue + "', '" + lNameValue + "', '" + messageValue + "', '" + timeValue + "');";
                // And execute this again ;D
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Inserting Successfull");
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Writing Message " + messageValue + " is failed");
                return false;
            }
        }
        public static bool CloseConn()
        {
            try
            {
                // We are ready, now lets cleanup and close our connection:
                sqlite_conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool GetData(string query)
        {
            try
            {
                sqlite_cmd = sqlite_conn.CreateCommand();
                Console.WriteLine("Passedd Query");
                sqlite_cmd.CommandText = query;
                Console.WriteLine("Took Query");
                //sqlite_cmd.
                // Now the SQLiteCommand object can give us a DataReader-Object:
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                Console.WriteLine("Execute Query");
                // The SQLiteDataReader allows us to run through the result lines:
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                    Console.WriteLine(Convert.ToString(sqlite_datareader["Sample"]));
                }
                return true;
            }catch(Exception e)
            {
                e.GetBaseException().ToString();
                return false;
            }
        }
    }
}
