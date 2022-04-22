using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderForm
{
    public partial class Form2 : Form
    {
        public String OrderID { get; set; }
        Order.OrderService os;
        
        public Form2(ref Order.OrderService os)
        {
            InitializeComponent();
            //textBox_OrderID.DataBindings.Add(Text, this, OrderID);
            this.os = os;
        }

        public Form2(string UploadOrderID, ref Order.OrderService os)
        {
            InitializeComponent();
            //textBox_OrderID.DataBindings.Add(Text,this,OrderID);
            textBox_OrderID.Enabled = false;
        }

        private void button_done_Click(object sender, EventArgs e)
        {
            this.Close();
            //alert OrderService
            os.AddOrder(Order.Order(dataGridView1.DataSource));
        }

        //created a line, write to os
        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int rownum = e.RowIndex;
            new Order.OrderDetails(this.dataGridView1.Rows[rownum].Cells[0].Value.ToString(), (int)this.dataGridView1.Rows[rownum].Cells[1].Value, (int)this.dataGridView1.Rows[rownum].Cells[2].Value);
        }
    }
}
