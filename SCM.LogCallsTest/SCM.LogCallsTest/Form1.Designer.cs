namespace SCM.LogCallsTest
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.chkExisting = new System.Windows.Forms.CheckBox();
            this.customerName = new System.Windows.Forms.Label();
            this.customerPhone = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkRequests = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.date = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExt = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(479, 103);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Insert Call";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(314, 207);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Close Call";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkExisting
            // 
            this.chkExisting.AutoSize = true;
            this.chkExisting.Location = new System.Drawing.Point(12, 19);
            this.chkExisting.Name = "chkExisting";
            this.chkExisting.Size = new System.Drawing.Size(109, 17);
            this.chkExisting.TabIndex = 2;
            this.chkExisting.Text = "Existing Customer";
            this.chkExisting.UseVisualStyleBackColor = true;
            // 
            // customerName
            // 
            this.customerName.AutoSize = true;
            this.customerName.Location = new System.Drawing.Point(30, 43);
            this.customerName.Name = "customerName";
            this.customerName.Size = new System.Drawing.Size(82, 13);
            this.customerName.TabIndex = 3;
            this.customerName.Text = "Customer Name";
            // 
            // customerPhone
            // 
            this.customerPhone.AutoSize = true;
            this.customerPhone.Location = new System.Drawing.Point(30, 77);
            this.customerPhone.Name = "customerPhone";
            this.customerPhone.Size = new System.Drawing.Size(85, 13);
            this.customerPhone.TabIndex = 4;
            this.customerPhone.Text = "Customer Phone";
            // 
            // phone
            // 
            this.phone.Location = new System.Drawing.Point(102, 110);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(206, 20);
            this.phone.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "New Phone";
            // 
            // chkRequests
            // 
            this.chkRequests.AutoSize = true;
            this.chkRequests.Location = new System.Drawing.Point(145, 17);
            this.chkRequests.Name = "chkRequests";
            this.chkRequests.Size = new System.Drawing.Size(96, 17);
            this.chkRequests.TabIndex = 7;
            this.chkRequests.Text = "With Requests";
            this.chkRequests.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Time";
            // 
            // date
            // 
            this.date.Location = new System.Drawing.Point(102, 148);
            this.date.Name = "date";
            this.date.Size = new System.Drawing.Size(206, 20);
            this.date.TabIndex = 9;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(271, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(140, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "Select Random";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(314, 148);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "Now";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Q IDs:";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(33, 207);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(275, 95);
            this.listBox1.TabIndex = 13;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(33, 320);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(606, 178);
            this.txtOutput.TabIndex = 14;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(314, 237);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 15;
            this.button5.Text = "Delete All";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(373, 291);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(137, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "Copy Text Open";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(476, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(479, 30);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(100, 20);
            this.txtUserName.TabIndex = 18;
            this.txtUserName.Text = "User102";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Ext.";
            // 
            // txtExt
            // 
            this.txtExt.Location = new System.Drawing.Point(479, 69);
            this.txtExt.Name = "txtExt";
            this.txtExt.Size = new System.Drawing.Size(100, 20);
            this.txtExt.TabIndex = 20;
            this.txtExt.Text = "102";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 510);
            this.Controls.Add(this.txtExt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.date);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkRequests);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.phone);
            this.Controls.Add(this.customerPhone);
            this.Controls.Add(this.customerName);
            this.Controls.Add(this.chkExisting);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "SCM - Log Phone Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkExisting;
        private System.Windows.Forms.Label customerName;
        private System.Windows.Forms.Label customerPhone;
        private System.Windows.Forms.TextBox phone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkRequests;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox date;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExt;
    }
}

