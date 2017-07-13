using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace InvokerHelperDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread t;
        private void button1_Click(object sender, EventArgs e)
        {
            if (t == null)
            {
                t = new Thread(multithread);
                t.Start();
                label4.Text = string.Format(
                    "Thread state:\n{0}",
                    t.ThreadState.ToString()
                    );
            }
        }

        public void DoWork(string msg)
        {
            this.label3.Text = string.Format("Invoke method: {0}", msg);
        }

        int count = 0;
        void multithread()
        {
            while (true)
            {
                InvokeHelper.Set(this.label1, "Text", string.Format("Set value: {0}", count));
                InvokeHelper.Set(this.label1, "Tag", count);
                string value = InvokeHelper.Get(this.label1, "Tag").ToString();
                InvokeHelper.Set(this.label2, "Text",
                   string.Format("Get value: {0}", value));

                InvokeHelper.Invoke(this, "DoWork", value);

                Thread.Sleep(500);
                count++;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (t != null && t.IsAlive)
                t.Abort();
        }
    }
}
