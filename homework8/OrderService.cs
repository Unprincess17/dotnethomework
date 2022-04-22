using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Order;

namespace OrderForm
{
    public partial class Form1 : Form
    {
        public int OrderID { get; set; }
        public String QueryMethod { get; set; }
        public OrderService os = new OrderService();
        public Form1()
        {
            InitializeComponent();
            QueryMethod = "All";
            Order.Order o1 = new Order.Order();
            o1.AddItem(new OrderDetails("car", 1, 2));
            os.AddOrder(o1);
            //dataGridView1.DataBindings.Add(Index, this, OrderID.ToString());
            //Cbx_SearchItem.DataBindings.Add(Text, this, QueryMethod);
        }


        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new Form2(os.Show().Count().ToString(),ref os).Show();//insert to the next order
        }

        private void UploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(OrderID <0 && OrderID >= os.Show().Count())
            {
                MessageBox.Show("Invalid OrderId, please check again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            new Form2(OrderID.ToString(), ref os).Show();
        }

        private void button_Search_Click(object sender, EventArgs e)
        {
            int SearchMethod = this.Cbx_SearchItem.SelectedIndex;
            switch (SearchMethod)
            {
                case 0://search all
                    this.dataGridView1.DataSource = os.OrderList;
                    //this.dataGridView1.Refresh();
                    break;
                case 1://OrderID this is no ID
                    break;
                case 2://Goods
                    this.dataGridView1.DataSource = os.SearchbyProduct(this.textBox_SearchInfo.Text);
                    break;
            }
                
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = os.OrderList[this.dataGridView1.CurrentRow.Index];
        }


        //import current os
        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog_import = new OpenFileDialog();
            if(openFileDialog_import.ShowDialog() == DialogResult.OK)
            {
                //catch used in Import
                os.Import(openFileDialog_import.FileName);
            }
        }

        //export current os
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog_import = new OpenFileDialog();
            if (openFileDialog_import.ShowDialog() == DialogResult.OK)
            {
                //catch used in Import
                os.Export(openFileDialog_import.FileName);
            }
        }
    }



}
