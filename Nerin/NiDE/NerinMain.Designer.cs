using System.Drawing;
using System.Text;
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

        #endregion
    }
}