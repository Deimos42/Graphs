
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab3_SppO
{
    struct Vertex
    {
        public int x;
        public int y;
        public int select;
        public int num;
        public int[] smej;
        public void Draw(System.Drawing.Graphics g)
        {
            SolidBrush hBrushVertex, hBrushSelect1, hBrushSelect2, textBrush;
            Pen pen = new Pen(Color.Black);
            hBrushSelect1 = new SolidBrush(Color.FromArgb(0, 0, 255));
            hBrushSelect2 = new SolidBrush(Color.FromArgb(0, 255, 0));  // Color.FromArgb(255, 0, 0));
            hBrushVertex = new SolidBrush(Color.White);
            textBrush = new SolidBrush(Color.Black);
            if (select == 1)
            {
                g.FillEllipse(hBrushSelect1, x - 20, y - 20, 40, 40);
                g.DrawEllipse(pen, x - 20, y - 20, 40, 40);
            }
            if (select == 2)
            {
                g.FillEllipse(hBrushSelect2, x - 20, y - 20, 40, 40);
                g.DrawEllipse(pen, x - 20, y - 20, 40, 40);
            }
            if (select == 0)
            {
                g.FillEllipse(hBrushVertex, x - 20, y - 20, 40, 40);
                g.DrawEllipse(pen, x - 20, y - 20, 40, 40);
            }
            Font font = new Font("Calibri", 10, System.Drawing.FontStyle.Bold);
            g.DrawString(num.ToString(), font, textBrush, x - 10, y - 10);

            /*
            if(Ex[0] != -1)
            {
                Pen pen2 = new Pen(Color.Red);
                hBrushSelect4 = new SolidBrush(Color.FromArgb(255, 0, 0));
                g.FillEllipse(hBrushSelect4, x - 20, y - 20, 40, 40);
                g.DrawEllipse(pen2, x - 20, y - 20, 40, 40);
            }
            */

        }
    }

class Drawing
{
    

    const int G_LENGTH = 200;

   
    
    static public Vertex [] CreateImageGraph(Graph_ext gr)
    {   
	    int N = gr.size();
	    Vertex [] v = new Vertex [N];
	    for (int i = 0; i < N; i++)
	    {
		    v[i].x = Convert.ToInt32(250+G_LENGTH*Math.Cos((double)i*(2*Math.PI/N)));
		    v[i].y = Convert.ToInt32(250-G_LENGTH*Math.Sin((double)i*(2*Math.PI/N)));
		    v[i].select = 0;
		    v[i].smej = gr.ListToVector(i);
		    v[i].num = i;
	    }
        
	    return v;
    }
    

    static public void DrawGraph(System.Drawing.Graphics g,Vertex [] gr,Graph_ext graph)
    {    
	    int N = gr.Length;
	    int n;
        Pen pen = new Pen(Color.Black);
        Point p1 = new Point();
        Point p2 = new Point();
	    for(int i = 0;i < N;i++)
	    {
		    n = gr[i].smej.Length;
		    for(int j = 0;j < n;j++)
		    {   
                p1.X = gr[i].x;
                p1.Y = gr[i].y;
                p2.X = gr[gr[i].smej[j]].x;
                p2.Y = gr[gr[i].smej[j]].y;
			    g.DrawLine(pen,p1,p2);
		    }
	    }
        
        DrawPath(g, gr, graph);
	    for(int i = 0;i < N;i++)
		    gr[i].Draw(g);
        
	 
    }


    static public bool Selection(int x,int y,Vertex [] igr,out int sel1,out int sel2,int type)
    {

	    int N = igr.Length;
	    sel1 = -1;
	    for(int i = 0;i < N;i++)
		    if(igr[i].select > 0)
			    sel1 = i;
	    for(int i = 0;i < N;i++)
	    {
		    if( (x-igr[i].x)*(x-igr[i].x)+(y-igr[i].y)*(y-igr[i].y) < 1600 )
		    {
			    sel2 =i;
			    if(sel1 == sel2)
			    {
				    igr[i].select = 0;
				    return false;
			    }
			    if(sel1 != -1)
			    {
				    return true;
			    }
			    else
			    {
				    igr[i].select = type;
				    return false;
			    }

		    }

	    }
	    sel2 = 0;
	    return false;
    }
    /*
    static void DrawPath(Graphics g,Vertex [] gr, Graph_ext graph)
    {
        string s1;
        int N2 = graph.MinSize;
        Point p1 = new Point();
        Point p2 = new Point();
        Pen pen = new Pen(Color.SlateBlue,3);
	    if (N2 > 0)
	    {
            p1.X = gr[graph.MinTrace[0]].x;
            p1.Y = gr[graph.MinTrace[0]].y;
            s1 = graph.MinTrace[0].ToString();
            //MessageBox.Show(s1);

            for (int k = 1;k < N2;k++)
			{
                p2.X = gr[graph.MinTrace[k]].x;
                p2.Y = gr[graph.MinTrace[k]].y;            
                g.DrawLine(pen,p1,p2);
                p1 = p2;
                s1 = graph.MinTrace[k].ToString();
                //MessageBox.Show("123");
                //MessageBox.Show(s1);
                //s1 = k.ToString();
                //MessageBox.Show(s1);
            }		    
	    }
        pen = new Pen(Color.Salmon, 3);
        N2 = graph.MinCycleSize;
        if (N2 > 0)
        {
            p1.X = gr[graph.MinCycle[0]].x;
            p1.Y = gr[graph.MinCycle[0]].y;
            for (int k = 1; k < N2; k++)
            {
                p2.X = gr[graph.MinCycle[k]].x;
                p2.Y = gr[graph.MinCycle[k]].y;
                g.DrawLine(pen, p1, p2);
                p1 = p2;
            }

        }
	    
    }
    */

    static void DrawPath(Graphics g, Vertex[] gr, Graph_ext graph)
    {
        string s1;
        string s2;
        int i = 0;
        int fin = graph.Trace22[graph.ske,0];
        int poi = -1;
        //int N2 = graph.MinCycleSize;
        int N2; // максимальное количество вершин
        Point p1 = new Point();
        Point p2 = new Point();
        Pen pen = new Pen(Color.ForestGreen, 3);   // Pen pen = new Pen(Color.SlateBlue, 3);

        while(fin != poi)
        {
            i += 1;
            poi = graph.Trace22[graph.ske,i];
        }
        N2 = i+1;

        if (N2 > 0)
        {
            //p1.X = gr[graph.MinCycle[0]].x;
            //p1.Y = gr[graph.MinCycle[0]].y;
            p1.X = gr[graph.Trace22[graph.ske,0]].x;
            p1.Y = gr[graph.Trace22[graph.ske,0]].y;

            for (int k = 1; k < N2; k++)
            {
                //p2.X = gr[graph.MinCycle[k]].x;
                //p2.Y = gr[graph.MinCycle[k]].y;
                p2.X = gr[graph.Trace22[graph.ske,k]].x;
                p2.Y = gr[graph.Trace22[graph.ske,k]].y;
                g.DrawLine(pen, p1, p2);
                p1 = p2;
            }
        }
        pen = new Pen(Color.DeepPink, 3);   // pen = new Pen(Color.Salmon, 3);
        N2 = graph.MinSize; 
        if (N2 > 0)
        {
            p1.X = gr[graph.MinTrace[0]].x;
            p1.Y = gr[graph.MinTrace[0]].y;
            s1 = graph.MinTrace[0].ToString();
            //MessageBox.Show(s1);

            for (int k = 1; k < N2; k++)
            {
                p2.X = gr[graph.MinTrace[k]].x;
                p2.Y = gr[graph.MinTrace[k]].y;
                g.DrawLine(pen, p1, p2);
                p1 = p2;
                s1 = graph.MinTrace[k].ToString();
                //MessageBox.Show("123");
                //MessageBox.Show(s1);
                //s1 = k.ToString();
                //MessageBox.Show(s1);
            }

        }

    }

    }

}