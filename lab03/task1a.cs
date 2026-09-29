namespace lab03
{
    public partial class task1a : Form
    {
        Bitmap Canvas;
        bool isDrawing = false;
        Point lastPoint;

        public task1a()
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
                Fill(e.X, e.Y, Color.Black);
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


        private void Fill(int x, int y, Color fillColor)
        {
            if (x < 0 || y < 0 || x >= Canvas.Width || y >= Canvas.Height) return;

            Color oldColor = Canvas.GetPixel(x, y);

            if (oldColor.ToArgb() == fillColor.ToArgb()) return;

            FillLine(x, y, oldColor, fillColor);
        }

        private void FillLine(int x, int y, Color oldColor, Color fillColor)
        {
            if (x < 0 || y < 0 || x >= Canvas.Width || y >= Canvas.Height)
                return;

            if (Canvas.GetPixel(x, y).ToArgb() != oldColor.ToArgb())
                return;

            int left = x;
            int right = x;

            while (left >= 0 && Canvas.GetPixel(left, y).ToArgb() == oldColor.ToArgb())
            {
                left--;
            }

            while (right < Canvas.Width && Canvas.GetPixel(right, y).ToArgb() == oldColor.ToArgb())
            {
                right++;
            }

            left++;
            right--;

            for (int i = left; i <= right; i++)
            {
                Canvas.SetPixel(i, y, fillColor);
            }

            for (int i = left; i <= right; i++)
            {
                if (y > 0 && Canvas.GetPixel(i, y - 1).ToArgb() == oldColor.ToArgb())
                {
                    FillLine(i, y - 1, oldColor, fillColor);
                }

                if (y < Canvas.Height - 1 && Canvas.GetPixel(i, y + 1).ToArgb() == oldColor.ToArgb())
                {
                    FillLine(i, y + 1, oldColor, fillColor);
                }
            }
        }

    }
}
