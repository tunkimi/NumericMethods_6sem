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


            Matrix alpha = new Matrix(N, N);
            Matrix beta = new Matrix(new double[N, 1]);

            #region Метод простых интераций

            for (int i = 0; i < N; i++)
            {                               
                beta[i, 0] = B[i] / A[i, i];                     //Определяем вектор Бета
                                                                 //свободный член разделить на диагональный элемент матрицы

                for (int j = 0; j < N; j++)                      //И матрицу Альфа
                {                                                //Элементы делятся на минус диагональный эдемент
                    if (i != j)                                  //А диагональный элемент обнуляется
                        alpha[i, j] = -A[i, j] / A[i, i];
                    else
                        alpha[i, j] = 0;
                }
            }
            double alpnorma = alpha.NormForRow();                   //высчитывается норма для определения сходимости СЛАУ
            Matrix x = null, xtemp = null;
            if (alpnorma >= 1)
            {
                Console.WriteLine("Не выполнено достаточное условие сходимости метода простых итераций");
            }
            else
            {
                x = new Matrix(beta);                               //берем любой вектор, от которого будем двигаться к ответу
                xtemp = beta + alpha * beta;                        //высчитываем следующий шаг

                bool usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / alpnorma;
                for (int i = 0; usl; i++)                                           //зацкливаем движение к ответу
                {
                    x = xtemp;
                    xtemp = beta + alpha * x;
                    usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / alpnorma;     //проверяем удовлетворение заданной точности
                }
                x = xtemp;
            }
            Console.WriteLine("\nМетод итераций Якоби: x:\n" + x);

            #endregion


            #region Метод Зейделя

            Matrix Bm = new Matrix(alpha);
            Matrix Cm = new Matrix(alpha);

            for (int i = 0; i < N; i++)             //Выражаем вспомогательные матрицы Bm и Cm из матрицы Альфа, вычисленной ранее
                for (int j = 0; j < N; j++)
                    if (i <= j)
                        Bm[i, j] = 0;               //Нижнетреугольная матрица (диагональ равна нулю)
                    else
                        Cm[i, j] = 0;               //Верхнетреугольная матрица


            Bm = Matrix.UnoMatrix(N) - Bm;
            Bm = Bm.Reverse();

            double cmnorma = Cm.Norm();


            if (alpnorma >= 1)
            {
                Console.WriteLine("Не выполнено достаточное условие сходимости метода простых итераций");
            }
            else
            {
                x = new Matrix(beta);
                xtemp = Bm * beta + Bm * Cm * x;                                    //Вычисляем следующий элемент с помощью новых матриц
                bool usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / cmnorma;
                for (int i = 0; usl; i++)                                           //Аналогично зацикливаем вычисление, пока не достигнем нужной точности
                {
                    x = xtemp;
                    xtemp = Bm * beta + Bm * Cm * x;
                    usl = (xtemp - x).Norm() > eps * (1 - alpnorma) / cmnorma;
                }
                x = xtemp;
            }
            Console.WriteLine("\nМетод итераций Зейделя: x:\n" + x);



            #endregion

        }
    }
}
