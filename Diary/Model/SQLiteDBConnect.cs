using System;
//using MySql.Data.MySqlClient;
using System.Data.SQLite;
using System.Data;

namespace Diary
{
    class DBConnect
    {
        static SQLiteConnection connection;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            string connectionString;
            connectionString = @"Data Source=C:\Users\Goran\Documents\Visual Studio 2013\Projects\Diary\Diary\SQLite\database.dat;Version=3;";

            try
            {
                connection = new SQLiteConnection(connectionString); 
            }
            catch
            {
                Console.Write(connection.ToString());
            }
        }

        //open connection to database
        private bool OpenConnection()
        {
            return true;
        }

        //Close connection
        private bool CloseConnection()
        {
            return true;
        }

        //Insert statement
        public void Insert()
        {
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
        public void FillDataTable(string TableName)
        {
            // create DataAdapter
            string SQLQuery = "Select * from " + TableName;
            //MySqlDataAdapter DataAdapter = new MySqlDataAdapter(SQLQuery, connection);
            SQLiteDataAdapter DataAdapter = new SQLiteDataAdapter(SQLQuery, connection);

            // Fill Table data into DataSet            
            DataAdapter.FillSchema(GlobalVar.DataSet, SchemaType.Source, TableName);
            DataAdapter.Fill(GlobalVar.DataSet, TableName);
        }

        //Select statement
        public void Select()
        {
        }

    } // end class
} // end namespace
