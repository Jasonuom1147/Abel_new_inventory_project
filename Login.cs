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

namespace Stock
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TO-DO check user pass
            string hostname;
            hostname = System.Net.Dns.GetHostName();
            string connection = "Data Source=" + hostname + "\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            SqlDataAdapter sda= new SqlDataAdapter(@"SELECT * 
                     FROM [dbo].[Login] Where userName='"+textBox1.Text+"' and Password='"+textBox2.Text+"'",con);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Wrong Username or Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_Click(sender, e);
            }

        }
    }
}
