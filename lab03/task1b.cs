namespace lab03
{
    public partial class task1b : Form
    {
        Bitmap Canvas;
        Bitmap? texture;
        bool isDrawing = false;
        Point lastPoint;

        public task1b()
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
                Fill(e.X, e.Y);
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


        private void Fill(int x, int y)
        {
            if (texture == null)
            {
                MessageBox.Show("Нужно загрузить картинку!");
                return;
            }

            if (x < 0 || y < 0 || x >= Canvas.Width || y >= Canvas.Height)
                return;

            Color oldColor = Canvas.GetPixel(x, y);
            bool[,] visited = new bool[Canvas.Width, Canvas.Height];

            FillLine(x, y, oldColor, visited);
        }

        private void FillLine(int x, int y, Color oldColor, bool[,] visited)
        {
            if (x < 0 || y < 0 || x >= Canvas.Width || y >= Canvas.Height) return;

            if (visited[x, y] || Canvas.GetPixel(x, y).ToArgb() != oldColor.ToArgb()) return;

            int left = x;
            int right = x;

            while (left >= 0 && !visited[left, y] && Canvas.GetPixel(left, y).ToArgb() == oldColor.ToArgb())
            {
                left--;
            }

            while (right < Canvas.Width && !visited[right, y] && Canvas.GetPixel(right, y).ToArgb() == oldColor.ToArgb())
            {
                right++;
            }

            left++;
            right--;

            for (int i = left; i <= right; i++)
            {
                visited[i, y] = true;

                int textureX = i % texture!.Width;
                int textureY = y % texture.Height;

                Canvas.SetPixel(i, y, texture.GetPixel(textureX, textureY));
            }

            for (int i = left; i <= right; i++)
            {
                if (y > 0 && !visited[i, y - 1] && Canvas.GetPixel(i, y - 1).ToArgb() == oldColor.ToArgb())
                {
                    FillLine(i, y - 1, oldColor, visited);
                }

                if (y < Canvas.Height - 1 && !visited[i, y + 1] && Canvas.GetPixel(i, y + 1).ToArgb() == oldColor.ToArgb())
                {
                    FillLine(i, y + 1, oldColor, visited);
                }
            }
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            texture = new Bitmap(dialog.FileName);
            picturePreview.Image = texture;
        }
    }
}
