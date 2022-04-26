using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab3_SppO
{
    public partial class Form2 : Form
    {
        public int n;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            n = Convert.ToInt32(textBox1.Text);
            if (n <= 0)
                return;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            n = 0;
            Close();
        }
    }
}
