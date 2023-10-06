using System.Drawing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Nerin;

namespace Nerin.NerinIDE
{
    partial class NideMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NideMain));
            this.SuspendLayout();
            // 
            // NideMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 588);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NideMain";
            this.Text = "NiDE";
            this.ResumeLayout(false);

        }

        private void SetConsole(StringBuilder resultBuilder)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NideMain));

            Form ConsoleWindow = new Form();
            ConsoleWindow.Text = "Console";
            ConsoleWindow.Size = new System.Drawing.Size(700, 400);
            ConsoleWindow.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));

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

        private void ShowSettingsDialog()
        {
            Form Settings = new Form();
            Settings.Text = "Settings";
            Settings.BackColor = Color.FromArgb(38, 38, 38);
            Settings.Size = new Size(300, 300);
            Settings.FormBorderStyle = FormBorderStyle.FixedSingle;
            Settings.StartPosition = FormStartPosition.CenterParent;

            Label FontLabel = new Label();
            FontLabel.Text = "Font size:";
            FontLabel.ForeColor = Color.White;
            FontLabel.Location = new Point(20, 20);

            NumericUpDown Font = new NumericUpDown();
            Font.Minimum = 5;
            Font.Maximum = 40;
            Font.Value = (decimal)MainWindow.Font.Size;
            Font.Location = new Point(150, 20);

            Label WidthLabel = new Label();
            WidthLabel.Text = "Window width:";
            WidthLabel.ForeColor = Color.White;
            WidthLabel.Location = new Point(20, 60);

            NumericUpDown WidthNumericUpDown = new NumericUpDown();
            WidthNumericUpDown.Minimum = 320;
            WidthNumericUpDown.Maximum = 7680;
            WidthNumericUpDown.Value = this.Width;
            WidthNumericUpDown.Location = new Point(150, 60);

            Label HeightLabel = new Label();
            HeightLabel.Text = "Window height:";
            HeightLabel.ForeColor = Color.White;
            HeightLabel.Location = new Point(20, 100);

            NumericUpDown HeightNumericUpDown = new NumericUpDown();
            HeightNumericUpDown.Minimum = 240;
            HeightNumericUpDown.Maximum = 4320;
            HeightNumericUpDown.Value = this.Height;
            HeightNumericUpDown.Location = new Point(150, 100);

            Button Apply = new Button();
            Apply.Text = "Apply";
            Apply.ForeColor = Color.Black;
            Apply.BackColor = Color.Gray;
            Apply.Location = new Point(100, 150);
            Apply.Click += (s, e) =>
            {
                if (float.TryParse(Font.Text, out float fontSize))
                {
                    MainWindow.Font = new Font(MainWindow.Font.FontFamily, fontSize);
                }

                this.Width = (int)WidthNumericUpDown.Value;
                this.Height = (int)HeightNumericUpDown.Value;
                Settings.Close();
            };

            Settings.Controls.Add(FontLabel);
            Settings.Controls.Add(Font);
            Settings.Controls.Add(WidthLabel);
            Settings.Controls.Add(WidthNumericUpDown);
            Settings.Controls.Add(HeightLabel);
            Settings.Controls.Add(HeightNumericUpDown);
            Settings.Controls.Add(Apply);

            Settings.ShowDialog();
        }

        #endregion
    }
}