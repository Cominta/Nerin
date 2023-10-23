namespace NiDE
{
    partial class NideMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NideMain));
            this.MainTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.load = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.Button();
            this.Compile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainTextBox
            // 
            this.MainTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.MainTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainTextBox.ForeColor = System.Drawing.Color.White;
            this.MainTextBox.Location = new System.Drawing.Point(0, 65);
            this.MainTextBox.Multiline = true;
            this.MainTextBox.Name = "MainTextBox";
            this.MainTextBox.Size = new System.Drawing.Size(800, 415);
            this.MainTextBox.TabIndex = 1;
            this.MainTextBox.TabStop = false;
            this.MainTextBox.TextChanged += new System.EventHandler(this.MainTextBox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.panel1.Controls.Add(this.load);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.Save);
            this.panel1.Controls.Add(this.Compile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 62);
            this.panel1.TabIndex = 2;
            // 
            // load
            // 
            this.load.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.load.Cursor = System.Windows.Forms.Cursors.Hand;
            this.load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.load.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.load.Location = new System.Drawing.Point(156, 12);
            this.load.MaximumSize = new System.Drawing.Size(80, 32);
            this.load.MinimumSize = new System.Drawing.Size(80, 32);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(80, 32);
            this.load.TabIndex = 3;
            this.load.TabStop = false;
            this.load.Text = "Load";
            this.load.UseVisualStyleBackColor = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::NiDE.Properties.Resources.settings;
            this.pictureBox1.InitialImage = global::NiDE.Properties.Resources.settings;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // Save
            // 
            this.Save.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.Location = new System.Drawing.Point(59, 12);
            this.Save.MaximumSize = new System.Drawing.Size(80, 32);
            this.Save.MinimumSize = new System.Drawing.Size(80, 32);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(80, 32);
            this.Save.TabIndex = 1;
            this.Save.TabStop = false;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = false;
            // 
            // Compile
            // 
            this.Compile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.Compile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Compile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Compile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Compile.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Compile.Location = new System.Drawing.Point(708, 12);
            this.Compile.MaximumSize = new System.Drawing.Size(80, 32);
            this.Compile.MinimumSize = new System.Drawing.Size(80, 32);
            this.Compile.Name = "Compile";
            this.Compile.Size = new System.Drawing.Size(80, 32);
            this.Compile.TabIndex = 0;
            this.Compile.TabStop = false;
            this.Compile.Text = "Compile";
            this.Compile.UseVisualStyleBackColor = false;
            this.Compile.Click += new System.EventHandler(this.Compile_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 3);
            this.panel2.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // NideMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 477);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(7680, 4320);
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "NideMain";
            this.Text = "NiDE";
            this.Load += new System.EventHandler(this.NideMain_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MainTextBox_PreviewKeyDown1(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.TextBox MainTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button Compile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button load;
        private System.Windows.Forms.Timer timer1;
    }
}

