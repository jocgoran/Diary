using Diary.View;
using System;
using System.Data;

namespace Diary.Controller
{
    public class GUIDataRenderer: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        Array PrimaryKesyToRender = new Array[3];

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

                // Get the Column of thatble that are rendered into the fields
                string ColName = FieldToUpdate["column"].ToString();

                // Render data onto the screen to each DataGridView
                if (ColName == "")
                { 
                    GUIObject.DataSource = GlobalVar.DataSet.Tables[TableName];
                    continue;
                }

                // Update the Field.Text attribute with the correct UniquePrimaryKey UPK_ID
                foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
                {
                    GUIObject.Text = row[ColName].ToString();
                    //PropertyInfo cntrlProperty = GUIObject.GetType().GetProperty("Text");
                    //cntrlProperty.SetValue(GUIObject, row[ColName].ToString());
                } // end loop

            } //end loop

        } // end function

    } // end class
}
