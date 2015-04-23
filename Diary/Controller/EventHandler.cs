using System.Windows.Forms;
using System;

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
        public void SaveTheDataSet(object sender, System.EventArgs e)
        {
            MessageBox.Show("Here come the save function");
        }  

    }
}
