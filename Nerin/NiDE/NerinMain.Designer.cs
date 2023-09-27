using System.Drawing;
using System.Windows.Forms;

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
            this.SuspendLayout();
            // 
            // NideMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 588);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "NideMain";
            this.Text = "NiDE";
            this.ResumeLayout(false);

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

        #endregion
    }
}