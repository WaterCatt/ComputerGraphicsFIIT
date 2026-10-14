using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace lab5
{
    public partial class Task1 : Form
    {
        string atom = "";
        double angle;
        double startDir;

        Dictionary<char, string> rules = new Dictionary<char, string>();
        List<(PointF p1, PointF p2, int level)> lines = new List<(PointF p1, PointF p2, int level)>();
        Random random = new Random();

        public Task1()
        {
            InitializeComponent();
        }


        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt";

            if (dialog.ShowDialog() != DialogResult.OK) return;

            labelFile.Text = dialog.FileName;

            string[] fileLines = File.ReadAllLines(dialog.FileName);
            string[] firstLine = fileLines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            atom = firstLine[0];
            angle = double.Parse(firstLine[1]);
            startDir = double.Parse(firstLine[2]);

            rules.Clear();

            for (int i = 1; i < fileLines.Length; i++)
            {
                rules.Add(fileLines[i][0], fileLines[i].Substring(3));
            }
        }

        private void buttonBuild_Click(object sender, EventArgs e)
        {
            if (atom == "")
            {
                MessageBox.Show("Сначала загрузите файл.");
                return;
            }

            string commands = buildString((int)iterBox.Value);

            createLines(commands);
            drawPanel.Invalidate();
        }


        private string buildString(int iterations)
        {
            string current = atom;

            for (int i = 0; i < iterations; i++)
            {
                string next = "";

                foreach (char symbol in current)
                {
                    if (rules.ContainsKey(symbol))
                    {
                        next += rules[symbol];
                    }
                    else
                    {
                        next += symbol;
                    }
                }
                current = next;
            }

            return current;
        }

        private void createLines(string s)
        {
            lines.Clear();

            PointF pos = new PointF(0, 0);
            double dir = startDir;
            int level = 0;

            Stack<(PointF position, double direction, int level)> stack = new Stack<(PointF position, double direction, int level)>();

            foreach (char sym in s)
            {

                if (sym == 'F')
                {
                    double rads = dir * Math.PI/180;
                    float x = pos.X + (float) Math.Cos(rads);
                    float y = pos.Y - (float) Math.Sin(rads);

                    PointF newPos = new PointF(x, y);

                    lines.Add((pos, newPos, level));
                    level++;
                    pos = newPos;
                }
                else if (sym == '+')
                {
                    double randomAngle = (double)randBox.Value;
                    dir += angle + (random.NextDouble()*2-1) * randomAngle;
                }
                else if (sym == '-')
                {
                    double randAngle = (double)randBox.Value;
                    dir -= angle + (random.NextDouble()*2-1) * randAngle;
                }
                else if (sym == '[')
                {
                    stack.Push((pos, dir, level));
                }
                else if (sym == ']')
                {
                    (pos, dir, level) = stack.Pop();
                }

            }
        }


        private void drawPanel_Paint(object sender, PaintEventArgs e)
        {
            if (lines.Count == 0) return;

            float minX = lines[0].p1.X;
            float maxX = lines[0].p1.X;
            float minY = lines[0].p1.Y;
            float maxY = lines[0].p1.Y;

            foreach (var line in lines)
            {
                minX = Math.Min(minX, Math.Min(line.p1.X, line.p2.X));
                maxX = Math.Max(maxX, Math.Max(line.p1.X, line.p2.X));

                minY = Math.Min(minY, Math.Min(line.p1.Y, line.p2.Y));
                maxY = Math.Max(maxY, Math.Max(line.p1.Y, line.p2.Y));

            }

            float graphWidth = maxX - minX;
            float graphHeight = maxY - minY;

            if (graphWidth == 0) graphWidth = 1;
            if (graphHeight == 0) graphHeight = 1;

            float mult = Math.Min((drawPanel.Width-20)/graphWidth, (drawPanel.Height-20)/graphHeight);

            float width = graphWidth * mult;
            float height = graphHeight * mult;

            float startX = (drawPanel.Width - width)/2;
            float startY = (drawPanel.Height - height)/2;

            int maxLevel = 1;
            foreach (var line in lines)
            {
                if (line.level > maxLevel)
                    maxLevel = line.level;
            }

            foreach (var line in lines)
            {
                float x1 = (line.p1.X - minX)*mult + startX;
                float y1 = (line.p1.Y - minY)*mult + startY;

                float x2 = (line.p2.X - minX)*mult + startX;
                float y2 = (line.p2.Y - minY)*mult + startY;

                if (!checkBox1.Checked)
                {
                    using (Pen pen = new Pen(Color.Black, (float)widthBox.Value))
                    {
                        e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }
                else // 1b
                {
                    float k = (float) line.level/maxLevel;

                    float w = 8 - 7*k;

                    int r = (int) (140 * (1-k));
                    int g = (int) (70 * (1-k) + 130*k);
                    int b = (int) (20 * (1-k));

                    // Коричневый - 140 70 20
                    // Зеленый - 0 130 0

                    Color c = Color.FromArgb(r, g, b);

                    using (Pen pen = new Pen(c, w))
                    {
                        e.Graphics.DrawLine(pen, x1, y1, x2, y2);
                    }
                }

            }
        }

        private void drawPanel_Resize(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }

        private void widthBox_ValueChanged(object sender, EventArgs e)
        {
            drawPanel.Invalidate();
        }
    }
}