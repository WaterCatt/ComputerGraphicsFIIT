using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab4
{
    public partial class Form1
    {
        private void Button5_Click(object sender, EventArgs e)
        {
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }
            label6.Text = "Кликните точку для поворота.";
            currentMode = Mode.PickPoint;
            pendingAction = "rotate";
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }
            label6.Text = "Кликните точку для масштабирования.";
            currentMode = Mode.PickPoint;
            pendingAction = "scale";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            label6.Text = "Режим динамического ребра.";
            currentMode = Mode.DynamicEdge;
            dynamicEdgeStart = null;
            isDrawingEdge = false;
        }

        private void Part2_PickPointClick(PointF p)
        {
            userPoint = p;
            if (selectedPolygon < 0)
            {
                label6.Text = "Полигон не выбран.";
                return;
            }

            if (pendingAction == "rotate")
            {
                double angle;
                if (!ParseDouble(textBox3.Text, out angle))
                {
                    label6.Text = "Ошибка угла.";
                    return;
                }
                var m = RotationAroundPointMatrix(p.X, p.Y, angle);
                polygons[selectedPolygon] = ApplyMatrixToPolygon(polygons[selectedPolygon], m);
                label6.Text = $"Поворот {angle}° вокруг ({p.X:F1},{p.Y:F1})";
            }
            else if (pendingAction == "scale")
            {
                double s;
                if (!ParseDouble(textBox4.Text, out s))
                {
                    label6.Text = "Ошибка масштаба.";
                    return;
                }
                var m = ScalingAroundPointMatrix(p.X, p.Y, s, s);
                polygons[selectedPolygon] = ApplyMatrixToPolygon(polygons[selectedPolygon], m);
                label6.Text = $"Масштаб x{s} вокруг ({p.X:F1},{p.Y:F1})";
            }

            userPoint = null;
            pendingAction = null;
            currentMode = Mode.Idle;
            RedrawAll();
        }

        private void Part2_DynamicEdgeClick(PointF p)
        {
            if (dynamicEdgeStart == null)
            {
                dynamicEdgeStart = p;
                dynamicEdgeEnd = p;
                isDrawingEdge = true;
            }
            else
            {
                isDrawingEdge = false;
                dynamicEdgeEnd = p;
                FindIntersections(dynamicEdgeStart.Value, dynamicEdgeEnd);
                polygons.Add(new List<PointF> { dynamicEdgeStart.Value, dynamicEdgeEnd });
                dynamicEdgeStart = null;
                selectedPolygon = polygons.Count - 1;
                currentMode = Mode.Idle;
            }
            RedrawAll();
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentMode == Mode.DynamicEdge && isDrawingEdge)
            {
                dynamicEdgeEnd = e.Location;
                RedrawAll();
            }
        }

        private void FindIntersections(PointF p1, PointF p2)
        {
            List<PointF> res = new List<PointF>();

            foreach (var poly in polygons)
            {
                if (poly.Count < 2)
                    continue;

                if (poly.Count == 2)
                {
                    PointF a = poly[0], b = poly[1];
                    if (EdgeIntersect(p1, p2, a, b, out PointF inter))
                        res.Add(inter);
                    continue;
                }

                for (int i = 0; i < poly.Count; i++)
                {
                    PointF a = poly[i];
                    PointF b = poly[(i + 1) % poly.Count];
                    if (EdgeIntersect(p1, p2, a, b, out PointF inter))
                        res.Add(inter);
                }
            }

            if (res.Count == 0)
                label6.Text = "Пересечений нет.";
            else
                label6.Text = "Пересечения: " + string.Join("; ", res.Select(p => $"({p.X:F1},{p.Y:F1})"));
        }
        private bool EdgeIntersect(PointF A, PointF B, PointF C, PointF D, out PointF P)
        {
            P = new PointF();

            PointF CD = new PointF(D.X - C.X, D.Y - C.Y);
            PointF n = new PointF(-CD.Y, CD.X);
            PointF AB = new PointF(B.X - A.X, B.Y - A.Y);

            float denom = n.X * AB.X + n.Y * AB.Y;
            if (Math.Abs(denom) < 1e-6f)
                return false;

            float t = -(n.X * (A.X - C.X) + n.Y * (A.Y - C.Y)) / denom;

            if (t < 0 || t > 1)
                return false;

            P = new PointF(A.X + t * AB.X, A.Y + t * AB.Y);

            float t1;
            if (Math.Abs(CD.X) > Math.Abs(CD.Y))
                t1 = (P.X - C.X) / CD.X;
            else
                t1 = (P.Y - C.Y) / CD.Y;

            return (t1 >= 0 && t1 <= 1);
        }
    }
}