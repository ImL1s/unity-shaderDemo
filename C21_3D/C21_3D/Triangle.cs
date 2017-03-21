using C21_3D;
using System;
using System.Drawing;

namespace C_18Rotate
{
    public class Triangle3D
    {
        private Vector4 mA;

        private Vector4 mB;

        private Vector4 mC;

        private Vector4 mCurrentA;

        private Vector4 mCurrentB;

        private Vector4 mCurrentC;

        private PointF mCurrentA2D;

        private PointF mCurrentB2D;

        private PointF mCurrentC2D;

        private Pen mPen;

        public Vector4 A { get => mA; set => mA = value; }

        public Vector4 B { get => mB; set => mB = value; }

        public Vector4 C { get => mC; set => mC = value; }

        public Pen Pen
        {
            get
            {
                if (mPen == null)
                {
                    mPen = new Pen(Color.Black);
                }
                return mPen;
            }
            set => mPen = value;
        }

        public Triangle3D(Vector4 a, Vector4 b, Vector4 c)
        {
            A = mCurrentA = a;
            B = mCurrentB = b;
            C = mCurrentC = c;

            mCurrentA2D = new PointF(0, 0);
            mCurrentB2D = new PointF(0, 0);
            mCurrentC2D = new PointF(0, 0);
        }

        public void Transform(Matrix4x4 matrix)
        {
            mCurrentA = matrix.Mul(mA);
            mCurrentB = matrix.Mul(mB);
            mCurrentC = matrix.Mul(mC);
        }

        public static Matrix4x4 GetScaleMatrix(double scaleX, double scaleY, double scaleZ)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix[1, 1] = scaleX;
            matrix[2, 2] = scaleY;
            matrix[3, 3] = scaleZ;
            matrix[4, 4] = 1;

            return matrix;
        }

        public static Matrix4x4 GetRotateMatrix(float degreeX, float degreeY, float degreeZ)
        {
            float x = degreeX / 180f * (float)Math.PI;
            float y = degreeY / 180f * (float)Math.PI;
            float z = degreeZ / 180f * (float)Math.PI;

            Matrix4x4 matrixRotateX = new Matrix4x4();
            matrixRotateX[1, 1] = 1;
            matrixRotateX[1, 2] = 0;
            matrixRotateX[1, 3] = 0;
            matrixRotateX[2, 1] = 0;
            matrixRotateX[2, 2] = Math.Cos(x);
            matrixRotateX[2, 3] = Math.Sin(x);
            matrixRotateX[3, 1] = 0;
            matrixRotateX[3, 2] = -Math.Sin(x);
            matrixRotateX[3, 3] = Math.Cos(x);
            matrixRotateX[4, 4] = 1;

            return matrixRotateX;
        }

        public static Matrix4x4 GetCameraMatrix(float cameraZ)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
            mat[4, 3] = cameraZ;
            mat[4, 4] = 1;

