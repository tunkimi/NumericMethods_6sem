using System;
using System.Collections.Generic;

namespace Lab_1_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int N = 3;

            const double eps = 0.01;

            //Matrix A = new Matrix(new double[,]
            //{   {  8, -1, -3},
            //    { -5,  9, -8},
            //    {  4, -5,  7},
            //});
            Matrix A = (new Matrix(new double[,]
            {   {  1,  3,  1},
                {  1,  1,  4},
                {  4,  3,  1},
            }));
            Matrix At = new Matrix(A);

            Matrix Q = Matrix.UnoMatrix(N);
            Matrix R;


            List<int> jp = new List<int>();

            double cond = condition(At);
            int asd = 0;
            while (cond>eps)
            {
                Matrix.QRDecompose(At, out Q, out R);
                At = R * Q;
                cond = condition(At);
                bool a = At.isQuaziTriangle(eps, out jp);
                if (a)
                    break;
            }

            Console.WriteLine(At + "\n");
            Console.WriteLine(At.isQuaziTriangle(eps, out jp));
            foreach(int i in jp)
                Console.WriteLine(i);

            //Console.WriteLine("At:\n" + At + "\n");
            //Console.WriteLine("QR:\n" + Q * R + "\n");





        }
        private static double condition(Matrix a)
        {
            double sum = 0;
            for (int m = 0; m < a.GetnCols; m++)
                for (int l = m + 1; l < a.GetnRows; l++)
                    sum += a[l, m] * a[l, m];
            return Math.Sqrt(sum);
        }
    }

}
