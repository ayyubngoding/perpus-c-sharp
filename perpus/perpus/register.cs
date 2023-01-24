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
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        private void register_Load(object sender, EventArgs e)
        {

        }

        private void guna2GradientTileButton1_Click(object sender, EventArgs e)
        {
            if (guna2TextBox1.Text == "")
            {
                MessageBox.Show("isi data terlebih dahulu");
            }
            else
            {
                conn.Open();
                string dini = "insert into petugas values('" + guna2TextBox1.Text + "','" + guna2TextBox2.Text + "','" + guna2TextBox3.Text + "','" + guna2TextBox4.Text + "','" +guna2TextBox5.Text + "')";
                SqlCommand cmd = new SqlCommand(dini, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("data berhasil disimpan");
                conn.Close();
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            login l = new login();
            l.Show();
            this.Hide();
        }

    }
}
