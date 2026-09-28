using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FastBitmap;

namespace lab03
{
    public partial class task3 : Form
    {
        private List<Point> currentPoints = new List<Point>();

        private class Triangle
        {
            public Point P1, P2, P3;
            public Color C1, C2, C3;
            public Triangle(Point p1, Point p2, Point p3, Color c1, Color c2, Color c3)
            {
                P1 = p1; P2 = p2; P3 = p3; C1 = c1; C2 = c2; C3 = c3;
            }
        }

        private List<Triangle> triangles = new List<Triangle>();

        private Color color1 = Color.Red;
        private Color color2 = Color.Green;
        private Color color3 = Color.Blue;

        private Bitmap Canvas;

        public task3()
        {
            InitializeComponent();
            this.Load += Task3_Load;
        }

        private void Task3_Load(object sender, EventArgs e)
        {
            pictureBox2.BackColor = color1;
            pictureBox3.BackColor = color2;
            pictureBox4.BackColor = color3;

            pictureBox1.MouseClick += PictureBox1_MouseClick;
            pictureBox1.Resize += PictureBox1_Resize;

            button1.Click += ButtonFill_Click;
            button2.Click += ButtonClearAll_Click;
            button3.Click += ButtonColor1_Click;
            button4.Click += ButtonColor2_Click;
            button5.Click += ButtonColor3_Click;

            if (pictureBox1.Width > 0 && pictureBox1.Height > 0)
                CreateNewCanvas(pictureBox1.Width, pictureBox1.Height);
        }

        private void PictureBox1_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Width <= 0 || pictureBox1.Height <= 0) return;
            CreateNewCanvas(pictureBox1.Width, pictureBox1.Height);
            RedrawAll();
        }

        private void CreateNewCanvas(int width, int height)
        {
            if (width <= 0 || height <= 0) return;

            var newCanvas = new Bitmap(width, height);
            using (Graphics gg = Graphics.FromImage(newCanvas))
                gg.Clear(Color.White);

            Canvas?.Dispose();
            Canvas = newCanvas;
            pictureBox1.Image = Canvas;
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (currentPoints.Count > 0)
                    currentPoints.RemoveAt(currentPoints.Count - 1);
                RedrawAll();
                return;
            }

            if (e.Button != MouseButtons.Left) return;

            if (Canvas == null || e.X < 0 || e.Y < 0 || e.X >= Canvas.Width || e.Y >= Canvas.Height)
                return;

            if (currentPoints.Count >= 3)
            {
                MessageBox.Show("Уже выбраны три точки. Нажмите «Залить» или ПКМ, чтобы удалить последнюю.",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            currentPoints.Add(new Point(e.X, e.Y));
            RedrawAll(); 
        }

        private void ButtonColor1_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    color1 = cd.Color;
                    pictureBox2.BackColor = cd.Color;
                }
        }

        private void ButtonColor2_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    color2 = cd.Color;
                    pictureBox3.BackColor = cd.Color;
                }
        }

        private void ButtonColor3_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    color3 = cd.Color;
                    pictureBox4.BackColor = cd.Color;
                }
        }

        private void ButtonClearAll_Click(object sender, EventArgs e)
        {
            if (Canvas == null) return;
            using (Graphics gg = Graphics.FromImage(Canvas))
                gg.Clear(Color.White);
            triangles.Clear();
            currentPoints.Clear();
            pictureBox1.Invalidate();
        }

        private void ButtonFill_Click(object sender, EventArgs e)
        {
            if (currentPoints.Count < 3)
            {
                MessageBox.Show("Нужно поставить три точки на холсте, чтобы образовать треугольник.",
                    "Недостаточно вершин", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var t = new Triangle(currentPoints[0], currentPoints[1], currentPoints[2], color1, color2, color3);
            triangles.Add(t);

            FillTriangleGradientAreaMethod(t.P1, t.P2, t.P3, t.C1, t.C2, t.C3);

            currentPoints.Clear();
            pictureBox1.Invalidate();
        }

        private void RedrawAll()
        {
            if (Canvas == null) return;

            using (Graphics gg = Graphics.FromImage(Canvas))
            {
                gg.Clear(Color.White);

                foreach (var tri in triangles)
                    FillTriangleGradientAreaMethod(tri.P1, tri.P2, tri.P3, tri.C1, tri.C2, tri.C3);

                using (SolidBrush br = new SolidBrush(Color.Black))
                    foreach (var p in currentPoints)
                        gg.FillEllipse(br, p.X - 3, p.Y - 3, 6, 6);
            }

            pictureBox1.Invalidate();
        }

        private void FillTriangleGradientAreaMethod(Point p1, Point p2, Point p3, Color c1, Color c2, Color c3)
        {
            Rectangle bounds = GetTriangleBounds(p1, p2, p3);

            int left = Math.Max(0, bounds.Left);
            int right = Math.Min(Canvas.Width - 1, bounds.Right);
            int top = Math.Max(0, bounds.Top);
            int bottom = Math.Min(Canvas.Height - 1, bounds.Bottom);

            float ABCarea = TriangleArea(p1, p2, p3);
            if (Math.Abs(ABCarea) < 1e-6f) return;

            using (var fb = new FastBitmap.FastBitmap(Canvas))
            {
                for (int y = top; y <= bottom; y++)
                {
                    for (int x = left; x <= right; x++)
                    {
                        Point p = new Point(x, y);
                        if (!IsPointInTriangle(p, p1, p2, p3)) continue;

                        float a = TriangleArea(p, p2, p3) / ABCarea;
                        float b = TriangleArea(p, p3, p1) / ABCarea;
                        float c = TriangleArea(p, p1, p2) / ABCarea;

                        int R = (int)(a * c1.R + b * c2.R + c * c3.R);
                        int G = (int)(a * c1.G + b * c2.G + c * c3.G);
                        int B = (int)(a * c1.B + b * c2.B + c * c3.B);

                        fb[x, y] = Color.FromArgb(R, G, B);
                    }
                }
            }
        }

        private float TriangleArea(Point a, Point b, Point c)
        {
            return Math.Abs((b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y)) / 2f;
        }

        private Rectangle GetTriangleBounds(Point p1, Point p2, Point p3)
        {
            int minX = Math.Min(p1.X, Math.Min(p2.X, p3.X));
            int maxX = Math.Max(p1.X, Math.Max(p2.X, p3.X));
            int minY = Math.Min(p1.Y, Math.Min(p2.Y, p3.Y));
            int maxY = Math.Max(p1.Y, Math.Max(p2.Y, p3.Y));
            return Rectangle.FromLTRB(minX, minY, maxX, maxY);
        }

        private bool IsPointInTriangle(Point p, Point p0, Point p1, Point p2)
        {
            float dX = p.X - p2.X;
            float dY = p.Y - p2.Y;
            float dX21 = p2.X - p1.X;
            float dY12 = p1.Y - p2.Y;

            float D = (p1.X - p0.X) * (p2.Y - p0.Y) - (p1.Y - p0.Y) * (p2.X - p0.X);
            float s = dY12 * dX + dX21 * dY;
            float t = (p2.Y - p0.Y) * dX + (p0.X - p2.X) * dY;

            if (D < 0)
                return (s <= 0) && (t <= 0) && (s + t >= D);
            return (s >= 0) && (t >= 0) && (s + t <= D);
        }
    }
}