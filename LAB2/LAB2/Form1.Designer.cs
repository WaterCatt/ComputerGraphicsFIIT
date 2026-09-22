namespace LAB2
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
            LoadButton = new Button();
            pictureOriginal = new PictureBox();
            pictureHDTV = new PictureBox();
            pictureNTSC = new PictureBox();
            pictureDif = new PictureBox();
            panelNTSC = new Panel();
            panelHDTV = new Panel();
            panelDif = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureOriginal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureHDTV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureNTSC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureDif).BeginInit();
            SuspendLayout();
            // 
            // LoadButton
            // 
            LoadButton.Anchor = AnchorStyles.Top;
            LoadButton.Location = new Point(552, 368);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(174, 30);
            LoadButton.TabIndex = 0;
            LoadButton.Text = "Загрузить изображение";
            LoadButton.UseVisualStyleBackColor = true;
            LoadButton.Click += LoadButton_Click;
            // 
            // pictureOriginal
            // 
            pictureOriginal.Anchor = AnchorStyles.Top;
            pictureOriginal.BackColor = Color.White;
            pictureOriginal.Location = new Point(464, 12);
            pictureOriginal.Name = "pictureOriginal";
            pictureOriginal.Size = new Size(350, 350);
            pictureOriginal.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureOriginal.TabIndex = 1;
            pictureOriginal.TabStop = false;
            // 
            // pictureHDTV
            // 
            pictureHDTV.Anchor = AnchorStyles.Top;
            pictureHDTV.BackColor = Color.White;
            pictureHDTV.Location = new Point(464, 413);
            pictureHDTV.Name = "pictureHDTV";
            pictureHDTV.Size = new Size(350, 350);
            pictureHDTV.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureHDTV.TabIndex = 2;
            pictureHDTV.TabStop = false;
            // 
            // pictureNTSC
            // 
            pictureNTSC.Anchor = AnchorStyles.Top;
            pictureNTSC.BackColor = Color.White;
            pictureNTSC.Location = new Point(12, 413);
            pictureNTSC.Name = "pictureNTSC";
            pictureNTSC.Size = new Size(350, 350);
            pictureNTSC.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureNTSC.TabIndex = 3;
            pictureNTSC.TabStop = false;
            // 
            // pictureDif
            // 
            pictureDif.Anchor = AnchorStyles.Top;
            pictureDif.BackColor = Color.White;
            pictureDif.Location = new Point(922, 413);
            pictureDif.Name = "pictureDif";
            pictureDif.Size = new Size(350, 350);
            pictureDif.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureDif.TabIndex = 4;
            pictureDif.TabStop = false;
            // 
            // panelNTSC
            // 
            panelNTSC.Anchor = AnchorStyles.Top;
            panelNTSC.BackColor = Color.White;
            panelNTSC.Location = new Point(12, 794);
            panelNTSC.Name = "panelNTSC";
            panelNTSC.Size = new Size(350, 189);
            panelNTSC.TabIndex = 5;
            panelNTSC.Paint += panelNTSC_Paint;
            // 
            // panelHDTV
            // 
            panelHDTV.Anchor = AnchorStyles.Top;
            panelHDTV.BackColor = Color.White;
            panelHDTV.Location = new Point(464, 794);
            panelHDTV.Name = "panelHDTV";
            panelHDTV.Size = new Size(350, 189);
            panelHDTV.TabIndex = 6;
            panelHDTV.Paint += panelHDTV_Paint;
            // 
            // panelDif
            // 
            panelDif.Anchor = AnchorStyles.Top;
            panelDif.BackColor = Color.White;
            panelDif.Location = new Point(922, 794);
            panelDif.Name = "panelDif";
            panelDif.Size = new Size(350, 189);
            panelDif.TabIndex = 7;
            panelDif.Paint += panelDif_Paint;
            // 
            // Task1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1284, 1061);
            Controls.Add(panelDif);
            Controls.Add(panelHDTV);
            Controls.Add(panelNTSC);
            Controls.Add(pictureDif);
            Controls.Add(pictureNTSC);
            Controls.Add(pictureHDTV);
            Controls.Add(pictureOriginal);
            Controls.Add(LoadButton);
            Name = "Task1";
            Text = "Task 1";
            ((System.ComponentModel.ISupportInitialize)pictureOriginal).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureHDTV).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureNTSC).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureDif).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button LoadButton;
        private PictureBox pictureOriginal;
        private PictureBox pictureHDTV;
        private PictureBox pictureNTSC;
        private PictureBox pictureDif;
        private Panel panelNTSC;
        private Panel panelHDTV;
        private Panel panelDif;
    }
}
