using System;
using System.Windows.Forms;
using System.Data;
using Diary.Controller;

namespace Diary
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Set GLOBAL DataSet
            GlobalVar.DataSet = new DataSet("Diary");

            // Create data access
            DAO DataAccess = new DAO();
            // create GUIElementes
            GUIElement GUIElements = new GUIElement();

            // Add GUIElements as observers to observee (data provider)
            DataAccess.Subscribe(GUIElements);
            
            // create all form
            DataAccess.GetTable("form"); // also create all forms
            // create all fields
            DataAccess.GetTable("field"); // also create all fields
            // create all tabs
            DataAccess.GetTable("tab"); // also create all fields

            // assign form fields
            DataAccess.GetTable("asgmt_form_field"); // assign fields to form

            // create EventsToTriggers
            EventTrigger EventsToTriggers = new EventTrigger();
            // Add GUIElements as observers to observee (data provider)
            DataAccess.Subscribe(EventsToTriggers);

            // load all EventsToTriggers
            DataAccess.GetTable("GUIEventToHandler"); // also create all fields

            // create GUIDataRenderer that render user generated data
            GUIDataRenderer GUIDataRenderers = new GUIDataRenderer();
            // Add GUIDataRenderer as observers to observee (data provider)
            DataAccess.Subscribe(GUIDataRenderers);


            // fill fields with use saved data
            DataAccess.GetTable("event"); // also create all fields

            // start the first Form
            PoolManager poolManager = PoolManager.Instance;
            dynamic GUIObject = null;
            poolManager.GetObject("form_1", ref GUIObject);
            GUIObject.ShowDialog();
        }

    } // end class
} // end namespace
