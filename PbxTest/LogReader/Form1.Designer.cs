namespace LogReader
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.srcStreamPort = new System.Windows.Forms.RadioButton();
            this.srcStreamFile = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.patternText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rawText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.srcStreamPort);
            this.groupBox1.Controls.Add(this.srcStreamFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 104);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Stream";
            // 
            // srcStreamPort
            // 
            this.srcStreamPort.AutoSize = true;
            this.srcStreamPort.Location = new System.Drawing.Point(7, 74);
            this.srcStreamPort.Name = "srcStreamPort";
            this.srcStreamPort.Size = new System.Drawing.Size(73, 17);
            this.srcStreamPort.TabIndex = 1;
            this.srcStreamPort.Text = "Serial Port";
            this.srcStreamPort.UseVisualStyleBackColor = true;
            // 
            // srcStreamFile
            // 
            this.srcStreamFile.AutoSize = true;
            this.srcStreamFile.Checked = true;
            this.srcStreamFile.Location = new System.Drawing.Point(7, 20);
            this.srcStreamFile.Name = "srcStreamFile";
            this.srcStreamFile.Size = new System.Drawing.Size(41, 17);
            this.srcStreamFile.TabIndex = 0;
            this.srcStreamFile.TabStop = true;
            this.srcStreamFile.Text = "File";
            this.srcStreamFile.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(25, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(289, 20);
            this.textBox1.TabIndex = 2;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(320, 41);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Pattern";
            // 
            // patternText
            // 
            this.patternText.Location = new System.Drawing.Point(19, 140);
            this.patternText.Name = "patternText";
            this.patternText.Size = new System.Drawing.Size(404, 20);
            this.patternText.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Raw Text";
            // 
            // rawText
            // 
            this.rawText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.rawText.Location = new System.Drawing.Point(19, 184);
            this.rawText.Multiline = true;
            this.rawText.Name = "rawText";
            this.rawText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.rawText.Size = new System.Drawing.Size(404, 109);
            this.rawText.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 296);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Matches";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(19, 313);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(625, 134);
            this.listBox1.TabIndex = 6;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 184);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Is Match";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(429, 213);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Parse";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // serialPort
            // 
            this.serialPort.BaudRate = global::LogReader.Properties.Settings.Default.BaudRate;
            this.serialPort.DataBits = global::LogReader.Properties.Settings.Default.DataBits;
            this.serialPort.DiscardNull = global::LogReader.Properties.Settings.Default.DiscardNull;
            this.serialPort.DtrEnable = global::LogReader.Properties.Settings.Default.DtrEnabled;
            this.serialPort.Handshake = global::LogReader.Properties.Settings.Default.Handshake;
            this.serialPort.NewLine = global::LogReader.Properties.Settings.Default.NewLine;
            this.serialPort.Parity = global::LogReader.Properties.Settings.Default.Parity;
            this.serialPort.ParityReplace = global::LogReader.Properties.Settings.Default.ParityPlace;
            this.serialPort.PortName = global::LogReader.Properties.Settings.Default.PName;
            this.serialPort.ReadBufferSize = global::LogReader.Properties.Settings.Default.ReadBufferSize;
            this.serialPort.ReadTimeout = global::LogReader.Properties.Settings.Default.ReadTimeout;
            this.serialPort.ReceivedBytesThreshold = global::LogReader.Properties.Settings.Default.RcvBytesThreshold;
            this.serialPort.RtsEnable = global::LogReader.Properties.Settings.Default.RtsEnabled;
            this.serialPort.StopBits = global::LogReader.Properties.Settings.Default.StopBits;
            this.serialPort.WriteBufferSize = global::LogReader.Properties.Settings.Default.WriteBufferSize;
            this.serialPort.WriteTimeout = global::LogReader.Properties.Settings.Default.WriteTimeout;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(429, 140);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(32, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 458);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rawText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.patternText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Log Reader";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton srcStreamPort;
        private System.Windows.Forms.RadioButton srcStreamFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox textBox1;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox patternText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox rawText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

