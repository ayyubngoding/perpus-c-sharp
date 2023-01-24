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
    public partial class buku : Form
    {
        public buku()
        {
            InitializeComponent();
            tampilkanbuku();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");

        void bersih1()
        {
            
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
            guna2TextBox6.Text = "";
        }
        
        void tampilkanbuku()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from buku", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "buku");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "buku";
            conn.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox6.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            guna2TextBox4.Text =guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
           guna2ComboBox1.Text= guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
        private void bunifuTextbox1_OnTextChange(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select * from buku where judulbuku like '%" + bunifuTextbox1.text + "%'", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "buku");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "buku";
            conn.Close();
        }
        void otomatis()
        {
            long hitung;
            string nourutan;
            SqlDataReader rd;
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            SqlCommand cmd = new SqlCommand("select kodebuku from buku where kodebuku in(select max(kodebuku)from buku) order by kodebuku desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kodebuku"].ToString().Length - 1, 1)) + 1;
                string kodeurutan = "0" + hitung;
                nourutan = "1" + kodeurutan.Substring(kodeurutan.Length - 1, 1);
            }
            else
            {
                nourutan = "1";
            }
            rd.Close();
            guna2TextBox6.Text = nourutan;
            conn.Close();
        
        }
      
        public string buku1;
        private void buku_Load(object sender, EventArgs e)
        {
            otomatis();
            label10.Text = dashboard.username;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from buku", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            label7.Text = dt.Rows[0][0].ToString();
        }
       
        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
             dashboard m = new dashboard();
            m.Show();
            this.Hide();
        }


        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text =="")
            {
                MessageBox.Show("pilih data yang akan diupdate");
            }
            else
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string aku = "update buku set judulbuku='" + guna2TextBox1.Text + "',kategori='" + guna2ComboBox1.SelectedItem.ToString() + "',penulis='" + guna2TextBox2.Text + "',penerbit='" + guna2TextBox3.Text + "',tahun='" + guna2TextBox4.Text + "',stock='" + guna2TextBox5.Text + "' where kodebuku='" + guna2TextBox6.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                tampilkanbuku();
            }
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
                string dini = "insert into buku values('" + guna2TextBox1.Text + "','" + guna2ComboBox1.SelectedItem.ToString() + "','" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "','" + guna2TextBox4.Text + "','" + guna2TextBox5.Text + "')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
                tampilkanbuku();
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text=="" && label10.Text=="admin")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
            if (label10.Text=="admin")
            {
                if (MessageBox.Show("apakah Anda akan menghapus : " + guna2TextBox1.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string aku = "delete from buku where kodebuku='" + guna2TextBox6.Text + "'";
                    SqlCommand cmd = new SqlCommand(aku, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("data berhasil di hapus", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                    tampilkanbuku();
                }
            }
            else
            {
                MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            otomatis();
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            bersih1();
        }
    }
}
