namespace kutuphane_otomasyonu
{
    partial class YoneticiMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoneticiMenu));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.alta_al2 = new System.Windows.Forms.Label();
            this.ekrankapa_2 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_ke = new Bunifu.Framework.UI.BunifuThinButton2();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ekrankapa_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 27);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // alta_al2
            // 
            this.alta_al2.AutoSize = true;
            this.alta_al2.BackColor = System.Drawing.Color.White;
            this.alta_al2.Location = new System.Drawing.Point(626, 8);
            this.alta_al2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.alta_al2.Name = "alta_al2";
            this.alta_al2.Size = new System.Drawing.Size(28, 16);
            this.alta_al2.TabIndex = 29;
            this.alta_al2.Text = "___";
            this.alta_al2.Click += new System.EventHandler(this.alta_al2_Click);
            // 
            // ekrankapa_2
            // 
            this.ekrankapa_2.BackColor = System.Drawing.Color.White;
            this.ekrankapa_2.Image = ((System.Drawing.Image)(resources.GetObject("ekrankapa_2.Image")));
            this.ekrankapa_2.Location = new System.Drawing.Point(663, 7);
            this.ekrankapa_2.Margin = new System.Windows.Forms.Padding(4);
            this.ekrankapa_2.Name = "ekrankapa_2";
            this.ekrankapa_2.Size = new System.Drawing.Size(21, 20);
            this.ekrankapa_2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ekrankapa_2.TabIndex = 28;
            this.ekrankapa_2.TabStop = false;
            this.ekrankapa_2.Click += new System.EventHandler(this.ekrankapa_2_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(257, 122);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(151, 118);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 31;
            this.pictureBox2.TabStop = false;
            // 
            // btn_ke
            // 
            this.btn_ke.ActiveBorderThickness = 1;
            this.btn_ke.ActiveCornerRadius = 20;
            this.btn_ke.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_ke.ActiveForecolor = System.Drawing.Color.Black;
            this.btn_ke.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_ke.BackColor = System.Drawing.Color.White;
            this.btn_ke.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ke.BackgroundImage")));
            this.btn_ke.ButtonText = "Çalışan İşlemleri";
            this.btn_ke.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ke.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_ke.ForeColor = System.Drawing.Color.Black;
            this.btn_ke.IdleBorderThickness = 1;
            this.btn_ke.IdleCornerRadius = 20;
            this.btn_ke.IdleFillColor = System.Drawing.Color.SaddleBrown;
            this.btn_ke.IdleForecolor = System.Drawing.Color.Black;
            this.btn_ke.IdleLineColor = System.Drawing.Color.SaddleBrown;
            this.btn_ke.Location = new System.Drawing.Point(192, 249);
            this.btn_ke.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btn_ke.Name = "btn_ke";
            this.btn_ke.Size = new System.Drawing.Size(292, 54);
            this.btn_ke.TabIndex = 32;
            this.btn_ke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btn_ke.Click += new System.EventHandler(this.btn_ke_Click);
            // 
            // YoneticiMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 448);
            this.Controls.Add(this.btn_ke);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.alta_al2);
            this.Controls.Add(this.ekrankapa_2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "YoneticiMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ekrankapa_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label alta_al2;
        private System.Windows.Forms.PictureBox ekrankapa_2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private Bunifu.Framework.UI.BunifuThinButton2 btn_ke;
    }
}