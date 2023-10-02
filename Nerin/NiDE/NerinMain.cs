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
using NerinLib;

namespace Nerin.NerinIDE
{
    public partial class NideMain : Form
    {
        private TextBox MainWindow;
        private Button Compile;
        private string _inputText;
        private bool error = false;

        public string GetText()
        {
            return _inputText;
        }

        //Main window settings
        public NideMain()
        {
            InitializeComponent();

            Panel panel = new Panel();
            panel.Dock = DockStyle.Fill;
            this.Controls.Add(panel);

            Compile = new Button();
            Compile.Text = "Compile";
            Compile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            Compile.Margin = new Padding(10);
            Compile.Location = new Point(panel.ClientSize.Width - Compile.Width - 50, 10); // Button position
            Compile.Click += Compile_Click;

            //Add events
            Compile.MouseEnter += Compile_MouseEnter;
            Compile.MouseLeave += Compile_MouseLeave;

            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Controls.Add(Compile);
            panel.BackColor = Color.FromArgb(38, 38, 38); //Panel color

            //Button colors
            Compile.BackColor = Color.FromArgb(67, 67, 67);
            Compile.ForeColor = Color.FromArgb(255, 255, 255);

            //Main window - only for text
            MainWindow = new TextBox();
            MainWindow.Multiline = true;
            MainWindow.Dock = DockStyle.Bottom;
            MainWindow.ScrollBars = ScrollBars.Both;
            MainWindow.TextChanged += Console_TextChanged;
            MainWindow.TextChanged += MainWindow_TextChanged;
            MainWindow.BorderStyle = BorderStyle.FixedSingle;

            //Main window colors
            MainWindow.BackColor = Color.FromArgb(67, 67, 67);
            MainWindow.ForeColor = Color.FromArgb(255, 255, 255);

            MainWindow.Height = ClientSize.Height - 40;

            panel.Controls.Add(MainWindow);

            this.KeyPreview = true;
            this.KeyDown += NideMain_KeyDown;
        }

        // Ctrl + F5 checkout
        private void NideMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F5 && !error)
            {
                ShowConsole();
            }
            else if (e.Control && e.KeyCode == Keys.F5 && error)
            {
                MessageBox.Show("The program cannot be compiled because the code contains errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Button checkout
        private void Compile_Click(object sender, EventArgs e)
        {
            if (error)
            {
                MessageBox.Show("The program cannot be compiled because the code contains errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                ShowConsole();
            }
        }

        private void ShowConsole()
        {
            string[] lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder resultBuilder = new StringBuilder();

            Dictionary<string, object> variables = new Dictionary<string, object>();

            foreach (string line in lines)
            {
                // Show result in console for each line
                Compilation compilation = new Compilation(line);
                EvaluationResult resultBound = compilation.EvaluateResult(variables);

                string lineResult = resultBound.Value.ToString();

                resultBuilder.AppendLine(lineResult);
            }

            SetConsole(resultBuilder);
        }

        private void Compile_MouseEnter(object sender, EventArgs e)
        {
            Compile.BackColor = Color.FromArgb(100, 100, 100);
        }

        private void Compile_MouseLeave(object sender, EventArgs e)
        {
            Compile.BackColor = Color.FromArgb(67, 67, 67); // start color
        }

        // Save text to _inputText
        private void Console_TextChanged(object sender, EventArgs e)
        {
            _inputText = MainWindow.Text;
        }

        //Text color change
        private void MainWindow_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MainWindow.Text))
            {
                MainWindow.ForeColor = Color.White;
                error = false;
                return;
            }

            try
            {
                string[] lines = MainWindow.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Parser parser = new Parser();
                Dictionary<string, object> variables = new Dictionary<string, object>();

                foreach (string line in lines)
                {
                    Compilation compilation = new Compilation(line);
                    EvaluationResult resultBound = compilation.EvaluateResult(variables);

                    string lineResult = resultBound.Value.ToString();
                }

                MainWindow.ForeColor = Color.FromArgb(255, 255, 255);
                error = false;
            }
            catch (Exception ex)
            {
                MainWindow.ForeColor = Color.Red;
                error = true;
            }
        }

        public void Start()
        {
            Application.Run(this);
        }
    }
}