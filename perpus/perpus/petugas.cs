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
    public partial class petugas : Form
    {
        public petugas()
        {
            InitializeComponent();
            tampildatapetugas();
            label9.Text = dashboard.username;

        }
       
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        void tampildatapetugas()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from petugas  ", conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "petugas");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "petugas";
            conn.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void bersih()
        {
            guna2TextBox5.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox1.Text = "";
            guna2TextBox6.Text = "";
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
            SqlCommand cmd = new SqlCommand("select kodepetugas from petugas where kodepetugas in(select max(kodepetugas)from petugas) order by kodepetugas desc", conn);
            rd = cmd.ExecuteReader();
            rd.Read();
            if (rd.HasRows)
            {
                hitung = Convert.ToInt64(rd[0].ToString().Substring(rd["kodepetugas"].ToString().Length - 1, 1)) + 1;
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
      
        private void petugas_Load(object sender, EventArgs e)
        {
            guna2TextBox5.UseSystemPasswordChar = true;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter("select count (*) from petugas", conn);
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
            SqlCommand cmd = new SqlCommand("select * from petugas where namapetugas like '%"+ bunifuTextbox1.text+"%'",conn);
            cmd.ExecuteNonQuery();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds, "petugas");
            guna2DataGridView1.DataSource = ds;
            guna2DataGridView1.DataMember = "petugas";
            conn.Close();
        }
        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox6.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            guna2TextBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            guna2TextBox2.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            guna2TextBox3.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            gunaComboBox1.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
           
        }


        private void guna2TileButton4_Click(object sender, EventArgs e)
        {
            dashboard d = new dashboard();
            d.Show();
            this.Hide();
        }

        private void guna2TileButton3_Click(object sender, EventArgs e)
        {
            bersih();
        }

        private void guna2TileButton5_Click(object sender, EventArgs e)
        {
            if (guna2TextBox6.Text == "")
            {
                MessageBox.Show("pilih data yang akan diupdate");
            }
            if (label9.Text == "admin")
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string aku = "update petugas set namapetugas='" + guna2TextBox1.Text + "',alamat='" + guna2TextBox2.Text + "',username='" + guna2TextBox3.Text + "',notelepon='" + guna2TextBox4.Text + "',password='" + guna2TextBox5.Text + "',level='"+gunaComboBox1.SelectedItem.ToString()+"' where kodepetugas='" + guna2TextBox6.Text + "'";
                SqlCommand cmd = new SqlCommand(aku, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil di update");
                conn.Close();
                tampildatapetugas();
            }
            else
            {
                MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2TileButton2_Click(object sender, EventArgs e)
        {

            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("pilih data yang akan dihapus");
            }
            else if(label9.Text=="admin")
            {
                if (MessageBox.Show("apakah Anda akan menghapus : " + guna2TextBox1.Text + "?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    string aku = "delete from petugas where kodepetugas='" + guna2TextBox6.Text + "'";
                    SqlCommand cmd = new SqlCommand(aku, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("data berhasil di hapus");
                    conn.Close();
                    tampildatapetugas();
                }
            }
             else
                {
                    MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        

        private void guna2TileButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            if(label9.Text=="admin")
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string dini = "insert into petugas values('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "','" + guna2TextBox4.Text + "','" + guna2TextBox5.Text + "','"+gunaComboBox1.SelectedItem.ToString()+"')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
                tampildatapetugas();
            }
            else
            {
                MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2TileButton6_Click(object sender, EventArgs e)
        {
            nootomatis();
        }

        private void guna2TileButton7_Click(object sender, EventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked==true && label9.Text=="admin")
            {
                guna2TextBox5.UseSystemPasswordChar = false;
            }
            else if (label9.Text=="user")
            {
                MessageBox.Show("Anda Bukan Admin", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                guna2TextBox5.UseSystemPasswordChar = true;
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex==5 && e.Value !=null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
