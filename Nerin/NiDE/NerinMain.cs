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
using NerinLib.Symbols;
using NerinLib.Analyzers;

namespace Nerin.NerinIDE
{
    public partial class NideMain : Form
    {
        private TextBox MainWindow;
        private Button Compile;
        private PictureBox Settings;
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

            this.MinimumSize = new Size(600, 400);
            this.MaximumSize = new Size(7680, 4320);
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

            Compile = CreateButton("Compile", Color.White, Color.FromArgb(67, 67, 67), AnchorStyles.Top | AnchorStyles.Right, topPanel.Width - 100, 10);

            Image settings = Properties.Resources.settings;//path to image "settings"

            Settings = CreatePictureBox(settings, topPanel.Width - 950, 5);

            topPanel.Controls.Add(Compile);
            topPanel.Controls.Add(Settings);

            topPanel.BackColor = Color.FromArgb(38, 38, 38);
            mainTextBox.BackColor = Color.FromArgb(67, 67, 67);
            mainTextBox.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private Button CreateButton(string text, Color fore_color, Color back_color, AnchorStyles anchor, int x, int y)
        {
            Button button = new Button();
            button.Text = text;
            button.Location = new Point(x, y);
            button.Anchor = anchor;
            button.Margin = new Padding(10);
            button.ForeColor = fore_color;
            button.BackColor = back_color;

            return button;
        }

        private PictureBox CreatePictureBox(Image image, int x, int y)
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.Location = new Point(x, y);
            pictureBox.Size = new Size(35, 35);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            return pictureBox;
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
            ShowSettings();
        }

        private void ShowConsole()
        {
            string[] lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder resultBuilder = new StringBuilder();

            Dictionary<VariableSymbol, object> variables = new Dictionary<VariableSymbol, object>();
            Compilation previous = null;

            foreach (string line in lines)
            {
                SyntaxTree tree = SyntaxTree.Parse(line);
                Compilation compilation = previous == null ? new Compilation(tree) : previous.ContinueWith(tree);
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

                Dictionary<VariableSymbol, object> variables = new Dictionary<VariableSymbol, object>();
                Compilation previous = null;

                foreach (string line in lines)
                {
                    SyntaxTree tree = SyntaxTree.Parse(line);
                    Compilation compilation = previous == null ? new Compilation(tree) : previous.ContinueWith(tree);
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
