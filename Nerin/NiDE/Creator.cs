using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nerin.NiDE
{
    public class Creator
    {
        public Button CreateButton(string text, Color fore_color, Color back_color, AnchorStyles anchor, int x, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Anchor = anchor;
            button.Location = new Point(x, y);
            button.Margin = new Padding(10);
            button.ForeColor = fore_color;
            button.BackColor = back_color;

            return button;
        }

        public PictureBox CreatePictureBox(Image image, int x, int y)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.Location = new Point(x, y);
            pictureBox.Size = new Size(35, 35);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            return pictureBox;
        }

        public Label CreateLabel(string txt, Color fore_color, int x, int y)
        {
            Label label = new Label();
            label.Text = txt;
            label.ForeColor = fore_color;
            label.Location = new Point(x, y);

            return label;
        }

        public NumericUpDown CreateNumericUpDown(int min, int max, int current_value, int x, int y)
        {
            NumericUpDown numeric = new NumericUpDown();
            numeric.Minimum = min;
            numeric.Maximum = max;
            numeric.Value = current_value;
            numeric.Location = new Point(x, y);

            return numeric;
        }
    }
}
