namespace NiDE
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.LabelFont = new System.Windows.Forms.Label();
            this.NumericFont = new System.Windows.Forms.NumericUpDown();
            this.NumericWidth = new System.Windows.Forms.NumericUpDown();
            this.LabelWidth = new System.Windows.Forms.Label();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.NumericHeight = new System.Windows.Forms.NumericUpDown();
            this.Apply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Basic = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ThemeListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHeight)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelFont
            // 
            this.LabelFont.AutoSize = true;
            this.LabelFont.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelFont.Location = new System.Drawing.Point(65, 57);
            this.LabelFont.Name = "LabelFont";
            this.LabelFont.Size = new System.Drawing.Size(72, 19);
            this.LabelFont.TabIndex = 0;
            this.LabelFont.Text = "Font size:";
            // 
            // NumericFont
            // 
            this.NumericFont.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.NumericFont.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumericFont.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.NumericFont.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericFont.Location = new System.Drawing.Point(150, 60);
            this.NumericFont.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.NumericFont.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NumericFont.Name = "NumericFont";
            this.NumericFont.Size = new System.Drawing.Size(120, 22);
            this.NumericFont.TabIndex = 1;
            this.NumericFont.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // NumericWidth
            // 
            this.NumericWidth.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.NumericWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumericWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumericWidth.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.NumericWidth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericWidth.Location = new System.Drawing.Point(150, 100);
            this.NumericWidth.Maximum = new decimal(new int[] {
            7680,
            0,
            0,
            0});
            this.NumericWidth.Minimum = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.NumericWidth.Name = "NumericWidth";
            this.NumericWidth.Size = new System.Drawing.Size(120, 22);
            this.NumericWidth.TabIndex = 2;
            this.NumericWidth.Value = this.Width;
            this.NumericWidth.ValueChanged += new System.EventHandler(this.NumericWidth_ValueChanged);
            // 
            // LabelWidth
            // 
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelWidth.Location = new System.Drawing.Point(22, 97);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(115, 19);
            this.LabelWidth.TabIndex = 3;
            this.LabelWidth.Text = "Window width:";
            // 
            // LabelHeight
            // 
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHeight.Location = new System.Drawing.Point(18, 140);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(119, 19);
            this.LabelHeight.TabIndex = 4;
            this.LabelHeight.Text = "Window height:";
            // 
            // NumericHeight
            // 
            this.NumericHeight.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.NumericHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.NumericHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.NumericHeight.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.NumericHeight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NumericHeight.Location = new System.Drawing.Point(150, 143);
            this.NumericHeight.Maximum = new decimal(new int[] {
            4320,
            0,
            0,
            0});
            this.NumericHeight.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.NumericHeight.Name = "NumericHeight";
            this.NumericHeight.Size = new System.Drawing.Size(120, 18);
            this.NumericHeight.TabIndex = 5;
            this.NumericHeight.Value = this.Height;
            this.NumericHeight.ValueChanged += new System.EventHandler(this.NumericHeight_ValueChanged);
            // 
            // Apply
            // 
            this.Apply.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Apply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Apply.Location = new System.Drawing.Point(57, 16);
            this.Apply.MaximumSize = new System.Drawing.Size(80, 32);
            this.Apply.MinimumSize = new System.Drawing.Size(80, 32);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(80, 32);
            this.Apply.TabIndex = 6;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = false;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(37)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.Basic);
            this.panel1.Controls.Add(this.Apply);
            this.panel1.Location = new System.Drawing.Point(0, 229);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 65);
            this.panel1.TabIndex = 7;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel3.Location = new System.Drawing.Point(0, 62);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(334, 3);
            this.panel3.TabIndex = 9;
            // 
            // Basic
            // 
            this.Basic.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Basic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Basic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Basic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Basic.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Basic.Location = new System.Drawing.Point(190, 16);
            this.Basic.MaximumSize = new System.Drawing.Size(80, 32);
            this.Basic.MinimumSize = new System.Drawing.Size(80, 32);
            this.Basic.Name = "Basic";
            this.Basic.Size = new System.Drawing.Size(80, 32);
            this.Basic.TabIndex = 7;
            this.Basic.Text = "Basic";
            this.Basic.UseVisualStyleBackColor = false;
            this.Basic.Click += new System.EventHandler(this.Basic_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Location = new System.Drawing.Point(0, 229);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(334, 3);
            this.panel2.TabIndex = 8;
            // 
            // ThemeListBox
            // 
            this.ThemeListBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ThemeListBox.FormattingEnabled = true;
            this.ThemeListBox.Items.AddRange(new object[] {
            "Classic",
            "Classic light",
            "Purple dark",
            "Purple light"});
            this.ThemeListBox.Location = new System.Drawing.Point(150, 178);
            this.ThemeListBox.Name = "ThemeListBox";
            this.ThemeListBox.ScrollAlwaysVisible = true;
            this.ThemeListBox.Size = new System.Drawing.Size(120, 30);
            this.ThemeListBox.TabIndex = 9;
            this.ThemeListBox.SelectedIndexChanged += new System.EventHandler(this.ThemeListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 176);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 19);
            this.label1.TabIndex = 10;
            this.label1.Text = "Choose theme:";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(334, 361);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ThemeListBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NumericHeight);
            this.Controls.Add(this.LabelHeight);
            this.Controls.Add(this.LabelWidth);
            this.Controls.Add(this.NumericWidth);
            this.Controls.Add(this.NumericFont);
            this.Controls.Add(this.LabelFont);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(350, 400);
            this.MinimumSize = new System.Drawing.Size(350, 400);
            this.Name = "Settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.NumericFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHeight)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelFont;
        private System.Windows.Forms.NumericUpDown NumericFont;
        private System.Windows.Forms.NumericUpDown NumericWidth;
        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.NumericUpDown NumericHeight;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Basic;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox ThemeListBox;
        private System.Windows.Forms.Label label1;
    }
}