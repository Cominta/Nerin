using Nerin.Analyzers.Items;
using Nerin.Analyzers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NerinLib;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Tracing;

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
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Controls.Add(mainTable);
            mainTable.Dock = DockStyle.Fill;
            mainTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            
            mainTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Fill;
            mainTable.Controls.Add(topPanel, 0, 0);

            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            TextBox mainTextBox = new TextBox();
            mainTextBox.Multiline = true;
            mainTextBox.ScrollBars = ScrollBars.Both;
            mainTextBox.Dock = DockStyle.Fill;
            mainTextBox.BorderStyle = BorderStyle.None;
            mainTable.Controls.Add(mainTextBox, 0, 1);

            MainWindow = mainTextBox;

            Compile = CreateButton("Compile", null, Color.White, Color.FromArgb(67, 67, 67), AnchorStyles.Top | AnchorStyles.Right, topPanel.Width - 100, 10);
            Settings = CreateButton("Settings", "asd", Color.White, Color.FromArgb(67, 67, 67), AnchorStyles.Top | AnchorStyles.Right, topPanel.Width - 210, 10);
            topPanel.Controls.Add(Compile);
            topPanel.Controls.Add(Settings);


            topPanel.BorderStyle = BorderStyle.None;
            topPanel.BackColor = Color.FromArgb(38, 38, 38);
            mainTextBox.BackColor = Color.FromArgb(67, 67, 67);
            mainTextBox.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private Button CreateButton(string text, string icon, Color fore_color, Color back_color, AnchorStyles anchor, int x, int y)
        {
            Button button = new Button();

            if(icon == null)
            {
                button.Text = text;
                button.Location = new Point(x, y);
                button.Anchor = anchor;
                button.Margin = new Padding(10);
                button.ForeColor = fore_color;
                button.BackColor = back_color;
                return button;
            }
            else
            {
                button.Image = Properties.Resources.settings;
                button.ImageAlign = ContentAlignment.MiddleLeft;
                button.Location = new Point(x, y);
                button.Anchor = anchor;
                button.Margin = new Padding(10);
                button.ForeColor = fore_color;
                button.BackColor = back_color;
                return button;
            }
        }

        private void InitializeEventHandlers()
        {
            Compile.Click += Compile_Click;
            Compile.MouseEnter += Compile_MouseEnter;
            Compile.MouseLeave += Compile_MouseLeave;

            Settings.Click += SettingsButton_Click;
            Settings.MouseEnter += Settings_MouseEnter;
            Settings.MouseLeave += Settings_MouseLeave;

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
            ShowSettings();
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

        private void Settings_MouseEnter(object sender, EventArgs e)
        {
            Settings.BackColor = Color.FromArgb(100, 100, 100);
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            Settings.BackColor = Color.FromArgb(67, 67, 67); // start color
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
