using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Lab6
{
    public class PolyhedronDrawer : Panel
    {
        private Polyhedron polyhedron;
        private Camera camera;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Polyhedron Polyhedron
        {
            get => polyhedron;
            set { polyhedron = value; Invalidate(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Camera Camera
        {
            get => camera;
            set { camera = value; Invalidate(); }
        }

        public PolyhedronDrawer()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            Camera = new Camera();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (polyhedron == null || camera == null) 
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            double[,] view, proj;

            if (camera.Type == ProjectionType.Perspective)
            {
                var perspCam = new Camera
                {
                    Position = new Point3D(2, 2, 2),
                    LookAt = new Point3D(0, 0, 0),
                    Up = new Point3D(0, 0, 1)
                };
                view = perspCam.GetViewMatrix();
                proj = perspCam.GetProjectionMatrix();
            }
            else
            {
                var isoCam = new Camera
                {
                    Position = new Point3D(1, 1, 1),
                    LookAt = new Point3D(0, 0, 0),
                    Up = new Point3D(0, 0, 1)
                };
                view = isoCam.GetViewMatrix();
                proj = MatrixOps.Identity();
            }

            double[,] transformMatrix = MatrixOps.Multiply(proj, view);

            DrawAxes(e.Graphics, transformMatrix);

            using (var pen = new Pen(Color.Black, 2))
            {
                foreach (var edge in polyhedron.Edges)
                    if (TryProject(polyhedron.Vertices[edge.Item1], transformMatrix, out PointF p1) && TryProject(polyhedron.Vertices[edge.Item2], transformMatrix, out PointF p2))
                        e.Graphics.DrawLine(pen, p1, p2);
            }
        }

        private void DrawAxes(Graphics g, double[,] transformMatrix)
        {
            double t = camera.Type == ProjectionType.Perspective ? 5.9 : 10;
            Point3D o = new Point3D(0, 0, 0);
            Point3D x = new Point3D(t, 0, 0);
            Point3D y = new Point3D(0, t, 0);
            Point3D z = new Point3D(0, 0, t);

            using (var penX = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dot })
            using (var penY = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dot })
            using (var penZ = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dot })
            {
                if (TryProject(o, transformMatrix, out PointF po))
                {
                    if (TryProject(x, transformMatrix, out PointF px))
                        g.DrawLine(penX, po, px);
                    if (TryProject(y, transformMatrix, out PointF py))
                        g.DrawLine(penY, po, py);
                    if (TryProject(z, transformMatrix, out PointF pz))
                        g.DrawLine(penZ, po, pz);
                }
            }
        }

        private bool TryProject(Point3D p, double[,] matrix, out PointF result)
        {
            double x = p.X * matrix[0, 0] + p.Y * matrix[0, 1] + p.Z * matrix[0, 2] + p.W * matrix[0, 3];
            double y = p.X * matrix[1, 0] + p.Y * matrix[1, 1] + p.Z * matrix[1, 2] + p.W * matrix[1, 3];
            double w = p.X * matrix[3, 0] + p.Y * matrix[3, 1] + p.Z * matrix[3, 2] + p.W * matrix[3, 3];

            if (camera.Type == ProjectionType.Perspective)
            {
                if (Math.Abs(w) < 0.001)
                {
                    result = PointF.Empty;
                    return false;
                }
                x /= w;
                y /= w;
            }

            float centerX = Width / 2;
            float centerY = Height / 2;

            float scale = camera.Type == ProjectionType.Perspective ? 100f : 120f;

            result = new PointF(centerX + (float)(x * scale), centerY - (float)(y * scale));
            return true;
        }
    }
}