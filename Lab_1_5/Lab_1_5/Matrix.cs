﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_5
{
    public class Matrix
    {
        private int nRows;
        private int nCols;
        private double[,] matrix;

        //только для квадратных
        //а больше в лабе и не надо
        public Matrix(string str)
        {
            string[] strArr = str.Split('\n');
            double[][] preMatrix = new double[strArr.Length][];
            nRows = strArr.Length;
            for (int i = 0; i < nRows; i++)
            {
                preMatrix[i] = strArr[i].Split(' ').Select(double.Parse).ToArray();
            }
            nCols = preMatrix[0].Length;
            matrix = new double[nRows, nCols];
            for (int i = 0; i < nRows; i++)
                for (int j = 0; j < nCols; j++)
                    matrix[i, j] = preMatrix[i][j];

        }

        public Matrix(int nRows, int nCols)
        {
            this.nRows = nRows;
            this.nCols = nCols;
            this.matrix = new double[nRows, nCols];
            for (int i = 0; i < nRows; i++)
            {
                for (int j = 0; j < nCols; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }

        public Matrix(double[,] matrix)
        {
            this.nRows = matrix.GetLength(0);
            this.nCols = matrix.GetLength(1);
            this.matrix = matrix;
        }

        public Matrix(Matrix m)
        {
            nRows = m.GetnRows;
            nCols = m.GetnCols;
            this.matrix = new double[nRows,nCols];
            for(int i = 0; i < nRows; i++)
                for(int j = 0; j < nCols; j++)
                    this[i, j] = m[i, j];
        }

        public double this[int m, int n]
        {
            get
            {
                if (m < 0 || m > nRows)
                {
                    throw new Exception("m-th row is out of range!");
                }
                if (n < 0 || n > nCols)
                {
                    throw new Exception("n-th col is out of range!");
                }
                return matrix[m, n];
            }
            set { matrix[m, n] = value; }
        }

        public int GetnRows
        {
            get { return nRows; }
        }

        public int GetnCols
        {
            get { return nCols; }
        }

        public static Matrix UnoMatrix(int size)
        {
            Matrix m = new Matrix(size, size);
            for(int i=0; i < size; i++)
                m[i,i] = 1;
            return m;
        }

        public override string ToString()
        {
            string strMatrix = "";
            int i, j;
            for (i = 0; i < nRows - 1; i++)
            {
                for (j = 0; j < nCols - 1; j++)
                    strMatrix += Math.Round(matrix[i, j], 3).ToString() + "\t";
                strMatrix += Math.Round(matrix[i, j], 3).ToString() + "\n";
            }
            for (j = 0; j < nCols - 1; j++)
                strMatrix += Math.Round(matrix[i, j], 3).ToString() + "\t";
            strMatrix += Math.Round(matrix[i, j], 3).ToString();
            return strMatrix;
        }

        public override bool Equals(object obj)
        {
            return (obj is Matrix) && this.Equals((Matrix)obj);
        }

        public bool Equals(Matrix m)
        {
            if (!CompareDimension(this, m))
                return false;
            for (int i = 0; i < m.GetnRows; i++)
                for (int j = 0; j < m.GetnCols; j++)
                    if (this[i, j] != m[i, j])
                        return false;
            return true;
        }

        public override int GetHashCode()
        {
            return matrix.GetHashCode();
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (!Matrix.CompareDimension(m1, m2))
            {
                throw new Exception("The dimensions of two matrices must be the same!");
            }
            Matrix result = new Matrix(m1.GetnRows, m1.GetnCols);
            for (int i = 0; i < m1.GetnRows; i++)
            {
                for (int j = 0; j < m1.GetnCols; j++)
                {
                    result[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return result;
        }

        public static Matrix operator -(Matrix m)
        {
            for (int i = 0; i < m.GetnRows; i++)
            {
                for (int j = 0; j < m.GetnCols; j++)
                {
                    m[i, j] = -m[i, j];
                }
            }
            return m;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (!Matrix.CompareDimension(m1, m2))
            {
                throw new Exception("The dimensions of two matrices must be the same!");
            }
            Matrix result = new Matrix(m1.GetnRows, m1.GetnCols);
            for (int i = 0; i < m1.GetnRows; i++)
            {
                for (int j = 0; j < m1.GetnCols; j++)
                {
                    result[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return result;
        }

        public static Matrix operator *(Matrix m, double d)
        {
            Matrix result = new Matrix(m.GetnRows, m.GetnCols);
            for (int i = 0; i < m.GetnRows; i++)
            {
                for (int j = 0; j < m.GetnCols; j++)
                {
                    result[i, j] = m[i, j] * d;
                }
            }
            return result;
        }

        public static Matrix operator *(double d, Matrix m)
        {
            Matrix result = new Matrix(m.GetnRows, m.GetnCols);
            for (int i = 0; i < m.GetnRows; i++)
            {
                for (int j = 0; j < m.GetnCols; j++)
                {
                    result[i, j] = m[i, j] * d;
                }
            }
            return result;
        }

        public static Matrix operator /(Matrix m, double d)
        {
            Matrix result = new Matrix(m.GetnRows, m.GetnCols);
            for (int i = 0; i < m.GetnRows; i++)
            {
                for (int j = 0; j < m.GetnCols; j++)
                {
                    result[i, j] = m[i, j] / d;
                }
            }
            return result;
        }
        public static double operator /(double a, Matrix m)     //????????
        {
            if (m.GetnRows == 1 && m.GetnCols == 1)
                return a / m[0, 0];
            else
                throw new ArgumentException();
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.GetnCols != m2.GetnRows)
            {
                throw new Exception("The numbers of columns of the" +
                 " first matrix must be equal to the number of " +
                 " rows of the second matrix!");
            }
            Matrix result = new Matrix(m1.GetnRows, m2.GetnCols);
            for (int i = 0; i < m1.GetnRows; i++)
            {
                for (int j = 0; j < m2.GetnCols; j++)
                {
                    for (int k = 0; k < m1.GetnCols; k++)
                    {
                        result[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return result;
        }

        public bool IsSquared()
        {
            if (nRows == nCols)
                return true;
            else
                return false;
        }

        public static bool CompareDimension(Matrix m1, Matrix m2)
        {
            if (m1.GetnRows == m2.GetnRows && m1.GetnCols == m2.GetnCols)
                return true;
            else
                return false;
        }

        public static double Determinant(Matrix mat)
        {
            double result = 0.0;
            if (!mat.IsSquared())
            {
                throw new Exception("The matrix must be squared!");
            }
            if (mat.GetnRows == 1)
                result = mat[0, 0];
            else
            {
                for (int i = 0; i < mat.GetnRows; i++)
                {
                    result += Math.Pow(-1, i) * mat[0, i] * Determinant(Matrix.Minor(mat, 0, i));
                }
            }
            return result;
        }

        public static Matrix Minor(Matrix mat, int row, int col)
        {
            Matrix mm = new Matrix(mat.GetnRows - 1, mat.GetnCols - 1);
            int ii = 0, jj = 0;
            for (int i = 0; i < mat.GetnRows; i++)
            {
                if (i == row)
                    continue;
                jj = 0;
                for (int j = 0; j < mat.GetnCols; j++)
                {
                    if (j == col)
                        continue;
                    mm[ii, jj] = mat[i, j];
                    jj++;
                }
                ii++;
            }
            return mm;
        }

        public Matrix Transponed()
        {
            Matrix res = new Matrix(nCols, nRows);
            for (int i = 0; i < this.nRows; i++)
                for (int j = 0; j < this.nCols; j++)
                    res[j, i] = this[i, j];
            return res;
        }

        public Matrix Reverse()
        {
            Matrix algAdd = new Matrix(nRows, nCols);
            for (int i = 0; i < nRows; i++)
                for (int j = 0; j < nCols; j++)
                    algAdd[i, j] = Math.Pow(-1, i + j) * Determinant(Matrix.Minor(this, i, j));
            Matrix algAddTr = algAdd.Transponed();
            return (algAddTr / Determinant(this));
        }

        public Matrix ToLowerTriangle()
        {
            Matrix res = new Matrix(this);
            double coef;
            Matrix[] strs = new Matrix[this.nRows];
            for (int i = 0; i < this.nRows; i++)
                strs[i] = new Matrix(1, this.nCols);
            for (int i = 0; i < this.nRows; i++)
                for (int j = 0; j < this.nCols; j++)
                    strs[i][0, j] = this[i, j];
            for (int i = Math.Min(this.nRows, this.nCols)-1; i >-1; i--)
            {
                for (int k = i - 1; k >-1; k--)
                {
                    coef = strs[k][0, i] / strs[i][0, i];
                    if (coef == 0) continue;
                    strs[k] -= strs[i] * coef;
                }
            }
            for (int i = 0; i < this.nRows; i++)
            {
                strs[i] /= strs[i][0, i];
                for (int j = 0; j < this.nCols; j++)
                    res[i, j] = strs[i][0, j];
            }

            return res;
        }
        public Matrix ToUpperTriangle()
        {
            Matrix res = new Matrix(this);
            double coef;
            Matrix[] strs = new Matrix[this.nRows];
            for (int i = 0; i < this.nRows; i++)
                strs[i] = new Matrix(1, this.nCols);
            for (int i = 0; i < this.nRows; i++)
                for (int j = 0; j < this.nCols; j++)
                    strs[i][0, j] = this[i, j];
            for (int i = 0; i < Math.Min(this.nRows, this.nCols); i++)
            {
                for (int k = i + 1; k < this.nRows; k++)
                {
                    coef = strs[k][0, i] / strs[i][0, i];
                    if (coef == 0) continue;
                    strs[k] -= strs[i] * coef;
                }
            }
            for (int i = 0; i < this.nRows; i++)
            {
                strs[i] /= strs[i][0, i];
                for (int j = 0; j < this.nCols; j++)
                    res[i, j] = strs[i][0, j];
            }

            return res;
        }
        public Matrix UTriangle()
        {
            Matrix res = new Matrix(this);
            int n = this.GetnRows;
            double coef;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    coef = res[j, i] / res[i, i];
                    for (int k = 0; k < n; k++)
                        res[j, k] -= coef * res[i, k];
                }
            }
            return res;
        }
        public Matrix LTriangle()
        {
            Matrix res = new Matrix(this);
            int n = this.GetnRows;
            double coef;
            for (int i = n-1; i > -1; i--)
            {
                for (int j = i - 1; j > -1; j--)
                {
                    coef = res[j, i] / res[i, i];
                    for (int k = 0; k < n; k++)
                        res[j, k] -= coef * res[i, k];
                }
            }
            for(int i=0;i < n; i++)
                for(int j=0; j < n; j++)
                    res[j, i] /= res[i, i];
            return res;
        }
        public double DiagonalMultiplication()
        {
            double res = 1;
            for (int i = 0; i < this.GetnCols; i++)
                res *= this[i, i];
            return res;
        }
        public double NormForRow()
        {
            double norma = 0, temp;
            for (int i = 0; i < this.GetnRows; i++)
            {
                temp = 0;
                for (int j = 0; j < this.GetnCols; j++)
                {
                    temp += (this[i, j] > 0) ? this[i, j] : (-this[i, j]);
                }
                if (temp > norma)
                    norma = temp;
            }
            return norma;
        }
        public double NormForCol()
        {
            double norma = 0, temp;
            for (int i = 0; i < this.GetnCols; i++)
            {
                temp = 0;
                for (int j = 0; j < this.GetnRows; j++)
                {
                    temp += (this[j, i] > 0) ? this[j, i] : (-this[j, i]);
                }
                if (temp > norma)
                    norma = temp;
            }
            return norma;
        }
        public double NormForElements()
        {
            double norma = 0;
            for (int i = 0; i < this.GetnRows; i++)
            {
                for (int j = 0; j < this.GetnCols; j++)
                {
                    norma += this[i, j] * this[i, j];
                }
            }
            return Math.Sqrt(norma);
        }
        public double Norm()
        {
            double r = NormForRow();
            double c = NormForCol();
            double e = NormForElements();
            double temp = (c < r) ? c : r;
            return (temp<e)?temp:e;
        }


        public static void QRDecompose(Matrix A, out Matrix Q, out Matrix R)
        {
            if (!A.IsSquared())
                throw new Exception();
            int N = A.GetnRows;
            Matrix At = new Matrix(A);
            Q = Matrix.UnoMatrix(N);
            for (int i = 0; i < N - 1; i++)
            {
                Matrix v = new Matrix(N, 1);

                for (int j = 0; j < i; j++)
                    v[j, 0] = 0;
                double tsum = 0;
                for (int j = i; j < N; j++)
                    tsum += At[j, i] * At[j, i];
                v[i, 0] = At[i, i] + Math.Sign(At[i, i]) * Math.Sqrt(tsum);
                for (int j = i + 1; j < N; j++)
                    v[j, 0] = At[j, i];
                Matrix Ht = Matrix.UnoMatrix(N) - 2 / (v.Transponed() * v) * v * v.Transponed();
                Q *= Ht;
                At = Ht * At;
            }
            R = new Matrix(At);
        }
        private bool AlmZer(double a, double eps)
        {
            return Math.Abs(a) <= eps;
        }
        private bool isFull(double eps)
        {
            if (Math.Abs(this[GetnRows - 1, 0]) > eps)
                return true;
            else
                return false;
        }
        public bool isQuaziTriangle(double eps, out List<int> jumps)
        {
            jumps = new List<int>();
            if (!this.IsSquared() || isFull(eps))
            {
                return false;
            }
            else
            {
                List<Matrix> list = new List<Matrix>();
                for (int j = 0; j < this.GetnCols; j++)
                {
                    Matrix tmp = new Matrix(this.GetnRows, 1);
                    for (int i = 0; i < this.GetnRows; i++)
                        tmp[i, 0] = AlmZer(this[i, j], eps) ? 0 : this[i, j];
                    list.Add(tmp);
                }

                foreach (Matrix v in list)
                {
                    bool zer = false;
                    for (int i = 0; i < this.GetnRows; i++)
                    {
                        if (v[i, 0] == 0)
                            zer = true;
                        if (zer && v[i, 0] != 0)
                            return false;
                    }
                }
                int[] h = new int[this.GetnCols];
                for (int j = 0; j < list.Count; j++)
                {
                    int tmp = 0;
                    for (int i = 0; i < this.GetnRows; i++)
                        if (list[j][i, 0] != 0)
                            tmp++;
                        else
                            break;
                    h[j] = tmp - j - 1;

                    //Console.WriteLine(h[j]);
                }




                for (int j = 0; j < h.Length; j++)
                {
                    if (h[j] != 0)
                        for (int i = 0; i < h[j]; i++)
                        {
                            if (h[i + j] != h[i + j + 1] + 1)
                                return false;
                        }
                }

                bool zerod = true;
                if (h[0] != 0) {
                    jumps.Add(0);
                    zerod = false; 
                }
                for(int i = 1; i < h.Length; i++)
                {
                    if (!zerod && h[i] != 0)
                        continue;
                    else
                        zerod = true;
                    if (h[i] != 0 && h[i - 1] == 0)
                        jumps.Add(i);
                }

                return true;

            }
        }
    }
}