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

                // Get run-time type of the GUIElement instance
                Type currentType = GUIObject.GetType();

                // Get from GUIElement the Event you want to tring
                EventInfo eventInfo = currentType.GetEvent(row["Event"].ToString());

                // Get the Action (functions) from EventHandler object to Run when event is trigged
                EventHandler EventHandlers = new EventHandler();
                MethodInfo methodInfo = EventHandlers.GetType().GetMethod(row["Handler"].ToString(), 
                                                                            new[] 
                                                                                { 
                                                                                typeof(object), 
                                                                                typeof(System.EventArgs) 
                                                                                }
                                                                            );

                // Use Delegate to hook the Action (function) to the GUIElement  
                Type tDelegate = eventInfo.EventHandlerType;
                Delegate del = Delegate.CreateDelegate(tDelegate, EventHandlers, methodInfo);
                
                //Add the Delegate to GUIElement
                eventInfo.AddEventHandler(GUIObject, del);

            } // end DataSet loop

        } // end eventTrigger
     
    } // end class


}
