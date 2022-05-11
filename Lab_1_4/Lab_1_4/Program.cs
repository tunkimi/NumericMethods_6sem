using System;
using System.Collections.Generic;

namespace Lab_1_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(new double[,]
            {   {  9,  2, -7},
                {  2, -4, -1},
                { -7, -1,  1},
            });
            Matrix Ak = new Matrix(A);
            double eps = 0.01;

            int N = 3;

            Matrix Uk = new Matrix(N, N);
            Matrix finU = Matrix.UnoMatrix(N);


            int itemp = 0, jtemp = 0;
            double aij = 0;
            double phik = 0;
            double tAk = 1;
            //Console.WriteLine(A + "\n\n");

            while (eps<=tAk)
            {
                aij = 0;
                tAk = 0;
                for (int i = 0; i < N; i++)
                {
                    for (int j = i+1; j < N; j++)
                    {
                        if (Math.Abs(aij) < Math.Abs(Ak[i, j]))
                        {
                            aij = Ak[i, j];
                            itemp = i;
                            jtemp = j;
                        }
                    }
                }
                if (Ak[itemp, itemp] != Ak[jtemp, jtemp])
                {
                    phik = 0.5 * Math.Atan((2 * aij / (Ak[itemp, itemp] - Ak[jtemp, jtemp])));
                }
                else
                {
                    phik = Math.PI / 4;
                }
                Uk = Matrix.UnoMatrix(N);
                Uk[itemp, itemp] = Uk[jtemp, jtemp] = Math.Cos(phik);
                Uk[jtemp, itemp] = Math.Sin(phik);
                Uk[itemp, jtemp] = -Math.Sin(phik);

                finU *= Uk;

                Ak = Uk.Transponed() * Ak * Uk;


                for (int m = 0; m < N; m++)
                    for(int l = 0; l < m; l++)
                        if(m!=l)
                            tAk+= Ak[l, m]* Ak[l, m];
                tAk = Math.Sqrt(tAk);

                //Console.WriteLine($"ak[{itemp},{jtemp}] = {aij}\tphi = {phik}\n");
                //Console.WriteLine(Ak + "\n\n");
                //Console.WriteLine(tAk + "\n\n");

            }
            Matrix x = new Matrix(N, 1);
            for(int i=0;i<N;i++)
                x[i,0] = Ak[i,i];
            List<Matrix> list = new List<Matrix>();
            for (int j = 0; j < N; j++)
            {
                Matrix temp = new Matrix(N, 1);
                for (int i = 0; i < N; i++)
                {
                    temp[i,0] = Ak[i,j];
                }
                list.Add(temp);
            }




            Console.WriteLine("Собственные значения матрицы А:");
            Console.WriteLine(x);
            Console.WriteLine("Собственные векторы матрицы А:");
            foreach(Matrix m in list)
                Console.WriteLine(m+"\n");
        }
    }
}
