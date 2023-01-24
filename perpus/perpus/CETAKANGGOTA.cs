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
    public partial class CETAKANGGOTA : Form
    {
        public CETAKANGGOTA()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("M/d/yyyy");
            
        }
        public string date,no,nama,alamat,jeniskelamin;
        public string noanggota;


        public void pictureBox3_Click(object sender, EventArgs e)
        {
            anggota N = new anggota();
            N.Show();
            this.Hide();
        }

        private void CETAKANGGOTA_Load(object sender, EventArgs e)
        {
            label11.Text = date;
            label6.Text = noanggota;
            label7.Text = nama;
            label8.Text = alamat;
            label9.Text = jeniskelamin;
        }
        Bitmap bitmap;
        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
         
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            anggota a = new anggota();
            a.Show();
            this.Hide();
        }

        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            Panel panel = new Panel();
            this.Controls.Add(panel);
            Graphics graphics = panel.CreateGraphics();
            Size size = this.ClientSize;
            bitmap = new Bitmap(size.Width, size.Height, graphics);
            graphics = Graphics.FromImage(bitmap);
            Point point = PointToScreen(panel.Location);
            graphics.CopyFromScreen(point.X, point.Y, 0, 0, size);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

 
    }
}
