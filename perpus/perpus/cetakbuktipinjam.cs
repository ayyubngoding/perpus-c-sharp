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
    public partial class cetakbuktipinjam : Form
    {
        public cetakbuktipinjam()
        {
            InitializeComponent();
            date = DateTime.Now.ToString("M/d/yyyy");
        }
        public  string date, kodepinjam, nama, judulbuku, tglpinjam, tglkembali;
        private void cetakbuktipinjam_Load(object sender, EventArgs e)
        {
            label11.Text = date;
            label6.Text = kodepinjam;
            label7.Text = nama;
            label8.Text = judulbuku;
            label9.Text = tglpinjam;
            label2.Text = tglkembali;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pinjam p = new pinjam();
            p.Show();
            this.Hide();
        }
        Bitmap bitmap;
        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
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
