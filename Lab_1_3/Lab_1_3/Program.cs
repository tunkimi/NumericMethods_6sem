using System;

namespace Lab_1_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(new double[,]
            {   { 23,  -6,  -5,   9},
                {  8,  22,  -2,   5},
                {  7,  -6,  18,  -1},
                {  3,   5,   5, -19},
            });
            double[] B = new double[] { 232, -82, 202, -57 };
            double eps = 0;
            int N = 4;
            while(eps == 0)
            {
                Console.WriteLine("Введите желаемую точность измерений");
                double.TryParse(Console.ReadLine().Replace('.',','), out eps);
            }

            #region Метод простых интераций

            Matrix alpha = new Matrix(N,N);
            Matrix beta = new Matrix(new double[N,1]);
            for(int i = 0; i < N; i++)
            {
                beta[i, 0] = B[i] / A[i, i];
                for(int j = 0; j < N; j++)
                {
                    if (i != j)
                        alpha[i, j] = -A[i, j] / A[i, i];
                    else
                        alpha[i, j] = 0;
                }
            }
            Console.WriteLine(alpha);
            double alpnorma = alpha.NormForRow();
            double k = Math.Log(eps) - Math.Log(beta.NormForRow());
            k += Math.Log(1 - alpnorma);
            k /= Math.Log(alpnorma);
            Matrix x = null, xtemp = null;
            if (alpnorma >= 1)
            {
                Console.WriteLine("Не выполнено достаточное условие сходимости метода простых итераций");
            }
            else
            {
                x = new Matrix(beta);
                xtemp = beta + alpha * beta;
                bool usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / alpnorma;
                for (int i = 0; usl; i++)
                {
                    Console.WriteLine($"\nx[{i}]:\n" + x);
                    x = xtemp;
                    xtemp = beta + alpha * x;
                    usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / alpnorma;
                }
                x = xtemp;
            }
            Console.WriteLine("\nx:\n" + x);

            #endregion

        }
    }
}
