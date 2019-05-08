using System;
using System.Data;
using System.Data.SQLite;

namespace Diary
{
    public class SQLiteAccess
    {
        // singleton design pattern for initalization
        private static volatile SQLiteAccess instance;
        private static object syncRoot = new Object();

        // some object variables
        private static SQLiteConnection sqLiteConnection;
        private static SQLiteCommand sqLiteCommand;
        private static SQLiteDataAdapter sqLiteDataAdapter;


        //Singleton
        public static SQLiteAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new SQLiteAccess();
                    }
                }

                return instance;
            }
        }


        //Constructor
        public SQLiteAccess()
        {
            OpenConnection();
        }

        //Open Connection to database
        private void OpenConnection()
        {
            // Set the connection string to SQLite
            string connectionString = "Data Source=DiaryFormFields.sqlite; Version = 3; Pooling=True; Max Pool Size=100; Compress = True; Legacy Format=True";

            // create connection
            sqLiteConnection = new SQLiteConnection(connectionString); 

            // create command
            sqLiteCommand = sqLiteConnection.CreateCommand();
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
            sqLiteDataAdapter = new SQLiteDataAdapter(SQLQuery, sqLiteConnection);

            // Fill Table data into DataSet            
            sqLiteDataAdapter.FillSchema(GlobalVar.DataSet, SchemaType.Source, TableName);
            sqLiteDataAdapter.Fill(GlobalVar.DataSet, TableName);
        }

        //Select statement
        public void Select()
        {
        }

    } // end class
} // end namespace
