using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stock
{
    public partial class StockMain : Form
    {
       

        public StockMain()
        {
            InitializeComponent();
            //load stock list at start
            PStock pro1 = new PStock();
            pro1.MdiParent = this;
            pro1.Show();
        }

        private void StockMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void StockMain_Load(object sender, EventArgs e)
        {

          
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.MdiParent = this; //align to main window 
            pro.Show();

        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PStock pro1 = new PStock();
            pro1.MdiParent = this;
            pro1.Show();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult iexit;
            iexit = MessageBox.Show("Confirm Exit", "?", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (iexit == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

     
    }
}
