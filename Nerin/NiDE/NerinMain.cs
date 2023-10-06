using Nerin.Analyzers.Items;
using Nerin.Analyzers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NerinLib;
using System.Text;
using System.Runtime.InteropServices;

namespace Nerin.NerinIDE
{
    public partial class NideMain : Form
    {
        private TextBox MainWindow;
        private Button Compile;
        private Button Settings;
        private string _inputText = "";
        private bool error = false;

        public string GetText()
        {
            return _inputText;
        }

        public NideMain()
        {
            InitializeComponent();
            InitializeLayout();
            InitializeEventHandlers();
        }

        private void InitializeLayout()
        {
            TableLayoutPanel mainTable = new TableLayoutPanel();
            mainTable.Dock = DockStyle.Fill;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Padding = new Padding(0);
            this.Controls.Add(mainTable);

            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Fill;
            mainTable.Controls.Add(topPanel, 0, 0);

            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            TextBox mainTextBox = new TextBox();
            mainTextBox.Multiline = true;
            mainTextBox.ScrollBars = ScrollBars.Both;
            mainTextBox.Dock = DockStyle.Fill;
            mainTable.Controls.Add(mainTextBox, 0, 1);

            MainWindow = mainTextBox;

            Compile = CreateButton("Compile", AnchorStyles.Top | AnchorStyles.Right, topPanel.Width / 2, 5);
            Settings = CreateButton("Settings", AnchorStyles.Top | AnchorStyles.Right, topPanel.Width / 10, 5);
            topPanel.Controls.Add(Compile);
            topPanel.Controls.Add(Settings);


            topPanel.BorderStyle = BorderStyle.FixedSingle;
            topPanel.BackColor = Color.FromArgb(38, 38, 38);
            mainTextBox.BackColor = Color.FromArgb(67, 67, 67);
            mainTextBox.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private Button CreateButton(string text, AnchorStyles anchor, int x, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Anchor = anchor;
            button.Margin = new Padding(10);
            return button;
        }

        private void InitializeEventHandlers()
        {
            Compile.Click += Compile_Click;
            Compile.MouseEnter += Compile_MouseEnter;
            Compile.MouseLeave += Compile_MouseLeave;

            Settings.Click += SettingsButton_Click;
            MainWindow.TextChanged += Console_TextChanged;
            MainWindow.TextChanged += MainWindow_TextChanged;

            this.KeyPreview = true;
            this.KeyDown += NideMain_KeyDown;
        }

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

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            ShowSettingsDialog();
        }

        private void ShowConsole()
        {
            string[] lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder resultBuilder = new StringBuilder();

            Dictionary<string, object> variables = new Dictionary<string, object>();

            foreach (string line in lines)
            {
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

        private void Console_TextChanged(object sender, EventArgs e)
        {
            if (MainWindow != null)
            {
                _inputText = MainWindow.Text;
            }
        }

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
