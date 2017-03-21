using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C_18Rotate;

namespace C21_3D
{
    public partial class Form1 : Form
    {
        Triangle3D triangle;

        public Form1()
        {
            InitializeComponent();

            ShowSharp();
        }

        protected void ShowSharp()
        {
            Matrix4x4 m1 = new Matrix4x4();
            Matrix4x4 m2 = new Matrix4x4();

            m1[1, 1] = 1;
            m1[1, 2] = 2;
            m1[1, 3] = 3;
            m1[1, 4] = 4;
            m1[2, 1] = 1;
            m1[2, 2] = 2;
            m1[2, 3] = 3;
            m1[2, 4] = 8;
            m1[3, 1] = 1;
            m1[3, 2] = 2;
            m1[3, 3] = 3;
            m1[3, 4] = 4;
            m1[4, 1] = 3;
            m1[4, 2] = 2;
            m1[4, 3] = 1;
            m1[4, 4] = 4;

            Matrix4x4 m3 = m1.Mul(m1);

            triangle = new Triangle3D(new Vector4(0d, -0.5d, 0d, 1d),
                new Vector4(0.5d, 0d, 0d, 1d),
                new Vector4(-0.5d, 0d, 0d, 1d));
        }
        static int rotateX = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            Matrix4x4 rotateMat = Triangle3D.GetRotateMatrix(rotateX+= 4, 0, 0);
            Matrix4x4 ScaleMat = Triangle3D.GetScaleMatrix(100, 100, 100);
            Matrix4x4 cameraMat = Triangle3D.GetCameraMatrix(250);
            Matrix4x4 pMat = Triangle3D.GetPerspectiveMatrix(250);

            triangle.Transform(rotateMat.Mul(ScaleMat).Mul(cameraMat).Mul(pMat));
            //triangle.Transform(rotateMat.Mul(ScaleMat));

            //triangle.Scale(100, 100, 100);
            //triangle.Rotate(rotateX++, 0, 0);
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            triangle.Draw(e.Graphics);
        }
    }
}
