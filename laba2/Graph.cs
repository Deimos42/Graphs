using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
//using System.Windows.Forms;


namespace Lab3_SppO
{
    class node
    {
        public int vert;
        public node next;
    };

    class Stack
    {
        node t;  // node t

        int n;
        public Stack()
        {
            t = null;
            n = 0;
        }
        public void push(int element)
        {
            node temp = new node();
            temp.vert = element;
            temp.next = t;
            t = temp;
            n++;
        }
        public int pop()
        {
            int temp;
            node x;
            x = t;
            temp = x.vert;
            t = t.next;
            n--;
            return temp;
        }
        public int top()
        {
            return t.vert;
        }
        public bool IsEmpty()
        {
            if (n == 0)
                return true;
            else
                return false;
        }
        public int[] StackToVector()
        {
            node temp = t;
            int[] v = new int[n];
            int i = n - 1;
            while (temp != null)
            {
                v[i] = temp.vert;
                temp = temp.next;
                i--;
            }
            return v;
        }
    };


    abstract class Graph
    {
        protected int s;
        protected Stack stack;
        public int[] MinTrace;
        public int MinSize;
        public int[] MinCycle;
        public int MinCycleSize;
        public int[,] Trace22 = new int[30, 20]; //*
        public int ske = 0;

        public int[] eccentricity = new int[20]; // 20 = N
        public int diameter;  // диаметр графа
        public int center;  // радиус графа

        public int arcCount;  //

        public Graph()
        {
            MinSize = -1;
            s = 0;
            stack = new Stack();
            MinCycleSize = -1;
        }
        public Graph(int size)
        {
            MinSize = -1;
            s = size;
            stack = new Stack();
            MinCycleSize = -1;
        }
        public int size()
        {
            return s;
        }
        public int[] FindMinTrace(int vFrom, int vTo)
        {
            string s4;
            MinSize = -1;
            MinCycleSize = -1;
            int[] M = new int[s];
            int[] Trace;
            int vert = vFrom;
            while (!stack.IsEmpty())  // возможно то же сделать для функции цикла 
            {
                stack.pop();
            }

            stack.push(vFrom);
            int start = -1;
            for (int i = 0; i < s; i++)
            {
                M[i] = 0;
            }
            M[vFrom] = 1;
            while (!stack.IsEmpty())
            {
                vert = NextVertex(stack.top(), start, M);
                if (vert == -1)
                {
                    start = stack.pop();
                    M[start] = 0;
                    continue;
                }
                if (vert == vTo)
                {
                    Trace = stack.StackToVector();
                    if ((MinSize == -1) || (Trace.Length < MinSize))
                        if (CheckTrace(Trace))
                        {
                            if (MinSize == -1)
                            {
                                MinSize = Trace.Length + 1;
                                MinTrace = new int[MinSize];
                                Trace.CopyTo(MinTrace, 0);
                                MinTrace[MinSize - 1] = vTo;
                            }
                            if ((MinSize > 0) & (Trace.Length < MinSize))
                            {
                                MinSize = Trace.Length + 1;
                                MinTrace = new int[MinSize];
                                Trace.CopyTo(MinTrace, 0);
                                MinTrace[MinSize - 1] = vTo;
                            }

                        }
                    start = vTo;
                    continue;

                }
                stack.push(vert);
                start = -1;
                M[vert] = 1;
            }
            return MinTrace;

        }
        abstract public int NextVertex(int v, int start, int[] M);
        abstract public bool CheckTrace(int[] T);
    };
    class Graph_ext : Graph
    {
        node[] List;

        public Graph_ext() : base() { }

        public Graph_ext(int n)
            : base(n)
        {
            List = new node[n];
        }

