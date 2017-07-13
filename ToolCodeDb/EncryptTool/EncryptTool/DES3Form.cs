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
    public partial class DES3Form : Form
    {
        public DES3Form()
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
                    if (cbBase64.Checked)
                    {
                        byte[] bytes = DES3.Encrypt(entryStr, key);
                        txtDecryptStr.Text = FormatString.ToBase64String(bytes);
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = DES3.Encrypt(entryStr, key);
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
                        byte[] bytes = FormatString.FromBase64String(entryStr);
                        txtDecryptStr.Text = Encoding.Default.GetString(DES3.Decrypt(bytes, key));
                    }
                    if (cbHex16.Checked)
                    {
                        byte[] bytes = FormatString.Hex_16To2(entryStr);
                        txtDecryptStr.Text = Encoding.Default.GetString(DES3.Decrypt(bytes, key));
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
            if (!string.IsNullOrWhiteSpace(txtKey.Text) && txtKey.Text.Length != 8)
            {
                MessageBox.Show("密钥必须为8位！");
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
        private void cbHex16_Click(object sender, EventArgs e)
        {
            cbBase64.Checked = false;
            if (!cbHex16.Checked)
            {
                cbHex16.Checked = true;
            }
        }
    }
}
