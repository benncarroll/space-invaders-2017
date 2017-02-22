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
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.tmrSlow = new System.Windows.Forms.Timer(this.components);
            this.gameProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.displayText = new System.Windows.Forms.Label();
            this.tmrMove = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // tmrGame
            // 
            this.tmrGame.Enabled = true;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // tmrSlow
            // 
            this.tmrSlow.Enabled = true;
            this.tmrSlow.Interval = 500;
            this.tmrSlow.Tick += new System.EventHandler(this.tmrSlow_Tick);
            // 
            // gameProgress
            // 
            this.gameProgress.Name = "gameProgress";
            this.gameProgress.Size = new System.Drawing.Size(100, 15);
            // 
            // displayText
            // 
            this.displayText.BackColor = System.Drawing.Color.Transparent;
            this.displayText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayText.Font = new System.Drawing.Font("NSimSun", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.displayText.Location = new System.Drawing.Point(0, 0);
            this.displayText.Name = "displayText";
            this.displayText.Size = new System.Drawing.Size(869, 553);
            this.displayText.TabIndex = 0;
            this.displayText.Text = "placeholder";
            this.displayText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.displayText.Visible = false;
            // 
            // tmrMove
            // 
            this.tmrMove.Enabled = true;
            this.tmrMove.Tick += new System.EventHandler(this.tmrMove_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(869, 553);
            this.Controls.Add(this.displayText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Space Invaders!";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.Timer tmrSlow;
        private System.Windows.Forms.ToolStripProgressBar gameProgress;
        private System.Windows.Forms.Label displayText;
        private System.Windows.Forms.Timer tmrMove;
    }
}

