using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Client_Tcp_
{
    public partial class Form1 : Form
    {
        SynchronizationContext uiContext;
        Controller.Controller controller;
        public Form1()
        {
            InitializeComponent();
            controller = new Controller.Controller(this);
            uiContext = new SynchronizationContext();


        }
        public string GetMassageFromTextBox()
        {
            string res = textBox1.Text;
            textBox1.Text = "";
            IniTextBox(res);
            return res;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
          controller.CloseSocket();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(controller.Exchange));
            thread.IsBackground = true;
            thread.Start();
           
        }

     
        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(controller.Connect));
            thread.IsBackground = true;
            thread.Start();

        }
        public void IniTextBox(string text)     //ошибочки)
        {
            
            uiContext.Send(x=>listBox1.Items.Add(text),null);

        }
    }
}
