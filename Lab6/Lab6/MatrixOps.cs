using System;

namespace Lab6
{
    public static class MatrixOps
    {
        public static double[,] Identity()
        {
            return new double[,] { //сл16
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            };
        }

        public static double[,] Multiply(double[,] a, double[,] b)
        {
            int rowsA = a.GetLength(0);
            int colsA = a.GetLength(1);
            int colsB = b.GetLength(1);
            double[,] result = new double[rowsA, colsB];

            for (int i = 0; i < rowsA; i++)
                for (int j = 0; j < colsB; j++)
                    for (int k = 0; k < colsA; k++)
                        result[i, j] += a[i, k] * b[k, j];

            return result;
        }

        public static double[,] Translate(double tx, double ty, double tz)
        {
            var m = Identity();
            m[0, 3] = tx; 
            m[1, 3] = ty; 
            m[2, 3] = tz;
            return m;
        }

        public static double[,] Scale(double sx, double sy, double sz)
        {
            var m = Identity();
            m[0, 0] = sx; 
            m[1, 1] = sy; 
            m[2, 2] = sz;
            return m;
        }

        public static double[,] Rotate(double angle, Point3D axis)
        {
            axis = axis.Copy();
            axis.Normalize();
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double l = axis.X, m = axis.Y, n = axis.Z;
            double s = 1 - cos;

            return new double[,] { //сл19
                { l*l*s + cos,   l*m*s + n*sin, l*n*s - m*sin, 0 },
                { l*m*s - n*sin, m*m*s + cos,   m*n*s + l*sin, 0 },
                { l*n*s + m*sin, m*n*s - l*sin, n*n*s + cos,   0 },
                { 0, 0, 0, 1 }
            };
        }

        public static double[,] Reflect(Point3D normal, Point3D pointOnPlane) //Строит матрицу отражения относительно плоскости, заданной нормалью и любой точкой на этой плоскости
        {
            normal = normal.Copy();
            normal.Normalize();
            double d = -Point3D.Dot(normal, pointOnPlane); //из формулы Ax + By + Cz + D = 0 проверка нахождения точки на плоскости
            double nx = normal.X, ny = normal.Y, nz = normal.Z;

            return new double[,] { 
                { 1 - 2*nx*nx, -2*nx*ny,    -2*nx*nz,    -2*nx*d }, //кэф перед х, у, з, свободный член   (развёрнутая формула P' = P - 2 * Расстояние * нормаль. От точки до плоскости *2)
                { -2*ny*nx,    1 - 2*ny*ny, -2*ny*nz,    -2*ny*d },
                { -2*nz*nx,    -2*nz*ny,    1 - 2*nz*nz, -2*nz*d },
                { 0, 0, 0, 1 }
            };
        }
    }
}