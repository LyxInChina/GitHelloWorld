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
    public partial class DESForm : Form
    {
        public DESForm()
        {
            InitializeComponent();
            cbBase64.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncryptStr.Text;
                string key = txtKey.Text;
                try
                {
                    if(cbBase64.Checked)
                    {
                        //返回加密后的base64密文
                        byte[] bytes = DES.Encrypt(entryStr, key);
                        txtDecryptStr.Text  = FormatString.ToBase64String(bytes);
                    }
                    if(cbHex16.Checked)
                    {
                        //返回加密后的16进制密文
                        byte[] bytes  = DES.Encrypt(entryStr, key);
                        txtDecryptStr.Text = FormatString.Hex_2To16(bytes);
                    }
                }
                catch (Exception ex)
                {
                    txtDecryptStr.Text = ex.Message;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncryptStr.Text;
                string key = txtKey.Text;
                try
                {
                    if (cbBase64.Checked)
                    {
                        //将base64转成byte后再解密
                        byte[] entryBytes = FormatString.FromBase64String(txtEncryptStr.Text);
                        byte[] bytes = DES.Decrypt(entryBytes, key);
                        txtDecryptStr.Text = Encoding.Default.GetString(bytes);

                    }
                    if (cbHex16.Checked)
                    {
                        //将16进制转成2进制后再解密
                        byte[] entryBytes = FormatString.Hex_16To2(txtEncryptStr.Text);
                        byte[] bytes = DES.Decrypt(entryBytes, key);
                        txtDecryptStr.Text = Encoding.Default.GetString(bytes);
                    }
                }
                catch (Exception ex)
                {
                    txtDecryptStr.Text = ex.Message;
                }
            }
        }

        private bool CheckTextBox()
        {
            if (string.IsNullOrWhiteSpace(txtEncryptStr.Text))
            {
                MessageBox.Show("总得输入点什么吧！");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtKey.Text))
            {
                MessageBox.Show("要密钥的啊！");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(txtKey.Text) && txtKey.Text.Length!=8)
            {
                MessageBox.Show("密钥必须为8位");
            }
            return true;
        }

        private void cbHex16_Click(object sender, EventArgs e)
        {
            cbBase64.Checked = false;
            if(!cbHex16.Checked)
            {
                cbHex16.Checked = true;
            }
        }

        private void cbBase64_Click(object sender, EventArgs e)
        {
            cbHex16.Checked = false;
            if (!cbBase64.Checked)
            {
                cbBase64.Checked = true;
            }
            
        }

    }
}
