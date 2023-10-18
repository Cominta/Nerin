using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nerin.NiDE
{
    public class FindDialog : Form
    {
        private TextBox searchTextBox;
        private Label Found;
        private RichTextBox richTextBox1;
        private Button searchButton;

        public event Action<string> Search;

        public FindDialog()
        {
            InitializeComponent();
        }

        public void Show()
        {
            Application.Run(this);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            Search?.Invoke(searchTextBox.Text);
            Close();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindDialog));
            this.Found = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Found
            // 
            this.Found.AutoSize = true;
            this.Found.Font = new System.Drawing.Font("Malgun Gothic", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Found.Location = new System.Drawing.Point(12, 13);
            this.Found.Name = "Found";
            this.Found.Size = new System.Drawing.Size(129, 20);
            this.Found.TabIndex = 1;
            this.Found.Text = "Found in cotext: ";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(260, 32);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // FindDialog
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(284, 81);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Found);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 120);
            this.MinimumSize = new System.Drawing.Size(300, 120);
            this.Name = "FindDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
