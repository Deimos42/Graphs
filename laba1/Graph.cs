using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using System.Windows.Forms; //

namespace Lab3_SppO
{

    class node
    {
        public int vert;
        public node next;
    };

    class Stack
    {
        node t;
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
            while(!stack.IsEmpty())  // возможно то же сделать для функции цикла 
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
                            if(MinSize == -1)
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
                            

                            //MessageBox.Show("134");
                            /*
                            for (int i = 0; i < MinSize; i++)
                            {
                                s4 = MinTrace[i].ToString();
                                MessageBox.Show(s4);
                            }
                            */

                            //return;
                        }
                    start = vTo;
                    continue;

                }
                stack.push(vert);
                start = -1;
                M[vert] = 1;
            }
            //FindMinCycle(Vert, Ed1, Ed2);
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

        public void FindMinCycle(int[] A, int[] B)  // public void FindMinCycle(int[] A, int[] B, int[,] Trace2) 

        {
            // не ищет циклы больше минимальной длины цикла
            // TODO: решить вопрос с добавлением массива в массив массивов чтобы можно было последовательно выводить каждый цикл
            // TODO: убрать повторяющиеся циклы
            string s2;
            //int Len = A.Length;  // число вершин в найденном пути

            bool flag2 = false;
            int vert;
            int[] M;
            string Bool = "";
            string[] Bool2 = new string[100];
            bool flag = true; // если false то не добавляем элемент в массив массивов
            M = new int[s];
            //int[,] Trace2 = new int[100,100];
            int inde = 0; // индекс Trace2 для добавления в него Trace
            int[] Trace;
            string s4 = "";
            string s5 = "";

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

                        // if ((MinCycleSize == -1 || Trace.Length < MinCycleSize) && CheckCycle(Trace,A))
                        // MinCycleSize заменить на число вершин чтобы он искал не только минимальные циклы

                        // if ((MinCycleSize == -1 || Trace.Length < MinCycleSize) && CheckCycle(Trace, A))   
                        if ((MinCycleSize == -1 || Trace.Length < 10) && CheckCycle(Trace, A))   // CheckCycle(Trace, Vert, Ed1, Ed2))
                        {
                            // добавить цикл Trace в массив циклов                         
                            MinCycleSize = Trace.Length + 1;
                            MinCycle = new int[MinCycleSize];
                            Trace.CopyTo(MinCycle, 0);
                            MinCycle[MinCycleSize - 1] = k;
                 
                            /*
                            for (int h = 0; h < MinCycleSize; h++)
                            {
                                Trace2[inde, h] = MinCycle[h];
                            }
                            inde += 1;
                            s2 = inde.ToString();
                            //MessageBox.Show("inde");
                            //MessageBox.Show(s2);
                            */
                            

                            
                            // функция которая не добавляет в массив массивов одинаковые циклы
                            Bool = "";
                            flag = true;                          
                            //MessageBox.Show("gg");
                            for (int g = 0; g < 10; g++)
                            {
                                flag2 = false;
                                foreach (int gg in MinCycle) // Trace                             
                                {
                                    //s2 = gg.ToString();                                   
                                    //MessageBox.Show(s2);
                                    if (gg == g)
                                    {
                                        Bool = Bool + "1";
                                        flag2 = true;
                                        break;
                                    }
                                }
                                if(flag2 == false)
                                {
                                    Bool = Bool + "0";
                                    //break;
                                }
                            }
                            //MessageBox.Show("Bool");
                            //MessageBox.Show(Bool);

                            s5 = "";
                            foreach (int kk in MinCycle)
                            {
                                //MessageBox.Show(Bool);
                                s4 = kk.ToString();
                                s5 = s5 + s4;
                            }
                            //MessageBox.Show("MinCycle");
                            //MessageBox.Show(s5);


                            if (inde > 0)
                            {
                                foreach(string bb in Bool2)
                                {
                                    //MessageBox.Show("bb");      
                                    //MessageBox.Show(bb);
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
                                //MessageBox.Show("inde");
                                //MessageBox.Show(s2);
                            }
                        
                        }                     
                            //B = new int[MinCycleSize];
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
            //MessageBox.Show("inde2");
            //MessageBox.Show(s2);
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

        bool CheckCycle(int[] C, int[] A)
        {
            // TODO: прописать чтобы циклы не имели общих ребер с путем(путь в массиве A)
            // проверить что обходит все циклы графа, изменить услоавия и в другой функции выдавать массив циклов а не один цикл
            string s2;
            int k2 = 0;
            int i;
            int j;
            int k = 0;
            int kk = 0;
            int star;
            int end;

            int n = C.Length; // длина цикла(число вершин)
            int m = A.Length; // число вершин в найденном пути
            bool flag1 = true;
            bool flag2 = true;

            //MessageBox.Show("12345");

            /*
            while (k2 < n)
            {
                s2 = C[k2].ToString();
                MessageBox.Show(s2);
                k2 += 1;
            }
            */

            j = A[0];
            while (k < m-1)
            {
                kk = 0;
                j = A[k];
                i = A[k + 1];
                while (kk < n-1)
                {
                    if((C[kk] == j)&(C[kk + 1] == i))
                    {
                        k2 = 0;
                        //MessageBox.Show("false1");
                        while (k2 < n)
                        {
                            s2 = C[k2].ToString();
                            //MessageBox.Show(s2);
                            k2 += 1;
                        }
                        flag1 = false;
                        flag2 = false;
                        break;
                    }
                    if ((C[kk] == i) & (C[kk + 1] == j))
                    {
                        k2 = 0;
                        //MessageBox.Show("false2");
                        while (k2 < n)
                        {
                            s2 = C[k2].ToString();
                            //MessageBox.Show(s2);
                            k2 += 1;
                        }
                        flag1 = false;
                        flag2 = false;
                        break;
                    }
                    
                    star = A[0];
                    end = A[m - 1];
                    /*
                    s2 = star.ToString();
                    MessageBox.Show("star");
                    MessageBox.Show(s2);
                    s2 = end.ToString();
                    MessageBox.Show("end");
                    MessageBox.Show(s2);
                    s2 = C[0].ToString();
                    MessageBox.Show("C[0]");
                    MessageBox.Show(s2);
                    s2 = C[n-1].ToString();
                    MessageBox.Show("C[n-1]");
                    MessageBox.Show(s2);
                    */
                    if (((C[0] == star)&(C[n-1] == end))|((C[0] == end) &(C[n-1] == star)))
                    {
                        flag1 = false;
                        flag2 = false;
                        break;
                    }
                     


                    /*
                    if((C[kk] == j)|(C[kk] == i))
                    {
                        flag1 = false;
                    }
                    if(flag1 == false)
                    {
                        if((C[kk] == i)|(C[kk] == j))
                        {
                            k2 = 0;
                            //MessageBox.Show("false3");
                            while (k2 < n)
                            {
                                s2 = C[k2].ToString();
                                //MessageBox.Show(s2);
                                k2 += 1;
                            }
                            flag2 = false;
                            break;
                        }
                    }
                    */
                    kk += 1;
                }
                k += 1;
            }

           
            if((flag1 | flag2) == true)
            {
                k2 = 0;
                while (k2 < n)
                {
                    s2 = C[k2].ToString();
                    //MessageBox.Show(s2);
                    k2 += 1;
                }
            }


            /*
            for (int i = 0; i < n; i++)
            {
                //s2 = C[i].ToString();
                //MessageBox.Show(s2);
                if (C[i] == Vert)
                {
                    flag1 = true;
                }
            }
            if (flag1 != true)
                return false;
            
            for (int i = 0; i < n; i++)
            {
                if (C[i] == Ed1)
                {
                    if (i > 0)
                        if (C[i - 1] == Ed2)
                            flag2 = true;
                    if (i < n - 1)
                        if (C[i + 1] == Ed2)
                            flag2 = true;
                }
            }
            */
            return flag1 | flag2;
          
            //return true;
            
        }
           
        public override bool CheckTrace(int[] T)
        {
            string s3;
            int j2 = 0;
            //MessageBox.Show("712");
            foreach (int j3 in T)
            {
                j2 += 1;
                //s3 = T[j2].ToString();
                //MessageBox.Show(s3);
            }
            s3 = j2.ToString();
            //MessageBox.Show(s3);
            /*
            if (MinCycleSize == -1)
                FindMinCycle(Vert, Ed1, Ed2);
            if (MinCycleSize == -1)
                MinCycleSize = -2;
            if (MinCycleSize == -2)
                return true;
            int n = T.Length; 
            */
            /*
            for (int i = 0; i < n; i++)
                for (int j = 0; j < MinCycleSize; j++)
                    if (T[i] == MinCycle[j])
                        return false;
            */

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

        public void FindAll(int n, int vn, int vk, int t, int[][] A)
        {
            int[][] RezMatr = new int[n][];
            int[] RezLen = new int[100];
            int[] St = new int[100];
            int[] M = new int[100];
            int v, i, j, L, Pr, Prt, ks;
            L = 0;
            int kolp = 0;
            for (j = 0; j < n; j++)
                M[j] = 0;
            ks = 0;
            St[ks] = vn;
            M[vn] = 1;
            while (ks >= 0)
            {
                Pr = 0;
                v = St[ks];
                for (j = L; j < n; j++)
                {
                    if (A[v][j] == 1)
                        if (j == vk)
                        {
                            Prt = 0;
                            for (i = 0; i <= ks; i++)
                                if (St[i] == t) Prt = 1;
                            if (Prt == 0)
                            {
                                for (i = 0; i <= ks; i++)
                                    RezMatr[kolp][i] = St[i];
                                RezMatr[kolp][ks + 1] = vk;
                                RezLen[kolp] = ks + 2;
                                kolp++;
                            }
                        }
                        else
                            if (M[j] == 0)
                        {
                            Pr = 1;
                            break;
                        }
                }
                if (Pr == 1)
                {
                    ks++;
                    St[ks] = j;
                    L = 0;
                    M[j] = 1;
                }
                else
                {
                    L = v + 1;
                    M[v] = 0;
                    ks--;
                }
            }          

        }
        /*
        internal void FindMinCycle(int[] a)
        {
            throw new NotImplementedException();
        }
        */
       
    }
}



