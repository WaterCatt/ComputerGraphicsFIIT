namespace lab03
{
    partial class task1b
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
            buttonClear = new Button();
            pictureBox1 = new PictureBox();
            picturePreview = new PictureBox();
            buttonLoad = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picturePreview).BeginInit();
            SuspendLayout();
            // 
            // buttonClear
            // 
            buttonClear.Location = new Point(12, 12);
            buttonClear.Name = "buttonClear";
            buttonClear.Size = new Size(88, 23);
            buttonClear.TabIndex = 0;
            buttonClear.Text = "Очистить";
            buttonClear.UseVisualStyleBackColor = true;
            buttonClear.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(180, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(962, 677);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // picturePreview
            // 
            picturePreview.BackColor = Color.White;
            picturePreview.Location = new Point(12, 536);
            picturePreview.Name = "picturePreview";
            picturePreview.Size = new Size(162, 153);
            picturePreview.SizeMode = PictureBoxSizeMode.Zoom;
            picturePreview.TabIndex = 2;
            picturePreview.TabStop = false;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new Point(12, 507);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new Size(88, 23);
            buttonLoad.TabIndex = 3;
            buttonLoad.Text = "Загрузить";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // task1b
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1154, 701);
            Controls.Add(buttonLoad);
            Controls.Add(picturePreview);
            Controls.Add(pictureBox1);
            Controls.Add(buttonClear);
            Name = "task1b";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)picturePreview).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonClear;
        private PictureBox pictureBox1;
        private PictureBox picturePreview;
        private Button buttonLoad;
    }
}
