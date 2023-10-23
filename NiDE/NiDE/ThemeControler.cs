using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiDE.NiDE
{
    public class ThemeControler
    {
        public TextBox TextBoxClassicTheme(NideMain nideMain)
        {
            TextBox textBox = nideMain.GetTextBox();
            textBox.BackColor = Color.FromArgb(64, 64, 64);
            textBox.ForeColor = Color.White;

            return textBox;
        }

        public Panel PanelClassicTheme(NideMain nideMain)
        {
            Panel panel = nideMain.GetPanel();
            panel.BackColor = Color.FromArgb(32, 32, 32);

            return panel;
        }

        public TextBox TextBoxClassicLightTheme(NideMain nideMain)
        {
            TextBox textBox = nideMain.GetTextBox();
            textBox.BackColor = Color.FromArgb(200, 200, 200);
            textBox.ForeColor = Color.Black;

            return textBox;
        }

        public Panel PanelClassicLightTheme(NideMain nideMain) 
        {
            Panel panel = nideMain.GetPanel();
            panel.BackColor = Color.FromArgb(160, 160, 160);

            return panel;
        }
    }
}
