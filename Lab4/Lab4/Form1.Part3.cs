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
            bool inside = false;

            for (int i = 0, j = poly.Count - 1; i < poly.Count; j = i++)
            {
                if (((poly[i].Y > p.Y) != (poly[j].Y > p.Y)) && (p.X < (poly[j].X - poly[i].X) * (p.Y - poly[i].Y) / (poly[j].Y - poly[i].Y) + poly[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        private double DistPointToSegment(PointF p, PointF a, PointF b)
        {
            double dx = b.X - a.X, dy = b.Y - a.Y;
            double t = ((p.X - a.X) * dx + (p.Y - a.Y) * dy) / (dx * dx + dy * dy);
            if (t < 0)
                t = 0;
            else if (t > 1)
                t = 1;
            double x = a.X + t * dx;
            double y = a.Y + t * dy;
            return Math.Sqrt((p.X - x) * (p.X - x) + (p.Y - y) * (p.Y - y));
        }

    }
}