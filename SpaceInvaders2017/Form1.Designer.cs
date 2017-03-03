namespace SpaceInvaders2017
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tmrPowerUp = new System.Windows.Forms.Timer(this.components);
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
            this.tmrFireRate = new System.Windows.Forms.Timer(this.components);
            this.beginnerButton = new System.Windows.Forms.Button();
            this.expertButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrPowerUp
            // 
            this.tmrPowerUp.Interval = 10000;
            this.tmrPowerUp.Tick += new System.EventHandler(this.tmrPowerUp_Tick);
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
            this.displayText.Size = new System.Drawing.Size(784, 0);
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
            this.debugText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.debugText.Location = new System.Drawing.Point(745, 0);
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
            this.logoBox.Image = ((System.Drawing.Image)(resources.GetObject("logoBox.Image")));
            this.logoBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("logoBox.InitialImage")));
            this.logoBox.Location = new System.Drawing.Point(200, 44);
            this.logoBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.logoBox.Name = "logoBox";
            this.logoBox.Size = new System.Drawing.Size(386, 168);
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
            // tmrFireRate
            // 
            this.tmrFireRate.Enabled = true;
            this.tmrFireRate.Interval = 500;
            this.tmrFireRate.Tick += new System.EventHandler(this.tmrFireRate_Tick);
            // 
            // beginnerButton
            // 
            this.beginnerButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.beginnerButton.BackColor = System.Drawing.Color.Black;
            this.beginnerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.beginnerButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.beginnerButton.Location = new System.Drawing.Point(264, 495);
            this.beginnerButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.beginnerButton.Name = "beginnerButton";
            this.beginnerButton.Size = new System.Drawing.Size(101, 38);
            this.beginnerButton.TabIndex = 3;
            this.beginnerButton.Text = "Beginner";
            this.beginnerButton.UseVisualStyleBackColor = false;
            this.beginnerButton.Click += new System.EventHandler(this.beginnerButton_Click);
            // 
            // expertButton
            // 
            this.expertButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.expertButton.BackColor = System.Drawing.Color.Black;
            this.expertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.expertButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.expertButton.Location = new System.Drawing.Point(410, 495);
            this.expertButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.expertButton.Name = "expertButton";
            this.expertButton.Size = new System.Drawing.Size(101, 38);
            this.expertButton.TabIndex = 4;
            this.expertButton.Text = "Expert";
            this.expertButton.UseVisualStyleBackColor = false;
            this.expertButton.Click += new System.EventHandler(this.expertButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::SpaceInvaders2017.Properties.Resources.starsmedium;
            this.ClientSize = new System.Drawing.Size(784, 0);
            this.Controls.Add(this.expertButton);
            this.Controls.Add(this.beginnerButton);
            this.Controls.Add(this.logoBox);
            this.Controls.Add(this.debugText);
            this.Controls.Add(this.displayText);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Space Invaders!";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrPowerUp;
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
        private System.Windows.Forms.Timer tmrFireRate;
        private System.Windows.Forms.Button beginnerButton;
        private System.Windows.Forms.Button expertButton;
    }
}

