using System;

namespace Lab_1_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n;
            Matrix A = new Matrix(new double[,] {{ 1,  2, -1, -7 },
                                                 { 2, -3,  7,  1 },
                                                 { 8,  0, -9, -3 },
                                                 { 1, -5, -6,  8 }});
            n = A.GetnRows;
            Matrix b = new Matrix(new double[,] {   { -23},
                                                    {  -7},
                                                    {  39},
                                                    {  30}});


            Matrix Ak = new Matrix(A);


            //получаем матрицы

            Matrix L = Matrix.UnoMatrix(n);
            Matrix U = new Matrix(n, n);

            for (int k = 0; k < n - 1; k++)
            {
                Matrix m = new Matrix(n, n);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++) 
                    {
                        if (i == j)
                            m[i, j] = 1;
                        else if (j != k || i < j)
                        {
                            m[i, j] = 0;
                        }
                        else
                        {
                            m[i, j] = -Ak[i, k] / Ak[k, k];
                        }
                    }
                Ak = m * Ak;
                L *= m.Reverse();
            }
            U = Ak;



            //проверка LU разложения
            Console.WriteLine("проверка LU разложения:");
            Console.WriteLine(L);
            Console.WriteLine("*");
            Console.WriteLine(U);
            Console.WriteLine("=");
            Console.WriteLine(L*U);




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
            Console.WriteLine();
            Console.WriteLine("x:\n" + x.ToString());

            Console.WriteLine();
            Console.WriteLine("det(A):");
            Console.WriteLine(Matrix.Determinant(A));

            Console.WriteLine();
            Console.WriteLine("A^-1");
            Console.WriteLine(A.Reverse());

            Console.WriteLine();
            Console.WriteLine("A*a^-1 = ");
            Console.WriteLine(A*A.Reverse());
        }
    }

}