            return mat;
        }

        public static Matrix4x4 GetPerspectiveMatrix(float z)
        {
            Matrix4x4 mat = new Matrix4x4();
            mat[1, 1] = 1;
            mat[2, 2] = 1;
            mat[3, 3] = 1;
            mat[3, 4] = 1.0 / z;

            return mat;
        }

        public void Scale(double scaleX, double scaleY, double scaleZ)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix[1, 1] = scaleX;
            matrix[2, 2] = scaleY;
            matrix[3, 3] = scaleZ;
            matrix[4, 4] = 1;

            mCurrentA = matrix.Mul(mA);
            mCurrentB = matrix.Mul(mB);
            mCurrentC = matrix.Mul(mC);
        }

        /// <summary>
        /// 使用初始的位置去計算旋轉矩陣，必須遞增/遞減傳入值才能旋轉.
        /// </summary>
        /// <param name="degree">角度(360)</param>
        public void Rotate(float degreeX, float degreeY, float degreeZ)
        {
            float x = degreeX / 180f * (float)Math.PI;
            Matrix4x4 matrixRotateX = new Matrix4x4();
            matrixRotateX[1, 1] = 1;
            matrixRotateX[1, 2] = 0;
            matrixRotateX[1, 3] = 0;
            matrixRotateX[2, 1] = 0;
            matrixRotateX[2, 2] = Math.Cos(x);
            matrixRotateX[2, 3] = Math.Sin(x);
            matrixRotateX[3, 1] = 0;
            matrixRotateX[3, 2] = -Math.Sin(x);
            matrixRotateX[3, 3] = Math.Cos(x);
            matrixRotateX[4, 4] = 1;

            Transform(matrixRotateX);

            //float radian = (float)((degree / 180) * Math.PI);

            //PointF nA = new PointF(
            //    (float)(A.X * Math.Cos(radian) - A.Y * Math.Sin(radian)),
            //    (float)(A.X * Math.Sin(radian) + A.Y * Math.Cos(radian))
            //    );

            //PointF nB = new PointF(
            //    (float)(B.X * Math.Cos(radian) - B.Y * Math.Sin(radian)),
            //    (float)(B.X * Math.Sin(radian) + B.Y * Math.Cos(radian))
            //    );

            //PointF nC = new PointF(
            //    (float)(C.X * Math.Cos(radian) - C.Y * Math.Sin(radian)),
            //    (float)(C.X * Math.Sin(radian) + C.Y * Math.Cos(radian))
            //    );

            //mCurrentA = nA;
            //mCurrentB = nB;
            //mCurrentC = nC;
        }

        /// <summary>
        /// 使用當前的位置去計算旋轉矩陣，給予固定的傳入值就可以旋轉.
        /// </summary>
        /// <param name="degree"></param>
        public void RotateBySave(float degree)
        {
            //float radian = (float)((degree / 180) * Math.PI);

            //PointF nA = new PointF(
            //    (float)(mCurrentA.X * Math.Cos(radian) - mCurrentA.Y * Math.Sin(radian)),
            //    (float)(mCurrentA.X * Math.Sin(radian) + mCurrentA.Y * Math.Cos(radian))
            //    );

            //PointF nB = new PointF(
            //    (float)(mCurrentB.X * Math.Cos(radian) - mCurrentB.Y * Math.Sin(radian)),
            //    (float)(mCurrentB.X * Math.Sin(radian) + mCurrentB.Y * Math.Cos(radian))
            //    );

            //PointF nC = new PointF(
            //    (float)(mCurrentC.X * Math.Cos(radian) - mCurrentC.Y * Math.Sin(radian)),
            //    (float)(mCurrentC.X * Math.Sin(radian) + mCurrentC.Y * Math.Cos(radian))
            //    );

            //mCurrentA = nA;
            //mCurrentB = nB;
            //mCurrentC = nC;
        }

        public void Draw(Graphics g)
        {
            PointF pa = Get2DPointF(mCurrentA);
            PointF pb = Get2DPointF(mCurrentB);
            PointF pc = Get2DPointF(mCurrentC);

            //mCurrentA2D.X = (float)mCurrentA.X;
            //mCurrentA2D.Y = (float)mCurrentA.Y;

            //mCurrentB2D.X = (float)mCurrentB.X;
            //mCurrentB2D.Y = (float)mCurrentB.Y;

            //mCurrentC2D.X = (float)mCurrentC.X;
            //mCurrentC2D.Y = (float)mCurrentC.Y;

            g.DrawLine(Pen, pa, pb);
            g.DrawLine(Pen, pb, pc);
            g.DrawLine(Pen, pc, pa);
        }

        private PointF Get2DPointF(Vector4 v)
        {
            PointF f = new PointF();
            f.X = (float)(v.X / v.W);
            f.Y = (float)(v.Y / v.W);

            return f;
        }
    }
}
