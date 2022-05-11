using System;

namespace Lab_1_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(new double[,]
            {   {  6,  -5,   0,   0,   0},
                { -6,  16,   9,   0,   0},
                {  0,   9, -17,  -3,   0},
                {  0,   0,   8,  22,  -8},
                {  0,   0,   0,   6, -13} 
            });
            int N = A.GetnRows;

            double[] d = new double[] { -58, 161, -114, -90, -55};


            double[] P = new double[N];
            double[] Q = new double[N];



            double[] a = new double[N];
            double[] b = new double[N];
            double[] c = new double[N];
            double[] x = new double[N];


            b[0] = A[0, 0];
            c[0] = A[0, 1];
            a[4] = A[N-1, N-2];
            b[4] = A[N-1, N-1];
            for(int i = 1; i < N-1; i++)
            {
                a[i] = A[i, i - 1];
                b[i] = A[i, i];
                c[i] = A[i, i + 1];
            }



            P[0] = -c[0] / b[0];
            Q[0] = d[0] / b[0];
            for(int i=1; i<N-1; i++)
            {
                P[i] = -c[i] / (b[i] + a[i] * P[i-1]);
                Q[i] = (d[i] - a[i] * Q[i-1]) / (b[i] + a[i] * P[i-1]);
            }
            Q[N-1] = (d[N - 1] - a[N - 1] * Q[N - 2]) / (b[N - 1] + a[N - 1] * P[N - 2]);





            x[N-1] = Q[N-1];
            for(int i = N - 2; i > -1; i--)
            {
                x[i] = P[i]*x[i+1]+Q[i];
            }

            Console.WriteLine("\n\n");
            for (int i = 0; i < N; i++)
            {
                Console.WriteLine($"x[{i}] = " + x[i]);
            }

        }
    }
}
