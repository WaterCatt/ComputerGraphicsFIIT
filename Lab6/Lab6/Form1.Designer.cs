namespace Lab6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel polyhedronDrawer;

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.ComboBox cmbFigure;
        private System.Windows.Forms.ComboBox cmbProjection;
        private System.Windows.Forms.Button btnReset;

        private System.Windows.Forms.Label lblTranslation;
        private System.Windows.Forms.NumericUpDown numTx;
        private System.Windows.Forms.NumericUpDown numTy;
        private System.Windows.Forms.NumericUpDown numTz;
        private System.Windows.Forms.Button btnTranslate;

        private System.Windows.Forms.Label lblScale;
        private System.Windows.Forms.NumericUpDown numSx;
        private System.Windows.Forms.NumericUpDown numSy;
        private System.Windows.Forms.NumericUpDown numSz;
        private System.Windows.Forms.Button btnScale;

        private System.Windows.Forms.Label lblRotateAxis;
        private System.Windows.Forms.ComboBox cmbAxis;
        private System.Windows.Forms.NumericUpDown numAxisAngle;
        private System.Windows.Forms.Button btnRotateAxis;

        private System.Windows.Forms.Label lblReflectCoord;
        private System.Windows.Forms.ComboBox cmbReflectCoord;
        private System.Windows.Forms.Button btnReflectCoord;

        private System.Windows.Forms.Label lblRotateLine;
        private System.Windows.Forms.Label lblP1;
        private System.Windows.Forms.NumericUpDown numLx1;
        private System.Windows.Forms.NumericUpDown numLy1;
        private System.Windows.Forms.NumericUpDown numLz1;
        private System.Windows.Forms.Label lblP2;
        private System.Windows.Forms.NumericUpDown numLx2;
        private System.Windows.Forms.NumericUpDown numLy2;
        private System.Windows.Forms.NumericUpDown numLz2;
        private System.Windows.Forms.Label lblAng;
        private System.Windows.Forms.NumericUpDown numLineAngle;
        private System.Windows.Forms.Button btnRotateLine;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            polyhedronDrawer = new Panel();
            panelControls = new Panel();
            cmbFigure = new ComboBox();
            cmbProjection = new ComboBox();
            btnReset = new Button();
            lblTranslation = new Label();
            numTx = new NumericUpDown();
            numTy = new NumericUpDown();
            numTz = new NumericUpDown();
            btnTranslate = new Button();
            lblScale = new Label();
            numSx = new NumericUpDown();
            numSy = new NumericUpDown();
            numSz = new NumericUpDown();
            btnScale = new Button();
            lblRotateAxis = new Label();
            cmbAxis = new ComboBox();
            numAxisAngle = new NumericUpDown();
            btnRotateAxis = new Button();
            lblReflectCoord = new Label();
            cmbReflectCoord = new ComboBox();
            btnReflectCoord = new Button();
            lblRotateLine = new Label();
            lblP1 = new Label();
            numLx1 = new NumericUpDown();
            numLy1 = new NumericUpDown();
            numLz1 = new NumericUpDown();
            lblP2 = new Label();
            numLx2 = new NumericUpDown();
            numLy2 = new NumericUpDown();
            numLz2 = new NumericUpDown();
            lblAng = new Label();
            numLineAngle = new NumericUpDown();
            btnRotateLine = new Button();
            panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numTx).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numTz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSx).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSy).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSz).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numAxisAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLx1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLy1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLz1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLx2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLy2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLz2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numLineAngle).BeginInit();
            SuspendLayout();
            // 
            // polyhedronDrawer
            // 
            polyhedronDrawer.BackColor = Color.White;
            polyhedronDrawer.Dock = DockStyle.Fill;
            polyhedronDrawer.Location = new Point(300, 0);
            polyhedronDrawer.Name = "polyhedronDrawer";
            polyhedronDrawer.Size = new Size(600, 600);
            polyhedronDrawer.TabIndex = 0;
            polyhedronDrawer.Paint += polyhedronDrawer_Paint;
            // 
            // panelControls
            // 
            panelControls.BackColor = SystemColors.Control;
            panelControls.Controls.Add(cmbFigure);
            panelControls.Controls.Add(cmbProjection);
            panelControls.Controls.Add(btnReset);
            panelControls.Controls.Add(lblTranslation);
            panelControls.Controls.Add(numTx);
            panelControls.Controls.Add(numTy);
            panelControls.Controls.Add(numTz);
            panelControls.Controls.Add(btnTranslate);
            panelControls.Controls.Add(lblScale);
            panelControls.Controls.Add(numSx);
            panelControls.Controls.Add(numSy);
            panelControls.Controls.Add(numSz);
            panelControls.Controls.Add(btnScale);
            panelControls.Controls.Add(lblRotateAxis);
            panelControls.Controls.Add(cmbAxis);
            panelControls.Controls.Add(numAxisAngle);
            panelControls.Controls.Add(btnRotateAxis);
            panelControls.Controls.Add(lblReflectCoord);
            panelControls.Controls.Add(cmbReflectCoord);
            panelControls.Controls.Add(btnReflectCoord);
            panelControls.Controls.Add(lblRotateLine);
            panelControls.Controls.Add(lblP1);
            panelControls.Controls.Add(numLx1);
            panelControls.Controls.Add(numLy1);
            panelControls.Controls.Add(numLz1);
            panelControls.Controls.Add(lblP2);
            panelControls.Controls.Add(numLx2);
            panelControls.Controls.Add(numLy2);
            panelControls.Controls.Add(numLz2);
            panelControls.Controls.Add(lblAng);
            panelControls.Controls.Add(numLineAngle);
            panelControls.Controls.Add(btnRotateLine);
            panelControls.Dock = DockStyle.Left;
            panelControls.Location = new Point(0, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(300, 600);
            panelControls.TabIndex = 1;
            // 
            // cmbFigure
            // 
            cmbFigure.Items.AddRange(new object[] { "Тетраэдр", "Куб", "Октаэдр", "Икосаэдр", "Додекаэдр" });
            cmbFigure.Location = new Point(10, 10);
            cmbFigure.Name = "cmbFigure";
            cmbFigure.Size = new Size(130, 23);
            cmbFigure.TabIndex = 0;
            cmbFigure.Text = "Куб";
            // 
            // cmbProjection
            // 
            cmbProjection.Items.AddRange(new object[] { "Перспектива", "Аксонометрия" });
            cmbProjection.Location = new Point(154, 10);
            cmbProjection.Name = "cmbProjection";
            cmbProjection.Size = new Size(130, 23);
            cmbProjection.TabIndex = 1;
            cmbProjection.Text = "Перспектива";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(10, 50);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(274, 25);
            btnReset.TabIndex = 2;
            btnReset.Text = "Сброс";
            // 
            // lblTranslation
            // 
            lblTranslation.Location = new Point(10, 90);
            lblTranslation.Name = "lblTranslation";
            lblTranslation.Size = new Size(78, 23);
            lblTranslation.TabIndex = 3;
            lblTranslation.Text = "Смещение:";
            // 
            // numTx
            // 
            numTx.DecimalPlaces = 1;
            numTx.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTx.Location = new Point(94, 90);
            numTx.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTx.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTx.Name = "numTx";
            numTx.Size = new Size(50, 23);
            numTx.TabIndex = 4;
            // 
            // numTy
            // 
            numTy.DecimalPlaces = 1;
            numTy.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTy.Location = new Point(149, 90);
            numTy.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTy.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTy.Name = "numTy";
            numTy.Size = new Size(50, 23);
            numTy.TabIndex = 5;
            // 
            // numTz
            // 
            numTz.DecimalPlaces = 1;
            numTz.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTz.Location = new Point(204, 90);
            numTz.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTz.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTz.Name = "numTz";
            numTz.Size = new Size(50, 23);
            numTz.TabIndex = 6;
            // 
            // btnTranslate
            // 
            btnTranslate.Location = new Point(259, 88);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(25, 25);
            btnTranslate.TabIndex = 7;
            btnTranslate.Text = ">";
            // 
            // lblScale
            // 
            lblScale.Location = new Point(10, 125);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(65, 23);
            lblScale.TabIndex = 8;
            lblScale.Text = "Масштаб:";
            // 
            // numSx
            // 
            numSx.DecimalPlaces = 1;
            numSx.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSx.Location = new Point(94, 125);
            numSx.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSx.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSx.Name = "numSx";
            numSx.Size = new Size(50, 23);
            numSx.TabIndex = 9;
            numSx.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numSy
            // 
            numSy.DecimalPlaces = 1;
            numSy.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSy.Location = new Point(149, 125);
            numSy.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSy.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSy.Name = "numSy";
            numSy.Size = new Size(50, 23);
            numSy.TabIndex = 10;
            numSy.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numSz
            // 
            numSz.DecimalPlaces = 1;
            numSz.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSz.Location = new Point(204, 125);
            numSz.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSz.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSz.Name = "numSz";
            numSz.Size = new Size(50, 23);
            numSz.TabIndex = 11;
            numSz.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnScale
            // 
            btnScale.Location = new Point(259, 123);
            btnScale.Name = "btnScale";
            btnScale.Size = new Size(25, 25);
            btnScale.TabIndex = 12;
            btnScale.Text = ">";
            // 
            // lblRotateAxis
            // 
            lblRotateAxis.Location = new Point(10, 160);
            lblRotateAxis.Name = "lblRotateAxis";
            lblRotateAxis.Size = new Size(78, 23);
            lblRotateAxis.TabIndex = 13;
            lblRotateAxis.Text = "Вокруг оси:";
            // 
            // cmbAxis
            // 
            cmbAxis.Items.AddRange(new object[] { "X", "Y", "Z" });
            cmbAxis.Location = new Point(94, 160);
            cmbAxis.Name = "cmbAxis";
            cmbAxis.Size = new Size(50, 23);
            cmbAxis.TabIndex = 14;
            cmbAxis.Text = "X";
            // 
            // numAxisAngle
            // 
            numAxisAngle.DecimalPlaces = 1;
            numAxisAngle.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numAxisAngle.Location = new Point(149, 160);
            numAxisAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numAxisAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numAxisAngle.Name = "numAxisAngle";
            numAxisAngle.Size = new Size(50, 23);
            numAxisAngle.TabIndex = 15;
            numAxisAngle.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnRotateAxis
            // 
            btnRotateAxis.Location = new Point(204, 158);
            btnRotateAxis.Name = "btnRotateAxis";
            btnRotateAxis.Size = new Size(80, 25);
            btnRotateAxis.TabIndex = 16;
            btnRotateAxis.Text = ">";
            // 
            // lblReflectCoord
            // 
            lblReflectCoord.Location = new Point(10, 195);
            lblReflectCoord.Name = "lblReflectCoord";
            lblReflectCoord.Size = new Size(78, 23);
            lblReflectCoord.TabIndex = 17;
            lblReflectCoord.Text = "Отражение:";
            // 
            // cmbReflectCoord
            // 
            cmbReflectCoord.Items.AddRange(new object[] { "XY", "XZ", "YZ" });
            cmbReflectCoord.Location = new Point(94, 195);
            cmbReflectCoord.Name = "cmbReflectCoord";
            cmbReflectCoord.Size = new Size(105, 23);
            cmbReflectCoord.TabIndex = 18;
            cmbReflectCoord.Text = "XY";
            // 
            // btnReflectCoord
            // 
            btnReflectCoord.Location = new Point(204, 193);
            btnReflectCoord.Name = "btnReflectCoord";
            btnReflectCoord.Size = new Size(80, 25);
            btnReflectCoord.TabIndex = 19;
            btnReflectCoord.Text = ">";
            // 
            // lblRotateLine
            // 
            lblRotateLine.Location = new Point(10, 236);
            lblRotateLine.Name = "lblRotateLine";
            lblRotateLine.Size = new Size(159, 23);
            lblRotateLine.TabIndex = 20;
            lblRotateLine.Text = "Вращение вокруг прямой:";
            // 
            // lblP1
            // 
            lblP1.AutoSize = true;
            lblP1.Location = new Point(10, 265);
            lblP1.Name = "lblP1";
            lblP1.Size = new Size(23, 15);
            lblP1.TabIndex = 21;
            lblP1.Text = "P1:";
            // 
            // numLx1
            // 
            numLx1.DecimalPlaces = 1;
            numLx1.Location = new Point(40, 262);
            numLx1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLx1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLx1.Name = "numLx1";
            numLx1.Size = new Size(50, 23);
            numLx1.TabIndex = 22;
            // 
            // numLy1
            // 
            numLy1.DecimalPlaces = 1;
            numLy1.Location = new Point(95, 262);
            numLy1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLy1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLy1.Name = "numLy1";
            numLy1.Size = new Size(50, 23);
            numLy1.TabIndex = 23;
            // 
            // numLz1
            // 
            numLz1.DecimalPlaces = 1;
            numLz1.Location = new Point(150, 262);
            numLz1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLz1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLz1.Name = "numLz1";
            numLz1.Size = new Size(50, 23);
            numLz1.TabIndex = 24;
            // 
            // lblP2
            // 
            lblP2.AutoSize = true;
            lblP2.Location = new Point(10, 294);
            lblP2.Name = "lblP2";
            lblP2.Size = new Size(23, 15);
            lblP2.TabIndex = 25;
            lblP2.Text = "P2:";
            // 
            // numLx2
            // 
            numLx2.DecimalPlaces = 1;
            numLx2.Location = new Point(40, 291);
            numLx2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLx2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLx2.Name = "numLx2";
            numLx2.Size = new Size(50, 23);
            numLx2.TabIndex = 26;
            numLx2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numLy2
            // 
            numLy2.DecimalPlaces = 1;
            numLy2.Location = new Point(95, 291);
            numLy2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLy2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLy2.Name = "numLy2";
            numLy2.Size = new Size(50, 23);
            numLy2.TabIndex = 27;
            // 
            // numLz2
            // 
            numLz2.DecimalPlaces = 1;
            numLz2.Location = new Point(150, 291);
            numLz2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLz2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLz2.Name = "numLz2";
            numLz2.Size = new Size(50, 23);
            numLz2.TabIndex = 28;
            // 
            // lblAng
            // 
            lblAng.AutoSize = true;
            lblAng.Location = new Point(10, 326);
            lblAng.Name = "lblAng";
            lblAng.Size = new Size(36, 15);
            lblAng.TabIndex = 29;
            lblAng.Text = "Угол:";
            // 
            // numLineAngle
            // 
            numLineAngle.DecimalPlaces = 1;
            numLineAngle.Location = new Point(60, 323);
            numLineAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numLineAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numLineAngle.Name = "numLineAngle";
            numLineAngle.Size = new Size(50, 23);
            numLineAngle.TabIndex = 30;
            numLineAngle.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnRotateLine
            // 
            btnRotateLine.Location = new Point(120, 321);
            btnRotateLine.Name = "btnRotateLine";
            btnRotateLine.Size = new Size(75, 25);
            btnRotateLine.TabIndex = 31;
            btnRotateLine.Text = ">";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 600);
            Controls.Add(polyhedronDrawer);
            Controls.Add(panelControls);
            Name = "Form1";
            Text = "Лабораторная работа №6";
            panelControls.ResumeLayout(false);
            panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numTx).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTy).EndInit();
            ((System.ComponentModel.ISupportInitialize)numTz).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSx).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSy).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSz).EndInit();
            ((System.ComponentModel.ISupportInitialize)numAxisAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLx1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLy1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLz1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLx2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLy2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLz2).EndInit();
            ((System.ComponentModel.ISupportInitialize)numLineAngle).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}