using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EncryptTool
{
    public partial class Base64Form : Form
    {
        public Base64Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncrypt.Text;
                try
                {
                    txtDecrypt.Text = BASE64.Encrypt(entryStr);
                }
                catch (Exception ex)
                {
                    txtDecrypt.Text = ex.Message;
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncrypt.Text;
                string result = string.Empty;

                try
                {
                    txtDecrypt.Text = BASE64.Decrypt(entryStr);
                }
                catch (Exception ex)
                {
                    txtDecrypt.Text = ex.Message;
                }
            }
        }


        private bool CheckTextBox()
        {
            if (string.IsNullOrWhiteSpace(txtEncrypt.Text))
            {
                MessageBox.Show("总得输入点什么吧！");
                return false;
            }
            return true;
        }

    }
}
