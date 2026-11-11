using System;

namespace Lab8
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
        public double Near { get; set; }
        public double Far { get; set; }

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

            Point3D up;
            if (Type == ProjectionType.Perspective)
            {
                up = Up.Copy();
                if (Math.Abs(Point3D.Dot(up, zAxis)) > 0.999)
                {
                    Point3D helper = Math.Abs(zAxis.Z) < 0.9 ? new Point3D(0, 0, 1) : new Point3D(1, 0, 0);
                    Point3D right = Point3D.Cross(helper, zAxis);
                    right.Normalize();
                    up = Point3D.Cross(zAxis, right);
                }
            }
            else
            {
                up = new Point3D(0, 0, 1);
            }

            Point3D xAxis = Point3D.Cross(up, zAxis);
            xAxis.Normalize();

            Point3D yAxis = Point3D.Cross(zAxis, xAxis);

            double[,] rotationMatrix = {
                { xAxis.X, xAxis.Y, xAxis.Z, 0 },
                { yAxis.X, yAxis.Y, yAxis.Z, 0 },
                { zAxis.X, zAxis.Y, zAxis.Z, 0 },
                { 0, 0, 0, 1 }
            };

            double[,] translationMatrix = {
                { 1, 0, 0, -Position.X },
                { 0, 1, 0, -Position.Y },
                { 0, 0, 1, -Position.Z },
                { 0, 0, 0, 1 }
            };

            return MatrixOps.Multiply(rotationMatrix, translationMatrix);
        }

        public double[,] GetProjectionMatrix()
        {
            double a = Fov * Math.PI / 180;
            double width = Math.Tan(a / 2);

            if (Type == ProjectionType.Perspective)
            {
                double znear = Near;
                double zfar = Far;

                if (znear <= 0) 
                    znear = 0.1;
                if (zfar <= znear) 
                    zfar = znear + 1.0;

                return new double[,] {
                    {1/width, 0, 0, 0},
                    {0, 1/width, 0, 0},
                    {0, 0, -(zfar + znear)/(zfar - znear), -(2 * zfar * znear)/(zfar - znear)},
                    {0, 0, -1, 0}
                };
            }

            double k = 1.0 / width;
            return new double[,] {
                {k, 0, 0, 0},
                {0, k, 0, 0},
                {0, 0, 1, 0},
                {0, 0, 0, 1}
            };
        }
    }
}