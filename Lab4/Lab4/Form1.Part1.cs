using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1
    {
        private void Part1_AddVertex(PointF p)
        {
            if (currentPolygon == null)
            {
                currentPolygon = new List<PointF>();
                currentMode = Mode.CreatingPolygon;
            }
            currentPolygon.Add(p);
            RedrawAll();
        }

        private void Part1_RightClick(PointF p)
        {
            if (currentPolygon != null && currentPolygon.Count > 0)
            {
                polygons.Add(new List<PointF>(currentPolygon));
                selectedPolygon = polygons.Count - 1;
                label6.Text = $"Создан полигон {selectedPolygon + 1}";
                currentPolygon = null;
                currentMode = Mode.Idle;
                RedrawAll();
                return;
            }

            int idx = FindPolygonAtPoint(p);
            if (idx >= 0)
            {
                selectedPolygon = idx;
                label6.Text = $"Выбран полигон {idx + 1}";
                RedrawAll();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            polygons.Clear();
            currentPolygon = null;
            userPoint = null;
            dynamicEdgeStart = null;
            isDrawingEdge = false;
            selectedPolygon = -1;
            label6.Text = "Холст очищен.";
            RedrawAll();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }
            double dx, dy;
            if (!ParseDouble(textBox1.Text, out dx) || !ParseDouble(textBox2.Text, out dy))
            {
                label6.Text = "Ошибка dx,dy";
                return;
            }
            polygons[selectedPolygon] = ApplyMatrixToPolygon(polygons[selectedPolygon], TranslationMatrix(dx, dy));
            label6.Text = $"Смещение на ({dx},{dy})";
            RedrawAll();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }
            double angle;
            if (!ParseDouble(textBox3.Text, out angle))
            {
                label6.Text = "Ошибка угла";
                return;
            }
            var c = PolygonCenter(polygons[selectedPolygon]);
            polygons[selectedPolygon] = ApplyMatrixToPolygon(polygons[selectedPolygon], RotationAroundPointMatrix(c.X, c.Y, angle));
            label6.Text = $"Поворот {angle}° вокруг центра";
            RedrawAll();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }
            double s;
            if (!ParseDouble(textBox4.Text, out s))
            {
                label6.Text = "Ошибка масштаба";
                return;
            }
            var c = PolygonCenter(polygons[selectedPolygon]);
            polygons[selectedPolygon] = ApplyMatrixToPolygon(polygons[selectedPolygon], ScalingAroundPointMatrix(c.X, c.Y, s, s));
            label6.Text = $"Масштаб {s} вокруг центра";
            RedrawAll();
        }

        private void InitCanvas(int width, int height)
        {
            if (width < 1 || height < 1)
                return;

            graphics?.Dispose();
            canvas?.Dispose();

            canvas = new Bitmap(width, height);
            graphics = Graphics.FromImage(canvas);
            graphics.Clear(Color.White);
            pictureBox1.Image = canvas;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Bitmap old = canvas;
            InitCanvas(pictureBox1.ClientSize.Width, pictureBox1.ClientSize.Height);
            if (old != null)
            {
                try
                {
                    using (Graphics g = Graphics.FromImage(canvas))
                        g.DrawImage(old, 0, 0);
                }
                catch { }
                finally { old.Dispose(); }
            }
            pictureBox1.Image = canvas;
            RedrawAll();
        }

        private void RedrawAll()
        {
            if (graphics == null)
                return;
            graphics.Clear(Color.White);

            for (int i = 0; i < polygons.Count; i++)
                DrawPolygon(graphics, polygons[i], i == selectedPolygon ? selectedPen : polygonPen);

            if (currentPolygon != null && currentPolygon.Count > 0)
            {
                DrawPolygon(graphics, currentPolygon, tempPen);
                foreach (var v in currentPolygon)
                    graphics.FillEllipse(pointBrush, v.X - 3, v.Y - 3, 6, 6);
            }

            if (isDrawingEdge && dynamicEdgeStart != null)
                graphics.DrawLine(previewPen, dynamicEdgeStart.Value, dynamicEdgeEnd);

            pictureBox1.Invalidate();
        }

        private void DrawPolygon(Graphics g, List<PointF> poly, Pen pen)
        {
            if (poly.Count == 1)
            {
                g.FillEllipse(Brushes.Black, poly[0].X - 2, poly[0].Y - 2, 4, 4);
                return;
            }
            if (poly.Count == 2)
            {
                g.DrawLine(pen, poly[0], poly[1]);
                return;
            }
            g.DrawPolygon(pen, poly.ToArray());
        }

        private bool ParseDouble(string s, out double v)
        {
            return double.TryParse(s.Replace(',', '.'), System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out v);
        }

        private double[,] TranslationMatrix(double dx, double dy) => new double[,] { { 1, 0, dx }, { 0, 1, dy }, { 0, 0, 1 } };

        private double[,] RotationMatrix(double a)
        {
            double r = a * Math.PI / 180.0;
            return new double[,] { { Math.Cos(r), -Math.Sin(r), 0 }, { Math.Sin(r), Math.Cos(r), 0 }, { 0, 0, 1 } };
        }

        private double[,] ScalingMatrix(double sx, double sy) => new double[,] { { sx, 0, 0 }, { 0, sy, 0 }, { 0, 0, 1 } };

        private double[,] Multiply(double[,] A, double[,] B)
        {
            var C = new double[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int k = 0; k < 3; k++)
                        C[i, j] += A[i, k] * B[k, j];
            return C;
        }

        private double[,] RotationAroundPointMatrix(double x, double y, double a)
        {
            var T1 = TranslationMatrix(-x, -y);
            var R = RotationMatrix(a);
            var T2 = TranslationMatrix(x, y);
            return Multiply(Multiply(T2, R), T1);
        }

        private double[,] ScalingAroundPointMatrix(double x, double y, double sx, double sy)
        {
            var T1 = TranslationMatrix(-x, -y);
            var S = ScalingMatrix(sx, sy);
            var T2 = TranslationMatrix(x, y);
            return Multiply(Multiply(T2, S), T1);
        }

        private PointF ApplyMatrix(PointF p, double[,] M)
        {
            double x = M[0, 0] * p.X + M[0, 1] * p.Y + M[0, 2];
            double y = M[1, 0] * p.X + M[1, 1] * p.Y + M[1, 2];
            return new PointF((float)x, (float)y);
        }

        private List<PointF> ApplyMatrixToPolygon(List<PointF> poly, double[,] M)
        {
            List<PointF> res = new List<PointF>();
            foreach (var p in poly)
                res.Add(ApplyMatrix(p, M));
            return res;
        }

        private PointF PolygonCenter(List<PointF> poly)
        {
            float sx = 0, sy = 0;
            foreach (var p in poly)
            {
                sx += p.X;
                sy += p.Y;
            }
            return new PointF(sx / poly.Count, sy / poly.Count);
        }
    }
}