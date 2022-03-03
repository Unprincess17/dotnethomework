using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "hello world";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //label1.Text = comboBox1.SelectedIndex.ToString();
            int s1 = Int32.Parse(textBox1.Text), s2 = Int32.Parse(textBox2.Text), sd = 0 ;
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    sd = s1 + s2;
                    break;
                case 1:
                    sd = s1 - s2;
                    break;
                case 2:
                    sd = s1 * s2;
                    break;
                case 3:
                    sd = s2 == 0 ? 0 : s1 / s2;
                    break;
            }
            label1.Text = sd.ToString();
        }
    }
}
