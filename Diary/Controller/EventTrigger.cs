using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.View;
using System.Data;

namespace Diary.Controller
{
    class EventTrigger: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        public EventTrigger()
        {
        } 
            
        public void Render(string TableName)
        {
            if (TableName != "EventToHandler") return;
            
            // used to create GUIElements
            string GUIObjectName;
            dynamic GUIObject = null;

            // create all Events and Handlers
            foreach (DataRow row in GlobalVar.DataSet.Tables[TableName].Rows) // Loop over the rows.
            {
                // compose GUIObjectName
                GUIObjectName = row["GUIElementType"].ToString() + "_" + row["GUIElementId"].ToString();                
                
                // Get GUIObject
                poolManager.GetObject(GUIObjectName, ref GUIObject);  
                
                // Set Event Trigger and Handler
                row["Event"].ToString();
                row["Handler"].ToString();
                //GUIObject.MouseDoubleClick+=new EventHandler(t_MouseDoubleClick);
                //GUIObject.Click += new EventHandler(item_click);
                    
            } // end DataSet loop

        } // end eventTrigger
               
    } // end class


}
