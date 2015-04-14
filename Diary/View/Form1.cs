using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Diary
{
    public class Form1 : Form
    {

        // Constructor 
        public Form1()
        {
            //Set up the form.
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.ForeColor = Color.Black;
            this.Size = new System.Drawing.Size(300, 250);
            this.Text = "";
            this.AutoSize = true;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

    } // end class
} // end namespace
