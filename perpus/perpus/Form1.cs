using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SayangDiniUas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }
        int key = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            key += 1;
            guna2CircleProgressBar1.Value = key;
            label1.Text = guna2CircleProgressBar1.Value + "%";
           
            if (guna2CircleProgressBar1.Value==100)
            {
                guna2CircleProgressBar1.Value = 100;
                timer1.Stop();
                login l = new login();
                l.Show();
                this.Hide();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
