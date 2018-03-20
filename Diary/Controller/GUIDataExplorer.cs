using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Diary.Controller
{
    public class GUIDataExplorer
    {
        PoolManager poolManager = PoolManager.Instance;

        public static Dictionary<int, List<int>> PKsToRender = new Dictionary<int, List<int>>();

        // singleton design pattern for initalization
        private static volatile GUIDataExplorer instance;
        private static object syncRoot = new Object();

        //Constructor
        public GUIDataExplorer() { }

        //Singleton
        public static GUIDataExplorer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GUIDataExplorer();
                    }
                }

                return instance;
            }
        }

        public void SetPKsToRender(string FieldID, int PKsValue)
        {
            PKsToRender.Remove(Int32.Parse(FieldID));

            //Set the PK comma delimited string of records in use
            List<int> list = new List<int>();

            // add the Max Pk of this table to rendering list for this field 
            list.Add(PKsValue);

            // each GUI FieldID can have a list of DB Table's IDs
            PKsToRender.Add(Int32.Parse(FieldID), list);
        }

        public void SetHigestPKsToRender(string FieldID)
        {
            PKsToRender.Remove(Int32.Parse(FieldID));

            //Set the PK comma delimited string of records in use
            List<int> list = new List<int>();

            //For this Table, get the last PK value
            int MaxPK = GetHigestPKsToRender(FieldID);

            // add the Max Pk of this table to rendering list for this field 
            list.Add(MaxPK);

            // each GUI FieldID can have a list of DB Table's IDs
            PKsToRender.Add(Int32.Parse(FieldID), list);
        }

        public int GetHigestPKsToRender(string FieldID)
        {
            //int MaxPK = 0;
            // Use the Select method to find info about this Field (only one row)
            DataRow FieldRow = GlobalVar.DataSet.Tables["field"].Rows.Find("id=" + FieldID);
            if (FieldRow == null) return 0;

            // read the tablename which is showed
            if (FieldRow["table"] == null) return 0;
            string TableName = FieldRow["table"].ToString();

            // get max ID of this table
            int MaxPK = Convert.ToInt32(GlobalVar.DataSet.Tables[TableName].AsEnumerable().Max(row => row["id"]));

            return MaxPK;
        }

        public string GetPKsToRender(string FieldID)
        {
            //Get the PK comma delimited string of records in use
            try
            {
                List<int> list = PKsToRender[Int32.Parse(FieldID)];
                string commaDelimitedPKs = String.Join(",", list);
                return commaDelimitedPKs;
            }
            catch
            {
                return "0";
            }
        }
    }
}
