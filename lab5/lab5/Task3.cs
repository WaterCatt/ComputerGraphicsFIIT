using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace lab5
{
    public partial class Task3 : Form
    {
        private List<PointF> points = new List<PointF>();

        private int draggingIndex = -1;

        private int hoveredIndex = -1;

        private const float PointRadius = 6f;
        private const float HitRadius = 12f;

        private readonly Color CurveColor = Color.FromArgb(30, 80, 200);
        private readonly Color PolygonColor = Color.FromArgb(180, 180, 180);
        private readonly Color PointColor = Color.Crimson;
        private readonly Color JointColor = Color.DarkRed;
        private readonly Color HoverColor = Color.Orange;
        private readonly Color TangentColor = Color.FromArgb(120, 180, 120);

        public Task3()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "Задание 3: Составная кубическая кривая Безье";
            this.BackColor = Color.White;
            this.ClientSize = new Size(900, 600);

            this.MouseDown += Task3_MouseDown;
            this.MouseMove += Task3_MouseMove;
            this.MouseUp += Task3_MouseUp;
            this.Paint += Task3_Paint;
        }

        private void Task3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            if (points.Count >= 4)
            {
                using (Pen curvePen = new Pen(CurveColor, 2.5f))
                {
                    curvePen.LineJoin = LineJoin.Round;
                    for (int i = 0; i + 3 < points.Count; i += 3)
                    {
                        DrawBezierSegment(g, points[i], points[i + 1],
                                             points[i + 2], points[i + 3], curvePen);
                    }
                }
            }

            if (points.Count >= 4)
            {
                using (Pen tangentPen = new Pen(TangentColor, 1.2f))
                {
                    tangentPen.DashStyle = DashStyle.Dot;
                    for (int j = 3; j + 1 < points.Count; j += 3)
                    {
                        g.DrawLine(tangentPen, points[j - 1], points[j + 1]);
                    }
                }
            }

            if (points.Count >= 2)
            {
                using (Pen polyPen = new Pen(PolygonColor, 1.5f))
                {
                    polyPen.DashStyle = DashStyle.Dash;
                    for (int i = 0; i < points.Count - 1; i++)
                        g.DrawLine(polyPen, points[i], points[i + 1]);
                }
            }

            for (int i = 0; i < points.Count; i++)
            {
                bool isJoint = (i > 0 && i % 3 == 0);
                Color c = isJoint ? JointColor : PointColor;
                if (i == hoveredIndex) c = HoverColor;

                using (Brush brush = new SolidBrush(c))
                    g.FillEllipse(brush,
                        points[i].X - PointRadius, points[i].Y - PointRadius,
                        PointRadius * 2, PointRadius * 2);

                using (Pen pen = new Pen(Color.Black, 1.5f))
                    g.DrawEllipse(pen,
                        points[i].X - PointRadius, points[i].Y - PointRadius,
                        PointRadius * 2, PointRadius * 2);

                using (Font font = new Font("Consolas", 9))
                using (Brush textBrush = new SolidBrush(Color.FromArgb(100, 100, 100)))
                    g.DrawString(i.ToString(), font, textBrush,
                        points[i].X + 10, points[i].Y - 20);
            }

            using (Font font = new Font("Segoe UI", 10))
            using (Brush brush = new SolidBrush(Color.FromArgb(80, 80, 80)))
            {
                int segments = points.Count >= 4 ? (points.Count - 1) / 3 : 0;
                string info =
                    $"Точек: {points.Count}   Сегментов: {segments}\n" +
                    "ЛКМ по пустому месту — добавить точку\n" +
                    "ЛКМ по точке — переместить (узел скользит по касательной)\n" +
                    "ПКМ по точке — удалить";
                g.DrawString(info, font, brush, 10, 10);
            }
        }

        private void DrawBezierSegment(Graphics g, PointF p0, PointF p1,
                                       PointF p2, PointF p3, Pen pen)
        {
            // B(t) = (1-t)^3*P0 + 3(1-t)^2*t*P1 + 3(1-t)*t^2*P2 + t^3*P3
            const int steps = 120;
            PointF prev = p0;

            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                PointF pt = BezierPoint(p0, p1, p2, p3, t);
                g.DrawLine(pen, prev, pt);
                prev = pt;
            }
        }

        private PointF BezierPoint(PointF p0, PointF p1, PointF p2, PointF p3, float t)
        {
            float u = 1 - t;
            float uu = u * u;
            float uuu = uu * u;
            float tt = t * t;
            float ttt = tt * t;

            float x = uuu * p0.X + 3 * uu * t * p1.X + 3 * u * tt * p2.X + ttt * p3.X;
            float y = uuu * p0.Y + 3 * uu * t * p1.Y + 3 * u * tt * p2.Y + ttt * p3.Y;

            return new PointF(x, y);
        }

        private int HitTest(PointF p)
        {
            for (int i = points.Count - 1; i >= 0; i--)
            {
                float dx = points[i].X - p.X;
                float dy = points[i].Y - p.Y;
                if (dx * dx + dy * dy <= HitRadius * HitRadius)
                    return i;
            }
            return -1;
        }

        private void Task3_MouseDown(object sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);

            if (e.Button == MouseButtons.Left)
            {
                if (idx >= 0)
                {
                    draggingIndex = idx;
                }
                else
                {
                    points.Add(e.Location);
                    Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (idx >= 0)
                {
                    points.RemoveAt(idx);
                    draggingIndex = -1;
                    hoveredIndex = -1;
                    Invalidate();
                }
            }
        }

        private void Task3_MouseMove(object sender, MouseEventArgs e)
        {
            int idx = HitTest(e.Location);
            if (idx != hoveredIndex)
            {
                hoveredIndex = idx;
                this.Cursor = idx >= 0 ? Cursors.Hand : Cursors.Default;
                Invalidate();
            }

            if (draggingIndex >= 0)
            {
                PointF newPos = new PointF(
                    Math.Max(0, Math.Min(ClientSize.Width, e.Location.X)),
                    Math.Max(0, Math.Min(ClientSize.Height, e.Location.Y)));

                if (draggingIndex > 0 && draggingIndex % 3 == 0 &&
                    draggingIndex - 1 >= 0 && draggingIndex + 1 < points.Count)
                {
                    PointF A = points[draggingIndex - 1];
                    PointF C = points[draggingIndex + 1];

                    float dx = C.X - A.X;
                    float dy = C.Y - A.Y;
                    float len2 = dx * dx + dy * dy;

                    if (len2 > 0.0001f)
                    {
                        float t = ((newPos.X - A.X) * dx + (newPos.Y - A.Y) * dy) / len2;
                        newPos = new PointF(A.X + t * dx, A.Y + t * dy);
                    }
                }

                points[draggingIndex] = newPos;
                Invalidate();
            }
        }

        private void Task3_MouseUp(object sender, MouseEventArgs e)
        {
            draggingIndex = -1;
        }
    }
}