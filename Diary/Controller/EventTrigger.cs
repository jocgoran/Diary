using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.View;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

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
            if (TableName != "GUIEventToHandler") return;
            
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

                // get run-time type of the instance
                Type currentType = GUIObject.GetType();

                // Get Event to tring
                EventInfo eventInfo = currentType.GetEvent(row["Event"].ToString());
                Type tDelegate = eventInfo.EventHandlerType;

                MethodInfo methodInfo = currentType.GetMethod(row["Handler"].ToString(), new [] { typeof(object), typeof(System.EventArgs) });
                
                //MethodInfo methodInfo = currentType.GetMethod(row["Handler"].ToString());

                
                Delegate del = Delegate.CreateDelegate(tDelegate, GUIObject, methodInfo);
                MethodInfo addHandler = eventInfo.GetAddMethod();

                //eventInfo.AddEventHandler(GUIObject, del);

            } // end DataSet loop

        } // end eventTrigger
        public void SaveTheDataSet(object sender, System.EventArgs e)
        {
            MessageBox.Show("Hello! This is save event");
        }      
    } // end class


}
