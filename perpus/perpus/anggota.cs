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
    public partial class anggota : Form
    {
        public anggota()
        {
            InitializeComponent();
            tampilanggota();
            label10.Text = dashboard.username;
            
        }

        
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        void tampilanggota()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from anggota", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "anggota");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "anggota";
            conn.Close();
        }
        void bersih()
        {
            guna2DateTimePicker1.Text = "";
            guna2TextBox4.Text = "";
            guna2ComboBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox1.Text = "";
            listAnggota.Items.Clear();
        }
        void editstatus()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            string query = "insert into anggota  (Status) values('" + "Tidak Meminjam" + "')";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            //  MessageBox.Show("data berhasil diupdate");
            conn.Close();

        }
        private void anggota_Load(object sender, EventArgs e)
        {
           
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from anggota", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label11.Text = dt.Rows[0][0].ToString();
        }

        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from anggota where namaanggota like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "anggota");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "anggota";
            conn.Close();
        }
        
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox3.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            guna2ComboBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
           guna2DateTimePicker1.Text= guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
          
           

            listAnggota.Items.Add("NO ANGGOTA        : " + guna2TextBox3.Text);
            listAnggota.Items.Add("NAMA                     : " + guna2TextBox1.Text);
            listAnggota.Items.Add("ALAMAT                 : " + guna2TextBox2.Text);
            listAnggota.Items.Add("JENIS KELAMIN     : " + guna2ComboBox1.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2GradientTileButton6_Click(object sender, EventArgs e)
        {
            CETAKANGGOTA c = new CETAKANGGOTA();
            c.noanggota = guna2TextBox3.Text;
            c.nama = guna2TextBox1.Text;
            c.alamat = guna2TextBox2.Text;
            c.jeniskelamin = guna2ComboBox1.SelectedItem.ToString();
            c.ShowDialog();
        }

        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            dashboard m = new dashboard();
            m.Show();
            this.Hide();
        }

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string dini = "insert into anggota (namaanggota,alamat,jeniskelamin,notelepon,tanggallahir,status) values  ('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2ComboBox1.SelectedItem.ToString() + "','" + guna2TextBox4.Text + "','" + guna2DateTimePicker1.Value.Date.ToString("yyyy-MM-dd") + "','"+"Tidak Meminjam"+"')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan","Pesan Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
                conn.Close();
                tampilanggota();
                
            }
           
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "")
            {
                MessageBox.Show("pilih data yang akan diupdate");
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string aku = "update anggota set namaanggota='" + guna2TextBox1.Text + "',alamat='" + guna2TextBox2.Text + "',jeniskelamin='" + guna2ComboBox1.SelectedItem.ToString() + "',notelepon='" + guna2TextBox4.Text + "',tanggallahir='" + guna2DateTimePicker1.Value.Date.ToString("yyyy-MM-dd") +"' where kodeanggota='" + guna2TextBox3.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update","Pesan Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                conn.Close();
                tampilanggota();
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox3.Text == "" && label10.Text=="admin")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
             if(label10.Text=="admin")
            {
             if (MessageBox.Show("apakah Anda akan menghapus : " + guna2TextBox1.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string aku = "delete from anggota where kodeanggota='" + guna2TextBox3.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di hapus","Pesan Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                conn.Close();
                tampilanggota();
            }
            }
             else if (label10.Text=="user")
             {
                 MessageBox.Show("anda bukan admin","Pesan Informasi",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
        void nootomatis()
        
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select kodeanggota from anggota where kodeanggota in(select max(kodeanggota)from anggota) order by kodeanggota desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kodeanggota"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "1" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            guna2TextBox3.Text = nourutan;
            conn.Close();
        }
        private void guna2TileButton7_Click(object sender, EventArgs e)
        {

        }
    }
}
