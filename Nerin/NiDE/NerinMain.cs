using Nerin.Analyzers.Items;
using Nerin.Analyzers;
using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NerinLib;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics.Tracing;
using NerinLib.Symbols;
using NerinLib.Analyzers;
using System.ComponentModel;
using Nerin.NiDE;
using System.Linq;
using System.Threading.Tasks;

namespace Nerin.NerinIDE
{
    public partial class NideMain : Form
    {
        private TabControl tabControl;
        private TextBox MainTextBox;
        private TextBox MainWindow;
        private Button Compile;
        private Button Save;
        private PictureBox Settings;
        private FindDialog findDialog;
        private Creator creator = new Creator();
        private string _inputText = "";
        private string error_txt = "";
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

            // Create a TabControl
            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Width = 100;
            mainTable.Controls.Add(tabControl, 0, 1);

            // Create a new tab page and add it to the tab control
            TabPage tabPage = new TabPage("Code");
            tabControl.TabPages.Add(tabPage);
            tabPage.ForeColor = Color.Black;

            MainTextBox = new TextBox();
            MainTextBox.Multiline = true;
            MainTextBox.ScrollBars = ScrollBars.Both;
            MainTextBox.Dock = DockStyle.Fill;
            MainTextBox.BorderStyle = BorderStyle.None;
            tabPage.Controls.Add(MainTextBox);

            MainWindow = MainTextBox;

            Compile = creator.CreateButton("Compile", Color.White, Color.FromArgb(67, 67, 67), AnchorStyles.Top | AnchorStyles.Right, MainTextBox.Width - 100, 10);
            topPanel.Controls.Add(Compile);

            Save = creator.CreateButton("Save", Color.White, Color.FromArgb(67, 67, 67), AnchorStyles.Top | AnchorStyles.Left, MainTextBox.Width - 900, 10);
            topPanel.Controls.Add(Save);

            Image settings = Properties.Resources.settings; //path to image "settings"

            Settings = creator.CreatePictureBox(settings, topPanel.Width - 950, 5);
            topPanel.Controls.Add(Settings);

            topPanel.BackColor = Color.FromArgb(38, 38, 38);
            MainTextBox.BackColor = Color.FromArgb(67, 67, 67);
            MainTextBox.ForeColor = Color.FromArgb(255, 255, 255);
        }

        private void InitializeEventHandlers()
        {
            Compile.Click += Compile_Click;
            Compile.MouseEnter += Compile_MouseEnter;
            Compile.MouseLeave += Compile_MouseLeave;

            Compile.TabStop = false;
            Save.TabStop = false;
            Settings.TabStop = false;

            //Save.Click += Save_Click;
            Save.MouseEnter += Save_MouseEnter;
            Save.MouseLeave += Save_MouseLeave;

            Settings.Click += Settings_Click;
            Settings.MouseEnter += Settings_MouseEnter;
            Settings.MouseLeave += Settings_MouseLeave;

            MainWindow.TextChanged += Console_TextChanged;
            MainWindow.TextChanged += MainWindow_TextChanged;
            MainWindow.PreviewKeyDown += MainWindow_PreviewKeyDown;

            this.KeyPreview = true;
            this.KeyDown += NideMain_CtrlF5;
            this.KeyDown += NideMain_CtrlF;
        }

        private void NideMain_CtrlF5(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F5 && !error)
            {
                ShowConsole();
            }
            else if (e.Control && e.KeyCode == Keys.F5 && error)
            {
                MessageBox.Show(error_txt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NideMain_CtrlF(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                findDialog = new FindDialog();
                findDialog.ShowDialog();
            }
        }

        private void SetCursorToText()
        {
            FindDialog findDialog = new FindDialog();
            string searchText = findDialog.GetSearchText();

            if (!string.IsNullOrEmpty(searchText))
            {
                int textIndex = MainWindow.Text.IndexOf(searchText);
                if (textIndex != -1)
                {
                    MainWindow.SelectionStart = textIndex;
                    MainWindow.SelectionLength = searchText.Length;
                }
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                int cursorPosition = MainWindow.SelectionStart;
                string spaces = new string(' ', 4);

                MainWindow.Text = MainWindow.Text.Insert(cursorPosition, spaces);

                MainWindow.SelectionStart = cursorPosition + 4;
            }
        }

        private void Compile_Click(object sender, EventArgs e)
        {
            if (error)
            {
                MessageBox.Show(error_txt, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ShowConsole();
            }
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void ShowConsole()
        {
            string inputText = GetText();
            string[] lines = inputText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder resultBuilder = new StringBuilder();

            Dictionary<VariableSymbol, object> variables = new Dictionary<VariableSymbol, object>();
            Compilation previous = null;

            foreach (string line in lines)
            {
                SyntaxTree tree = SyntaxTree.Parse(line);

                if (string.IsNullOrWhiteSpace(line) || tree.Diagnostics.Any())
                {
                    continue;
                }

                Compilation compilation = previous == null ? new Compilation(tree) : previous.ContinueWith(tree);
                EvaluationResult resultBound = compilation.EvaluateResult(variables);

                if (!tree.Diagnostics.Any())
                {
                    string lineResult = resultBound.Value.ToString();
                    resultBuilder.AppendLine(lineResult);
                    previous = compilation;
                }
                else
                {
                    foreach (var diagnostic in tree.Diagnostics)
                    {
                        resultBuilder.AppendLine(diagnostic.ToString());
                    }
                }
            }

            SetConsole(resultBuilder);
        }

        private void Compile_MouseEnter(object sender, EventArgs e)
        {
            Compile.BackColor = Color.FromArgb(100, 100, 100);
            Compile.Cursor = Cursors.Hand;
        }

        private void Compile_MouseLeave(object sender, EventArgs e)
        {
            Compile.BackColor = Color.FromArgb(67, 67, 67); // start color
            Compile.Cursor = Cursors.Default;
        }

        private void Save_MouseEnter(object sender, EventArgs e)
        {
            Save.BackColor = Color.FromArgb(100, 100, 100);
            Save.Cursor = Cursors.Hand;
        }

        private void Save_MouseLeave(object sender, EventArgs e)
        {
            Save.BackColor = Color.FromArgb(67, 67, 67); // start color
            Save.Cursor = Cursors.Default;
        }

        private void Settings_MouseEnter(object sender, EventArgs e)
        {
            Settings.Cursor = Cursors.Hand;
        }

        private void Settings_MouseLeave(object sender, EventArgs e)
        {
            Settings.Cursor = Cursors.Default;
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
                string[] lines = GetText().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder resultBuilder = new StringBuilder();

                Dictionary<VariableSymbol, object> variables = new Dictionary<VariableSymbol, object>();
                Compilation previous = null;

                foreach (string line in lines)
                {
                    SyntaxTree tree = SyntaxTree.Parse(line);

                    if (!string.IsNullOrWhiteSpace(line) && tree.Diagnostics.Any())
                    {
                        continue;
                    }

                    Compilation compilation = previous == null ? new Compilation(tree) : previous.ContinueWith(tree);
                    EvaluationResult resultBound = compilation.EvaluateResult(variables);

                    if (!tree.Diagnostics.Any())
                    {
                        string lineResult = resultBound.Value.ToString();
                        resultBuilder.AppendLine(lineResult);
                        previous = compilation;
                    }
                }

                MainWindow.ForeColor = Color.FromArgb(255, 255, 255);
                error = false;
            }
            catch (Exception ex)
            {
                error_txt = ex.Message;
                error = true;
                MainWindow.ForeColor = Color.Red;
                
            }
        }

        public void Start()
        {
            Application.Run(this);
        }
    }
}