using System;
using System.Data;
using System.Data.SqlClient;

namespace Diary
{
    class MSSQLAccess
    {
        private SqlConnection connection;
        private string server;
        private string database;
        //private string uid;
        //private string password;

        //Constructor
        public MSSQLAccess()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "LAPTOPT\\SQLEXPRESS";
            database = "diary";
            //uid = "root";
            //password = "root";
            string connectionString = "server=" + server + ";Trusted_Connection=yes;database=" + database  + ";connection timeout=30";
            try
            {
                connection = new SqlConnection(connectionString); 
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
            string SQLQuery = "Select * from [" + TableName + "];";
            SqlDataAdapter DataAdapter = new SqlDataAdapter(SQLQuery, connection);

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
