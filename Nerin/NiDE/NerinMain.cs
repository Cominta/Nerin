using Nerin.Analyzers.Items;
using Nerin.Analyzers;
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
        private string _inputText;
        private string _result;

        public string GetText()
        {
            return _inputText;
        }

        //Main window settings
        public NideMain()
        {
            InitializeComponent();

            this.Text = "NiDE";

            Console = new TextBox();
            Console.Multiline = true;
            Console.Dock = DockStyle.Fill;
            Console.ScrollBars = ScrollBars.Both;
            Console.TextChanged += Console_TextChanged;
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

        // Ctrl + F5 checkout
        private void NideMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F5)
            {
                ShowConsole();
            }
        }

        private void ShowConsole()
        {
            string currentStr = GetText();
            Parser parser = new Parser();

            //Show result in console
            Expr result = null;
            parser.SetText(currentStr);
            result = parser.Parse();
            Evaulator evaulator = new Evaulator(result);
            _result = evaulator.Evaluate().ToString();

            //Console settings
            Form textDisplayForm = new Form();
            textDisplayForm.Text = "Console";
            textDisplayForm.Size = new System.Drawing.Size(700, 400);

            TextBox displayTextBox = new TextBox();
            displayTextBox.Multiline = true;
            displayTextBox.Dock = DockStyle.Fill;
            displayTextBox.ScrollBars = ScrollBars.Both;

            displayTextBox.ReadOnly = true;

            displayTextBox.Text = _result;
            
            textDisplayForm.Controls.Add(displayTextBox);

            textDisplayForm.ShowDialog();
        }

        // Save text to _inputText
        private void Console_TextChanged(object sender, EventArgs e)
        {
            _inputText = Console.Text;
        }

        public void Start()
        {
            Application.Run(this);
        }
    }
}
