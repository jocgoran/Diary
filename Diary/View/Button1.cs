using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diary.View
{
    public class Button1: Button
    {
        public Button1()
        {
            // Set to no text.
            //this.Text = "";
        }

        public void SaveTheDataSet(System.Object sender, System.EventArgs e)
        {
            MessageBox.Show("Hello! This is save event 11");
        }     
    } // end class
} // end namespace
