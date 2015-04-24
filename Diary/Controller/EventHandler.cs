using System.Windows.Forms;
using System;
using System.IO;

namespace Diary.Controller
{
    public sealed class EventHandler
    {

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
        public void Login(object sender, System.EventArgs e)
        {
            {

            }
        }
        
        // begin with the list of functions of forms
        public void SaveTheDataSet(object sender, System.EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to save?", "Save", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
 
            }
        }

        public void TraceEvent(object sender, System.EventArgs e)
        {
            using (StreamWriter w = File.AppendText("C:\\TEMP\\log.txt"))
            {
                w.Write("{0} - {1}", DateTime.Now.ToString("yyyy.MM.dd HH.mm.s"),"Log Text");
            }
        } // end trace event
    }
}
