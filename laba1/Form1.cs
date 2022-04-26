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
    
    public partial class Form1 : Form
    {
        Graph_ext Gr;
        Vertex[] GrI;
        int N = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(N > 0)
                Drawing.DrawGraph(e.Graphics, GrI, Gr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                       
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int sel1, sel2;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Drawing.Selection(e.X, e.Y, GrI,out sel1,out sel2,1))
                {
                    Gr.Edge(sel1, sel2);
                    GrI = Drawing.CreateImageGraph(Gr);
                }
            }
            else
                if (Drawing.Selection(e.X, e.Y, GrI, out sel1, out sel2, 2))
                {
                    Gr.DelEdge(sel1, sel2);
                    GrI = Drawing.CreateImageGraph(Gr);
                }
            Invalidate();
        }

        private void создатьГрафToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
            N = f.n;
            Gr = new Graph_ext(N);
            GrI = Drawing.CreateImageGraph(Gr);
            Invalidate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
          
            int Len;
            int[] A;
            int[] B = new int[10];
            int[,] Trace2 = new int[30,20];  // *
            string s;
            string s2;
            string s3 = "";
            int vv = 0;
            int j = 1;
            int inda = 0;
            int a,b;
            try
            {
                a = Convert.ToInt32(textBox1.Text);
                b = Convert.ToInt32(textBox2.Text);
            }
            catch
            {
                return;
            }
     
            A = Gr.FindMinTrace(a, b);
            //MessageBox.Show("1111");
            foreach (int i in A)
            {             
                s = i.ToString();
                //MessageBox.Show(s);
            }
            Gr.FindMinCycle(A,B); //   Gr.FindMinCycle(A,B,Trace2); 

            Trace2 = Gr.Trace22;

            foreach (int k2 in Trace2)
            {
                vv += 1;
                s2 = k2.ToString();
                //MessageBox.Show("k2");
                //MessageBox.Show(s2);
            }

            s2 = vv.ToString();
            MessageBox.Show("vv");
            MessageBox.Show(s2);

            Len = Gr.MinCycleSize;
            /*
            MessageBox.Show("24680");
            for (int i = 0; i < Len; i++)
            {
                s2 = B[i].ToString();
                s3 = s3 + s2;
                MessageBox.Show(s2);
            }
            this.dataGridView1.Rows.Add(s3);
            */
            /*
            s3 = "";
            for (int i = 0; i < 10; i++)
            {
                s2 = Trace2[0,i].ToString();
                s3 = s3 + s2;
                //MessageBox.Show(s2);
            }
            */
            while ((Trace2[inda, 0].ToString() != "0") | (Trace2[inda, 1].ToString() != "0"))
            {
                s3 = "";
                j = 1;
                s2 = Trace2[inda,0].ToString();
                s3 = s3 + s2;
                while (Trace2[inda,j].ToString() != "0")
                {
                    s2 = Trace2[inda,j].ToString();
                    s3 = s3 + s2;
                    j += 1;
                }
                if ((Trace2[inda,j].ToString() == "0") & (Trace2[inda,0].ToString() == "0"))
                {
                    s2 = Trace2[inda,j].ToString();
                    s3 = s3 + s2;
                    j += 1;
                }
                this.dataGridView1.Rows.Add(s3);
                inda += 1;
            }

            GrI = Drawing.CreateImageGraph(Gr);
            Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string ske;
            Gr.ske = dataGridView1.SelectedCells[0].RowIndex;
            MessageBox.Show("vib");
            //MessageBox.Show(Gr.ske);

            GrI = Drawing.CreateImageGraph(Gr);
            Invalidate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /*
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex.Equals(0))
            {
                if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.Value != null)
                    MessageBox.Show("123");
            }
        }
        */



    }
}
