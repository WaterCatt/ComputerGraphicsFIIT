using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace lab03
{
    public partial class task2 : Form
    {
        private Bitmap Canvas;
        private Point? FirstPoint = null;

        private List<(Point Start, Point End, int Algorithm)> Lines = new List<(Point Start, Point End, int Algorithm)>();

        public task2()
        {
            InitializeComponent();

            Canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = Canvas;
            pictureBox1.BackColor = Color.White;
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;

            comboBox1.Items.Add("Брезенхем");
            comboBox1.Items.Add("Ву");
            comboBox1.SelectedIndex = 0;

            pictureBox1.MouseClick += PictureBox1_MouseClick;
            pictureBox1.SizeChanged += PictureBox1_SizeChanged;
            button1.Click += Button1_Click;
        }

        private void PictureBox1_SizeChanged(object sender, EventArgs e)
        {
            if (pictureBox1.Width <= 0 || pictureBox1.Height <= 0)
                return;

            Canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            RedrawAll();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Lines.Clear();
            FirstPoint = null;
            RedrawAll();
        }

        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                FirstPoint = null;
                RedrawAll();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (FirstPoint == null)
                {
                    FirstPoint = new Point(e.X, e.Y);
                    RedrawAll();
                }
                else
                {
                    Lines.Add((FirstPoint.Value, new Point(e.X, e.Y), comboBox1.SelectedIndex));
                    FirstPoint = null;
                    RedrawAll();
                }
            }
        }

        private void RedrawAll()
        {
            using (Graphics g = Graphics.FromImage(Canvas))
                g.Clear(Color.White);

            foreach (var line in Lines)
            {
                if (line.Algorithm == 0)
                    DrawBresenham(line.Start.X, line.Start.Y, line.End.X, line.End.Y);
                else
                    DrawWu(line.Start.X, line.Start.Y, line.End.X, line.End.Y);
            }

            if (FirstPoint.HasValue)
            {
                using (Graphics g = Graphics.FromImage(Canvas))
                using (SolidBrush br = new SolidBrush(Color.Black))
                    g.FillEllipse(br, FirstPoint.Value.X - 3, FirstPoint.Value.Y - 3, 3, 3);
            }

            pictureBox1.Image = Canvas;
            pictureBox1.Invalidate();
        }

        private void DrawBresenham(int x1, int y1, int x2, int y2)
        {
            int dx = Math.Abs(x2 - x1), sx = x1 < x2 ? 1 : -1;
            int dy = -Math.Abs(y2 - y1), sy = y1 < y2 ? 1 : -1;
            int err = dx + dy, e2;

            while (true)
            {
                if (x1 >= 0 && y1 >= 0 && x1 < Canvas.Width && y1 < Canvas.Height)
                    Canvas.SetPixel(x1, y1, Color.Black);

                if (x1 == x2 && y1 == y2)
                    break;
                e2 = 2 * err;
                if (e2 >= dy)
                {
                    err += dy;
                    x1 += sx;
                }
                if (e2 <= dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        private void DrawWu(int x0, int y0, int x1, int y1)
        {
            void Plot(int px, int py, double c)
            {
                if (px < 0 || py < 0 || px >= Canvas.Width || py >= Canvas.Height)
                    return;

                Color old = Canvas.GetPixel(px, py);
                int r = (int)(old.R * (1 - c));
                int g = (int)(old.G * (1 - c));
                int b = (int)(old.B * (1 - c));

                Canvas.SetPixel(px, py, Color.FromArgb(r, g, b));
            }

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                (x0, y0) = (y0, x0);
                (x1, y1) = (y1, x1);
            }
            if (x0 > x1)
            {
                (x0, x1) = (x1, x0);
                (y0, y1) = (y1, y0);
            }

            double dx = x1 - x0;
            double dy = y1 - y0;
            double gradient = dy / dx;

            double y = y0;
            for (int x = x0; x <= x1; x++)
            {
                if (steep)
                {
                    Plot((int)y, x, 1 - (y - Math.Floor(y)));
                    Plot((int)y + 1, x, y - Math.Floor(y));
                }
                else
                {
                    Plot(x, (int)y, 1 - (y - Math.Floor(y)));
                    Plot(x, (int)y + 1, y - Math.Floor(y));
                }
                y += gradient;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
    }
}