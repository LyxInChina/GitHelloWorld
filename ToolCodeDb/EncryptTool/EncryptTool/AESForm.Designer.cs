namespace EncryptTool
{
    partial class AESForm
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
            this.密钥 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbHex16 = new System.Windows.Forms.CheckBox();
            this.cbBase64 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtDecrypt = new System.Windows.Forms.TextBox();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnEnCrypt = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 密钥
            // 
            this.密钥.AutoSize = true;
            this.密钥.Location = new System.Drawing.Point(32, 31);
            this.密钥.Name = "密钥";
            this.密钥.Size = new System.Drawing.Size(35, 12);
            this.密钥.TabIndex = 10;
            this.密钥.Text = "密钥:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbHex16);
            this.groupBox1.Controls.Add(this.cbBase64);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.txtDecrypt);
            this.groupBox1.Controls.Add(this.txtEncrypt);
            this.groupBox1.Controls.Add(this.txtKey);
            this.groupBox1.Location = new System.Drawing.Point(17, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 437);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "AES";
            // 
            // cbHex16
            // 
            this.cbHex16.AutoSize = true;
            this.cbHex16.Location = new System.Drawing.Point(366, 25);
            this.cbHex16.Name = "cbHex16";
            this.cbHex16.Size = new System.Drawing.Size(60, 16);
            this.cbHex16.TabIndex = 8;
            this.cbHex16.Text = "16进制";
            this.cbHex16.UseVisualStyleBackColor = true;
            this.cbHex16.Click += new System.EventHandler(this.cbHexTo16_Click);
            // 
            // cbBase64
            // 
            this.cbBase64.AutoSize = true;
            this.cbBase64.Location = new System.Drawing.Point(281, 26);
            this.cbBase64.Name = "cbBase64";
            this.cbBase64.Size = new System.Drawing.Size(60, 16);
            this.cbBase64.TabIndex = 7;
            this.cbBase64.Text = "Base64";
            this.cbBase64.UseVisualStyleBackColor = true;
            this.cbBase64.Click += new System.EventHandler(this.cbBase64_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(400, 223);
            this.button1.Name = "button1";
            this.button1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "加密";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button2_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(481, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "解密";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(17, 254);
            this.txtDecrypt.Multiline = true;
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(544, 162);
            this.txtDecrypt.TabIndex = 2;
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(11, 51);
            this.txtEncrypt.Multiline = true;
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(550, 162);
            this.txtEncrypt.TabIndex = 2;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(56, 24);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(201, 21);
            this.txtKey.TabIndex = 4;
            // 
            // btnEnCrypt
            // 
            this.btnEnCrypt.Location = new System.Drawing.Point(445, 221);
            this.btnEnCrypt.Name = "btnEnCrypt";
            this.btnEnCrypt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEnCrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEnCrypt.TabIndex = 8;
            this.btnEnCrypt.Text = "加密";
            this.btnEnCrypt.UseVisualStyleBackColor = true;
            // 
            // AESForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 454);
            this.Controls.Add(this.密钥);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnEnCrypt);
            this.Name = "AESForm";
            this.Text = "AESForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label 密钥;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDecrypt;
        private System.Windows.Forms.TextBox txtEncrypt;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnEnCrypt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbHex16;
        private System.Windows.Forms.CheckBox cbBase64;
    }
}