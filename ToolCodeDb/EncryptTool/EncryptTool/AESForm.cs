using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EncryptTool.EncryptType;

namespace EncryptTool
{
    public partial class AESForm : Form
    {
        public AESForm()
        {
            InitializeComponent();
            cbBase64.Checked = true;
        }


        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                string entryStr = txtEncrypt.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        byte[] bytes = AES.Encrypt(entryStr, key);
                        txtDecrypt.Text = FormatString.ToBase64String(bytes);
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = AES.Encrypt(entryStr, key);
                        txtDecrypt.Text = FormatString.Hex_2To16(bytes);
                    }
                }
                catch (Exception ex)
                {
                    txtDecrypt.Text = ex.Message;
                }
            }
        }


        /// <summary>
        ///AES 解密
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                string entryStr = txtEncrypt.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        byte[] bytes = FormatString.FromBase64String(entryStr);
                        txtDecrypt.Text = Encoding.Default.GetString(AES.Decrypt(bytes, key));
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = FormatString.Hex_16To2(entryStr);
                        txtDecrypt.Text = Encoding.Default.GetString(AES.Decrypt(bytes, key));
                    }
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
            if(string.IsNullOrWhiteSpace(txtKey.Text))
            {
                MessageBox.Show("密钥是必须的啊！");
                return false;
            }
            if(!string.IsNullOrWhiteSpace(txtKey.Text))
            {
                if(txtKey.Text.Length!=16&&txtKey.Text.Length!=32)
                {
                    MessageBox.Show("密钥长度必须为16或32位");
                }
            }
            return true;

        }

        private void cbBase64_Click(object sender, EventArgs e)
        {
            cbHex16.Checked = false;

            if (!cbBase64.Checked)
            {
                cbBase64.Checked = true;
            }
        }

        private void cbHexTo16_Click(object sender, EventArgs e)
        {
            cbBase64.Checked = false;
            if (!cbHex16.Checked)
            {
                cbHex16.Checked = true;
            }
        }
    }
}