        public void FindMinCycle(int[] A, int[] B, int[] Ex)
        {
            string s2;

            bool flag2 = false;
            int vert;
            int[] M;
            string Bool = "";
            string[] Bool2 = new string[100];
            bool flag = true; // если false то не добавляем элемент в массив массивов
            M = new int[s];
            int inde = 0; // индекс Trace2 для добавления в него Trace
            int[] Trace;
            string s4 = "";
            string s5 = "";

            countEccentricities();
            MessageBox.Show("диаметр графа: " + diameter);
            MessageBox.Show("радиус графа: " + center);

            int xx = 0;
            while (xx < s)
            {
                //MessageBox.Show("эксентриситет " + xx + ": " + eccentricity[xx]);
                xx += 1;
            }

            int ij = 0;
            int f = 0;

            if (center == diameter)
            {
                Ex[0] = -1;
            }
            else
            {
                while (ij < s)
                {
                    if (eccentricity[ij] == center)   // if(eccentricity[C[i]] == center)
                    {
                        Ex[f] = ij;
                        f += 1;
                    }
                    ij += 1;
                }
            }


            Stack st = new Stack();
            for (int k = 0; k < s; k++)
            {
                st.push(k);
                int start = -1;
                for (int i = 0; i < s; i++)
                {
                    M[i] = 0;
                }
                M[k] = s + 1;
                while (!st.IsEmpty())
                {
                    vert = NextVertex2(st.top(), start, M);
                    if (vert == -1)
                    {
                        start = st.pop();
                        M[start] = 0;
                        continue;
                    }
                    if (vert == k)
                    {
                        Trace = st.StackToVector(); // цикл


                        if (((MinCycleSize == -1 || Trace.Length < 10)) && CheckCycle2(Trace))
                        {
                            // добавить цикл Trace в массив циклов                         
                            MinCycleSize = Trace.Length + 1;
                            MinCycle = new int[MinCycleSize];
                            Trace.CopyTo(MinCycle, 0);
                            MinCycle[MinCycleSize - 1] = k;

                            // функция которая не добавляет в массив массивов одинаковые циклы
                            Bool = "";
                            flag = true;
                            //MessageBox.Show("gg");
                            for (int g = 0; g < 10; g++)
                            {
                                flag2 = false;
                                foreach (int gg in MinCycle) // Trace                             
                                {
                                    if (gg == g)
                                    {
                                        Bool = Bool + "1";
                                        flag2 = true;
                                        break;
                                    }
                                }
                                if (flag2 == false)
                                {
                                    Bool = Bool + "0";
                                }
                            }

                            s5 = "";
                            foreach (int kk in MinCycle)
                            {
                                s4 = kk.ToString();
                                s5 = s5 + s4;
                            }


                            if (inde > 0)
                            {
                                foreach (string bb in Bool2)
                                {
                                    if (bb == Bool)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                            }
                            if (flag == true)
                            {
                                Bool2[inde] = Bool;
                                for (int h = 0; h < MinCycleSize; h++)
                                {
                                    Trace22[inde, h] = MinCycle[h];
                                }
                                inde += 1;
                                s2 = inde.ToString();
                            }

                        }
                        if (MinCycleSize != -1)
                        {
                            Trace.CopyTo(B, 0); //
                            B[MinCycleSize - 1] = k; //
                        }

                        start = k;
                        continue;
                    }
                    M[vert] = st.top();
                    st.push(vert);
                    start = -1;
                }
            }
            s2 = inde.ToString();

        }

        void countEccentricities()
        {
            diameter = 0;
            center = 20;
            for (int i = 0; i < s; i++) // s - число вершин(вместо nodeCount)
            {
                eccentricity[i] = countEccentricity(i);
                if (eccentricity[i] > diameter)
                {
                    diameter = eccentricity[i];
                }
                if (eccentricity[i] < center)
                {
                    center = eccentricity[i];
                }
            }
        }

        int countEccentricity(int in2)
        {
            int v, j, f, r;
            int[] m = new int[s];
            int[] q = new int[s];
            for (int jj = 0; jj < s; jj++)
            {
                m[jj] = 0;
            }
            f = -1;
            r = 0;
            q[r] = in2;
            m[in2] = 1;

            while (f != r)
            {
                f += 1;
                v = q[f];
                for (j = 0; j < s; j++)
                {
                    if (existsArc(v, j) != 0)
                    {
                        if (m[j] == 0)
                        {
                            r += 1;
                            q[r] = j;
                            m[j] = m[v] + 1;
                        }
                    }
                }
            }
            v = q[r];
            return m[v] - 1;
        }

        int existsArc(int a, int b)
        {
            //MessageBox.Show("existsArc ");
            node[] List2 = new node[s];
            Array.Copy(List, List2, List.Length); // копирование из массива List в массив List2

            while (List2[a] != null)
            {
                if (List2[a].vert == b)
                    return 1;
                else
                    List2[a] = List2[a].next;
            }
            return 0;

        }



        int NextVertex2(int v, int start, int[] M)
        {
            node temp;
            temp = List[v];
            if (temp == null)
                return -1;
            if (start != -1)
                while (temp != null)
                {
                    if (temp.vert == start)
                    {
                        temp = temp.next;
                        break;
                    }
                    else
                        temp = temp.next;
                }
            while (temp != null)
            {
                if ((M[temp.vert] == 0 || M[temp.vert] == s + 1) && M[v] != temp.vert)
                    return temp.vert;
                else
                    temp = temp.next;
            }
            return -1;
        }

        public void Edge(int i, int j)
        {
            node temp = new node();
            temp.vert = j;
            temp.next = null;
            node t = List[i];
            if (t == null)
                List[i] = temp;
            else
            {
                if (t.vert == j)
                    return;
                while (t.next != null)
                {
                    if (t.vert == j)
                        return;
                    t = t.next;
                }
                if (t.vert == j)
                    return;
                t.next = temp;
            }
            temp = new node();
            temp.vert = i;
            temp.next = null;
            t = List[j];
            if (t == null)
                List[j] = temp;
            else
            {
                while (t.next != null)
                    t = t.next;
                t.next = temp;
            }
        }

        public void DelEdge(int i, int j)
        {
            node t = List[i];
            node temp;
            if (t == null)
                return;
            if (t.vert == j)
            {
                List[i] = t.next;
            }
            else
            {
                while (t.next != null)
                {
                    if (t.next.vert == j)
                    {
                        temp = t.next;
                        t.next = t.next.next;
                        break;
                    }
                    t = t.next;
                }
            }

            t = List[j];
            if (t == null)
                return;
            if (t.vert == i)
            {
                List[j] = t.next;
            }
            else
            {
                while (t.next != null)
                {
                    if (t.next.vert == i)
                    {
                        temp = t.next;
                        t.next = t.next.next;
                        break;
                    }
                    t = t.next;
                }
            }
        }

        public override int NextVertex(int v, int start, int[] M)
        {
            node temp;
            temp = List[v];
            if (temp == null)
                return -1;
            if (start != -1)
                while (temp != null)
                {
                    if (temp.vert == start)
                    {
                        temp = temp.next;
                        break;
                    }
                    else
                        temp = temp.next;
                }
            while (temp != null)
            {
                if (M[temp.vert] == 0)
                    return temp.vert;
                else
                    temp = temp.next;
            }
            return -1;
        }

        bool CheckCycle2(int[] C)
        {
            int i = 0;
            int f = 0;

            int n = C.Length; // длина цикла(число вершин)

            if (center == diameter)
            {
                return true;
            }

            while (i < n)  // (i < n)
            {
                if (eccentricity[C[i]] == center)
                {
                    if (f < 2)
                    {
                        f++;
                    }
                    else
                    {
                        return false;
                    }
                }
                i += 1;
            }
            return true;
        }



        public override bool CheckTrace(int[] T)
        {
            string s3;
            int j2 = 0;
            foreach (int j3 in T)
            {
                j2 += 1;
            }
            s3 = j2.ToString();

            return true;
        }

        public int[] ListToVector(int i)
        {
            node t;
            int[] v = new int[0];
            int c = 0;
            t = List[i];
            while (t != null)
            {
                c++;
                t = t.next;
            }
            if (c == 0)
                return v;
            v = new int[c];
            t = List[i];
            c = 0;
            while (t != null)
            {
                v[c] = t.vert;
                t = t.next;
                c++;
            }
            return v;
        }

    }
}



