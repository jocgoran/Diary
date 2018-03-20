using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Controller
{
    class DataSetUpdater
    {
        PoolManager poolManager = PoolManager.Instance;

        // singleton design pattern for initalization
        private static volatile DataSetUpdater instance;
        private static object syncRoot = new Object();

        // the renderer have imperatively to now about Data that are explored now
        GUIDataExplorer DataExplorer = new GUIDataExplorer();

        //Constructor
        public DataSetUpdater() { }

        //Singleton
        public static DataSetUpdater Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataSetUpdater();
                    }
                }

                return instance;
            }
        }

        public void UpdateDataSet()
        {
            
        }

        public void AcceptDataSetChanges(string GUIObjectName)
        {
            foreach (DataTable table in GlobalVar.DataSet.Tables)
            {
                // do commit of dataset
                table.AcceptChanges();
            }            
        }

        public void WriteFieldToDataSetRecord(string GUIObjectName,string TextValue)
        {
        // scompose GUIObjectName (field_[fieldID] )
        int FoundAt = GUIObjectName.IndexOf("_", 0)+1;
        string FieldID = GUIObjectName.Substring(FoundAt, GUIObjectName.Length - FoundAt);

        // Get GUIObject
        dynamic GUIObject = null;
        poolManager.GetObject(GUIObjectName, ref GUIObject);

        // Find the Table & Column of Fields
        DataRow[] fieldRows = GlobalVar.DataSet.Tables["field"].Select("id = " + FieldID);

        // Get Table and Column to update
        string Table = fieldRows[0]["table"].ToString();
        string Column = fieldRows[0]["column"].ToString();

        // Update DataSet
        int PKsToRender = Int32.Parse(DataExplorer.GetPKsToRender(FieldID));

        // Find the Table & Column of Fields
        DataRow[] UpdateTableRows = GlobalVar.DataSet.Tables[Table].Select("id = " + PKsToRender);  
                
        // Update Rows
        foreach(DataRow UpdateTableRow in UpdateTableRows)
            {
                UpdateTableRow[Column] = TextValue;
            }         
        }
    }
}
