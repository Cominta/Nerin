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
            this.Font = new System.Windows.Forms.Label();
            this.NumericFont = new System.Windows.Forms.NumericUpDown();
            this.NumericWidth = new System.Windows.Forms.NumericUpDown();
            this.Width = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.Label();
            this.NumericHeight = new System.Windows.Forms.NumericUpDown();
            this.Apply = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Basic = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.NumericFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericHeight)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Font
            // 
            this.Font.AutoSize = true;
            this.Font.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Font.Location = new System.Drawing.Point(65, 57);
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(72, 19);
            this.Font.TabIndex = 0;
            this.Font.Text = "Font size:";
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
            12,
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
            this.NumericWidth.Value = new decimal(new int[] {
            816,
            0,
            0,
            0});
            this.NumericWidth.ValueChanged += new System.EventHandler(this.NumericWidth_ValueChanged);
            // 
            // Width
            // 
            this.Width.AutoSize = true;
            this.Width.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Width.Location = new System.Drawing.Point(22, 97);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(115, 19);
            this.Width.TabIndex = 3;
            this.Width.Text = "Window width:";
            // 
            // Height
            // 
            this.Height.AutoSize = true;
            this.Height.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Height.Location = new System.Drawing.Point(18, 140);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(119, 19);
            this.Height.TabIndex = 4;
            this.Height.Text = "Window height:";
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
            this.NumericHeight.Value = new decimal(new int[] {
            516,
            0,
            0,
            0});
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
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(334, 361);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.NumericHeight);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.NumericWidth);
            this.Controls.Add(this.NumericFont);
            this.Controls.Add(this.Font);
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

        private System.Windows.Forms.Label Font;
        private System.Windows.Forms.NumericUpDown NumericFont;
        private System.Windows.Forms.NumericUpDown NumericWidth;
        private System.Windows.Forms.Label Width;
        private System.Windows.Forms.Label Height;
        private System.Windows.Forms.NumericUpDown NumericHeight;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Basic;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}