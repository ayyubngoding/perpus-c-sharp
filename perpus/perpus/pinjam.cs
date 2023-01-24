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
    public partial class pinjam : Form
    {
        public pinjam()
        {
            InitializeComponent();
            tampilkanpinjam();
        }
        public string pinjam1;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");

        void editstatus()
        {
            conn.Open();
            string query = "update anggota set Status='" + "Meminjam" + "'where kodeanggota='" + cbkdanggota.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            //  MessageBox.Show("data berhasil diupdate");
            conn.Close();
          
        }
        
        private void kurangbuku()
        {
            int stock, berkurang;
            conn.Open();
            string query = "select * from buku where judulbuku ='" + txtbuku.Text + "'";
            SqlCommand cmd = new SqlCommand(query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                stock = Convert.ToInt32(dr["stock"].ToString());
                berkurang = stock - 1;
                string query1 = "update buku set stock=" + berkurang + " where judulbuku='" + txtbuku.Text + "';";
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                cmd1.ExecuteReader();
            }

            conn.Close();
        }

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
        void kodeanggota()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from anggota where  status='" + "tidak meminjam" + "'", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("kodeanggota", typeof(int));
            dt.Load(rd);
            cbkdanggota.ValueMember = "kodeanggota";
           cbkdanggota.DataSource = dt;
            conn.Close();
        }
        void namaanggota()
        {
            if (conn.State==ConnectionState.Closed)
            {
                
            conn.Open();
            }
            var ayubku = "select * from anggota where kodeanggota ='" + cbkdanggota.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(ayubku, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow ayubdini in dt.Rows)
            {
                txtnamaanggota.Text = ayubdini["namaanggota"].ToString();
                guna2TextBox2.Text = ayubdini["alamat"].ToString();
                guna2TextBox7.Text = ayubdini["status"].ToString();
            }
            conn.Close();
        }

        void kodebuku()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select kodebuku from buku", conn);
            SqlDataReader rd;
            rd = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("kodebuku", typeof(int));
            dt.Load(rd);
            cbkdbuku.ValueMember = "kodebuku";
            cbkdbuku.DataSource = dt;
            conn.Close();
        }
        void namabuku()
        {
            conn.Open();
            var ayubku = "select * from buku where kodebuku ='" + cbkdbuku.SelectedValue.ToString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(ayubku, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow ayubdini in dt.Rows)
            {
                txtbuku.Text = ayubdini["judulbuku"].ToString();
               
                guna2TextBox9.Text = ayubdini["stock"].ToString();
            }
            conn.Close();
        }
        void nootomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            if (conn.State==ConnectionState.Closed)
            {
                conn.Open();
            }
           
            SqlCommand cmd = new SqlCommand("select kodepinjam from pinjam where kodepinjam in(select max(kodepinjam)from pinjam) order by kodepinjam desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kodepinjam"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "1" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            txtkdpinjam.Text = nourutan;
            conn.Close();

        }
        void kodepetugas()
        {
            conn.Open();
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
      
        private void pinjam_Load(object sender, EventArgs e)
        {
            tglpinjam.Value = DateTime.Now;
            tglkembali.Value = DateTime.Now;
           
            kodeanggota();
            kodebuku();
            label10.Text = dashboard.username;
            kodepetugas();
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from pinjam", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label8.Text = dt.Rows[0][0].ToString();
        }
        private void guna2ComboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            namaanggota();
        }

        private void guna2ComboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            namabuku();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


       

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtkdpinjam.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            cbkdanggota.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtnamaanggota.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cbkdbuku.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtbuku.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            tglpinjam.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            tglkembali.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            cbpetugas.Text = guna2DataGridView1.SelectedRows[0].Cells[7].Value.ToString();
           
            Cetak_Kartu_Pinjam.Items.Add("KODE PINJAM                   : " + txtkdpinjam.Text);
            Cetak_Kartu_Pinjam.Items.Add("NAMA ANGGOTA               : " + txtnamaanggota.Text);
            Cetak_Kartu_Pinjam.Items.Add("JUDUL BUKU                    : " + txtbuku.Text);
            Cetak_Kartu_Pinjam.Items.Add("TANGGAL PINJAM            : " + tglpinjam.Text);
            Cetak_Kartu_Pinjam.Items.Add("BATAS PENGEMBALIAN  : " + tglkembali.Text);
        }


        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from pinjam where kodepinjam like'%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "kodepinjam");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "kodepinjam";
            conn.Close();
        
       
            }

      void bersih()
        {
            cbkdanggota.Text = "";
            Cetak_Kartu_Pinjam.Items.Clear();
            cbkdbuku.Text = "";
            txtnamaanggota.Text = "";
            guna2TextBox2.Text = "";
            txtbuku.Text = "";
           
            guna2TextBox5.Text = "";
            txtkdpinjam.Text = "";
            guna2TextBox9.Text = "";
        }

        private void guna2GradientTileButton6_Click(object sender, EventArgs e)
        {
            cetakbuktipinjam ce = new cetakbuktipinjam();
            ce.kodepinjam = txtkdpinjam.Text;
            ce.nama = txtnamaanggota.Text;
            ce.judulbuku = txtbuku.Text;
            ce.tglpinjam = tglpinjam.Text;
            ce.tglkembali = tglkembali.Text;
            ce.Show();
        }

      
        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            dashboard m = new dashboard();
            m.Show();
            this.Hide();
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            if (txtnamaanggota.Text == "" && label10.Text=="admin")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            if(label10.Text=="admin")
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string dini = "update pinjam set kodeanggota='" + cbkdanggota.SelectedValue.ToString() + "',namaanggota='" + txtnamaanggota.Text + "',alamat='" + guna2TextBox2.Text + "',kodebuku='" + cbkdbuku.SelectedValue.ToString() + "',judulbuku='" + txtbuku.Text + "',tanggalpinjam='" + tglpinjam.Value.Date.ToString("yyyy-MM-dd") + "',maksimalpengembalian='" + tglkembali.Value.Date.ToString("yyyy-MM-dd") + "',kodepetugas='"+cbpetugas.SelectedValue.ToString()+"' where kodepinjam='" + txtkdpinjam.Text + "'";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil diupdate", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                tampilkanpinjam();
                kurangbuku();
            }
            else
            {
                MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtkdpinjam.Text == "" && label10.Text=="admin")
                {
                    MessageBox.Show("pilih data yang akan dihapus");
                }
                if (label10.Text=="admin")
                {
                    if (MessageBox.Show("apakah Anda akan menghapus : " + txtnamaanggota.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                        string aku = "delete from pinjam where kodepinjam='" + txtkdpinjam.Text + "'";
                        SqlCommand cmd = new SqlCommand(aku, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("data berhasil di hapus", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        conn.Close();
                        tampilkanpinjam();
                    }
                }
                else
                {
                    MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception dini)
            {

                MessageBox.Show(dini.Message);

            }
            
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (txtnamaanggota.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            else if (guna2TextBox9.Text == "0")
            {
                MessageBox.Show("Stock Buku Yang Akan Anda Pinjam Habis");
            }
            else if (Convert.ToInt32(guna2TextBox5.Text) ==0 || Convert.ToInt32(guna2TextBox5.Text) >1)
            {
                MessageBox.Show("Maksimal/Minimal Pinjam 1");
            }
            
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string dini = "insert into pinjam values('"+txtkdpinjam.Text+"','" + cbkdanggota.SelectedValue.ToString() + "','" + txtnamaanggota.Text + "', '" + cbkdbuku.SelectedValue.ToString() + "','" + txtbuku.Text + "','" + tglpinjam.Value.Date.ToString("yyyy-MM-dd") + "','" + tglkembali.Value.Date.ToString("yyyy-MM-dd") + "','"+cbpetugas.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampilkanpinjam();
                kurangbuku();
                editstatus();
            }
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }
        }
    }

