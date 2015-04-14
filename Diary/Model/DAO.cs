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
                DBConnect DBConnection = new DBConnect();
                DBConnection.FillDataTable(TableName);
                Render(TableName);
            }           
        }

        public void GetTupla(DataTable tableName)
        {
            DBConnect DBConnection = new DBConnect();
           // DBConnection.LoadTableInDataSet();

        }

    } // end class
} // end namespace
