namespace lab5
{
    partial class Task2
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelCanvas;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.TrackBar trackRoughness;
        private System.Windows.Forms.TrackBar trackIterations;
        private System.Windows.Forms.TrackBar trackDelay;
        private System.Windows.Forms.Label labelRoughness;
        private System.Windows.Forms.Label labelIterations;
        private System.Windows.Forms.Label labelDelay;
        private System.Windows.Forms.Label labelRoughnessValue;
        private System.Windows.Forms.Label labelIterationsValue;
        private System.Windows.Forms.Label labelDelayValue;
        private System.Windows.Forms.Button buttonGenerate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelCanvas = new Panel();
            panelControls = new Panel();
            buttonGenerate = new Button();
            labelRoughness = new Label();
            trackRoughness = new TrackBar();
            labelRoughnessValue = new Label();
            labelIterations = new Label();
            trackIterations = new TrackBar();
            labelIterationsValue = new Label();
            labelDelay = new Label();
            trackDelay = new TrackBar();
            labelDelayValue = new Label();
            panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackRoughness).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackIterations).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackDelay).BeginInit();
            SuspendLayout();
            // 
            // panelCanvas
            // 
            panelCanvas.BackColor = Color.White;
            panelCanvas.BorderStyle = BorderStyle.Fixed3D;
            panelCanvas.Dock = DockStyle.Fill;
            panelCanvas.Location = new Point(0, 100);
            panelCanvas.Name = "panelCanvas";
            panelCanvas.Size = new Size(800, 350);
            panelCanvas.TabIndex = 0;
            panelCanvas.Paint += PanelCanvas_Paint;
            panelCanvas.Resize += PanelCanvas_Resize;
            // 
            // panelControls
            // 
            panelControls.Controls.Add(buttonGenerate);
            panelControls.Controls.Add(labelRoughness);
            panelControls.Controls.Add(trackRoughness);
            panelControls.Controls.Add(labelRoughnessValue);
            panelControls.Controls.Add(labelIterations);
            panelControls.Controls.Add(trackIterations);
            panelControls.Controls.Add(labelIterationsValue);
            panelControls.Controls.Add(labelDelay);
            panelControls.Controls.Add(trackDelay);
            panelControls.Controls.Add(labelDelayValue);
            panelControls.Dock = DockStyle.Top;
            panelControls.Location = new Point(0, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(800, 100);
            panelControls.TabIndex = 1;
            // 
            // buttonGenerate
            // 
            buttonGenerate.Location = new Point(278, 61);
            buttonGenerate.Name = "buttonGenerate";
            buttonGenerate.Size = new Size(269, 30);
            buttonGenerate.TabIndex = 9;
            buttonGenerate.Text = "Generate";
            buttonGenerate.UseVisualStyleBackColor = true;
            buttonGenerate.Click += ButtonGenerate_Click;
            // 
            // labelRoughness
            // 
            labelRoughness.AutoSize = true;
            labelRoughness.Location = new Point(12, 15);
            labelRoughness.Name = "labelRoughness";
            labelRoughness.Size = new Size(68, 15);
            labelRoughness.TabIndex = 0;
            labelRoughness.Text = "Roughness:";
            // 
            // trackRoughness
            // 
            trackRoughness.Location = new Point(80, 10);
            trackRoughness.Maximum = 20;
            trackRoughness.Minimum = 1;
            trackRoughness.Name = "trackRoughness";
            trackRoughness.Size = new Size(150, 45);
            trackRoughness.TabIndex = 1;
            trackRoughness.Value = 10;
            trackRoughness.ValueChanged += TrackRoughness_ValueChanged;
            // 
            // labelRoughnessValue
            // 
            labelRoughnessValue.AutoSize = true;
            labelRoughnessValue.Location = new Point(236, 15);
            labelRoughnessValue.Name = "labelRoughnessValue";
            labelRoughnessValue.Size = new Size(22, 15);
            labelRoughnessValue.TabIndex = 2;
            labelRoughnessValue.Text = "1.0";
            // 
            // labelIterations
            // 
            labelIterations.AutoSize = true;
            labelIterations.Location = new Point(556, 15);
            labelIterations.Name = "labelIterations";
            labelIterations.Size = new Size(59, 15);
            labelIterations.TabIndex = 3;
            labelIterations.Text = "Iterations:";
            // 
            // trackIterations
            // 
            trackIterations.Location = new Point(624, 10);
            trackIterations.Maximum = 12;
            trackIterations.Minimum = 1;
            trackIterations.Name = "trackIterations";
            trackIterations.Size = new Size(150, 45);
            trackIterations.TabIndex = 4;
            trackIterations.Value = 5;
            trackIterations.ValueChanged += TrackIterations_ValueChanged;
            // 
            // labelIterationsValue
            // 
            labelIterationsValue.AutoSize = true;
            labelIterationsValue.Location = new Point(780, 15);
            labelIterationsValue.Name = "labelIterationsValue";
            labelIterationsValue.Size = new Size(13, 15);
            labelIterationsValue.TabIndex = 5;
            labelIterationsValue.Text = "5";
            // 
            // labelDelay
            // 
            labelDelay.AutoSize = true;
            labelDelay.Location = new Point(278, 15);
            labelDelay.Name = "labelDelay";
            labelDelay.Size = new Size(65, 15);
            labelDelay.TabIndex = 6;
            labelDelay.Text = "Step Delay:";
            // 
            // trackDelay
            // 
            trackDelay.Location = new Point(347, 10);
            trackDelay.Maximum = 2000;
            trackDelay.Minimum = 100;
            trackDelay.Name = "trackDelay";
            trackDelay.Size = new Size(150, 45);
            trackDelay.TabIndex = 7;
            trackDelay.TickFrequency = 100;
            trackDelay.Value = 500;
            trackDelay.ValueChanged += TrackDelay_ValueChanged;
            // 
            // labelDelayValue
            // 
            labelDelayValue.AutoSize = true;
            labelDelayValue.Location = new Point(503, 15);
            labelDelayValue.Name = "labelDelayValue";
            labelDelayValue.Size = new Size(44, 15);
            labelDelayValue.TabIndex = 8;
            labelDelayValue.Text = "500 ms";
            // 
            // Task2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelCanvas);
            Controls.Add(panelControls);
            Name = "Task2";
            Text = "Task2 - Midpoint Displacement";
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackRoughness).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackIterations).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackDelay).EndInit();
            ResumeLayout(false);
        }
    }
}