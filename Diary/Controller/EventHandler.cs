using System.Windows.Forms;
using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

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
            string GUIObjectName = ((dynamic)sender).Name;

            //System.Windows.Forms.MouseEventArgs e)
            MouseEventArgs me = e as MouseEventArgs;
            if (me == null) return;
            
            // Get Handlers (functions) for this Event
            string HandlersToBind = "(Event = 'all'";

            // handle the Events
            if(me.Button == MouseButtons.Left)  
            HandlersToBind = HandlersToBind + " OR Event = 'MouseClick'";            

            // Get Handlers (functions) for this Event
            HandlersToBind = HandlersToBind + ")";

            // Get Handlers (functions) for this Field
            HandlersToBind = HandlersToBind + " AND GUIElementType + '_' + GUIElementId = '" + GUIObjectName + "'";

            // Use the Select method to find all Handlers to bind
            DataRow[] RowsToBind = GlobalVar.DataSet.Tables["GUIEventToHandler"].Select(HandlersToBind);

            // Get GUIObject
            dynamic GUIObject = null;
            poolManager.GetObject(GUIObjectName, ref GUIObject);

            // Invoke all assigned handlers
            foreach (DataRow row in RowsToBind) // Loop over the rows.
            {

                // bind method
                System.Reflection.MethodInfo method = this.GetType().GetMethod(row["Handler"].ToString(), new[] 
                                        { 
                                        typeof(object), 
                                        typeof(EventArgs) 
                                        });
                method.Invoke(this, new object[] { sender, e});
                
                // always call this function to write Log
                TraceEvent(sender, e, row["Handler"].ToString());
                    
            }
        }

        // begin with the list of functions of forms
        public void Login(object sender, EventArgs e)
        {

            // Get Login
            dynamic GUIObject = null;
            poolManager.GetObject("field_1", ref GUIObject);
            string loginName = GUIObject.Text;
            // Get Password
            poolManager.GetObject("field_2", ref GUIObject);
            string password = GUIObject.Text;

            DataRow[] LoggedUser = GlobalVar.DataSet.Tables["user"].Select("login = '" + loginName + "' AND password = '" + password + "'");
            if (LoggedUser.Length > 0)
            {
                DialogResult dialogResult = MessageBox.Show("Ok, you are logged in", "Yes", MessageBoxButtons.YesNo);

                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
                DateTime when = DateTime.FromBinary(BitConverter.ToInt64(time, 0));

                GlobalVar.dicToken.Add(token, time);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Username or password are wrong.", "Message");
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
