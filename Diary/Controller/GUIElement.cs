using Diary.View;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Diary.Controller
{
    public class GUIElement: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        public GUIElement()
        {
        } 
            

        public void Render(string TableName)
        {
            switch (TableName)
            {
                case "form":
                case "tab":
                case "field":
                {
                    BuildGuiElements(TableName);
                    SetAttributesOfGUIElements(TableName);
                    break;
                }
                case "asgmt_form_tab":
                case "asgmt_form_field":
                case "asgmt_tab_field":
                {
                    InterlaceGUIElements(TableName);
                    break;
                }
                case "GUIEventToHandler":
                {
                    break;
                }
                default:
                {
                    UdpdateFieldValue(TableName);
                    break;
                }
            }
        //MessageBox.Show(poolManager.CurrentObjectsInPool.ToString());
        }


        public void BuildGuiElements(String TableName)
        {
            // used to create GUIElements
            string GUIObjectName;
            dynamic GUIObject = null;

            // create all Forms, tabs and fields
            foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
            {
                // compose GUIObjectName
                GUIObjectName = TableName + "_" + row["id"].ToString();

                // get Object to manage
                poolManager.GetObject(GUIObjectName, ref GUIObject);

                // check if objct need to be created
                if (null == GUIObject)
                {
                    // create the object of correct class-type
                    switch (TableName)
                    {
                        case "form":
                        {
                            GUIObject = new Form1();
                            break;
                        }
                        case "tab":
                        {
                            GUIObject = new TabPage();
                            break;
                        }
                        case "field":
                        {
                            switch (row["type"].ToString())
                            {
                                case "Text":
                                {
                                    GUIObject = new TextBox1();
                                    break;
                                }
                                case "Button":
                                {
                                    GUIObject = new Button1();
                                    break;
                                }
                                case "Label":
                                {
                                    GUIObject = new Label1();
                                    break;
                                }
                                default:
                                {
                                    // column "type" has undefined Field Type 
                                    break;
                                }
                            }
                            break;
                        }
                    } // end switch  
  
                    // Add the new generated object to pool
                    poolManager.AddObject(GUIObjectName, GUIObject);
                    //MessageBox.Show(poolManager.CurrentObjectsInPool.ToString());

                } //endif   
            } // end loop
        }


        public void SetAttributesOfGUIElements(String TableName)
        {
            foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
            {
                // used to create GUIElements
                string GUIObjectName;
                dynamic GUIObject = null;

                // compose GUIObjectName
                GUIObjectName = TableName + "_" + row["id"].ToString();

                // get Object to manage
                poolManager.GetObject(GUIObjectName, ref GUIObject);

                // set all Attributes of Form/Tab/Field
                foreach (DataColumn column in GlobalVar.DataSet.Tables[TableName].Columns)
                {
                    string ColName = column.ColumnName;
                    switch (ColName)
                    {
                        case "id":
                        case "type":
                        case "table":
                        case "column":
                        case "Name":
                        {
                            break;
                        }
                        default:
                        {

                            // set value of the object attribute
                            if (row[ColName].ToString() == "") break;
                            PropertyInfo cntrlProperty = GUIObject.GetType().GetProperty(ColName);
                           
                            // define correct type
                            switch (cntrlProperty.PropertyType.ToString())
                            {
                                case "System.Byte":
                                case "System.Boolean":
                                {
                                    cntrlProperty.SetValue(GUIObject, Convert.ToBoolean(row[ColName].ToString()));
                                    break;
                                }
                                case "System.Char":
                                {
                                    cntrlProperty.SetValue(GUIObject, Convert.ToChar(row[ColName].ToString()[0]));
                                    break;
                                }
                                case "System.String":
                                {
                                    cntrlProperty.SetValue(GUIObject, row[ColName].ToString());
                                    break;
                                }
                            }
                                
                            //var value = cntrlProperty.GetValue(GUIObject);
                            
                            break;
                        }
                    }
                } // end col loop

                // redraw Form and all included controls
                GUIObject.Refresh();

            } // end row loop
        }

        public void InterlaceGUIElements(String TableName)
        {
            // used to create GUIElements
            string GUIObjectName;
            dynamic GUIObject = null;

            // execute assignment
            foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
            {
                // used only for GUIElements assignments
                string[] TableNameGUIElements = TableName.Split('_');

                //Compose GUIContainer name (Form of Tab)
                string GUIContainerName = TableNameGUIElements[1] + "_" + row[TableNameGUIElements[1] + "_id"].ToString();

                //Get GUIContainer (form or tab)
                dynamic GUIContainer = null;
                poolManager.GetObject(GUIContainerName, ref GUIContainer);

                //Compose GUIObjectName (field or tab)
                GUIObjectName = TableNameGUIElements[2] + "_" + row[TableNameGUIElements[2] + "_id"].ToString();

                //Get GUIObject
                poolManager.GetObject(GUIObjectName, ref GUIObject);

                // Set position
                if (TableNameGUIElements[2] == "field")
                {
                    int x=0, y=0;
                    if (!DBNull.Value.Equals(row["locationX"]))
                        x = Convert.ToInt32(row["locationX"]);
                    if (!DBNull.Value.Equals(row["locationY"]))
                        y = Convert.ToInt32(row["locationY"]);
                 
                    GUIObject.Location = new Point(x * 60, y * 20);
                }

                // Add GUIObject
                GUIContainer.Controls.Add(GUIObject);
            } //end loop
        }


        public void UdpdateFieldValue(String TableName)
        {
            // Update the correct Field.Value attribute
            // execute assignment
            foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
            {
                // set Text Attributes of the Field
                foreach (DataColumn column in GlobalVar.DataSet.Tables[TableName].Columns)
                {
                    string ColName = column.ColumnName;

                    // Search table and column in Field DataTable 
                    string expression = "table = '" + TableName + "' and column = '" + ColName + "'";

                    // Use the Select method to find all Fields that use matching the filter.
                    DataRow[] foundRows = GlobalVar.DataSet.Tables["field"].Select(expression);

                    foreach (DataRow dr in foundRows)
                    {
                        // Get ID of Form
                        string FieldID = dr["id"].ToString();
                    
                        //Get GUIObject
                        dynamic GUIObject = null;
                        poolManager.GetObject("field_" + FieldID, ref GUIObject);

                        // Set Text
                        GUIObject.Text = row[ColName].ToString();
                        //PropertyInfo cntrlProperty = GUIObject.GetType().GetProperty("Text");
                        //cntrlProperty.SetValue(GUIObject, row[ColName].ToString());
                    }

                }

            }

        }

    } // end class
} // end namespace
