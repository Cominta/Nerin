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
        public TextBox TextBoxClassicTheme(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(64, 64, 64);
            textBox.ForeColor = Color.White;

            return textBox;
        }

        public Panel PanelClassicTheme(Panel panel)
        {
            panel.BackColor = Color.FromArgb(32, 32, 32);
            return panel;
        }

        public Panel DecorPanelClassicTheme(Panel panel)
        {
            panel.BackColor = Color.FromArgb(192, 64, 0);
            return panel;
        }

        public Button ButtonClassicTheme(Button button)
        {
            button.BackColor = Color.FromArgb(192, 64, 0);
            return button;
        }

        public TextBox TextBoxLightTheme(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(255, 255, 255);
            textBox.ForeColor = Color.Black;

            return textBox;
        }

        public Panel PanelLightTheme(Panel panel) 
        {
            panel.BackColor = Color.FromArgb(222, 222, 222);
            return panel;
        }

        public Button ButtonPurpleTheme(Button button)
        {
            button.BackColor = Color.FromArgb(138, 63, 196);
            return button;
        }

        public Panel PanelPurpleTheme(Panel panel)
        {
            panel.BackColor = Color.FromArgb(138, 63, 196);
            return panel;
        }

        
    }
}
