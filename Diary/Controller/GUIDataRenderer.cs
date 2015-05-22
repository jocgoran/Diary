using Diary.View;
using System;
using System.Collections.Generic;
using System.Data;

namespace Diary.Controller
{

    public class GUIDataRenderer: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        Dictionary<int, List<int>> PKsToRender = new Dictionary<int, List<int>>();

//        List<int> lPrimaryKesyToRender = new List<int>();

        public GUIDataRenderer()
        {
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
                if (!PKsToRender.ContainsKey(Int32.Parse(FieldID)))
                {
                    continue;
                }
                
                // Get all Records with PrimaryKey for this Field from Field_PrimaryKesyToRender
                string DataToDisplay = "id IN (" + PKsToRender[Int32.Parse(FieldID)] + ")";

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
