using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_3D
{
    public class Matrix4x4
    {
        private double[,] mNodes = new double[4, 4];

        public double this[int i, int j]
        {
            get
            {
                return mNodes[i - 1, j - 1];
            }

            set
            {
                mNodes[i - 1, j - 1] = value;
            }
        }

        public Matrix4x4 Mul(Matrix4x4 m)
        {
            Matrix4x4 matrix = new Matrix4x4();

            for (int w = 1; w <= 4; w++)
            {
                for (int h = 1; h <= 4; h++)
                {
                    for (int n = 1; n <= 4; n++)
                    {
                        matrix[w, h] += this[w, n] * m[n, h];
                    }
                }
            }

            return matrix;
        }

        public Vector4 Mul(Vector4 m)
        {
            return Mul(this, m);
        }

        /// <summary>
        /// 矩陣與向量相乘
        /// </summary>
        /// <param name="m"></param>
        /// <param name="v4"></param>
        /// <returns></returns>
        public static Vector4 Mul(Matrix4x4 m, Vector4 v4)
        {
            Vector4 rv4 = new Vector4();
            rv4.X = v4.X * m[1, 1] + v4.Y * m[2, 1] + v4.Z * m[3, 1] + v4.W * m[4, 1];
            rv4.Y = v4.X * m[1, 2] + v4.Y * m[2, 2] + v4.Z * m[3, 2] + v4.W * m[4, 2];
            rv4.Z = v4.X * m[1, 3] + v4.Y * m[2, 3] + v4.Z * m[3, 3] + v4.W * m[4, 3];
            rv4.W = v4.X * m[1, 4] + v4.Y * m[2, 4] + v4.Z * m[3, 4] + v4.W * m[4, 4];

            return rv4;
        }

        public static Matrix4x4 Plus(Matrix4x4 m1, Matrix4x4 m2)
        {
            Matrix4x4 m = new Matrix4x4();

            for (int i = 1; i < 5; i++)
            {
                for (int j = 1; j < 5; j++)
                {
                    m[i, j] = m1[i, j] + m2[i, j];
                }
            }

            return m;
        }

        public static Vector4 operator *(Matrix4x4 m1, Vector4 m2)
        {
            return Mul(m1, m2);
        }

        public static Matrix4x4 operator +(Matrix4x4 m1, Matrix4x4 m2)
        {
            return Plus(m1, m2);
        }

    }
}
