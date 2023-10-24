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
                    //Classic
                    case -1:
                    case 0:
                        theme = ThemeListBox.SelectedIndex;

                        this.BackColor = Color.FromArgb(60, 60, 60);
                        Apply.BackColor = Color.FromArgb(192, 64, 0);
                        Basic.BackColor = Color.FromArgb(192, 64, 0);
                        panel1.BackColor = Color.FromArgb(37, 37, 37);
                        panel2.BackColor = Color.FromArgb(192, 64, 0);
                        panel3.BackColor = Color.FromArgb(192, 64, 0);

                        this.Close();
                        break;

                    //Classic light
                    case 1:
                        theme = ThemeListBox.SelectedIndex;

                        this.BackColor = Color.FromArgb(255, 255, 255);
                        Apply.BackColor = Color.FromArgb(192, 64, 0);
                        Basic.BackColor = Color.FromArgb(192, 64, 0);
                        panel1.BackColor = Color.FromArgb(222, 222, 222);
                        panel2.BackColor = Color.FromArgb(192, 64, 0);
                        panel3.BackColor = Color.FromArgb(192, 64, 0);

                        this.Close();
                        break;
                    
                    //Purple dark
                    case 2:
                        theme = ThemeListBox.SelectedIndex;

                        this.BackColor = Color.FromArgb(60, 60, 60);
                        Apply.BackColor = Color.FromArgb(138, 63, 196);
                        Basic.BackColor = Color.FromArgb(138, 63, 196);
                        panel1.BackColor = Color.FromArgb(37, 37, 37);
                        panel2.BackColor = Color.FromArgb(138, 63, 196);
                        panel3.BackColor = Color.FromArgb(138, 63, 196);

                        this.Close();
                        break;

                    //Purple light
                    case 3:
                        theme = ThemeListBox.SelectedIndex;

                        this.BackColor = Color.FromArgb(255, 255, 255);
                        Apply.BackColor = Color.FromArgb(138, 63, 196);
                        Basic.BackColor = Color.FromArgb(138, 63, 196);
                        panel1.BackColor = Color.FromArgb(222, 222, 222);
                        panel2.BackColor = Color.FromArgb(138, 63, 196);
                        panel3.BackColor = Color.FromArgb(138, 63, 196);

                        this.Close();
                        break;
                }
            }
        }

        private void Basic_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(60, 60, 60);
            Apply.BackColor = Color.FromArgb(192, 64, 0);
            Basic.BackColor = Color.FromArgb(192, 64, 0);
            panel1.BackColor = Color.FromArgb(37, 37, 37);
            panel2.BackColor = Color.FromArgb(192, 64, 0);
            panel3.BackColor = Color.FromArgb(192, 64, 0);
        }

        private void ThemeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
