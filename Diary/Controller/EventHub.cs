using Diary.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diary.Controller
{
    class EventHub: IViewer
    {
        PoolManager poolManager = PoolManager.Instance;

        public void Render(string TableName)
        {
            if (TableName != "GUIEventToHandler") return;

            // used to create GUIElements
            dynamic GUIObject = null;

            foreach (string GUIObjectName in poolManager.objPool.Keys)
            {
                // Get GUIObject
                poolManager.GetObject(GUIObjectName, ref GUIObject);

                // Get run-time type of the GUIElement instance
                Type currentType = GUIObject.GetType();

                // prepare to list all Events for trigger
                DataView view = new DataView(GlobalVar.DataSet.Tables[TableName]);
                DataTable AllEvents = new DataTable();
                AllEvents = view.ToTable(true, "Event");

                // add all Events to every GUIElement
                foreach (DataRow row in AllEvents.Rows) // Loop over the rows.
                {
                    // Get from GUIElement the Event you want to tring
                    EventInfo eventInfo = currentType.GetEvent(row["Event"].ToString());

                    // if the GUIElement support the event
                    if (eventInfo!=null)
                    {
                        // Get the Action (functions) from EventHandler object to Run when event is trigged
                        EventHandler EventHandlers = new EventHandler();
                        MethodInfo methodInfo = EventHandlers.GetType().GetMethod("HandlerDispatcher",
                                                                                    new[] 
                                                                                        { 
                                                                                        typeof(object), 
                                                                                        typeof(EventArgs) 
                                                                                        }
                                                                                    );

                        // Use Delegate to hook the Action (function) to the GUIElement  
                        Type tDelegate = eventInfo.EventHandlerType;
                        Delegate del = Delegate.CreateDelegate(tDelegate, EventHandlers, methodInfo);

                        //Add the Delegate to GUIElement
                        eventInfo.AddEventHandler(GUIObject, del);
                    }
             
                } // end DataView loop
    
            } // end Object loop

        } // end eventTrigger
    }
}
