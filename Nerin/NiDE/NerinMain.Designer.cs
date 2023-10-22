using System;
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1083, 657);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NideMain";
            this.Text = "NiDE";
            this.ResumeLayout(false);

        }

        private void SetConsole(StringBuilder resultBuilder)
        {
            //To receive data from NideMain class 
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

        protected void ShowSettings()
        {
            Form Settings = new Form();
            Settings.Text = "Settings";
            Settings.BackColor = Color.FromArgb(38, 38, 38);
            Settings.Size = new Size(300, 300);
            Settings.FormBorderStyle = FormBorderStyle.FixedSingle;
            Settings.StartPosition = FormStartPosition.CenterParent;

            NumericUpDown Font, Width, Height;
            Font = creator.CreateNumericUpDown(5, 40, (int)MainWindow.Font.Size, 150, 20);
            Width = creator.CreateNumericUpDown(320, 7680, this.Width, 150, 60);
            Height = creator.CreateNumericUpDown(240, 4320, this.Height, 150, 100);

            Label FontLabel, HeightLabel, WidthLabel;
            FontLabel = creator.CreateLabel("Font size:", Color.White, 20, 20);
            WidthLabel = creator.CreateLabel("Width size:", Color.White, 20, 60);
            HeightLabel = creator.CreateLabel("Window height:", Color.White, 20, 100);

            Button Apply;
            Apply = creator.CreateButton("Apply", Color.Black, Color.Gray, AnchorStyles.Bottom, 100, 150);

            Apply.Click += (s, e) =>
            {
                if (float.TryParse(Font.Value.ToString(), out float fontSize))
                {
                    MainWindow.Font = new Font(MainWindow.Font.FontFamily, fontSize);
                }

                this.Width = (int)Width.Value;
                this.Height = (int)Height.Value;
                Settings.Close();
            };

            Apply.MouseEnter += Apply_MouseEnter;
            Apply.MouseLeave += Apply_MouseLeave;

            Settings.Controls.Add(FontLabel);
            Settings.Controls.Add(Font);
            Settings.Controls.Add(WidthLabel);
            Settings.Controls.Add(Width);
            Settings.Controls.Add(HeightLabel);
            Settings.Controls.Add(Height);
            Settings.Controls.Add(Apply);

            Settings.ShowDialog();
        }

        public delegate void EventHandler(object sender, EventArgs e);


        private void Apply_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button applyButton)
            {
                applyButton.BackColor = Color.FromArgb(100, 100, 100);
            }
        }

        private void Apply_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button applyButton)
            {
                applyButton.BackColor = Color.Gray;
            }
        }

        #endregion
    }
}