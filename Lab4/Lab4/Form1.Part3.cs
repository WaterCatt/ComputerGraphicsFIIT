using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace Lab4
{
    public partial class Form1
    {
        private void HandlePointInPolygon(PointF p)
        {
            if (polygons.Count == 0)
            {
                label6.Text = "Нет полигонов.";
                return;
            }

            List<PointF> poly = selectedPolygon >= 0 ? polygons[selectedPolygon] : polygons[polygons.Count - 1];

            if (poly.Count >= 3)
            {
                bool inside = PointInPolygon(p, poly);
                if (inside) label6.Text = "Точка внутри полигона.";
                else label6.Text = "Точка вне полигона.";
            }
            else label6.Text = "Выбранный полигон содержит меньше трёх вершин";
        }

        private void HandlePointEdgeSide(PointF p)
        {
            if (polygons.Count == 0)
            {
                label6.Text = "Нет рёбер.";
                return;
            }

            List<PointF> poly = selectedPolygon >= 0 ? polygons[selectedPolygon] : polygons[polygons.Count - 1];

            if (poly.Count == 1)
            {
                label6.Text = "Полигон — точка, ребра отсутствуют.";
                return;
            }

            double minDist = double.MaxValue;
            PointF bestA = new PointF(), bestB = new PointF();

            int n = poly.Count;

            for (int i = 0; i < n; i++)
            {
                PointF a = poly[i];
                PointF b = poly[(i + 1) % n];

                double d = DistPointToSegment(p, a, b);

                if (d < minDist)
                {
                    minDist = d;
                    bestA = a;
                    bestB = b;
                }
            }

            double cross = (bestB.X - bestA.X) * (p.Y - bestA.Y) - (bestB.Y - bestA.Y) * (p.X - bestA.X);

            if (cross > 0) label6.Text = "Точка слева от ребра.";
            else label6.Text = "Точка справа от ребра.";
        }

        private int FindPolygonAtPoint(PointF p)
        {

            for (int i = polygons.Count - 1; i >= 0; i--)
            {
                var poly = polygons[i];

                if (poly.Count == 1)
                {
                    if (Math.Abs(poly[0].X - p.X) < 5 && Math.Abs(poly[0].Y - p.Y) < 5) return i;
                }
                else if (poly.Count == 2)
                {
                    if (DistPointToSegment(p, poly[0], poly[1]) < 5) return i;
                }
                else if (PointInPolygon(p, poly)) return i;
            }

            return -1;
        }

        private bool PointInPolygon(PointF p, List<PointF> poly)
        {
            int crossings = 0;

            for (int i = 0; i < poly.Count; i++)
            {
                PointF a = poly[i];
                PointF b = poly[(i + 1) % poly.Count];

                if ((a.Y > p.Y) == (b.Y > p.Y))
                    continue;

                double x = a.X + (b.X - a.X) * (p.Y - a.Y) / (b.Y - a.Y);

                if (x > p.X)
                    crossings++;
            }

            return crossings % 2 != 0;
        }

        private double DistPointToSegment(PointF p, PointF a, PointF b)
        {
            double dx = b.X - a.X;
            double dy = b.Y - a.Y;

            double lengthSquared = dx * dx + dy * dy;

            double t = 0;

            if (lengthSquared > 0)
            {
                t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / lengthSquared;
            }

            if (t < 0) t = 0;
            else if (t > 1) t = 1;

            double nearestX = a.X + t * dx;
            double nearestY = a.Y + t * dy;

            double diffX = p.X - nearestX;
            double diffY = p.Y - nearestY;

            return Math.Sqrt((diffX * diffX) + (diffY * diffY));
        }

    }
}