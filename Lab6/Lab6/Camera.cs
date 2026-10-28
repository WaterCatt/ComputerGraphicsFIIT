using System;

namespace Lab6
{
    public enum ProjectionType
    {
        Perspective,
        Axonometric
    }

    public class Camera
    {
        public Point3D Position { get; set; }
        public Point3D LookAt { get; set; }
        public Point3D Up { get; set; }

        public ProjectionType Type { get; set; }
        public double Fov { get; set; }
        public double Near { get; set; } //ближняя плоскость отсечения
        public double Far { get; set; } //дальняя плоскость отсечения

        public Camera()
        {
            Position = new Point3D(2, 2, 2);
            LookAt = new Point3D(0, 0, 0);
            Up = new Point3D(0, 0, 1);
            Type = ProjectionType.Perspective;
            Fov = 90;
            Near = 0.1;
            Far = 1000;
        }

        public double[,] GetViewMatrix()
        {
            Point3D zAxis = Position - LookAt;
            zAxis.Normalize();

            Point3D xAxis = Point3D.Cross(Up, zAxis); //правая ось
            xAxis.Normalize();

            Point3D yAxis = Point3D.Cross(zAxis, xAxis); //верхняя ось для камеры

            double[,] rotationMatrix = { //сл16, использую обобщённый вид, басисные векторы записываю в строки матрицы
                { xAxis.X, xAxis.Y, xAxis.Z, 0 },
                { yAxis.X, yAxis.Y, yAxis.Z, 0 },
                { zAxis.X, zAxis.Y, zAxis.Z, 0 },
                { 0, 0, 0, 1 }
            };

            double[,] translationMatrix = { //мы двигаем мир, а не камеру, поэтому и минусы
                { 1, 0, 0, -Position.X },
                { 0, 1, 0, -Position.Y },
                { 0, 0, 1, -Position.Z },
                { 0, 0, 0, 1 }
            };

            return MatrixOps.Multiply(rotationMatrix, translationMatrix);
        }

        public double[,] GetProjectionMatrix()
        {
            if (Type == ProjectionType.Perspective)
            {
                double a = 90 * Math.PI/180; //fov = 90
                double width = Math.Tan(a/2); //кэфф масштабирования, больше фов - меньше объекты на экране
                double znear = 0.1;
                double zfar = 10;
                //54 сл
                return new double[,] {
                    {1/(width), 0, 0, 0},
                    {0, 1/(width), 0, 0},
                    {0, 0, -(zfar + znear)/(zfar - znear), -(2 * zfar * znear)/(zfar - znear)},
                    {0, 0, -1, 0}
                };
                /*
                 0,0 и 1,1: масштабирвоание по х и у
                 2,2: глубина z 
                 2,3: сдвиг по глубине
                 3,2: нужно чтобы x и y уменьшались по расстоянию 34сл лекции
                 */
            }
            else
            {
                double psi = Math.PI / 4;
                double phi = Math.PI / 6;

                var Ry = MatrixOps.Rotate(psi, new Point3D(0, 1, 0));
                var Rx = MatrixOps.Rotate(phi, new Point3D(1, 0, 0));

                var Ortho = new double[,] {
                    {1, 0, 0, 0 },
                    {0, 1, 0, 0 },
                    {0, 0, 0, 0 },
                    {0, 0, 0, 1 }
                };
                return MatrixOps.Multiply(MatrixOps.Multiply(Ry, Rx), Ortho);
            }
        }
    }
}