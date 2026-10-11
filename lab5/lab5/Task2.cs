using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace lab5
{
    public partial class Task2 : Form
    {
        private List<PointF> points = new List<PointF>();
        private System.Windows.Forms.Timer animationTimer;
        private int currentStep;
        private int totalSteps;
        private double currentRoughness;
        private double initialRoughness;
        private Random random = new Random();

        public Task2()
        {
            InitializeComponent();
            animationTimer = new System.Windows.Forms.Timer();
            animationTimer.Tick += AnimationTimer_Tick;
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            labelRoughnessValue.Text = (trackRoughness.Value / 10.0).ToString("F1");
            labelIterationsValue.Text = trackIterations.Value.ToString();
            labelDelayValue.Text = trackDelay.Value.ToString() + " ms";
        }

        private void TrackRoughness_ValueChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void TrackIterations_ValueChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void TrackDelay_ValueChanged(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            StartGeneration();
        }

        private void PanelCanvas_Resize(object sender, EventArgs e)
        {
            if (points.Count > 0)
            {
                StartGeneration();
            }
        }

        private void StartGeneration()
        {
            animationTimer.Stop();

            int width = panelCanvas.Width;
            int height = panelCanvas.Height;

            if (width < 10 || height < 10)
                return;

            points.Clear();
            points.Add(new PointF(0, height / 2.0f));
            points.Add(new PointF(width, height / 2.0f));

            totalSteps = trackIterations.Value;
            currentStep = 0;
            initialRoughness = trackRoughness.Value / 10.0 * (height / 2.0);
            currentRoughness = initialRoughness;

            animationTimer.Interval = trackDelay.Value;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (currentStep >= totalSteps)
            {
                animationTimer.Stop();
                return;
            }

            currentStep++;
            PerformStep();
            currentRoughness /= 2.0;

            panelCanvas.Invalidate();
        }

        private void PerformStep()
        {
            List<PointF> newPoints = new List<PointF>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                PointF p1 = points[i];
                PointF p2 = points[i + 1];

                float midX = (p1.X + p2.X) / 2.0f;
                float midY = (p1.Y + p2.Y) / 2.0f;

                double displacement = (random.NextDouble() * 2.0 - 1.0) * currentRoughness;
                midY += (float)displacement;

                newPoints.Add(p1);
                newPoints.Add(new PointF(midX, midY));
            }

            newPoints.Add(points[points.Count - 1]);
            points = newPoints;
        }

        private void PanelCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (points.Count < 2)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(Color.Black, 2))
            {
                for (int i = 0; i < points.Count - 1; i++)
                    e.Graphics.DrawLine(pen, points[i], points[i + 1]);
            }
        }
    }
}