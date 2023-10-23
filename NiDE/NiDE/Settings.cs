using NiDE.NiDE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiDE
{
    public partial class Settings : Form
    {

        public int theme = -1;

        public Settings()
        {
            InitializeComponent();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (ThemeListBox.SelectedIndex != -1) 
            {
                switch (ThemeListBox.SelectedIndex)
                {
                    case -1:
                    case 0:
                        theme = ThemeListBox.SelectedIndex;
                        this.Close();
                        break;

                    case 1:
                        theme = ThemeListBox.SelectedIndex;
                        this.Close();
                        break;
                }
            }
        }

        private void Basic_Click(object sender, EventArgs e)
        {

        }

        private void NumericHeight_ValueChanged(object sender, EventArgs e)
        {

        }

        private void NumericWidth_ValueChanged(object sender, EventArgs e)
        {

        }

        private void ThemeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
