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
    public partial class pengembalian : Form
    {
        public pengembalian()
        {
            InitializeComponent();
            tampilkanpinjam();
            tampilkankembali();
        }
        public string pengembalian1;
        //koneksi ke database
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");

        void statusanggota()
        {
            conn.Open();
            string query = "update anggota set Status='" + "Tidak Meminjam" + "'where namaanggota='" + namaanggotatb.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            //  MessageBox.Show("data berhasil diupdate");
            conn.Close();
        }

        //method untuk menambah stock agar bertambah 
        private void tambahbuku()
        {
            int stock, bertambah;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string dini = "select * from buku where judulbuku ='" + judulbukutb.Text + "'";
            SqlCommand cmd = new SqlCommand(dini, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stock = Convert.ToInt32(dr["stock"].ToString());
                bertambah = stock + 1;
                string query1 = "update buku set stock=" + bertambah + " where judulbuku='" + judulbukutb.Text + "';";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.ExecuteReader();
            }

            conn.Close();
        }
        //method memunculkan tarif denda ketika terlambat mengembalikan
        //method untk menampilkan tabel di database ke datagridview
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
        //method untk menampilkan tabel di database ke datagridview
        void tampilkankembali()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select kodekembali,kodepinjam,Namaanggota,judulbuku,tanggalkembali,terlambat,denda,tanggalpinjam,bataspengembalian,kodepetugas from kembali", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kembali");
            guna2DataGridView2.DataSource = ds;
            guna2DataGridView2.DataMember = "kembali";
            conn.Close();
        }
        void otomatis()
        {
            DateTime tglkembalid;
            DateTime tglbataskembali;
            TimeSpan tambahhari;
            int total;

            tglkembalid = tglkembali.Value;
            tglbataskembali = batas.Value;
            tambahhari = tglkembalid.Subtract(tglbataskembali);

            total = Convert.ToInt32(tambahhari.Days);

            if (total > 0)
            {
                terlambat.Text = total.ToString() + "Hari";
                denda.Text = (total * 15000).ToString();
            }
            else if (total <= 0)
            {
                denda.Text = "0";
                terlambat.Text = "Tidak";
            }
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select KodeKembali from kembali where KodeKembali in(select max(KodeKembali)from kembali) order by KodeKembali desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["KodeKembali"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "1" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            kembalitb.Text = nourutan;
            conn.Close();

        }
        void kodepetugas()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select KodePetugas from petugas", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("KodePetugas", typeof(int));
            dt.Load(rd);
            cbpetugas.ValueMember = "KodePetugas";
            cbpetugas.DataSource = dt;
            conn.Close();
        }
        string date;
        private void pengembalian_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from kembali", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label16.Text = dt.Rows[0][0].ToString();
            conn.Close();
            kodepetugas();
            tglkembali.Value = DateTime.Now;
            nootomatis();
            date = label7.Text;
            label7.Text = DateTime.Now.ToString("yyyy-MM-dd");
            label9.Text = dashboard.username;
        }
        // memunculkan data ke textbox ketika data di datagridview di klik
       
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            kpinjamtb.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            tglpinjam.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            batas.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            namaanggotatb.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            judulbukutb.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
           
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            if (kpinjamtb.Text=="")
            {
              MessageBox.Show("isi data terlebih dahulu");
            }
          
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string dini = "insert into kembali values('"+kembalitb.Text+"','" + kpinjamtb.Text + "','" + tglkembali.Value.Date.ToString("yyyy-MM-dd") + "','" + terlambat.Text + "','" + denda.Text + "','" + tglpinjam.Value.Date.ToString("yyyy-MM-dd") + "','" + batas.Value.Date.ToString("yyyy-MM-dd") + "','" + namaanggotatb.Text + "','" + judulbukutb.Text + "','"+cbpetugas.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil Mengembaikan Buku");
                conn.Close();
                tampilkanpinjam();
                tambahbuku();
                tampilkankembali();
                hapus();
                statusanggota();
                
            }
        }
        void hapus()
        {
            try
            {
                conn.Open();
                string aku = "delete from pinjam where kodepinjam='" + kpinjamtb.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                tampilkanpinjam();
            }
            catch (Exception dini)
            {

                MessageBox.Show(dini.Message);
            }
           
            
                
            
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from pinjam where kodepinjam like'%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kodepinjam");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "kodepinjam";
            conn.Close();
        }

        private void bunifuTextbox2_OnTextChange(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from kembali where kodekembali like'%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kodekembali");
            guna2DataGridView2.DataSource = ds;
            guna2DataGridView2.DataMember = "kodekembali";
            conn.Close();
        }
        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            dashboard m = new dashboard();
            m.Show();
            this.Hide();
        }
        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            otomatis();
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }
    }
}
