using Diary.View;
using System;
using System.Data;

namespace Diary.Controller
{

    public class GUIDataRenderer: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        // singleton design pattern for initalization
        private static volatile GUIDataRenderer instance;
        private static object syncRoot = new Object();

        // the renderer have imperatively to now about Data that are explored now
        GUIDataExplorer DataExplorer = new GUIDataExplorer();

        //Constructor
        public GUIDataRenderer() { }

        //Singleton
        public static GUIDataRenderer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GUIDataRenderer();
                    }
                }

                return instance;
            }
        }

        public void Render(string TableName)
        {
            UdpdateFieldsTextes(TableName);
        }

        private void UdpdateFieldsTextes(string TableName)
        {
            // Get all Fields with data of this table
            string expression = "table = '" + TableName + "'";

            // Use the Select method to find all Fields that use matching the filter.
            DataRow[] foundRows = GlobalVar.DataSet.Tables["field"].Select(expression);

            // Elaborate the Fields
            foreach (DataRow FieldToUpdate in foundRows)
            {
                // Get ID of Form
                string FieldID = FieldToUpdate["id"].ToString();

                //Get GUIObject
                dynamic GUIObject = null;
                poolManager.GetObject("field_" + FieldID, ref GUIObject);

                // Get the Column of the table that are rendered into the fields
                string ColName = FieldToUpdate["column"].ToString();

                // Render data onto the screen to each DataGridView
                if (ColName == "")
                { 
                    GUIObject.DataSource = GlobalVar.DataSet.Tables[TableName];
                    continue;
                }

              	// See whether it contains this string.
                if (DataExplorer.GetPKsToRender(FieldID) == "0")
                {
                    continue;
                }
                
                // Get all Records with PrimaryKey for this Field from Field_PrimaryKesyToRender
                string DataToDisplay = "id IN (" + DataExplorer.GetPKsToRender(FieldID) + ")";

                // Use the Select method to find all Fields that use matching the filter.
                DataRow[] RowsToDisplay = GlobalVar.DataSet.Tables[TableName].Select(DataToDisplay);

                // Update the Field.Text attribute with the correct UniquePrimaryKey UPK_ID
                foreach (DataRow row in RowsToDisplay) // Loop over the rows.
                {
                    GUIObject.Text = row[ColName].ToString();
                    //PropertyInfo cntrlProperty = GUIObject.GetType().GetProperty("Text");
                    //cntrlProperty.SetValue(GUIObject, row[ColName].ToString());
                } // end loop

            } //end loop

        } // end function



    } // end class
}
