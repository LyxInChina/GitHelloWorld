namespace EncryptTool
{
    partial class Base64Form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnEncrypt = new System.Windows.Forms.Button();
            this.txtDecrypt = new System.Windows.Forms.TextBox();
            this.txtEncrypt = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDecrypt);
            this.groupBox1.Controls.Add(this.btnEncrypt);
            this.groupBox1.Controls.Add(this.txtDecrypt);
            this.groupBox1.Controls.Add(this.txtEncrypt);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(570, 390);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BASE64";
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Location = new System.Drawing.Point(489, 171);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(75, 23);
            this.btnDecrypt.TabIndex = 3;
            this.btnDecrypt.Text = "解密";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnEncrypt
            // 
            this.btnEncrypt.Location = new System.Drawing.Point(408, 171);
            this.btnEncrypt.Name = "btnEncrypt";
            this.btnEncrypt.Size = new System.Drawing.Size(75, 23);
            this.btnEncrypt.TabIndex = 2;
            this.btnEncrypt.Text = "加密";
            this.btnEncrypt.UseVisualStyleBackColor = true;
            this.btnEncrypt.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDecrypt
            // 
            this.txtDecrypt.Location = new System.Drawing.Point(7, 200);
            this.txtDecrypt.Multiline = true;
            this.txtDecrypt.Name = "txtDecrypt";
            this.txtDecrypt.Size = new System.Drawing.Size(555, 170);
            this.txtDecrypt.TabIndex = 1;
            // 
            // txtEncrypt
            // 
            this.txtEncrypt.Location = new System.Drawing.Point(7, 20);
            this.txtEncrypt.Multiline = true;
            this.txtEncrypt.Name = "txtEncrypt";
            this.txtEncrypt.Size = new System.Drawing.Size(555, 145);
            this.txtEncrypt.TabIndex = 0;
            // 
            // Base64Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 409);
            this.Controls.Add(this.groupBox1);
            this.Name = "Base64Form";
            this.Text = "Base64";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.TextBox txtDecrypt;
        private System.Windows.Forms.TextBox txtEncrypt;
    }
}