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
        public int font_size = 12;

        public Font font;

        public Settings()
        {
            InitializeComponent();
            ThemeListBox.SelectedIndex = 0;
            FontStyleListBox.SelectedIndex = 0;
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Basic_Click(object sender, EventArgs e)
        {
            ThemeListBox.SelectedIndex = 0;
            font = new Font("Consolas", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));

            FontStyleListBox.SelectedIndex = 0;

            font_size = 12;
            NumericFont.Value = 12;

            this.Close();
        }

        private void ThemeListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (FontStyleListBox.SelectedIndex)
            {
                //Classic
                case 0:
                    theme = ThemeListBox.SelectedIndex;

                    this.BackColor = Color.FromArgb(60, 60, 60);
                    Apply.BackColor = Color.FromArgb(192, 64, 0);
                    Basic.BackColor = Color.FromArgb(192, 64, 0);
                    panel1.BackColor = Color.FromArgb(37, 37, 37);
                    panel2.BackColor = Color.FromArgb(192, 64, 0);
                    panel3.BackColor = Color.FromArgb(192, 64, 0);

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

                    break;
            }

        }

        private void NumericFont_ValueChanged(object sender, EventArgs e)
        {
            font_size = (int)NumericFont.Value;
        }

        private void FontStyleListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (FontStyleListBox.SelectedIndex)
            {
                //Consolas
                case 0:

                    font = new Font("Consolas", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    break;

                //Times New Roman
                case 1:

                    font = new Font("Times New Roman", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    break;

                //Microsoft Sans Serif
                case 2:

                    font = new Font("Microsoft Sans Serif", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    break;

                //Arial
                case 3:

                    font = new Font("Arial", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    break;

                //Comic Sans MS
                case 4:

                    font = new Font("Comic Sans MS", font_size, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    break;
            }
        }
    }
}
