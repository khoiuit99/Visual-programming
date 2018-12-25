namespace Doan
{
    partial class Caroplayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Caroplayer));
            this.pnplay = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnhienthi = new System.Windows.Forms.Panel();
            this.btnquit = new System.Windows.Forms.Button();
            this.txbLAN = new System.Windows.Forms.TextBox();
            this.btnLAN = new System.Windows.Forms.Button();
            this.prgcooldown = new System.Windows.Forms.ProgressBar();
            this.picMark = new System.Windows.Forms.PictureBox();
            this.btnagain = new System.Windows.Forms.Button();
            this.textName = new System.Windows.Forms.TextBox();
            this.timecooldown = new System.Windows.Forms.Timer(this.components);
            this.pnhienthi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMark)).BeginInit();
            this.SuspendLayout();
            // 
            // pnplay
            // 
            this.pnplay.Location = new System.Drawing.Point(1, 1);
            this.pnplay.Name = "pnplay";
            this.pnplay.Size = new System.Drawing.Size(491, 489);
            this.pnplay.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(498, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(304, 289);
            this.panel2.TabIndex = 1;
            // 
            // pnhienthi
            // 
            this.pnhienthi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnhienthi.Controls.Add(this.btnquit);
            this.pnhienthi.Controls.Add(this.txbLAN);
            this.pnhienthi.Controls.Add(this.btnLAN);
            this.pnhienthi.Controls.Add(this.prgcooldown);
            this.pnhienthi.Controls.Add(this.picMark);
            this.pnhienthi.Controls.Add(this.btnagain);
            this.pnhienthi.Controls.Add(this.textName);
            this.pnhienthi.Location = new System.Drawing.Point(498, 296);
            this.pnhienthi.Name = "pnhienthi";
            this.pnhienthi.Size = new System.Drawing.Size(304, 194);
            this.pnhienthi.TabIndex = 2;
            // 
            // btnquit
            // 
            this.btnquit.Font = new System.Drawing.Font("Lucida Sans Typewriter", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnquit.Location = new System.Drawing.Point(4, 167);
            this.btnquit.Name = "btnquit";
            this.btnquit.Size = new System.Drawing.Size(191, 23);
            this.btnquit.TabIndex = 10;
            this.btnquit.Text = "Quit";
            this.btnquit.UseVisualStyleBackColor = true;
            this.btnquit.Click += new System.EventHandler(this.btnquit_Click);
            // 
            // txbLAN
            // 
            this.txbLAN.Location = new System.Drawing.Point(1, 108);
            this.txbLAN.Name = "txbLAN";
            this.txbLAN.Size = new System.Drawing.Size(194, 20);
            this.txbLAN.TabIndex = 8;
            // 
            // btnLAN
            // 
            this.btnLAN.Font = new System.Drawing.Font("Lucida Handwriting", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLAN.Location = new System.Drawing.Point(1, 134);
            this.btnLAN.Name = "btnLAN";
            this.btnLAN.Size = new System.Drawing.Size(195, 23);
            this.btnLAN.TabIndex = 7;
            this.btnLAN.Text = "Connect LAN";
            this.btnLAN.UseVisualStyleBackColor = true;
            this.btnLAN.Click += new System.EventHandler(this.btnLAN_Click);
            // 
            // prgcooldown
            // 
            this.prgcooldown.Location = new System.Drawing.Point(1, 29);
            this.prgcooldown.Name = "prgcooldown";
            this.prgcooldown.Size = new System.Drawing.Size(194, 23);
            this.prgcooldown.TabIndex = 6;
            // 
            // picMark
            // 
            this.picMark.Location = new System.Drawing.Point(201, 3);
            this.picMark.Name = "picMark";
            this.picMark.Size = new System.Drawing.Size(100, 86);
            this.picMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMark.TabIndex = 5;
            this.picMark.TabStop = false;
            // 
            // btnagain
            // 
            this.btnagain.Font = new System.Drawing.Font("Lucida Handwriting", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnagain.Location = new System.Drawing.Point(1, 58);
            this.btnagain.Name = "btnagain";
            this.btnagain.Size = new System.Drawing.Size(195, 31);
            this.btnagain.TabIndex = 3;
            this.btnagain.Text = "Play Again";
            this.btnagain.UseVisualStyleBackColor = true;
            this.btnagain.Click += new System.EventHandler(this.btnagain_Click);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(0, 3);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(195, 20);
            this.textName.TabIndex = 2;
            // 
            // timecooldown
            // 
            this.timecooldown.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Caroplayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 502);
            this.Controls.Add(this.pnhienthi);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnplay);
            this.Name = "Caroplayer";
            this.Text = "Game Caro";
            this.Shown += new System.EventHandler(this.Caroplayer_Shown);
            this.pnhienthi.ResumeLayout(false);
            this.pnhienthi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnplay;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnhienthi;
        private System.Windows.Forms.Timer timecooldown;
        private System.Windows.Forms.Button btnagain;
        private System.Windows.Forms.TextBox textName;
        private System.Windows.Forms.PictureBox picMark;
        private System.Windows.Forms.ProgressBar prgcooldown;
        private System.Windows.Forms.TextBox txbLAN;
        private System.Windows.Forms.Button btnLAN;
        private System.Windows.Forms.Button btnquit;
    }
}