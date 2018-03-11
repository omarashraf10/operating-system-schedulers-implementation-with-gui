using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace os_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Priority (Preemptive)") 
            {
                label5.Visible = true;
                textBox3.Visible = true;
            }
            else if (comboBox1.Text == "Priority (Non Preemptive)")
            {
                label5.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label5.Visible = false;
                textBox3.Visible = false;
            }
        }
    }
}
