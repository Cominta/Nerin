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
        private TextBox MainWindow;
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
        }

        // Ctrl + F5 checkout
        private void NideMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F5)
            {
                try
                {
                    ShowConsole();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The program cannot be compiled because the code contains errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MainWindow.ForeColor = Color.Red;
                }
            }
        }

        //Button checkout
        private void Compile_Click(object sender, EventArgs e)
        {
            try
            {
                ShowConsole();
            }
            catch (Exception ex)
            {
                MessageBox.Show("The program cannot be compiled because the code contains errors", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MainWindow.ForeColor = Color.Red;
            }
        }

        private void MainWindow_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MainWindow.Text))
            {
                MainWindow.ForeColor = Color.White;
                return;
            }

            try
            {
                string[] lines = MainWindow.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Parser parser = new Parser();

                foreach (string line in lines)
                {
                    Expr result = null;
                    parser.SetText(line);
                    result = parser.Parse();
                    //Evaulator evaulator = new Evaulator(result);

                    //string lineResult = evaulator.Evaluate().ToString();
                }

                MainWindow.ForeColor = Color.FromArgb(255, 255, 255);
            }
            catch (Exception ex)
            {
                MainWindow.ForeColor = Color.Red;
            }
        }

        private void ShowConsole()
        {
            string[] lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder resultBuilder = new StringBuilder();

            Parser parser = new Parser();

            if (string.IsNullOrWhiteSpace(GetText()))
            {
                MainWindow.ForeColor = Color.White;
                return;
            }

            try
            {
                foreach (string line in lines)
                {
                    // Show result in console for each line
                    Expr result = null;
                    parser.SetText(line);
                    result = parser.Parse();
                    //Evaulator evaulator = new Evaulator(result);

                    //string lineResult = evaulator.Evaluate().ToString();

                    //resultBuilder.AppendLine(lineResult);
                }
            }
            catch (Exception ex)
            {
                MainWindow.ForeColor = Color.Red;
                return;
            }

            MainWindow.ForeColor = Color.White;

            // Console settings
            Form ConsoleWindow = new Form();
            ConsoleWindow.Text = "Console";
            ConsoleWindow.Size = new System.Drawing.Size(700, 400);

            TextBox console = new TextBox();
            console.Multiline = true;
            console.Dock = DockStyle.Fill;
            console.ScrollBars = ScrollBars.Both;

            console.BackColor = Color.Black;
            console.ForeColor = Color.White;
            console.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            console.ReadOnly = true;

            console.Text = resultBuilder.ToString();

            ConsoleWindow.Controls.Add(console);

            ConsoleWindow.ShowDialog();
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

        public void Start()
        {
            Application.Run(this);
        }
    }
}