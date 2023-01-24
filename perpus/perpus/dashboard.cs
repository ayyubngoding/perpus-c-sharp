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
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
            label2.Text = login.nama;
        }
        
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        public static string username = "";
        private void dashboard_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from buku", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            bukulbl.Text = dt.Rows[0][0].ToString();
            SqlDataAdapter da1 = new SqlDataAdapter("select count (*) from petugas", conn);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            petugas.Text = dt1.Rows[0][0].ToString();
            SqlDataAdapter da2 = new SqlDataAdapter("select count (*) from pinjam", conn);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            pinjam.Text = dt2.Rows[0][0].ToString();
            SqlDataAdapter da3 = new SqlDataAdapter("select count (*) from kembali", conn);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);
            kembali.Text = dt3.Rows[0][0].ToString();
            SqlDataAdapter da4 = new SqlDataAdapter("select count (*) from anggota", conn);
            DataTable dt4 = new DataTable();
            da4.Fill(dt4);
            anggota.Text = dt4.Rows[0][0].ToString();
            conn.Close();
            
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }



       

      

   


        private void guna2GradientTileButton9_Click(object sender, EventArgs e)
        {
            username = label2.Text;
            anggota a = new anggota();
            a.Show();
            this.Hide();
        }

        private void guna2GradientTileButton12_Click(object sender, EventArgs e)
        {
           
                username = label2.Text;
                laporan m = new laporan();
                m.Show();
                this.Hide();
          
        }

        private void guna2GradientTileButton11_Click(object sender, EventArgs e)
        {
            username = label2.Text;
            pengembalian p = new pengembalian();
            p.Show();
            this.Hide();
        }

        private void guna2GradientTileButton10_Click(object sender, EventArgs e)
        {
            username = label2.Text;
            pinjam p = new pinjam();
            p.Show();
            this.Hide();
        }

        private void guna2GradientTileButton8_Click(object sender, EventArgs e)
        {
            username = label2.Text;
            buku p = new buku();
            p.Show();
            this.Hide();
        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
                username = label2.Text;
                petugas p = new petugas();
                p.Show();
                this.Hide(); 
           
        }

        private void guna2GradientTileButton13_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
