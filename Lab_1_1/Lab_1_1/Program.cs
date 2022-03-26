using System;

namespace Lab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n;
            Matrix A = new Matrix(new double[,] {{ 1,  2, -1, -7 },
                                                 { 8,  0, -9, -3 },
                                                 { 2, -3,  7,  1 },
                                                 { 1, -5, -6,  8 }});
            n = A.GetnRows;
            Matrix b = new Matrix(new double[,] {   { -23},
                                                    {  39},
                                                    {  -7},
                                                    {  30}});


            //получаем матрицы

            Matrix U = new Matrix(A);
            Matrix L = new Matrix(n,n);

            for (int i = 0; i < n; i++)
                for (int j = i; j < n; j++)
                    L[j,i] = U[j,i] / U[i,i];

            for (int k = 1; k < n; k++)
            {
                for (int i = k - 1; i < n; i++)
                    for (int j = i; j < n; j++)
                        L[j, i] = U[j, i] / U[i, i];

                for (int i = k; i < n; i++)
                    for (int j = k - 1; j < n; j++)
                        U[i,j] = U[i,j] - L[i,k - 1] * U[k - 1,j];
            }

            //первый этап

            Matrix z = new Matrix(n, 1);
            z[0, 0] = b[0, 0];
            for (int i = 1; i < n; i++)
            {
                double sum = 0;
                for(int j = 0; j <= i-1; j++)
                {
                    sum += L[i, j] * z[j, 0];
                }
                z[i, 0] = b[i, 0] - sum;
            }

            //второй этап

            Matrix x = new Matrix(n, 1);
            x[n - 1, 0] = z[n - 1, 0] / U[n - 1, n - 1];
            for (int i = n - 1; i > -1; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                {
                    sum += U[i, j] * x[j, 0];
                }
                x[i, 0] = 1 / U[i, i] * (z[i, 0] - sum);

            }

            //вывод
            Console.WriteLine("x:\n" + x.ToString());
            Console.WriteLine("\ndet(A) = " + Matrix.Determinant(A));
            Console.WriteLine("\nA^-1:\n" + A.Reverse());
        }
    }

}
