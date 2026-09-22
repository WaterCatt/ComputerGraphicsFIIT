using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FastBitmap;

namespace LAB2
{
    public partial class Task2 : Form
    {
        private readonly PictureBox pictureOriginal;
        private readonly PictureBox pictureRed;
        private readonly PictureBox pictureGreen;
        private readonly PictureBox pictureBlue;
        private readonly PictureBox histRed;
        private readonly PictureBox histGreen;
        private readonly PictureBox histBlue;
        private readonly Button buttonLoad;

        public Task2()
        {
            InitializeComponent();

            this.Text = "Выделение каналов из изображения";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            buttonLoad = new Button();
            buttonLoad.Text = "Загрузить изображение";
            buttonLoad.Dock = DockStyle.Top;
            buttonLoad.Click += ButtonLoad_Click;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 3,
                ColumnCount = 1
            };
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 34));
            mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33));

            var imagesPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 3,
            };
            for (int i = 0; i < 3; i++)
                imagesPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            var histPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 1,
                ColumnCount = 3
            };
            for (int i = 0; i < 3; i++)
                histPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33));

            pictureOriginal = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pictureRed = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pictureGreen = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pictureBlue = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            histRed = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            histGreen = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage
            };
            histBlue = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.CenterImage
            };

            imagesPanel.Controls.Add(pictureRed, 0, 0);
            imagesPanel.Controls.Add(pictureGreen, 1, 0);
            imagesPanel.Controls.Add(pictureBlue, 2, 0);

            histPanel.Controls.Add(histRed, 0, 0);
            histPanel.Controls.Add(histGreen, 1, 0);
            histPanel.Controls.Add(histBlue, 2, 0);

            mainPanel.Controls.Add(pictureOriginal, 0, 0);
            mainPanel.Controls.Add(imagesPanel, 0, 1);
            mainPanel.Controls.Add(histPanel, 0, 2);

            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonLoad);
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    DisposeImages();

                    Bitmap original;
                    using (var fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read))
                    using (var tmp = new Bitmap(fs))
                    {
                        original = new Bitmap(tmp);
                    }

                    pictureOriginal.Image = original;

                    pictureRed.Image = ExtractChannel(original, "R");
                    pictureGreen.Image = ExtractChannel(original, "G");
                    pictureBlue.Image = ExtractChannel(original, "B");

                    histRed.Image = CreateHistogram(original, "R");
                    histGreen.Image = CreateHistogram(original, "G");
                    histBlue.Image = CreateHistogram(original, "B");
                }
            }
        }

        private void DisposeImages()
        {
            pictureOriginal.Image?.Dispose();
            pictureRed.Image?.Dispose();
            pictureGreen.Image?.Dispose();
            pictureBlue.Image?.Dispose();
            histRed.Image?.Dispose();
            histGreen.Image?.Dispose();
            histBlue.Image?.Dispose();

            pictureOriginal.Image = null;
            pictureRed.Image = null;
            pictureGreen.Image = null;
            pictureBlue.Image = null;
            histRed.Image = null;
            histGreen.Image = null;
            histBlue.Image = null;
        }

        private Bitmap ExtractChannel(Bitmap img, string channel)
        {
            return img.Select(c =>
            {
                switch (channel)
                {
                    case "R": return Color.FromArgb(c.R, 0, 0);
                    case "G": return Color.FromArgb(0, c.G, 0);
                    default: return Color.FromArgb(0, 0, c.B);
                }
            });
        }

        private Bitmap CreateHistogram(Bitmap img, string channel)
        {
            int[] histogram = new int[256];
            img.ForEach(c =>
            {
                int v = channel == "R" ? c.R : channel == "G" ? c.G : c.B;
                histogram[v]++;
            });

            int maxVal = 0;
            foreach (var v in histogram)
                if (v > maxVal)
                    maxVal = v;

            int width = 512, height = 220;
            int marginLeft = 10, marginRight = 10, marginTop = 30, marginBottom = 15;

            Bitmap histImg = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(histImg))
            {
                g.Clear(Color.White);

                int plotWidth = width - marginLeft - marginRight;
                int plotHeight = height - marginTop - marginBottom;
                int baseY = height - marginBottom;

                for (int i = 0; i < 256; i++)
                {
                    int xLeft = marginLeft + (int)Math.Round((double)i * plotWidth / 256);
                    int xRight = marginLeft + (int)Math.Round((double)(i + 1) * plotWidth / 256);
                    int barWidth = Math.Max(1, xRight - xLeft);

                    int barHeight = maxVal == 0 ? 0 : (int)((double)histogram[i] / maxVal * plotHeight);

                    if (barHeight > 0)
                        g.FillRectangle(Brushes.Black, xLeft, baseY - barHeight, barWidth, barHeight);
                }

                using (var font = new Font("Arial", 14))
                {
                    g.DrawString($"Гистограмма {channel}", font, Brushes.Black,
                        new PointF(marginLeft + 110, 5));
                }
            }
            return histImg;
        }
    }
}