using System;
using System.Drawing;
using System.Windows.Forms;
using FastBitmap;

namespace LAB2
{
    public partial class Task3 : Form
    {
        private PictureBox pictureBox;
        private TrackBar sliderHue;
        private TrackBar sliderSaturation;
        private TrackBar sliderValue;
        private Button buttonLoad;
        private Button buttonProcess;
        private Button buttonSave;
        private Bitmap originalImage;
        private Bitmap processedImage;

        public Task3()
        {
            InitializeComponent();

            this.Text = "Преобразование RGB изображения в HSV";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            mainPanel.Controls.Add(pictureBox, 0, 0);

            var controlsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true
            };

            buttonLoad = new Button
            {
                Text = "Загрузить изображение",
                Width = 200
            };
            buttonLoad.Click += ButtonLoad_Click;
            controlsPanel.Controls.Add(buttonLoad);

            sliderHue = new TrackBar
            {
                Minimum = 0,
                Maximum = 360,
                TickFrequency = 60,
                Width = 200
            };
            controlsPanel.Controls.Add(new Label() { Text = "Hue" });
            controlsPanel.Controls.Add(sliderHue);

            sliderSaturation = new TrackBar
            {
                Minimum = -100,
                Maximum = 100,
                TickFrequency = 20,
                Width = 200
            };
            controlsPanel.Controls.Add(new Label() { Text = "Saturation" });
            controlsPanel.Controls.Add(sliderSaturation);

            sliderValue = new TrackBar
            {
                Minimum = -100,
                Maximum = 100,
                TickFrequency = 20,
                Width = 200
            };
            controlsPanel.Controls.Add(new Label() { Text = "Value" });
            controlsPanel.Controls.Add(sliderValue);

            buttonProcess = new Button
            {
                Text = "Обработать",
                Width = 200
            };
            buttonProcess.Click += ButtonProcess_Click;
            controlsPanel.Controls.Add(buttonProcess);

            buttonSave = new Button
            {
                Text = "Сохранить результат",
                Width = 200
            };
            buttonSave.Click += ButtonSave_Click;
            controlsPanel.Controls.Add(buttonSave);

            mainPanel.Controls.Add(controlsPanel, 1, 0);
            this.Controls.Add(mainPanel);
        }

        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = null;
                    originalImage?.Dispose();
                    processedImage?.Dispose();

                    originalImage = new Bitmap(ofd.FileName);
                    processedImage = new Bitmap(originalImage);
                    pictureBox.Image = processedImage;
                }
                sliderHue.Value = 0;
                sliderSaturation.Value = 0;
                sliderValue.Value = 0;
            }
        }

        private void ButtonProcess_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Сначала загрузите изображение!");
                return;
            }

            int hShift = sliderHue.Value;
            double sShift = sliderSaturation.Value / 100.0;
            double vShift = sliderValue.Value / 100.0;

            Bitmap bmp = new Bitmap(originalImage);

            using (var fast = new FastBitmap.FastBitmap(bmp))
            {
                for (int y = 0; y < fast.Height; y++)
                {
                    for (int x = 0; x < fast.Width; x++)
                    {
                        Color c = fast[x, y];

                        double h, s, v;
                        RgbToHsv(c, out h, out s, out v);

                        h = (h + hShift) % 360;
                        if (h < 0)
                            h += 360;
                        s = Math.Min(1, Math.Max(0, s + sShift));
                        v = Math.Min(1, Math.Max(0, v + vShift));

                        fast[x, y] = HsvToRgb(h, s, v);
                    }
                }
            }

            pictureBox.Image = null;
            processedImage?.Dispose();

            processedImage = bmp;
            pictureBox.Image = processedImage;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (processedImage == null)
            {
                MessageBox.Show("Нет изображения для сохранения!");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap|*.bmp";
                sfd.FileName = "processed_image.jpg";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    processedImage.Save(sfd.FileName);
                    MessageBox.Show("Изображение сохранено: " + sfd.FileName);
                }
            }
        }

        private void RgbToHsv(Color color, out double h, out double s, out double v)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            v = max;

            double delta = max - min;

            if (max == 0)
                s = 0;
            else
                s = 1 - (min / max);

            if (delta == 0)
                h = 0;
            else if (max == r)
            {
                if (g >= b)
                    h = 60 * ((g - b) / delta);
                else
                    h = 60 * ((g - b) / delta) + 360;
            }
            else if (max == g)
                h = 60 * ((b - r) / delta) + 120;
            else
                h = 60 * ((r - g) / delta) + 240;
        }

        private Color HsvToRgb(double h, double s, double v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;

            double vMin = (1 - s) * v;
            double a = (v - vMin) * (h % 60.0) / 60.0;
            double vInc = vMin + a;
            double vDec = v - a;

            double r = 0, g = 0, b = 0;

            switch (hi)
            {
                case 0: r = v; g = vInc; b = vMin; break;
                case 1: r = vDec; g = v; b = vMin; break;
                case 2: r = vMin; g = v; b = vInc; break;
                case 3: r = vMin; g = vDec; b = v; break;
                case 4: r = vInc; g = vMin; b = v; break;
                case 5: r = v; g = vMin; b = vDec; break;
            }

            byte r1 = (byte)Math.Min(255, Math.Max(0, (int)(r * 255 + 0.5)));
            byte g1 = (byte)Math.Min(255, Math.Max(0, (int)(g * 255 + 0.5)));
            byte b1 = (byte)Math.Min(255, Math.Max(0, (int)(b * 255 + 0.5)));

            return Color.FromArgb(r1, g1, b1);
        }

        private void Task3_Load(object sender, EventArgs e)
        {
        }
    }
}