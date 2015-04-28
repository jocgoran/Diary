using System.Windows.Forms;
using System;
using System.IO;
using System.Data;

namespace Diary.Controller
{
    public sealed class EventHandler
    {
        PoolManager poolManager = PoolManager.Instance;

        // singleton design pattern for initalization
        private static volatile EventHandler instance;
        private static object syncRoot = new Object();

        public EventHandler() { }

        public static EventHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new EventHandler();
                    }
                }

                return instance;
            }
        }

        // begin with the list of functions of forms
        public void HandlerDispatcher(object sender, EventArgs e)
        {
            // used to create GUIElements
            string GUIObjectName;
            dynamic GUIObject = null;

            // Dispatch Handlers according to DB entries
            foreach (DataRow row in GlobalVar.DataSet.Tables["GUIEventToHandler"].Rows) // Loop over the rows.
            {
                // compose GUIObjectName
                GUIObjectName = row["GUIElementType"].ToString() + "_" + row["GUIElementId"].ToString();

                // Get GUIObject
                poolManager.GetObject(GUIObjectName, ref GUIObject);

                if (GUIObject.Equals(sender))
                {
                    System.Reflection.MethodInfo method = this.GetType().GetMethod(row["Handler"].ToString(), new[] 
                                            { 
                                            typeof(object), 
                                            typeof(EventArgs) 
                                            });
                    method.Invoke(this, new object[] { sender, e});
                    
                    TraceEvent(sender, e, row["Handler"].ToString());
                    
                }
            }
        }
       

        // begin with the list of functions of forms
        public void Login(object sender, EventArgs e)
        {
            {

            }
        }
        
        // begin with the list of functions of forms
        public void SaveTheDataSet(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save?", "Save", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
 
            }
        }

        public void TraceEvent(object sender, EventArgs e, string HandlerName)
        {
            using (StreamWriter w = File.AppendText("C:\\TEMP\\log.txt"))
            {
                w.WriteLine("{0} - Event triggered from {1} - Called handler: {2}", DateTime.Now.ToString("yyyy.MM.dd HH.mm.s"), sender.ToString(), HandlerName);
            }
        } // end trace event
    }
}
