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
    public partial class PStock : Form
    {
    
        public PStock()
        {         
            InitializeComponent();
            this.Size = new Size(800, 470);
          
            LoadData();
        }
        public void LoadData()
        {
            string hostName;
            hostName = System.Net.Dns.GetHostName();
            string connection = "Data Source=" + hostName + "\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True";
            SqlConnection con = new SqlConnection(connection);
            SqlDataAdapter sda = new SqlDataAdapter("Select * From [Stock].[dbo].[Products]", con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

       

       
    }
}
