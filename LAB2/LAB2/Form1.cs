namespace LAB2
{
    public partial class Task1 : Form
    {
        Bitmap? originalImage;
        Bitmap? grayImageNTSC;
        Bitmap? grayImageHDTV;
        Bitmap? grayImageDif;

        int[]? histogramNTSC;
        int[]? histogramHDTV;
        int[]? histogramDif;
        public Task1()
        {
            InitializeComponent();
        }

        Bitmap? GetGrayImage(float a, float b, float c, out int[] histogram)
        {
            histogram = new int[256];
            if (originalImage == null) return null;

            Bitmap grayImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int y = 0; y < originalImage.Height; y++)
            {
                for (int x = 0; x < originalImage.Width; x++)
                {
                    Color pixel = originalImage.GetPixel(x, y);

                    int g = (int)(a * pixel.R + b * pixel.G + c * pixel.B);

                    histogram[g]++;

                    Color grayPixel = Color.FromArgb(pixel.A, g, g, g);

                    grayImage.SetPixel(x, y, grayPixel);
                }
            }

            return grayImage;
        }

        Bitmap? GetDifferenceImage(out int[] histogram)
        {
            histogram = new int[256];
            if (grayImageNTSC == null || grayImageHDTV == null)
                return null;

            Bitmap result = new Bitmap(grayImageNTSC.Width, grayImageNTSC.Height);

            for (int y = 0; y < grayImageNTSC.Height; y++)
            {
                for (int x = 0; x < grayImageNTSC.Width; x++)
                {
                    int dif = Math.Abs(grayImageNTSC.GetPixel(x, y).R - grayImageHDTV.GetPixel(x, y).R);

                    histogram[dif]++;

                    Color grayPixel = Color.FromArgb(dif, dif, dif);

                    result.SetPixel(x, y, grayPixel);
                }
            }

            return result;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Загрузите изображение",
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            originalImage = new Bitmap(dialog.FileName);

            grayImageNTSC = GetGrayImage(0.299f, 0.587f, 0.114f, out histogramNTSC);
            grayImageHDTV = GetGrayImage(0.2126f, 0.7152f, 0.0722f, out histogramHDTV);
            grayImageDif = GetDifferenceImage(out histogramDif);


            pictureOriginal.Image = originalImage;
            pictureNTSC.Image = grayImageNTSC;
            pictureHDTV.Image = grayImageHDTV;
            pictureDif.Image = grayImageDif;

            panelNTSC.Invalidate();
            panelHDTV.Invalidate();
            panelDif.Invalidate();

        }

        void DrawHistogram(Graphics g, int[] histogram, int width, int height)
        {
            g.Clear(Color.White);

            int max = 0;

            for (int i = 0; i < 256; i++)
            {
                if (histogram[i] > max)
                    max = histogram[i];
            }

            if (max == 0)
                return;

            float barWidth = width / 256f;

            for (int i = 0; i < 256; i++)
            {
                float barHeight = (float)histogram[i] / max * (height - 5);
                float x = i * barWidth;
                float y = height - barHeight;

                g.FillRectangle(Brushes.Black, x, y, barWidth + 1, barHeight);
            }
        }

        private void panelNTSC_Paint(object sender, PaintEventArgs e)
        {
            if (histogramNTSC == null) return;
            DrawHistogram(e.Graphics, histogramNTSC, panelNTSC.ClientSize.Width, panelNTSC.ClientSize.Height);
        }

        private void panelHDTV_Paint(object sender, PaintEventArgs e)
        {
            if (histogramHDTV == null) return;
            DrawHistogram(e.Graphics, histogramHDTV, panelNTSC.ClientSize.Width, panelNTSC.ClientSize.Height);
        }

        private void panelDif_Paint(object sender, PaintEventArgs e)
        {
            if (histogramDif == null) return;
            DrawHistogram(e.Graphics, histogramDif, panelNTSC.ClientSize.Width, panelNTSC.ClientSize.Height);
        }
    }
}
