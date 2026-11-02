namespace lab5
{
    partial class Task1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonLoad = new Button();
            drawPanel = new Panel();
            buttonBuild = new Button();
            iterBox = new NumericUpDown();
            randBox = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            checkBox1 = new CheckBox();
            label3 = new Label();
            widthBox = new NumericUpDown();
            labelFile = new Label();
            ((System.ComponentModel.ISupportInitialize)iterBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)randBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)widthBox).BeginInit();
            SuspendLayout();
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(12, 10);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(109, 26);
            buttonLoad.TabIndex = 0;
            buttonLoad.Text = "Загрузить";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // drawPanel
            // 
            drawPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            drawPanel.BackColor = SystemColors.Window;
            drawPanel.Location = new Point(12, 78);
            drawPanel.Name = "drawPanel";
            drawPanel.Size = new Size(1086, 589);
            drawPanel.TabIndex = 1;
            drawPanel.Paint += drawPanel_Paint;
            drawPanel.Resize += drawPanel_Resize;
            // 
            // buttonBuild
            // 
            buttonBuild.Location = new Point(12, 42);
            buttonBuild.Name = "buttonBuild";
            buttonBuild.Size = new Size(109, 26);
            buttonBuild.TabIndex = 2;
            buttonBuild.Text = "Построить";
            buttonBuild.UseVisualStyleBackColor = true;
            buttonBuild.Click += buttonBuild_Click;
            // 
            // iterBox
            // 
            iterBox.Location = new Point(140, 29);
            iterBox.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            iterBox.Name = "iterBox";
            iterBox.Size = new Size(120, 23);
            iterBox.TabIndex = 3;
            iterBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // randBox
            // 
            randBox.Location = new Point(279, 29);
            randBox.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            randBox.Name = "randBox";
            randBox.Size = new Size(120, 23);
            randBox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(140, 10);
            label1.Name = "label1";
            label1.Size = new Size(101, 15);
            label1.TabIndex = 5;
            label1.Text = "Кол-во итераций";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(279, 10);
            label2.Name = "label2";
            label2.Size = new Size(106, 15);
            label2.TabIndex = 6;
            label2.Text = "Случайность угла";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(559, 29);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(39, 19);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "1b";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(416, 10);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 9;
            label3.Text = "Толщина";
            // 
            // widthBox
            // 
            widthBox.Location = new Point(416, 29);
            widthBox.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            widthBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            widthBox.Name = "widthBox";
            widthBox.Size = new Size(120, 23);
            widthBox.TabIndex = 8;
            widthBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            widthBox.ValueChanged += widthBox_ValueChanged;
            // 
            // labelFile
            // 
            labelFile.AutoSize = true;
            labelFile.Location = new Point(613, 30);
            labelFile.Name = "labelFile";
            labelFile.Size = new Size(106, 15);
            labelFile.TabIndex = 10;
            labelFile.Text = "Файл не загружен";
            // 
            // Task1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1110, 679);
            Controls.Add(labelFile);
            Controls.Add(label3);
            Controls.Add(checkBox1);
            Controls.Add(widthBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(randBox);
            Controls.Add(iterBox);
            Controls.Add(buttonBuild);
            Controls.Add(drawPanel);
            Controls.Add(buttonLoad);
            Name = "Task1";
            Text = "Task 1";
            ((System.ComponentModel.ISupportInitialize)iterBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)randBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)widthBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonLoad;
        private Panel drawPanel;
        private Button buttonBuild;
        private NumericUpDown iterBox;
        private NumericUpDown randBox;
        private Label label1;
        private Label label2;
        private CheckBox checkBox1;
        private Label label3;
        private NumericUpDown widthBox;
        private Label labelFile;
    }
}
