namespace lab03
{
    public partial class task1c : Form
    {
        Bitmap Canvas;
        bool isDrawing = false;
        Point lastPoint;
        List<Point> border = new List<Point>();

        public task1c()
        {
            InitializeComponent();

            Canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            pictureBox1.Image = Canvas;
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


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;
            }
            else if (e.Button == MouseButtons.Right)
            {
                FindBorder(e.X, e.Y);
                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing) return;

            DrawBresenham(lastPoint.X, lastPoint.Y, e.X, e.Y);

            lastPoint = e.Location;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(Canvas))
            {
                g.Clear(Color.White);
            }

            pictureBox1.Invalidate();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Изображения|*.png;*.bmp;*.jpg;*.jpeg";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            Canvas = new Bitmap(dialog.FileName);
            pictureBox1.Image = Canvas;
        }

        private void FindBorder(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Canvas.Width || y >= Canvas.Height) return;

            Color borderColor = Canvas.GetPixel(x, y);

            Point[] directions =
            {
                new Point(1, 0),    // 0
                new Point(1, -1),   // 1
                new Point(0, -1),   // 2
                new Point(-1, -1),  // 3
                new Point(-1, 0),   // 4
                new Point(-1, 1),   // 5
                new Point(0, 1),    // 6
                new Point(1, 1)     // 7
            };

            border.Clear();

            Point start = new Point(x, y);
            Point current = start;

            int direction = 6;
            bool firstStep = true;

            do
            {
                border.Add(current);

                int searchDirection;

                if (firstStep)
                    searchDirection = 6;
                else
                    searchDirection = (direction + 6) % 8;

                bool found = false;

                for (int i = 0; i < 8; i++)
                {
                    int d = (searchDirection + i) % 8;

                    int nx = current.X + directions[d].X;
                    int ny = current.Y + directions[d].Y;

                    if (nx >= 0 && ny >= 0 &&
                        nx < Canvas.Width && ny < Canvas.Height &&
                        Canvas.GetPixel(nx, ny).ToArgb() == borderColor.ToArgb())
                    {
                        current = new Point(nx, ny);
                        direction = d;
                        found = true;
                        break;
                    }
                }

                if (!found) break;

                firstStep = false;

            } while (current != start && border.Count < Canvas.Width * Canvas.Height);

            foreach (Point point in border)
            {
                Canvas.SetPixel(point.X, point.Y, Color.Red);
            }

            pictureBox1.Invalidate();

          
        }
    }
}

