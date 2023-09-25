using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nerin.NerinIDE
{
    public partial class NideMain : Form
    {
        private TextBox Console;
        private Button Compile;

        public NideMain()
        {
            InitializeComponent();

            this.Text = "NiDE";

            Console = new TextBox();
            Console.Multiline = true;
            Console.Dock = DockStyle.Fill;
            Console.ScrollBars = ScrollBars.Both;
            this.Controls.Add(Console);

            Compile = new Button();
            Compile.Text = "Compile";
            Compile.Dock = DockStyle.Top;
            Compile.Click += ShowTextButton_Click;
            this.Controls.Add(Compile);

            this.KeyPreview = true;
            this.KeyDown += NideMain_KeyDown;
        }

        private void ShowTextButton_Click(object sender, EventArgs e)
        {
            ShowConsole();
        }

        //Ctrl + F5 checkout
        private void NideMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F5)
            {
                ShowConsole();
            }
        }

        private void ShowConsole()
        {
            Form textDisplayForm = new Form();
            textDisplayForm.Text = "Console";
            textDisplayForm.Size = new System.Drawing.Size(700, 400);

            TextBox displayTextBox = new TextBox();
            displayTextBox.Multiline = true;
            displayTextBox.Dock = DockStyle.Fill;
            displayTextBox.ScrollBars = ScrollBars.Both;

            displayTextBox.ReadOnly = true;
            displayTextBox.Text = Console.Text;
            textDisplayForm.Controls.Add(displayTextBox);

            textDisplayForm.ShowDialog();
        }

        public void Start()
        {
            Application.Run(this);
        }
    }
}
