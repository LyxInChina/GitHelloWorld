namespace EncryptTool
{
    partial class DESForm
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
            this.txtEncryptStr = new System.Windows.Forms.TextBox();
            this.txtDecryptStr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbHex16 = new System.Windows.Forms.CheckBox();
            this.cbBase64 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEncryptStr
            // 
            this.txtEncryptStr.Location = new System.Drawing.Point(26, 47);
            this.txtEncryptStr.Multiline = true;
            this.txtEncryptStr.Name = "txtEncryptStr";
            this.txtEncryptStr.Size = new System.Drawing.Size(543, 149);
            this.txtEncryptStr.TabIndex = 0;
            // 
            // txtDecryptStr
            // 
            this.txtDecryptStr.Location = new System.Drawing.Point(26, 231);
            this.txtDecryptStr.Multiline = true;
            this.txtDecryptStr.Name = "txtDecryptStr";
            this.txtDecryptStr.Size = new System.Drawing.Size(542, 153);
            this.txtDecryptStr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "密钥:";
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(64, 16);
            this.txtKey.MaxLength = 8;
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(197, 21);
            this.txtKey.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(399, 202);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "加密";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(493, 202);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "解密";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbHex16);
            this.groupBox1.Controls.Add(this.cbBase64);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 398);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DES";
            // 
            // cbHex16
            // 
            this.cbHex16.AutoSize = true;
            this.cbHex16.Location = new System.Drawing.Point(358, 17);
            this.cbHex16.Name = "cbHex16";
            this.cbHex16.Size = new System.Drawing.Size(60, 16);
            this.cbHex16.TabIndex = 1;
            this.cbHex16.Text = "16进制";
            this.cbHex16.UseVisualStyleBackColor = true;
            this.cbHex16.Click += new System.EventHandler(this.cbHex16_Click);
            // 
            // cbBase64
            // 
            this.cbBase64.AutoSize = true;
            this.cbBase64.Location = new System.Drawing.Point(273, 18);
            this.cbBase64.Name = "cbBase64";
            this.cbBase64.Size = new System.Drawing.Size(60, 16);
            this.cbBase64.TabIndex = 0;
            this.cbBase64.Text = "Base64";
            this.cbBase64.UseVisualStyleBackColor = true;
            this.cbBase64.Click += new System.EventHandler(this.cbBase64_Click);
            // 
            // DESForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 418);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtKey);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDecryptStr);
            this.Controls.Add(this.txtEncryptStr);
            this.Controls.Add(this.groupBox1);
            this.Name = "DESForm";
            this.Text = "DESForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtEncryptStr;
        private System.Windows.Forms.TextBox txtDecryptStr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbHex16;
        private System.Windows.Forms.CheckBox cbBase64;
    }
}