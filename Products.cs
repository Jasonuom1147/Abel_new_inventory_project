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
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
          /*  Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width,
                                      workingArea.Bottom - Size.Height);*/
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;

            // Add this for the real edge of the screen:
            x = 0; // for Left Border or Get the screen Dimension to set it on the Right

            this.Location = new Point(x, y);

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            LoadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string hostName;
            hostName = System.Net.Dns.GetHostName();
            string connection = "Data Source=" + hostName + "\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            //Insert
            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }
            
            var Sqlquery = "";
            
            if (ProductExsist(con, textBox1.Text))
            {
                Sqlquery = @"UPDATE [Products] SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatus] = '" + status + "' WHERE [ProductCode] = '" + textBox1.Text + "' ";
            }
            else
            {
                Sqlquery = @"INSERT INTO [dbo].[Products] ([ProductCode],[ProductName],[ProductStatus],[Lottos],[Quantity],[Workcost]) VALUES   ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
       
            }
            SqlCommand cmd = new SqlCommand(Sqlquery,con);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
            //Readind Data Product
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [Stock].[dbo].[Products]",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Available";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Sold";
                }
                dataGridView1.Rows[n].Cells[3].Value = item["Lottos"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Quantity"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Workcost"].ToString();
                
            }
         }
        private bool ProductExsist(SqlConnection con, string productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select 1 From [Stock].[dbo].[Products] WHERE [productCode]= '"+productCode+"' ", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void LoadData()
        {
            string connection = "Data Source= HP-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [Stock].[dbo].[Products]",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                if ((bool)item["ProductStatus"])
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Active";
                }
                else
                {
                    dataGridView1.Rows[n].Cells[2].Value = "Deactive";
                }
                dataGridView1.Rows[n].Cells[3].Value = item["Lottos"].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item["Quantity"].ToString();
                dataGridView1.Rows[n].Cells[5].Value = item["Workcost"].ToString();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Active")
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                comboBox1.SelectedIndex = 1;
            }
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            comboBox1.SelectedText = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hostName;
            hostName = System.Net.Dns.GetHostName();
            string connection = "Data Source="+hostName+" \\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            var Sqlquery = "";

            if (ProductExsist(con, textBox1.Text))
            {
                con.Open();
                Sqlquery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(Sqlquery, con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                MessageBox.Show("Product not exist...!");
            }
          
            LoadData();
        
     

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string connection = "Data Source= HP-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            //Insert
            con.Open();
            bool status = false;
            if (comboBox1.SelectedIndex == 0)
            {
                status = true;
            }
            else
            {
                status = false;
            }

            var Sqlquery = "";

            if (ProductExsist(con, textBox1.Text))
            {
                Sqlquery = @"UPDATE [Products] SET [ProductName] = '" + textBox2.Text + "' ,[ProductStatus] = '" + status + "', [Lottos] ='"+textBox3.Text+"', [Quantity]='"+textBox4.Text+"',[Workcost]='"+textBox5.Text+"' WHERE [ProductCode] = '" + textBox1.Text + "' ";
            }
 
            SqlCommand cmd = new SqlCommand(Sqlquery, con);
            cmd.ExecuteNonQuery();
            con.Close();
            LoadData();
        }
    }
}
