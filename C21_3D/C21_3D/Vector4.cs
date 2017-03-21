using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C21_3D
{
    public class Vector4
    {
        private double mx;
        private double my;
        private double mz;
        private double mw;

        public double X { get => mx; set => mx = value; }
        public double Y { get => my; set => my = value; }
        public double Z { get => mz; set => mz = value; }
        public double W { get => mw; set => mw = value; }

        public Vector4() { }

        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static Vector4 operator *(Vector4 v, Matrix4x4 m)
        {
            return Matrix4x4.Mul(m, v);
        }
    }
}
