namespace SpaceInvaders2017
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tmrPowerUp = new System.Windows.Forms.Timer(this.components);
            this.statusLabel = new System.Windows.Forms.Label();
            this.tmrSlow = new System.Windows.Forms.Timer(this.components);
            this.displayText = new System.Windows.Forms.Label();
            this.tmrMove = new System.Windows.Forms.Timer(this.components);
            this.tmrFast = new System.Windows.Forms.Timer(this.components);
            this.debugText = new System.Windows.Forms.Label();
            this.tmrTextFlash = new System.Windows.Forms.Timer(this.components);
            this.debugUpdate = new System.Windows.Forms.Timer(this.components);
            this.logoBox = new System.Windows.Forms.PictureBox();
            this.tmrLogoAnimation = new System.Windows.Forms.Timer(this.components);
            this.tmrWindowAnimation = new System.Windows.Forms.Timer(this.components);
            this.buttonBeginner = new System.Windows.Forms.Button();
            this.buttonExpert = new System.Windows.Forms.Button();
            this.tmrScreenFlash = new System.Windows.Forms.Timer(this.components);
            this.screenFlashBox = new System.Windows.Forms.Label();
            this.buttonInstructions = new System.Windows.Forms.Button();
            this.tmrClose = new System.Windows.Forms.Timer(this.components);
            this.tmrStun = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPowerUp
            // 
            this.tmrPowerUp.Interval = 2000;
            this.tmrPowerUp.Tick += new System.EventHandler(this.tmrPowerUp_Tick);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.BackColor = System.Drawing.Color.Transparent;
            this.statusLabel.Font = new System.Drawing.Font("NSimSun", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLabel.ForeColor = System.Drawing.Color.Lime;
            this.statusLabel.Location = new System.Drawing.Point(10, 41);
            this.statusLabel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(373, 30);
            this.statusLabel.TabIndex = 7;
            this.statusLabel.Text = "test";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusLabel.Visible = false;
            // 
            // tmrSlow
            // 
            this.tmrSlow.Interval = 500;
            this.tmrSlow.Tick += new System.EventHandler(this.tmrSlow_Tick);
            // 
            // displayText
            // 
            this.displayText.BackColor = System.Drawing.Color.Transparent;
            this.displayText.Cursor = System.Windows.Forms.Cursors.Cross;
            this.displayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayText.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayText.ForeColor = System.Drawing.Color.Lime;
            this.displayText.Location = new System.Drawing.Point(0, 0);
            this.displayText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.displayText.Name = "displayText";
            this.displayText.Size = new System.Drawing.Size(627, 96);
            this.displayText.TabIndex = 0;
            this.displayText.Text = "displayText";
            this.displayText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.displayText.Visible = false;
            // 
            // tmrMove
            // 
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
            // 
            // tmrFast
            // 
            this.tmrFast.Interval = 30;
            this.tmrFast.Tick += new System.EventHandler(this.tmrFast_Tick);
            // 
            // debugText
            // 
            this.debugText.AutoSize = true;
            this.debugText.BackColor = System.Drawing.Color.Black;
            this.debugText.Dock = System.Windows.Forms.DockStyle.Right;
            this.debugText.ForeColor = System.Drawing.Color.Green;
            this.debugText.Location = new System.Drawing.Point(588, 0);
            this.debugText.Margin = new System.Windows.Forms.Padding(10);
            this.debugText.Name = "debugText";
            this.debugText.Size = new System.Drawing.Size(39, 13);
            this.debugText.TabIndex = 1;
            this.debugText.Text = "Debug";
            this.debugText.Visible = false;
            // 
            // tmrTextFlash
            // 
            this.tmrTextFlash.Interval = 400;
            this.tmrTextFlash.Tick += new System.EventHandler(this.tmrTextFlash_Tick);
            // 
            // debugUpdate
            // 
            this.debugUpdate.Interval = 20;
            this.debugUpdate.Tick += new System.EventHandler(this.debugUpdate_Tick);
            // 
            // logoBox
            // 
            this.logoBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.logoBox.BackColor = System.Drawing.Color.Transparent;
            this.logoBox.Image = global::SpaceInvaders2017.Properties.Resources.Space_invaders_logo;
            this.logoBox.Location = new System.Drawing.Point(-39, 20);
            this.logoBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(705, 294);
            this.logoBox.TabIndex = 2;
            this.logoBox.TabStop = false;
            // 
            // tmrLogoAnimation
            // 
            this.tmrLogoAnimation.Interval = 10;
            this.tmrLogoAnimation.Tick += new System.EventHandler(this.tmrLogoAnimation_Tick);
            // 
            // tmrWindowAnimation
            // 
            this.tmrWindowAnimation.Enabled = true;
            this.tmrWindowAnimation.Interval = 10;
            this.tmrWindowAnimation.Tick += new System.EventHandler(this.tmrWindowAnimation_Tick);
            // 
            // buttonBeginner
            // 
            this.buttonBeginner.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonBeginner.BackColor = System.Drawing.Color.Black;
            this.buttonBeginner.Enabled = false;
            this.buttonBeginner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBeginner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonBeginner.Location = new System.Drawing.Point(114, 538);
            this.buttonBeginner.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonBeginner.Name = "buttonBeginner";
            this.buttonBeginner.Size = new System.Drawing.Size(101, 38);
            this.buttonBeginner.TabIndex = 3;
            this.buttonBeginner.Text = "Beginner";
            this.buttonBeginner.UseVisualStyleBackColor = false;
            this.buttonBeginner.Click += new System.EventHandler(this.buttonBeginner_Click);
            // 
            // buttonExpert
            // 
            this.buttonExpert.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonExpert.BackColor = System.Drawing.Color.Black;
            this.buttonExpert.Enabled = false;
            this.buttonExpert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExpert.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonExpert.Location = new System.Drawing.Point(259, 538);
            this.buttonExpert.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonExpert.Name = "buttonExpert";
            this.buttonExpert.Size = new System.Drawing.Size(101, 38);
            this.buttonExpert.TabIndex = 4;
            this.buttonExpert.Text = "Expert";
            this.buttonExpert.UseVisualStyleBackColor = false;
            this.buttonExpert.Click += new System.EventHandler(this.buttonExpert_Click);
            // 
            // tmrScreenFlash
            // 
            this.tmrScreenFlash.Interval = 50;
            this.tmrScreenFlash.Tick += new System.EventHandler(this.tmrScreenFlash_Tick);
            // 
            // screenFlashBox
            // 
            this.screenFlashBox.BackColor = System.Drawing.Color.Transparent;
            this.screenFlashBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.screenFlashBox.Location = new System.Drawing.Point(0, 0);
            this.screenFlashBox.Name = "screenFlashBox";
            this.screenFlashBox.Size = new System.Drawing.Size(627, 96);
            this.screenFlashBox.TabIndex = 5;
            this.screenFlashBox.Visible = false;
            // 
            // buttonInstructions
            // 
            this.buttonInstructions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonInstructions.BackColor = System.Drawing.Color.Black;
            this.buttonInstructions.Enabled = false;
            this.buttonInstructions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInstructions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.buttonInstructions.Location = new System.Drawing.Point(403, 538);
            this.buttonInstructions.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.buttonInstructions.Name = "buttonInstructions";
            this.buttonInstructions.Size = new System.Drawing.Size(101, 38);
            this.buttonInstructions.TabIndex = 6;
            this.buttonInstructions.Text = "Instructions";
            this.buttonInstructions.UseVisualStyleBackColor = false;
            this.buttonInstructions.Click += new System.EventHandler(this.buttonInstructions_Click);
            // 
            // tmrClose
            // 
            this.tmrClose.Interval = 2000;
            this.tmrClose.Tick += new System.EventHandler(this.tmrClose_Tick);
            // 
            // tmrStun
            // 
            this.tmrStun.Interval = 5000;
            this.tmrStun.Tick += new System.EventHandler(this.tmrStun_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(627, 96);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.buttonInstructions);
            this.Controls.Add(this.buttonExpert);
            this.Controls.Add(this.buttonBeginner);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.debugText);
            this.Controls.Add(this.displayText);
            this.Controls.Add(this.screenFlashBox);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Invaders!";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrPowerUp;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Timer tmrSlow;
        private System.Windows.Forms.Label displayText;
        private System.Windows.Forms.Timer tmrMove;
        private System.Windows.Forms.Timer tmrFast;
        private System.Windows.Forms.Label debugText;
        private System.Windows.Forms.Timer tmrTextFlash;
        private System.Windows.Forms.Timer debugUpdate;
        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Timer tmrLogoAnimation;
        private System.Windows.Forms.Timer tmrWindowAnimation;
        private System.Windows.Forms.Button buttonBeginner;
        private System.Windows.Forms.Button buttonExpert;
        private System.Windows.Forms.Timer tmrScreenFlash;
        private System.Windows.Forms.Label screenFlashBox;
        private System.Windows.Forms.Button buttonInstructions;
        private System.Windows.Forms.Timer tmrClose;
        private System.Windows.Forms.Timer tmrStun;
    }
}

