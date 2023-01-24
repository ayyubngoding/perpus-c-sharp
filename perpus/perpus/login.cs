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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            gunaLineTextBox2.UseSystemPasswordChar = true;

        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-0R16R5N\AYYUB;Initial Catalog=diniperpus;Integrated Security=True");
        public static string nama = "";
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (gunaLineTextBox1.Text=="" || gunaLineTextBox2.Text=="" || cb.Text=="")
            {
                MessageBox.Show("Lengkapi Data!", "Pesan Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gunaLineTextBox1.Focus();
            }
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select count(*) from petugas where Username='" + gunaLineTextBox1.Text + "' and password='" + gunaLineTextBox2.Text + "' and level='"+cb.SelectedItem.ToString()+"'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                if (cb.Text=="admin")
                {
                nama = cb.Text;
                dashboard m = new dashboard();
                m.Show();
                this.Hide();
                }
                else if (cb.Text=="user")
                {
                nama = cb.Text;
                dashboard m = new dashboard();
                m.Show();
                this.Hide();
                }
            }
            else
            {
                MessageBox.Show("Masukan Data Dengan Benar!","Pesan Kesalahan",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            conn.Close();
        }
     
        private void label4_Click(object sender, EventArgs e)
        {
            register r = new register();
            r.Show();
            this.Hide();
        }


        private void gunaLineTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                gunaLineTextBox2.Focus();
            }
        }

        private void gunaLineTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                guna2GradientButton1.Focus();
            }
        }

        private void guna2GradientButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)13)
            {
                nama = gunaLineTextBox1.Text;
                dashboard m = new dashboard();
                m.Show();
                this.Hide();
            }       
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                gunaLineTextBox2.UseSystemPasswordChar = false;
            }
            else
            {
                gunaLineTextBox2.UseSystemPasswordChar = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gunaLineTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

