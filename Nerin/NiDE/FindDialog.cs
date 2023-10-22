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
        private Label Found;
        private RichTextBox richTextBox1;
        private Button searchButton;

        public event Action<string> Search;

        private string searchText = string.Empty;

        public FindDialog()
        {
            InitializeComponent();
        }

        public void Show()
        {
            Application.Run(this);
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
            this.Found.Size = new System.Drawing.Size(138, 20);
            this.Found.TabIndex = 1;
            this.Found.Text = "Found in context: ";
            this.Found.Click += new System.EventHandler(this.Found_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 37);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(260, 32);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // FindDialog
            // 
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(284, 81);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.Found);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(300, 120);
            this.MinimumSize = new System.Drawing.Size(300, 120);
            this.Name = "FindDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public string GetSearchText()
        {
            return searchText;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            searchText = richTextBox1.Text;
        }

        private void Found_Click(object sender, EventArgs e)
        {

        }
    }
}
