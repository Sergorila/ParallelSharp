using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParallelSharp
{
    class My_Class
    {
        public double My_Task(double x)
        {
            double S = 0;
            for (int k = 1; k <= 20; k++)
            {
                for (int j = 1; j <= 20; j++)
                {
                    S += x*x*j*Math.Cos((k+j)*x)/(x*x+k*k*k+j*j);
                }
            }
            return S;
        }
    };

    class Program
    {
        const int N = 200000;

        static void Main(string[] args)
        {
            Task4();
        }

        static void Task1()
        {
            My_Class Obj = new My_Class();
            double[] V = new double[N];
            Int64 Tms = (DateTime.Now).Ticks;
            for (int k = 0; k < N; k++)
            {
                V[k] = Obj.My_Task(100 * Math.Cos(k));
            }
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательного цикла " + (Tmss.TotalSeconds).ToString() + " c");
            Tms = (DateTime.Now).Ticks;
            System.Threading.Tasks.Parallel.For(0, N, k => { V[k] = Obj.My_Task(k); });
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного цикла " + (Tmss.TotalSeconds).ToString() + " с");
        }

        static void Task2()
        {
            My_Class fobj = new My_Class();
            double[] X = new double[N + 1];
            for (int k = 0; k <= N; k++)
            {
                X[k] = 100 * Math.Cos(1.0 * k);
            }
            double S = 0;
            long Tms = (DateTime.Now).Ticks;
            foreach (double x in X) S += fobj.My_Task(x);
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach " + (Tmss.TotalSeconds).ToString() + " c");

            object obj = new();
            S = 0;
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X,
                 x => {
                     double Tmp = fobj.My_Task(x);
                     lock (obj) { S += Tmp; }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach " + (Tmss.TotalSeconds).ToString() + " c");
        }

        static void Task3()
        {
            My_Class fobj = new My_Class();
            List<double> X = new List<double>();
            for (int k = 0; k <= N; k++)
            {
                X.Add(100 * Math.Cos(1.0 * k));
            }
            double S = 0;
            Int64 Tms = (DateTime.Now).Ticks;
            foreach (double x in X) S += fobj.My_Task(x);
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach List " + (Tmss.TotalSeconds).ToString() + " c");

            object obj = new Object();
            S = 0;
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X,
                 x => {
                     double Tmp = fobj.My_Task(x);
                     lock (obj) { S += Tmp; }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach List " + (Tmss.TotalSeconds).ToString() + " c");


            Stack<double> X1 = new Stack<double>();
            for (int k = 0; k <= N; k++)
            {
                X1.Push(100 * Math.Cos(1.0 * k));
            }
            S = 0;
            Tms = (DateTime.Now).Ticks;
            foreach (double x in X1) S += fobj.My_Task(x);
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Stack " + (Tmss.TotalSeconds).ToString() + " c");

            obj = new Object();
            S = 0;
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X1,
                 x => {
                     double Tmp = fobj.My_Task(x);
                     lock (obj) { S += Tmp; }
                 }
                             );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Stack " + (Tmss.TotalSeconds).ToString() + " c");

            Queue<double> X2 = new Queue<double>();
            for (int k = 0; k <= N; k++)
            {
                X2.Enqueue(100 * Math.Cos(1.0 * k));
            }
            S = 0;
            Tms = (DateTime.Now).Ticks;
            foreach (double x in X2) S += fobj.My_Task(x);
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Queue " + (Tmss.TotalSeconds).ToString() + " c");

            obj = new Object();
            S = 0;
            Tms = (DateTime.Now).Ticks;
            System.Threading.Tasks.Parallel.ForEach(X2,
                 x => {
                     double Tmp = fobj.My_Task(x);
                     lock (obj) { S += Tmp; }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Queue " + (Tmss.TotalSeconds).ToString() + " c");
        }

        static void Task4()
        {
            My_Class fobj = new My_Class();
            List<double> X = new List<double>();
            List<double> Y = new List<double>();

            for (int k = 0; k <= N; k++)
            {
                X.Add(100 * Math.Cos(1.0 * k));
            }
            Int64 Tms = (DateTime.Now).Ticks;
            foreach (double x in X)
            {
                Y.Add(fobj.My_Task(x));
            }
            Tms = (DateTime.Now).Ticks - Tms;
            TimeSpan Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach List " + (Tmss.TotalSeconds).ToString() + " c");

            Y.Clear();
            Object obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X,
                 x =>
                 {
                     lock (obj)
                     {
                         Y.Add(fobj.My_Task(x));
                     }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach List " + (Tmss.TotalSeconds).ToString() + " c");


            Stack<double> X1 = new Stack<double>();
            List<double> Y1 = new List<double>();
            for (int k = 0; k <= N; k++)
            {
                X1.Push(100 * Math.Cos(1.0 * k));
            }
            Tms = (DateTime.Now).Ticks;
            foreach (double x in X1)
            {
                Y1.Add(fobj.My_Task(x));
            }
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Stack " + (Tmss.TotalSeconds).ToString() + " c");

            Y1.Clear();
            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X1,
                 x =>
                 {
                     lock (obj)
                     {
                         Y1.Add(fobj.My_Task(x));
                     }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Stack " + (Tmss.TotalSeconds).ToString() + " c");

            Queue<double> X2 = new Queue<double>();
            List<double> Y2 = new List<double>();
            for (int k = 0; k <= N; k++)
            {
                X2.Enqueue(100 * Math.Cos(1.0 * k));
            }
            Tms = (DateTime.Now).Ticks;
            foreach (double x in X2)
            {
                Y2.Add(fobj.My_Task(x));
            }
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach Queue " + (Tmss.TotalSeconds).ToString() + " c");

            Y2.Clear();
            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X2,
                 x =>
                 {
                     lock (obj)
                     {
                         Y2.Add(fobj.My_Task(x));
                     }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach Queue " + (Tmss.TotalSeconds).ToString() + " c");

            double[] X3 = new double[N + 1];
            List<double> Y3 = new List<double>();
            for (int k = 0; k <= N; k++)
            {
                X3[k] = 100 * Math.Cos(1.0 * k);
            }
            Tms = (DateTime.Now).Ticks;
            foreach (double x in X3)
            {
                Y3.Add(fobj.My_Task(x));
            }
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения последовательной операции foreach " + (Tmss.TotalSeconds).ToString() + " c");

            Y3.Clear();
            obj = new Object();
            Tms = (DateTime.Now).Ticks;
            Parallel.ForEach(X3,
                 x =>
                 {
                     double Tmp = fobj.My_Task(x);
                     lock (obj)
                     {
                         Y3.Add(fobj.My_Task(x));
                     }
                 }
            );
            Tms = (DateTime.Now).Ticks - Tms;
            Tmss = new TimeSpan(Tms);
            Console.WriteLine("Время выполнения параллельного метода ForEach " + (Tmss.TotalSeconds).ToString() + " c");
        }
    }
}
