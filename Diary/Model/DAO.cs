using System.Data;
using Diary.Model;

namespace Diary
{
    class DAO : ADataProvider
    {

        public void GetTable(string TableName)
        {
            // if necessary, load the data of the table in the Global dataset
            if (GlobalVar.DataSet.Tables.Contains(TableName) == false)
            {
                SQLiteAccess DBConnection = new SQLiteAccess();
                DBConnection.FillDataTable(TableName);
                Render(TableName);
            }           
        }

        public void GetTupla(DataTable tableName)
        {
            SQLiteAccess DBConnection = new SQLiteAccess();
           // DBConnection.LoadTableInDataSet();

        }

    } // end class
} // end namespace
