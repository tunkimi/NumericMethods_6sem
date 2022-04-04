using System;

namespace Lab_1_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Matrix A = new Matrix(new double[,]
            {   { 23,  -6,  -5},
                {  8,  22,  -2},
                {  7,  -6,  18},
            });
            Matrix Ak = new Matrix(A);
            double eps = 0.01;

            int N = 3;

            Matrix Uk = new Matrix(N, N);


            int itemp = 0, jtemp = 0;
            double aij = 0;
            double phik = 0;
            double tAk = 1;

            while(eps<=tAk)
            {
                for (int i = 0; i < N; i++)
                {
                    for (int j = 0; j < N; j++)
                    {
                        if (i != j && aij < Math.Abs(Ak[i, j]))
                        {
                            aij = Ak[i, j];
                            //aij = Math.Abs(Ak[i, j]);
                            itemp = i;
                            jtemp = j;
                        }
                    }
                }
                if (Ak[itemp, itemp] != Ak[jtemp, jtemp])
                {
                    phik = 0.5 * Math.Atan(2 * aij / (Ak[itemp, itemp] - Ak[jtemp, jtemp]));
                }
                else
                {
                    phik = Math.PI / 4;
                }

                for (int i = 0; i < N; i++)
                {
                    Uk[i, i] = 1;
                }
                Uk[itemp, itemp] = Uk[jtemp, jtemp] = Math.Cos(phik);
                Uk[jtemp, itemp] = Math.Sin(phik);
                Uk[itemp, jtemp] = -Uk[jtemp, itemp];

                Ak = Uk.Transponed() * Ak * Uk;


                for(int m = 0; m < N; m++)
                    for(int l = 0; l < N; l++)
                        if(m!=l)
                            tAk+= Ak[l, m]* Ak[l, m];
                tAk = Math.Sqrt(tAk);

                Console.WriteLine($"ak[{itemp},{jtemp}] = {aij}\tphi = {phik}");
                Console.WriteLine(Ak + "\n");

            }
            Matrix x = new Matrix(N, 1);
            for(int i=0;i<N;i++)
                x[i,0] = Ak[i,i];

            Console.WriteLine(x);

        }
    }
}
