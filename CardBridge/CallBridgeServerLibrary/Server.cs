using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Server
{
    public delegate void AppentTextDelegate(string s);
    public delegate void LogTextDelegate(string s);

    public partial class Server : Form
    {
        TalkServ server = new TalkServ();
        Thread serverThread;

        public Server()
        {
            InitializeComponent();
            server.AppendServerMessage = new AppentTextDelegate(AppendText);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
        serverThread = new Thread(new ThreadStart(server.StartServer));
            serverThread.Start();
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(server.StopServer)).Start();
            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Server_Load(object sender, EventArgs e)
        {
            textBox1.Visible = checkBox1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Visible = checkBox1.Checked;
        }

        public void AppendText(string msg)
        {
            Action action = () => textBox1.AppendText(Environment.NewLine + msg);
            Invoke(action);
        }

        public void LogText(string msg)
        {
            Action action = () => {  };
            Invoke(action);
        }
    }
}
