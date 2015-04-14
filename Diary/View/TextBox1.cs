using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Diary.Controller;

namespace Diary.View
{
    public class TextBox1 : TextBox
    {

        public TextBox1()
        {
            // Set to no text.
            this.Text = "";
            // The password character is an asterisk.
            this.PasswordChar = '\0';
            // The control will allow no more than 14 characters.
            this.MaxLength = 14;
            this.Location = new Point(1 * 60, 1 * 10);
            this.Visible = true;
        }

    } // end class
} // end namespace
