namespace Lab8
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private PolyhedronDrawer polyhedronDrawer;

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.ComboBox cmbFigure;
        private System.Windows.Forms.ComboBox cmbProjection;
        private System.Windows.Forms.ComboBox cmbDisplayMode;
        private System.Windows.Forms.CheckBox checkSeveralObjects;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnResetCamera;

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

        private System.Windows.Forms.Button btnLoadObj;
        private System.Windows.Forms.Button btnSaveObj;

        private System.Windows.Forms.Label lblProfile;
        private System.Windows.Forms.TextBox txtProfile;
        private System.Windows.Forms.Label lblRevolutionAxis;
        private System.Windows.Forms.ComboBox cmbRevolutionAxis;
        private System.Windows.Forms.Label lblSegments;
        private System.Windows.Forms.NumericUpDown numSegments;
        private System.Windows.Forms.Button btnBuildRevolution;

        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.NumericUpDown numX0;
        private System.Windows.Forms.Label lblX1;
        private System.Windows.Forms.NumericUpDown numX1;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.NumericUpDown numY0;
        private System.Windows.Forms.Label lblY1;
        private System.Windows.Forms.NumericUpDown numY1;
        private System.Windows.Forms.Label lblSurfaceSegments;
        private System.Windows.Forms.NumericUpDown numSurfaceSegments;
        private System.Windows.Forms.Label lblFunction;
        private System.Windows.Forms.ComboBox cmbFunction;
        private System.Windows.Forms.Button btnBuildSurface;

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
            polyhedronDrawer = new PolyhedronDrawer();
            panelControls = new Panel();
            cmbFigure = new ComboBox();
            cmbProjection = new ComboBox();
            cmbDisplayMode = new ComboBox();
            checkSeveralObjects = new CheckBox();
            btnReset = new Button();
            btnResetCamera = new Button();
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
            btnLoadObj = new Button();
            btnSaveObj = new Button();
            lblProfile = new Label();
            txtProfile = new TextBox();
            lblRevolutionAxis = new Label();
            cmbRevolutionAxis = new ComboBox();
            lblSegments = new Label();
            numSegments = new NumericUpDown();
            btnBuildRevolution = new Button();
            lblX0 = new Label();
            numX0 = new NumericUpDown();
            lblX1 = new Label();
            numX1 = new NumericUpDown();
            lblY0 = new Label();
            numY0 = new NumericUpDown();
            lblY1 = new Label();
            numY1 = new NumericUpDown();
            lblSurfaceSegments = new Label();
            numSurfaceSegments = new NumericUpDown();
            lblFunction = new Label();
            cmbFunction = new ComboBox();
            btnBuildSurface = new Button();
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
            ((System.ComponentModel.ISupportInitialize)numSegments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numX1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY0).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numY1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numSurfaceSegments).BeginInit();
            SuspendLayout();
            // 
            // polyhedronDrawer
            // 
            polyhedronDrawer.BackColor = Color.White;
            polyhedronDrawer.BorderStyle = BorderStyle.Fixed3D;
            polyhedronDrawer.Dock = DockStyle.Fill;
            polyhedronDrawer.Location = new Point(300, 0);
            polyhedronDrawer.Name = "polyhedronDrawer";
            polyhedronDrawer.Size = new Size(1124, 852);
            polyhedronDrawer.TabIndex = 0;
            polyhedronDrawer.TabStop = true;
            polyhedronDrawer.Paint += polyhedronDrawer_Paint;
            // 
            // panelControls
            // 
            panelControls.AutoScroll = true;
            panelControls.BackColor = SystemColors.Control;
            panelControls.Controls.Add(cmbFigure);
            panelControls.Controls.Add(cmbProjection);
            panelControls.Controls.Add(cmbDisplayMode);
            panelControls.Controls.Add(checkSeveralObjects);
            panelControls.Controls.Add(btnReset);
            panelControls.Controls.Add(btnResetCamera);
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
            panelControls.Controls.Add(btnLoadObj);
            panelControls.Controls.Add(btnSaveObj);
            panelControls.Controls.Add(lblProfile);
            panelControls.Controls.Add(txtProfile);
            panelControls.Controls.Add(lblRevolutionAxis);
            panelControls.Controls.Add(cmbRevolutionAxis);
            panelControls.Controls.Add(lblSegments);
            panelControls.Controls.Add(numSegments);
            panelControls.Controls.Add(btnBuildRevolution);
            panelControls.Controls.Add(lblX0);
            panelControls.Controls.Add(numX0);
            panelControls.Controls.Add(lblX1);
            panelControls.Controls.Add(numX1);
            panelControls.Controls.Add(lblY0);
            panelControls.Controls.Add(numY0);
            panelControls.Controls.Add(lblY1);
            panelControls.Controls.Add(numY1);
            panelControls.Controls.Add(lblSurfaceSegments);
            panelControls.Controls.Add(numSurfaceSegments);
            panelControls.Controls.Add(lblFunction);
            panelControls.Controls.Add(cmbFunction);
            panelControls.Controls.Add(btnBuildSurface);
            panelControls.Dock = DockStyle.Left;
            panelControls.Location = new Point(0, 0);
            panelControls.Name = "panelControls";
            panelControls.Size = new Size(300, 852);
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
            cmbProjection.Text = "Перспективная";
            // 
            // cmbDisplayMode
            // 
            cmbDisplayMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDisplayMode.Items.AddRange(new object[] { "Обычное", "Отсечение граней", "Z-буфер" });
            cmbDisplayMode.Location = new Point(10, 45);
            cmbDisplayMode.Name = "cmbDisplayMode";
            cmbDisplayMode.Size = new Size(274, 23);
            cmbDisplayMode.TabIndex = 2;
            // 
            // checkSeveralObjects
            // 
            checkSeveralObjects.AutoSize = true;
            checkSeveralObjects.Checked = true;
            checkSeveralObjects.CheckState = CheckState.Checked;
            checkSeveralObjects.Location = new Point(10, 75);
            checkSeveralObjects.Name = "checkSeveralObjects";
            checkSeveralObjects.Size = new Size(148, 19);
            checkSeveralObjects.TabIndex = 3;
            checkSeveralObjects.Text = "Несколько объектов";
            checkSeveralObjects.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(10, 105);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(134, 25);
            btnReset.TabIndex = 4;
            btnReset.Text = "Сброс";
            // 
            // btnResetCamera
            // 
            btnResetCamera.Location = new Point(150, 105);
            btnResetCamera.Name = "btnResetCamera";
            btnResetCamera.Size = new Size(134, 25);
            btnResetCamera.TabIndex = 5;
            btnResetCamera.Text = "Сброс камеры";
            // 
            // lblTranslation
            // 
            lblTranslation.Location = new Point(10, 145);
            lblTranslation.Name = "lblTranslation";
            lblTranslation.Size = new Size(78, 23);
            lblTranslation.TabIndex = 6;
            lblTranslation.Text = "Смещение:";
            // 
            // numTx
            // 
            numTx.DecimalPlaces = 1;
            numTx.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTx.Location = new Point(94, 145);
            numTx.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTx.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTx.Name = "numTx";
            numTx.Size = new Size(50, 23);
            numTx.TabIndex = 7;
            // 
            // numTy
            // 
            numTy.DecimalPlaces = 1;
            numTy.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTy.Location = new Point(149, 145);
            numTy.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTy.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTy.Name = "numTy";
            numTy.Size = new Size(50, 23);
            numTy.TabIndex = 8;
            // 
            // numTz
            // 
            numTz.DecimalPlaces = 1;
            numTz.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numTz.Location = new Point(204, 145);
            numTz.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numTz.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numTz.Name = "numTz";
            numTz.Size = new Size(50, 23);
            numTz.TabIndex = 9;
            // 
            // btnTranslate
            // 
            btnTranslate.Location = new Point(259, 143);
            btnTranslate.Name = "btnTranslate";
            btnTranslate.Size = new Size(25, 25);
            btnTranslate.TabIndex = 10;
            btnTranslate.Text = ">";
            // 
            // lblScale
            // 
            lblScale.Location = new Point(10, 180);
            lblScale.Name = "lblScale";
            lblScale.Size = new Size(65, 23);
            lblScale.TabIndex = 11;
            lblScale.Text = "Масштаб:";
            // 
            // numSx
            // 
            numSx.DecimalPlaces = 1;
            numSx.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSx.Location = new Point(94, 180);
            numSx.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSx.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSx.Name = "numSx";
            numSx.Size = new Size(50, 23);
            numSx.TabIndex = 12;
            numSx.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numSy
            // 
            numSy.DecimalPlaces = 1;
            numSy.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSy.Location = new Point(149, 180);
            numSy.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSy.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSy.Name = "numSy";
            numSy.Size = new Size(50, 23);
            numSy.TabIndex = 13;
            numSy.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numSz
            // 
            numSz.DecimalPlaces = 1;
            numSz.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numSz.Location = new Point(204, 180);
            numSz.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numSz.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numSz.Name = "numSz";
            numSz.Size = new Size(50, 23);
            numSz.TabIndex = 14;
            numSz.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnScale
            // 
            btnScale.Location = new Point(259, 178);
            btnScale.Name = "btnScale";
            btnScale.Size = new Size(25, 25);
            btnScale.TabIndex = 15;
            btnScale.Text = ">";
            // 
            // lblRotateAxis
            // 
            lblRotateAxis.Location = new Point(10, 215);
            lblRotateAxis.Name = "lblRotateAxis";
            lblRotateAxis.Size = new Size(78, 23);
            lblRotateAxis.TabIndex = 16;
            lblRotateAxis.Text = "Вокруг оси:";
            // 
            // cmbAxis
            // 
            cmbAxis.Items.AddRange(new object[] { "X", "Y", "Z" });
            cmbAxis.Location = new Point(94, 215);
            cmbAxis.Name = "cmbAxis";
            cmbAxis.Size = new Size(50, 23);
            cmbAxis.TabIndex = 17;
            cmbAxis.Text = "Z";
            // 
            // numAxisAngle
            // 
            numAxisAngle.DecimalPlaces = 1;
            numAxisAngle.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            numAxisAngle.Location = new Point(149, 215);
            numAxisAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numAxisAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numAxisAngle.Name = "numAxisAngle";
            numAxisAngle.Size = new Size(50, 23);
            numAxisAngle.TabIndex = 18;
            numAxisAngle.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnRotateAxis
            // 
            btnRotateAxis.Location = new Point(204, 213);
            btnRotateAxis.Name = "btnRotateAxis";
            btnRotateAxis.Size = new Size(80, 25);
            btnRotateAxis.TabIndex = 19;
            btnRotateAxis.Text = ">";
            // 
            // lblReflectCoord
            // 
            lblReflectCoord.Location = new Point(10, 250);
            lblReflectCoord.Name = "lblReflectCoord";
            lblReflectCoord.Size = new Size(78, 23);
            lblReflectCoord.TabIndex = 20;
            lblReflectCoord.Text = "Отражение:";
            // 
            // cmbReflectCoord
            // 
            cmbReflectCoord.Items.AddRange(new object[] { "XY", "XZ", "YZ" });
            cmbReflectCoord.Location = new Point(94, 250);
            cmbReflectCoord.Name = "cmbReflectCoord";
            cmbReflectCoord.Size = new Size(105, 23);
            cmbReflectCoord.TabIndex = 21;
            cmbReflectCoord.Text = "XY";
            // 
            // btnReflectCoord
            // 
            btnReflectCoord.Location = new Point(204, 248);
            btnReflectCoord.Name = "btnReflectCoord";
            btnReflectCoord.Size = new Size(80, 25);
            btnReflectCoord.TabIndex = 22;
            btnReflectCoord.Text = ">";
            // 
            // lblRotateLine
            // 
            lblRotateLine.Location = new Point(10, 291);
            lblRotateLine.Name = "lblRotateLine";
            lblRotateLine.Size = new Size(159, 23);
            lblRotateLine.TabIndex = 23;
            lblRotateLine.Text = "Вращение вокруг прямой:";
            // 
            // lblP1
            // 
            lblP1.AutoSize = true;
            lblP1.Location = new Point(10, 320);
            lblP1.Name = "lblP1";
            lblP1.Size = new Size(23, 15);
            lblP1.TabIndex = 24;
            lblP1.Text = "P1:";
            // 
            // numLx1
            // 
            numLx1.DecimalPlaces = 1;
            numLx1.Location = new Point(40, 317);
            numLx1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLx1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLx1.Name = "numLx1";
            numLx1.Size = new Size(50, 23);
            numLx1.TabIndex = 25;
            // 
            // numLy1
            // 
            numLy1.DecimalPlaces = 1;
            numLy1.Location = new Point(95, 317);
            numLy1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLy1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLy1.Name = "numLy1";
            numLy1.Size = new Size(50, 23);
            numLy1.TabIndex = 26;
            // 
            // numLz1
            // 
            numLz1.DecimalPlaces = 1;
            numLz1.Location = new Point(150, 317);
            numLz1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLz1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLz1.Name = "numLz1";
            numLz1.Size = new Size(50, 23);
            numLz1.TabIndex = 27;
            // 
            // lblP2
            // 
            lblP2.AutoSize = true;
            lblP2.Location = new Point(10, 349);
            lblP2.Name = "lblP2";
            lblP2.Size = new Size(23, 15);
            lblP2.TabIndex = 28;
            lblP2.Text = "P2:";
            // 
            // numLx2
            // 
            numLx2.DecimalPlaces = 1;
            numLx2.Location = new Point(40, 346);
            numLx2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLx2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLx2.Name = "numLx2";
            numLx2.Size = new Size(50, 23);
            numLx2.TabIndex = 29;
            numLx2.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numLy2
            // 
            numLy2.DecimalPlaces = 1;
            numLy2.Location = new Point(95, 346);
            numLy2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLy2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLy2.Name = "numLy2";
            numLy2.Size = new Size(50, 23);
            numLy2.TabIndex = 30;
            // 
            // numLz2
            // 
            numLz2.DecimalPlaces = 1;
            numLz2.Location = new Point(150, 346);
            numLz2.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numLz2.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numLz2.Name = "numLz2";
            numLz2.Size = new Size(50, 23);
            numLz2.TabIndex = 31;
            // 
            // lblAng
            // 
            lblAng.AutoSize = true;
            lblAng.Location = new Point(10, 381);
            lblAng.Name = "lblAng";
            lblAng.Size = new Size(36, 15);
            lblAng.TabIndex = 32;
            lblAng.Text = "Угол:";
            // 
            // numLineAngle
            // 
            numLineAngle.DecimalPlaces = 1;
            numLineAngle.Location = new Point(60, 378);
            numLineAngle.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            numLineAngle.Minimum = new decimal(new int[] { 360, 0, 0, int.MinValue });
            numLineAngle.Name = "numLineAngle";
            numLineAngle.Size = new Size(50, 23);
            numLineAngle.TabIndex = 33;
            numLineAngle.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnRotateLine
            // 
            btnRotateLine.Location = new Point(120, 376);
            btnRotateLine.Name = "btnRotateLine";
            btnRotateLine.Size = new Size(75, 25);
            btnRotateLine.TabIndex = 34;
            btnRotateLine.Text = ">";
            // 
            // btnLoadObj
            // 
            btnLoadObj.Location = new Point(10, 415);
            btnLoadObj.Name = "btnLoadObj";
            btnLoadObj.Size = new Size(130, 25);
            btnLoadObj.TabIndex = 35;
            btnLoadObj.Text = "Загрузить OBJ";
            // 
            // btnSaveObj
            // 
            btnSaveObj.Location = new Point(150, 415);
            btnSaveObj.Name = "btnSaveObj";
            btnSaveObj.Size = new Size(130, 25);
            btnSaveObj.TabIndex = 36;
            btnSaveObj.Text = "Сохранить OBJ";
            // 
            // lblProfile
            // 
            lblProfile.Location = new Point(10, 450);
            lblProfile.Name = "lblProfile";
            lblProfile.Size = new Size(274, 18);
            lblProfile.TabIndex = 37;
            lblProfile.Text = "Образующая:";
            // 
            // txtProfile
            // 
            txtProfile.Location = new Point(10, 470);
            txtProfile.Name = "txtProfile";
            txtProfile.Size = new Size(274, 23);
            txtProfile.TabIndex = 38;
            txtProfile.Text = "1,0,0; 1,0,1; 0,0,1";
            // 
            // lblRevolutionAxis
            // 
            lblRevolutionAxis.Location = new Point(10, 500);
            lblRevolutionAxis.Name = "lblRevolutionAxis";
            lblRevolutionAxis.Size = new Size(60, 23);
            lblRevolutionAxis.TabIndex = 39;
            lblRevolutionAxis.Text = "Ось:";
            // 
            // cmbRevolutionAxis
            // 
            cmbRevolutionAxis.Items.AddRange(new object[] { "X", "Y", "Z" });
            cmbRevolutionAxis.Location = new Point(70, 498);
            cmbRevolutionAxis.Name = "cmbRevolutionAxis";
            cmbRevolutionAxis.Size = new Size(50, 23);
            cmbRevolutionAxis.TabIndex = 40;
            cmbRevolutionAxis.Text = "X";
            // 
            // lblSegments
            // 
            lblSegments.Location = new Point(130, 500);
            lblSegments.Name = "lblSegments";
            lblSegments.Size = new Size(60, 23);
            lblSegments.TabIndex = 41;
            lblSegments.Text = "Разб.:";
            // 
            // numSegments
            // 
            numSegments.Location = new Point(190, 498);
            numSegments.Maximum = new decimal(new int[] { 256, 0, 0, 0 });
            numSegments.Minimum = new decimal(new int[] { 3, 0, 0, 0 });
            numSegments.Name = "numSegments";
            numSegments.Size = new Size(50, 23);
            numSegments.TabIndex = 42;
            numSegments.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // btnBuildRevolution
            // 
            btnBuildRevolution.Location = new Point(244, 496);
            btnBuildRevolution.Name = "btnBuildRevolution";
            btnBuildRevolution.Size = new Size(40, 25);
            btnBuildRevolution.TabIndex = 43;
            btnBuildRevolution.Text = ">";
            // 
            // lblX0
            // 
            lblX0.Location = new Point(10, 535);
            lblX0.Name = "lblX0";
            lblX0.Size = new Size(30, 23);
            lblX0.TabIndex = 44;
            lblX0.Text = "X0:";
            // 
            // numX0
            // 
            numX0.DecimalPlaces = 1;
            numX0.Location = new Point(40, 533);
            numX0.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numX0.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numX0.Name = "numX0";
            numX0.Size = new Size(50, 23);
            numX0.TabIndex = 45;
            numX0.Value = new decimal(new int[] { 2, 0, 0, int.MinValue });
            // 
            // lblX1
            // 
            lblX1.Location = new Point(95, 535);
            lblX1.Name = "lblX1";
            lblX1.Size = new Size(30, 23);
            lblX1.TabIndex = 46;
            lblX1.Text = "X1:";
            // 
            // numX1
            // 
            numX1.DecimalPlaces = 1;
            numX1.Location = new Point(125, 533);
            numX1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numX1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numX1.Name = "numX1";
            numX1.Size = new Size(50, 23);
            numX1.TabIndex = 47;
            numX1.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblY0
            // 
            lblY0.Location = new Point(10, 565);
            lblY0.Name = "lblY0";
            lblY0.Size = new Size(30, 23);
            lblY0.TabIndex = 48;
            lblY0.Text = "Y0:";
            // 
            // numY0
            // 
            numY0.DecimalPlaces = 1;
            numY0.Location = new Point(40, 563);
            numY0.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numY0.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numY0.Name = "numY0";
            numY0.Size = new Size(50, 23);
            numY0.TabIndex = 49;
            numY0.Value = new decimal(new int[] { 2, 0, 0, int.MinValue });
            // 
            // lblY1
            // 
            lblY1.Location = new Point(95, 565);
            lblY1.Name = "lblY1";
            lblY1.Size = new Size(30, 23);
            lblY1.TabIndex = 50;
            lblY1.Text = "Y1:";
            // 
            // numY1
            // 
            numY1.DecimalPlaces = 1;
            numY1.Location = new Point(125, 563);
            numY1.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numY1.Minimum = new decimal(new int[] { 1000, 0, 0, int.MinValue });
            numY1.Name = "numY1";
            numY1.Size = new Size(50, 23);
            numY1.TabIndex = 51;
            numY1.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblSurfaceSegments
            // 
            lblSurfaceSegments.Location = new Point(10, 595);
            lblSurfaceSegments.Name = "lblSurfaceSegments";
            lblSurfaceSegments.Size = new Size(70, 23);
            lblSurfaceSegments.TabIndex = 52;
            lblSurfaceSegments.Text = "Разб. N:";
            // 
            // numSurfaceSegments
            // 
            numSurfaceSegments.Location = new Point(80, 593);
            numSurfaceSegments.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            numSurfaceSegments.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numSurfaceSegments.Name = "numSurfaceSegments";
            numSurfaceSegments.Size = new Size(50, 23);
            numSurfaceSegments.TabIndex = 53;
            numSurfaceSegments.Value = new decimal(new int[] { 20, 0, 0, 0 });
            // 
            // lblFunction
            // 
            lblFunction.Location = new Point(140, 595);
            lblFunction.Name = "lblFunction";
            lblFunction.Size = new Size(40, 23);
            lblFunction.TabIndex = 54;
            lblFunction.Text = "f(x,y):";
            // 
            // cmbFunction
            // 
            cmbFunction.Items.AddRange(new object[] { "sin(sqrt(x^2 + y^2))", "x^2 - y^2", "cos(x) * sin(y)", "sin(x) * cos(y) * x", "exp(-(x^2 + y^2))" });
            cmbFunction.Location = new Point(180, 593);
            cmbFunction.Name = "cmbFunction";
            cmbFunction.Size = new Size(104, 23);
            cmbFunction.TabIndex = 55;
            // 
            // btnBuildSurface
            // 
            btnBuildSurface.Location = new Point(10, 623);
            btnBuildSurface.Name = "btnBuildSurface";
            btnBuildSurface.Size = new Size(274, 25);
            btnBuildSurface.TabIndex = 56;
            btnBuildSurface.Text = "Построить поверхность";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1424, 852);
            Controls.Add(polyhedronDrawer);
            Controls.Add(panelControls);
            Name = "Form1";
            Text = "Лабораторная работа №8";
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
            ((System.ComponentModel.ISupportInitialize)numSegments).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX0).EndInit();
            ((System.ComponentModel.ISupportInitialize)numX1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY0).EndInit();
            ((System.ComponentModel.ISupportInitialize)numY1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numSurfaceSegments).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}