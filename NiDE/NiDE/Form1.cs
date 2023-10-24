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
using System.Linq;
using System.Threading.Tasks;
using NiDE.NiDE;

namespace NiDE
{
    public partial class NideMain : Form
    {
        private Console console = new Console();
        private ThemeControler controler = new ThemeControler();
        private Settings settings = new Settings();

        private string _inputText = "";
        private string error_txt = "";
        private bool error = false;
        private bool isLight = false;


        public NideMain()
        {
            InitializeComponent();
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

        //Settings
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            settings.ShowDialog();

            switch (settings.theme)
            {
                //Classic
                case -1:
                case 0:

                    MainTextBox = controler.TextBoxClassicTheme(MainTextBox);
                    panel1 = controler.PanelClassicTheme(panel1);
                    panel2 = controler.DecorPanelClassicTheme(panel2);
                    Compile = controler.ButtonClassicTheme(Compile);
                    Save = controler.ButtonClassicTheme(Save);
                    load = controler.ButtonClassicTheme(load);

                    console.ConsoleOutput.BackColor = Color.Black;
                    console.ConsoleOutput.ForeColor = Color.White;

                    isLight = false;
                    break;
                
                //Classic light
                case 1:

                    MainTextBox = controler.TextBoxLightTheme(MainTextBox);
                    panel1 = controler.PanelLightTheme(panel1);
                    panel2 = controler.DecorPanelClassicTheme(panel2);
                    Compile = controler.ButtonClassicTheme(Compile);
                    Save = controler.ButtonClassicTheme(Save);
                    load = controler.ButtonClassicTheme(load);

                    console.ConsoleOutput.BackColor = Color.White;
                    console.ConsoleOutput.ForeColor = Color.Black;

                    isLight = true;
                    break;
                
                //Purple dark
                case 2:

                    MainTextBox = controler.TextBoxClassicTheme(MainTextBox);
                    panel1 = controler.PanelClassicTheme(panel1);
                    panel2 = controler.PanelPurpleTheme(panel2);
                    Compile = controler.ButtonPurpleTheme(Compile);
                    load = controler.ButtonPurpleTheme(load);
                    Save = controler.ButtonPurpleTheme(Save);

                    console.ConsoleOutput.BackColor = Color.Black;
                    console.ConsoleOutput.ForeColor = Color.White;

                    isLight = false;
                    break;

                //Purple light
                case 3:

                    MainTextBox = controler.TextBoxLightTheme(MainTextBox);
                    panel1 = controler.PanelLightTheme(panel1);
                    panel2 = controler.PanelPurpleTheme(panel2);
                    Compile = controler.ButtonPurpleTheme(Compile);
                    load = controler.ButtonPurpleTheme(load);
                    Save = controler.ButtonPurpleTheme(Save);

                    console.ConsoleOutput.BackColor = Color.White;
                    console.ConsoleOutput.ForeColor = Color.Black;

                    isLight = true;
                    break;
            }
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

        private void ShowConsole()
        {
            string inputText = GetText();

            if(inputText == null)
            {
                return;
            }

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

            console.ConsoleOutput.Text = resultBuilder.ToString();
            console.ShowDialog();
        }

        private void MainTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MainTextBox != null)
            {
                _inputText = MainTextBox.Text;
            }

            if (string.IsNullOrWhiteSpace(MainTextBox.Text))
            {
                MainTextBox.ForeColor = Color.White;
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

                if(isLight)
                {
                    MainTextBox.ForeColor = Color.Black;
                }
                else
                {
                    MainTextBox.ForeColor = Color.White;
                }
                
                error = false;
            }
            catch (Exception ex)
            {
                error_txt = ex.Message;
                error = true;
                MainTextBox.ForeColor = Color.Red;
            }
        }

        public string GetText()
        {
            return _inputText;
        }

        public TextBox GetTextBox() 
        {
            return this.MainTextBox;        
        }

        public Panel GetPanel()
        {
            return this.panel1;
        }

        public Button GetCompile()
        {
            return this.Compile;
        }

        public Button GetSave()
        {
            return this.Save;
        }

        public Button GetLoad()
        {
            return this.load;
        }

        private void NideMain_Load(object sender, EventArgs e) { }
    }
}
