using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SayangDiniUas
{
    public partial class laporan : Form
    {
        public laporan()
        {
            InitializeComponent();
            tampilkankembali();
            tampilkanpinjam();
            tampilbuku();
            tampilanggota();
        }
        public string laporan1;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        void tampilkanpinjam()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pinjam", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "pinjam");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "pinjam";
            conn.Close();
        }
        void tampilkankembali()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select kodekembali,kodepinjam,Namaanggota,judulbuku,tanggalkembali,terlambat,denda,tanggalpinjam,bataspengembalian from kembali", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kembali");
            guna2DataGridView2.DataSource = ds;
            guna2DataGridView2.DataMember = "kembali";
            conn.Close();
        }
        void tampilbuku()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from buku", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "buku");
            dgvbuku.DataSource = ds;
            dgvbuku.DataMember = "buku";
            conn.Close();
        }
        void tampilanggota()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from anggota", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "anggota");
            dgvanggota.DataSource = ds;
            dgvanggota.DataMember = "anggota";
            conn.Close();
        }
        
        private void laporan_Load(object sender, EventArgs e)
        {
            label1.Text = dashboard.username;
        }
        Bitmap bitmap;
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {

           
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            dashboard b = new dashboard();
            b.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
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

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            dashboard b = new dashboard();
            b.Show();
            this.Hide();
        }
    }
}
