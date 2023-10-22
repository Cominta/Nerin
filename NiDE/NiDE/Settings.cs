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
        private NideMain nideMain = new NideMain();
        private NideMain MainTextBox = new NideMain();

        public Settings()
        {
            InitializeComponent();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            if (float.TryParse(NumericFont.Value.ToString(), out float fontSize))
            {
                MainTextBox.Font = new Font(MainTextBox.Font.FontFamily, fontSize);
            }

            nideMain.Width = (int)NumericWidth.Value;
            nideMain.Height = (int)NumericHeight.Value;

            this.Close();
        }

        private void Basic_Click(object sender, EventArgs e)
        {
            MainTextBox.Font = new Font(MainTextBox.Font.FontFamily, 12);
            nideMain.Width = 816;
            nideMain.Height = 516;

            this.Close();
        }

        private void NumericHeight_ValueChanged(object sender, EventArgs e)
        {
            NumericHeight.Value = nideMain.Height;
        }

        private void NumericWidth_ValueChanged(object sender, EventArgs e)
        {
            NumericWidth.Value = nideMain.Width;
        }
    }
}
