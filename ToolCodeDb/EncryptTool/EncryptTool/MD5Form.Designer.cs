namespace EncryptTool
{
    partial class MD5Form
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
            this.txtEncryptStr = new System.Windows.Forms.TextBox();
            this.txtDecryptStr = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(481, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "加密";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtEncryptStr
            // 
            this.txtEncryptStr.Location = new System.Drawing.Point(23, 25);
            this.txtEncryptStr.Multiline = true;
            this.txtEncryptStr.Name = "txtEncryptStr";
            this.txtEncryptStr.Size = new System.Drawing.Size(546, 149);
            this.txtEncryptStr.TabIndex = 11;
            // 
            // txtDecryptStr
            // 
            this.txtDecryptStr.Location = new System.Drawing.Point(23, 222);
            this.txtDecryptStr.Multiline = true;
            this.txtDecryptStr.Name = "txtDecryptStr";
            this.txtDecryptStr.Size = new System.Drawing.Size(546, 153);
            this.txtDecryptStr.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(13, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 408);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MD5";
            // 
            // MD5Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 435);
            this.Controls.Add(this.txtDecryptStr);
            this.Controls.Add(this.txtEncryptStr);
            this.Controls.Add(this.groupBox1);
            this.Name = "MD5Form";
            this.Text = "MD5";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtEncryptStr;
        private System.Windows.Forms.TextBox txtDecryptStr;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}