using System.Drawing;
using System.Windows.Forms;

namespace Diary.View
{
    public class Label1: Label
    {
  
        // Constructor 
        public Label1()
        {
            //Initialize label's property
            //this.Text = "";
            //this.Location = new Point(0 * 60, 1 * 20);
            this.AutoSize = true;

            // Set the border to a three-dimensional border.
            //this.BorderStyle = System.Windows.Forms.BorderStyle.None;

            // Specify that the text can display mnemonic characters.
            this.UseMnemonic = true;
 
            /* Set the size of the control based on the PreferredHeight and PreferredWidth values. */
            this.Size = new Size(this.PreferredWidth, this.PreferredHeight);

        }
    } // end class
} // end namespace
