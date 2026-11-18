using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Lab9
{
    public enum RenderMode
    {
        Standard,
        BackFaceCulling,
        ZBuffer,
        Gouraud,
        Phong,
        Textured
    }

    public static class TextureRenderer
    {
        private static Bitmap? savedTexture;
        private static int[] texturePixels = Array.Empty<int>();

        public static void Draw(Graphics graphics, Polyhedron polyhedron, Camera camera, int width, int height, Bitmap? texture)
        {
            if (width <= 0 || height <= 0 || texture == null)
            {
                return;
            }

            LoadTexturePixels(texture);
            SetTexCoords(polyhedron);
            Rasterizer.ResetBuffers(width, height, out double[] zBuffer, out int[] pixels);

            double[,] view = camera.GetViewMatrix();
            double[,] projection = camera.GetProjectionMatrix();

            double[,] matrix = MatrixOps.Multiply(projection, view);
            Point3D[] points = new Point3D[polyhedron.Vertices.Count];
            double[] invW = new double[polyhedron.Vertices.Count];
            bool[] visible = new bool[polyhedron.Vertices.Count];

            float mult;

            if (camera.Type == ProjectionType.Perspective)
            {
                mult = Math.Min(width, height) / 2f;
            }
            else
            {
                mult = Math.Min(width, height) / (2f * 3.5f);
            }

            for (int i = 0; i < polyhedron.Vertices.Count; i++)
            {
                Point3D p = polyhedron.Vertices[i];

                double x = p.X * matrix[0, 0] + p.Y * matrix[0, 1] + p.Z * matrix[0, 2] + p.W * matrix[0, 3];
                double y = p.X * matrix[1, 0] + p.Y * matrix[1, 1] + p.Z * matrix[1, 2] + p.W * matrix[1, 3];
                double z = p.X * matrix[2, 0] + p.Y * matrix[2, 1] + p.Z * matrix[2, 2] + p.W * matrix[2, 3];
                double w = p.X * matrix[3, 0] + p.Y * matrix[3, 1] + p.Z * matrix[3, 2] + p.W * matrix[3, 3];

                if (camera.Type == ProjectionType.Perspective)
                {
                    if (w <= 0.05)
                    {
                        continue;
                    }

                    invW[i] = 1.0 / w;
                    x /= w;
                    y /= w;
                    z /= w;
                }
                else
                {
                    invW[i] = 1;
                    z = -z;
                }

                double screenX = width / 2.0 + x * mult;
                double screenY = height / 2.0 - y * mult;

                points[i] = new Point3D(screenX, screenY, z);
                visible[i] = true;
            }

            foreach (Polygon face in polyhedron.Faces)
            {
                if (face.VertexIndices.Count < 3 || face.TexCoordIndices.Count != face.VertexIndices.Count)
                {
                    continue;
                }

                int a = face.VertexIndices[0];
                PointF ta = polyhedron.TexCoords[face.TexCoordIndices[0]];

                for (int i = 1; i < face.VertexIndices.Count - 1; i++)
                {
                    int b = face.VertexIndices[i];
                    int c = face.VertexIndices[i + 1];

                    if (!visible[a] || !visible[b] || !visible[c])
                    {
                        continue;
                    }

                    PointF tb = polyhedron.TexCoords[face.TexCoordIndices[i]];
                    PointF tc = polyhedron.TexCoords[face.TexCoordIndices[i + 1]];

                    Rasterizer.FillTriangle(points[a], points[b], points[c], width, height, (index, k1, k2, k3, z) =>
                    {
                        if (z >= zBuffer[index])
                        {
                            return;
                        }

                        double k1p = k1 * invW[a];
                        double k2p = k2 * invW[b];
                        double k3p = k3 * invW[c];
                        double sum = k1p + k2p + k3p;
                        if (sum <= 1e-12)
                        {
                            return;
                        }

                        double u = (ta.X * k1p + tb.X * k2p + tc.X * k3p) / sum;
                        double v = (ta.Y * k1p + tb.Y * k2p + tc.Y * k3p) / sum;

                        int textureX = (int)(u * (texture.Width - 1));
                        int textureY = (int)((1 - v) * (texture.Height - 1));

                        textureX = Math.Max(0, Math.Min(texture.Width - 1, textureX));
                        textureY = Math.Max(0, Math.Min(texture.Height - 1, textureY));

                        zBuffer[index] = z;
                        pixels[index] = texturePixels[textureY * texture.Width + textureX];
                    });
                }

            }

            using (Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                BitmapData data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                Marshal.Copy(pixels, 0, data.Scan0, pixels.Length);
                bitmap.UnlockBits(data);
                graphics.DrawImageUnscaled(bitmap, 0, 0);
            }
        }

        private static void LoadTexturePixels(Bitmap texture)
        {
            if (savedTexture == texture)
            {
                return;
            }

            texturePixels = new int[texture.Width * texture.Height];

            for (int y = 0; y < texture.Height; y++)
            {
                for (int x = 0; x < texture.Width; x++)
                {
                    texturePixels[y * texture.Width + x] = texture.GetPixel(x, y).ToArgb();
                }
            }

            savedTexture = texture;
        }

        private static void SetTexCoords(Polyhedron polyhedron)
        {
            foreach (Polygon face in polyhedron.Faces)
            {
                if (face.TexCoordIndices.Count == face.VertexIndices.Count)
                {
                    continue;
                }

                face.TexCoordIndices.Clear();
                int count = face.VertexIndices.Count;
                PointF[] coords;

                if (count == 3)
                {
                    coords = new PointF[]
                    {
                        new PointF(0.5f, 1),
                        new PointF(0, 0),
                        new PointF(1, 0)
                    };
                }
                else if (count == 4)
                {
                    coords = new PointF[]
                    {
                        new PointF(0, 1),
                        new PointF(0, 0),
                        new PointF(1, 0),
                        new PointF(1, 1)
                    };
                }
                else
                {
                    coords = new PointF[count];

                    for (int i = 0; i < count; i++)
                    {
                        double angle = 2 * Math.PI * i / count - Math.PI / 2;
                        coords[i] = new PointF(0.5f + 0.5f * (float)Math.Cos(angle), 0.5f + 0.5f * (float)Math.Sin(angle));
                    }
                }

                foreach (PointF coord in coords)
                {
                    polyhedron.TexCoords.Add(coord);
                    face.TexCoordIndices.Add(polyhedron.TexCoords.Count - 1);
                }
            }
        }
    }

    public class PolyhedronDrawer : Panel
    {
        private Polyhedron polyhedron;
        private Camera camera;
        private Bitmap texture;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RenderMode RenderMode { get; set; } = RenderMode.Standard;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShowSeveralObjects { get; set; } = true;

        private Point lastMousePos;
        private bool isDragging;

        private double perspYaw, perspPitch, perspRadius;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Polyhedron Polyhedron
        {
            get => polyhedron;
            set
            {
                polyhedron = value;
                ApplyOrbit();
                Invalidate();
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Camera Camera
        {
            get => camera;
            set { camera = value; Invalidate(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Bitmap Texture => texture;

        public PolyhedronDrawer()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            TabStop = true;
            Camera = new Camera();
            ResetCamera();
        }

        public void SetTexture(string path)
        {
            if (texture != null)
            {
                texture.Dispose();
                texture = null;
            }
            if (!string.IsNullOrEmpty(path))
                texture = new Bitmap(path);
        }

        public void ResetCamera()
        {
            perspRadius = Math.Sqrt(2 * 2 + 2 * 2 + 2 * 2);
            perspPitch = Math.Asin(2.0 / perspRadius);
            perspYaw = Math.Atan2(2.0, 2.0);

            ApplyOrbit();
            Invalidate();
        }

        public void RefreshPolyhedron()
        {
            ApplyOrbit();
            Invalidate();
        }

        private void ApplyOrbit()
        {
            Point3D target = polyhedron != null ? polyhedron.GetCenter() : new Point3D(0, 0, 0);

            if (camera.Type == ProjectionType.Axonometric)
            {
                camera.LookAt = target;
                camera.Position = new Point3D(target.X + 1, target.Y + 1, target.Z + 1);
                camera.Up = new Point3D(0, 0, 1);
                return;
            }

            double cp = Math.Cos(perspPitch);
            double sp = Math.Sin(perspPitch);
            double cy = Math.Cos(perspYaw);
            double sy = Math.Sin(perspYaw);

            camera.LookAt = target;
            camera.Position = new Point3D(
                target.X + cp * cy * perspRadius,
                target.Y + cp * sy * perspRadius,
                target.Z + sp * perspRadius);
            camera.Up = new Point3D(-sp * cy, -sp * sy, cp);
        }

        public void ApplyProjectionType(ProjectionType type)
        {
            camera.Type = type;
            ApplyOrbit();
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right && camera.Type == ProjectionType.Perspective)
            {
                isDragging = true;
                lastMousePos = e.Location;
                Focus();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!isDragging)
                return;

            int dx = e.X - lastMousePos.X;
            int dy = e.Y - lastMousePos.Y;
            lastMousePos = e.Location;

            perspYaw -= dx * 0.01;
            perspPitch += dy * 0.01;

            ApplyOrbit();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Right)
                isDragging = false;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (camera.Type != ProjectionType.Perspective)
                return;

            const double zoomFactor = 1.1;
            perspRadius = Math.Max(0.5, perspRadius * (e.Delta > 0 ? 1.0 / zoomFactor : zoomFactor));
            ApplyOrbit();
            Invalidate();
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right || keyData == Keys.W || keyData == Keys.A || keyData == Keys.S || keyData == Keys.D)
                return true;
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (camera.Type != ProjectionType.Perspective)
                return;

            const double moveStep = 0.1;
            const double zoomStep = 0.1;
            bool changed = false;

            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
            {
                perspRadius = Math.Max(0.5, perspRadius - zoomStep);
                changed = true;
            }
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
            {
                perspRadius += zoomStep;
                changed = true;
            }
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
            {
                perspYaw += moveStep;
                changed = true;
            }
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
            {
                perspYaw -= moveStep;
                changed = true;
            }

            if (changed)
            {
                ApplyOrbit();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(BackColor);

            if (polyhedron == null || camera == null)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            double[,] view = camera.GetViewMatrix();
            double[,] proj = camera.GetProjectionMatrix();
            double[,] transformMatrix = MatrixOps.Multiply(proj, view);

            DrawAxes(e.Graphics, transformMatrix);

            if (polyhedron.VertexNormals.Count != polyhedron.Vertices.Count)
                polyhedron.ComputeVertexNormals();
            if (polyhedron.FaceNormals.Count != polyhedron.Faces.Count)
                polyhedron.ComputeFaceNormals();

            if (RenderMode == RenderMode.ZBuffer && polyhedron.Faces.Count > 0)
            {
                ZBufferRenderer.Draw(e.Graphics, polyhedron, camera, Width, Height, ShowSeveralObjects);
                return;
            }

            if (RenderMode == RenderMode.Gouraud)
            {
                GouraudRenderer.Draw(e.Graphics, polyhedron, camera, Width, Height);
                return;
            }

            if (RenderMode == RenderMode.Phong)
            {
                DrawAxes(e.Graphics, transformMatrix);
                PhongRenderer.Draw(e.Graphics, polyhedron, camera, Width, Height);
                return;
            }

            if (RenderMode == RenderMode.Textured)
            {
                TextureRenderer.Draw(e.Graphics, polyhedron, camera, Width, Height, texture);
                return;
            }

            var visibleEdges = RenderMode == RenderMode.BackFaceCulling
                ? Culling.GetVisibleEdges(polyhedron, camera)
                : polyhedron.Edges;
            using (var pen = new Pen(Color.Black, 2))
            {
                foreach (var edge in visibleEdges)
                    if (TryProjectEdge(polyhedron.Vertices[edge.Item1], polyhedron.Vertices[edge.Item2], transformMatrix, out PointF p1, out PointF p2))
                        e.Graphics.DrawLine(pen, p1, p2);
            }
        }

        public void DrawAxes(Graphics g, double[,] transformMatrix)
        {
            double t = camera.Type == ProjectionType.Perspective ? 3 : 10;
            Point3D o = new Point3D(0, 0, 0);
            Point3D x = new Point3D(t, 0, 0);
            Point3D y = new Point3D(0, t, 0);
            Point3D z = new Point3D(0, 0, t);

            using (var pen = new Pen(Color.Gray, 1) { DashStyle = DashStyle.Dot })
            {
                if (TryProjectEdge(o, x, transformMatrix, out PointF px1, out PointF px2))
                    g.DrawLine(pen, px1, px2);
                if (TryProjectEdge(o, y, transformMatrix, out PointF py1, out PointF py2))
                    g.DrawLine(pen, py1, py2);
                if (TryProjectEdge(o, z, transformMatrix, out PointF pz1, out PointF pz2))
                    g.DrawLine(pen, pz1, pz2);
            }
        }

        private bool TryProjectEdge(Point3D a, Point3D b, double[,] matrix, out PointF pa, out PointF pb)
        {
            pa = PointF.Empty;
            pb = PointF.Empty;

            double ax, ay, aw, bx, by, bw;
            TransformToClip(a, matrix, out ax, out ay, out aw);
            TransformToClip(b, matrix, out bx, out by, out bw);

            if (camera.Type == ProjectionType.Perspective)
            {
                double near = 0.05;
                if (aw < near && bw < near)
                    return false;

                if (aw < near)
                {
                    double t = (near - aw) / (bw - aw);
                    ax += (bx - ax) * t;
                    ay += (by - ay) * t;
                    aw = near;
                }
                else if (bw < near)
                {
                    double t = (near - bw) / (aw - bw);
                    bx += (ax - bx) * t;
                    by += (ay - by) * t;
                    bw = near;
                }

                ax /= aw;
                ay /= aw;
                bx /= bw;
                by /= bw;
            }

            pa = ToScreen(ax, ay);
            pb = ToScreen(bx, by);
            return true;
        }

        private void TransformToClip(Point3D p, double[,] m, out double x, out double y, out double w)
        {
            x = p.X * m[0, 0] + p.Y * m[0, 1] + p.Z * m[0, 2] + p.W * m[0, 3];
            y = p.X * m[1, 0] + p.Y * m[1, 1] + p.Z * m[1, 2] + p.W * m[1, 3];
            w = p.X * m[3, 0] + p.Y * m[3, 1] + p.Z * m[3, 2] + p.W * m[3, 3];
        }

        private PointF ToScreen(double x, double y)
        {
            float centerX = Width / 2f;
            float centerY = Height / 2f;
            float scale = camera.Type == ProjectionType.Perspective ? Math.Min(Width, Height) / 2f : Math.Min(Width, Height) / (2f * 3.5f);
            return new PointF(centerX + (float)(x * scale), centerY - (float)(y * scale));
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Invalidate();
        }
    }
}
