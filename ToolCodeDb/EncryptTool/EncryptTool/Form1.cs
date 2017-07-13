using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace EncryptTool
{
    public partial class Form1 : Form
    {
        public int[] formFlag = {0, 0, 0, 0, 0, 0, 0};

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BuilderForm("EncryptTool.DESForm", tabControl1);
        }

        private void tabx_Click(object sender, EventArgs e)
        {

            var formTag = string.Format("EncryptTool.{0}", ((TabControl) sender).SelectedTab.Tag);
            BuilderForm(formTag, sender);
        }

        private void BuilderForm(string formTag, object sender)
        {

            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(formTag);
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            formFlag[((TabControl)sender).SelectedIndex] = 1;

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (formFlag[tabControl1.SelectedIndex] == 0)    //只生成一次  
            {
                tabx_Click(sender, e);
            } 
        }





    }
}
