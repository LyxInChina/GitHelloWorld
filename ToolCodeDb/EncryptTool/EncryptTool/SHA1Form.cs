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
    public partial class SHA1Form : Form
    {
        public SHA1Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckTextBox())
            {
                var entryStr = txtEncryptStr.Text;
                try
                {
                    txtDecryptStr.Text = RHA_1.Encrypt(entryStr);
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
            return true;
        }
    }
}
