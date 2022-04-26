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

            textBox1.Text = "1";
            textBox2.Text = "2";
            int[] Ex = new int[20];

            
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
            foreach (int i in A)
            {             
                s = i.ToString();
            }
            Gr.FindMinCycle(A,B,Ex);   

            int povt = 0;
            int ff = 0;
            if (Ex[0] == -1)
                MessageBox.Show("центральных вершин нет");
            else
            {
                while (ff < Ex.Length)
                {
                    if (Ex[ff] == 0)
                        povt += 1;
                    if (povt >= 2)
                        break;
                    MessageBox.Show("центральная вершина: " + Ex[ff]);
                    ff += 1;
                }
            }


            Trace2 = Gr.Trace22;

            foreach (int k2 in Trace2)
            {
                vv += 1;
                s2 = k2.ToString();
            }

            Len = Gr.MinCycleSize;
            
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
            Gr.ske = dataGridView1.SelectedCells[0].RowIndex;

            GrI = Drawing.CreateImageGraph(Gr);
            Invalidate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


    }
}
