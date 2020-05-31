namespace Save_Syncer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonAddNew = new System.Windows.Forms.Button();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxFolderA = new System.Windows.Forms.TextBox();
            this.textBoxFolderB = new System.Windows.Forms.TextBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerSyncTick = new System.Windows.Forms.Timer(this.components);
            this.numericUpDownTimer = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelActivityNotice = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownWrite = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownOutput = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonHide = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWrite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutput)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonAddNew
            // 
            this.buttonAddNew.Location = new System.Drawing.Point(13, 13);
            this.buttonAddNew.Name = "buttonAddNew";
            this.buttonAddNew.Size = new System.Drawing.Size(75, 23);
            this.buttonAddNew.TabIndex = 0;
            this.buttonAddNew.Text = "Add New";
            this.buttonAddNew.UseVisualStyleBackColor = true;
            this.buttonAddNew.Click += new System.EventHandler(this.buttonAddNew_Click);
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Location = new System.Drawing.Point(94, 45);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(197, 20);
            this.domainUpDown1.TabIndex = 1;
            this.domainUpDown1.Text = "Add an item";
            this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Remove";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxFolderA
            // 
            this.textBoxFolderA.Location = new System.Drawing.Point(297, 45);
            this.textBoxFolderA.Name = "textBoxFolderA";
            this.textBoxFolderA.Size = new System.Drawing.Size(332, 20);
            this.textBoxFolderA.TabIndex = 3;
            this.textBoxFolderA.TextChanged += new System.EventHandler(this.textBoxFolderA_TextChanged);
            // 
            // textBoxFolderB
            // 
            this.textBoxFolderB.Location = new System.Drawing.Point(297, 71);
            this.textBoxFolderB.Name = "textBoxFolderB";
            this.textBoxFolderB.Size = new System.Drawing.Size(332, 20);
            this.textBoxFolderB.TabIndex = 4;
            this.textBoxFolderB.TextChanged += new System.EventHandler(this.textBoxFolderB_TextChanged);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(297, 97);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.Size = new System.Drawing.Size(332, 20);
            this.textBoxFile.TabIndex = 5;
            this.textBoxFile.TextChanged += new System.EventHandler(this.textBoxFile_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerSyncTick
            // 
            this.timerSyncTick.Interval = 1000;
            this.timerSyncTick.Tick += new System.EventHandler(this.timerSyncTick_Tick);
            // 
            // numericUpDownTimer
            // 
            this.numericUpDownTimer.DecimalPlaces = 2;
            this.numericUpDownTimer.Location = new System.Drawing.Point(13, 157);
            this.numericUpDownTimer.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownTimer.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTimer.Name = "numericUpDownTimer";
            this.numericUpDownTimer.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownTimer.TabIndex = 6;
            this.numericUpDownTimer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTimer.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Update Frequency (Seconds)";
            // 
            // labelActivityNotice
            // 
            this.labelActivityNotice.AutoSize = true;
            this.labelActivityNotice.Location = new System.Drawing.Point(162, 141);
            this.labelActivityNotice.Name = "labelActivityNotice";
            this.labelActivityNotice.Size = new System.Drawing.Size(20, 13);
            this.labelActivityNotice.TabIndex = 8;
            this.labelActivityNotice.Text = "!|.!|";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.WindowText;
            this.richTextBox1.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.LimeGreen;
            this.richTextBox1.Location = new System.Drawing.Point(13, 183);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(801, 378);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(211, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Minimum Write Difference (Seconds)";
            // 
            // numericUpDownWrite
            // 
            this.numericUpDownWrite.DecimalPlaces = 2;
            this.numericUpDownWrite.Location = new System.Drawing.Point(214, 157);
            this.numericUpDownWrite.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.numericUpDownWrite.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownWrite.Name = "numericUpDownWrite";
            this.numericUpDownWrite.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWrite.TabIndex = 10;
            this.numericUpDownWrite.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownWrite.ValueChanged += new System.EventHandler(this.numericUpDownWrite_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(407, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Max Output Lines";
            // 
            // numericUpDownOutput
            // 
            this.numericUpDownOutput.Location = new System.Drawing.Point(410, 157);
            this.numericUpDownOutput.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDownOutput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOutput.Name = "numericUpDownOutput";
            this.numericUpDownOutput.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownOutput.TabIndex = 12;
            this.numericUpDownOutput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOutput.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged_1);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "NDS Save Syncer";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.closeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(739, 12);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(75, 23);
            this.buttonHide.TabIndex = 14;
            this.buttonHide.Text = "Hide";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(658, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "Help";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(635, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Folder A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(635, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Folder B";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(635, 100);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "File Name";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 573);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonHide);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownOutput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDownWrite);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.labelActivityNotice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownTimer);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.textBoxFolderB);
            this.Controls.Add(this.textBoxFolderA);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.domainUpDown1);
            this.Controls.Add(this.buttonAddNew);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NDS Save Syncer";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWrite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOutput)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddNew;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxFolderA;
        private System.Windows.Forms.TextBox textBoxFolderB;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer timerSyncTick;
        private System.Windows.Forms.NumericUpDown numericUpDownTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelActivityNotice;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownWrite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownOutput;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.Button buttonHide;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}

