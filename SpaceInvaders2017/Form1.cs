using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Windows.Media;
using System.IO;

#pragma warning disable CS0252

namespace SpaceInvaders2017
{
    public partial class Form1 : Form
    {

        //------------------------------------------------------------------------
        //---------------------- GLOBAL VARIABLES --------------------------------
        //------------------------------------------------------------------------

        // Defines alien array, and other important global variables 
        // needed in various parts of the game

        List<PictureBox> arrAliens = new List<PictureBox>();
        List<PictureBox> arrBullets = new List<PictureBox>();

        bool moveDir = true;
        bool finished = false;
        bool playerdied = false;
        bool started = false;
        bool windowAnimVar = true;
        bool allowFire = true;
        bool noWaveDupe = false;
        bool allowUpdate = true;
        bool pwrUsed = false;
        bool specialFireHelper = false;
        bool specialFireHelper2 = false;
        bool powerUpTextHelper = false;
        bool playedBefore = false;
        bool laserOwned = false;

        int highScore = 0;
        int playerHealth = 5;
        int playerAllowedHealth = 5;
        int ax = 3;
        int ay = 10;
        int shipDir = 0;
        int scHeight = 0;
        int scWidth = 0;
        int flasherHelper = 0;
        int waveNumber = 0;
        int bulletcount = 0;
        int allowedBulletCount = 5;
        int stretchSpeed = 2;
        int specialFireCounter = 0;
        int playerScore = 0;

        PictureBox ship = new PictureBox();
        PictureBox playerHealthBar = new PictureBox();
        PictureBox powerUp = new PictureBox();

        Random r = new Random();
        KeyPressEventArgs checkKeysHelper = new KeyPressEventArgs((char)1);
        MediaPlayer backgroundMusic = new MediaPlayer();



        //------------------------------------------------------------------------
        //----------------------- Event Listeners --------------------------------
        //------------------------------------------------------------------------


        public Form1()
        {

            InitializeComponent();
            this.Click += Form1_Click;
            this.KeyPress += Form1_KeyPress;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;

        }

        // Is called when any alphanumeric key pressed, and checks it against a number
        // of different keys which proceed to call other functions
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            checkKeysHelper = e;
            if (checkKeys(" ") && !finished && started)
            {
                createBullet(0);
            }

            if (checkKeys("n") || checkKeys("N") && finished && !playerdied && started)
            {
                newWave();
            }
            if (checkKeys("r") || checkKeys("R") && finished && playerdied && started)
            {
                newWave();
                playerdied = false;
            }
            if (checkKeys("O") || checkKeys("o"))
            {
                toggleDebug();
                fileHandler(0);

            }
            if (checkKeys("q") || checkKeys("Q"))
            {
                stretchSpeed = 5;
            }
            if (checkKeys("b") || checkKeys("B"))
            {
                if (specialFireHelper2)
                {
                    powerUpTextHelper = true;
                    specialFireHelper = true;
                    specialFireHelper2 = false;
                }
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                shipDir = -10;
            }
            else if (e.KeyCode == Keys.Right)
            {
                shipDir = 10;
            }

            if (e.KeyCode == Keys.Escape)
            {
                string savedScore = playerScore.ToString();
                gameOver();
                tmrPowerUp.Enabled = false;
                statusLabel.Dispose();
                setText("YOU SCORED " + savedScore + " POINTS.\n\nTHANKS FOR PLAYING", 0);
                tmrClose.Enabled = true;
            }

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                shipDir = 0;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (!msg.HWnd.Equals(Handle) &&
                (keyData == Keys.Left || keyData == Keys.Right ||
                 keyData == Keys.Up || keyData == Keys.Down || 
                 keyData == Keys.Space || keyData == Keys.Escape))
                return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }


        // This function is called on load, and is used to generate a ship
        // and some aliens for the first wave.
        private void Form1_Load(object sender, EventArgs e)
        {
            fileHandler(0);

            this.Size = new Size(550, 0);

            scHeight = Screen.FromControl(this).Bounds.Height;
            scWidth = Screen.FromControl(this).Bounds.Width;

            powerUp.Visible = false;
            //    "pack://application:,,,/Resources/default1.mp3"
            //    "C:\\Users\\Ben Carroll\\BitBucket\\Ben's Space Invaders\\SpaceInvaders2017\\sound\\gameMusic.mp3"
            backgroundMusic.Open(new Uri("C:\\Users\\Ben Carroll\\BitBucket\\Ben's Space Invaders\\SpaceInvaders2017\\sound\\gameMusic.mp3  "));
            backgroundMusic.MediaEnded += new EventHandler(Media_Ended);
            backgroundMusic.Play();
            backgroundMusic.SpeedRatio = 1;

        }
        private void Media_Ended(object sender, EventArgs e)
        {
            backgroundMusic.Position = TimeSpan.FromSeconds(111);
            backgroundMusic.Play();
        }


        // This function is called when the user clicks anywhere on the window
        private void Form1_Click(object sender, EventArgs e)
        {
            if (started && !finished)
            {
                createBullet(0);
            }
        }

        // These functions are called when a home screen button is clicked
        // and set game variables based on difiiculty.
        private void buttonBeginner_Click(object sender, EventArgs e)
        {
            allowedBulletCount = 3;
            playerAllowedHealth = 5;
            startGame();
            this.Focus();

        }
        private void buttonExpert_Click(object sender, EventArgs e)
        {
            allowedBulletCount = 1;
            playerAllowedHealth = 2;
            startGame();
            this.Focus();

        }
        private void buttonInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CONTROLS:\n\nMOVE SHIP:      ←  →\nFIRE:                   SPACE\nFIRE LASER:       B");
            this.Focus();
        }


        //------------------------------------------------------------------------
        //------------------------ Game Initiation -------------------------------
        //------------------------------------------------------------------------

        // This function is called at the very start of a game, and
        // starts the timers and spawns the aliens and ship.
        private void startGame()
        {
            if (!noWaveDupe)
            {
                started = true;
                noWaveDupe = true;
                // Generates ship and makes it a global variable to be called in other functions
                createShip();
                ship = (PictureBox)Controls["ship"];
                playerHealthBar = (PictureBox)Controls["playerHealthBar"];
                createAliens(3, 10);
                hideText(0);
                playerHealth = playerAllowedHealth;
                tmrLogoAnimation.Enabled = true;
                tmrFast.Enabled = true;
                tmrSlow.Enabled = true;
                tmrPowerUp.Enabled = true;
                tmrMove.Enabled = true;
            }
        }

        // This function calls a new wave of aliens after the user has 
        // completed a level.
        private void newWave()
        {
            if (finished || !started)
            {
                // Disables text
                hideText(0);
                finished = false;
                shipDir = 0;
                tmrMove.Interval = 100;
                ship.Location = new Point(this.Width / 2, this.Height - 100);

                playerHealth = playerAllowedHealth;
                updateplayerHealthBar();

                //Generates aliens based on current level.
                //Choose type of level 
                if (waveNumber % 5 == 0)
                {
                    createBoss();
                }
                else
                {
                    createAliens(ax, ay);
                }
            }
        }


        //------------------------------------------------------------------------
        //---------------------------- Generation --------------------------------
        //------------------------------------------------------------------------

        // This function is called when the user presses the fire key, 
        // and spawn a bullet headed toward the aliens.
        private void createBullet(int a)
        {
            updateBulletCount();

            if (a == 1) allowFire = true;

            if (allowFire)
            {
                // Generates bullet
                PictureBox shipbullet = new PictureBox();
                shipbullet.Size = new Size(2, 15);
                shipbullet.Name = "shipbullet";
                shipbullet.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
                // Places bullet within ship bounds
                shipbullet.Location = new Point(ship.Left + (ship.Width / 2) - (shipbullet.Size.Width / 2), ship.Top);
                playSound(2);
                arrBullets.Add(shipbullet);
                Controls.Add(shipbullet);
            }
            updateBulletCount();
        }

        // This function is called at the very start of a game and 
        // generates the ship for the user to interact with.
        private void createShip()
        {
            // Generates ship
            PictureBox ship = new PictureBox();
            ship.Size = new Size(44, 26);
            ship.BackColor = System.Drawing.Color.Black;
            ship.Image = SpaceInvaders2017.Properties.Resources.ship;
            ship.Name = "ship";
            ship.Location = new Point(this.Width / 2, this.Height - 100);
            Controls.Add(ship);

            // Generates Health Bar
            PictureBox playerHealthBar = new PictureBox();
            playerHealthBar.Size = new Size(this.Width, 10);
            playerHealthBar.BackColor = System.Drawing.Color.FromArgb(0, 255, 0);
            playerHealthBar.Name = "playerHealthBar";
            playerHealthBar.Location = new Point(0, this.Height - 10);
            Controls.Add(playerHealthBar);

        }

        // This function is called every wave, and takes variables 
        // corresponding to the amount of rows and columns of aliens
        // requested.
        private void createAliens(int f, int g)
        {
            // Loops based on amount of aliens specified
            for (int y = 0; y < f; y++)
            {
                for (int x = 0; x < g; x++)
                {
                    // Creates new alien
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(70 * x + 50, 70 * y + 50);
                    pic.BackColor = System.Drawing.Color.Black;
                    //System.Drawing.Color.FromArgb(r.Next(1, 256), r.Next(1, 256), r.Next(1, 256));
                    pic.Name = "alien";
                    // Adds new alien to array and Controls
                    Controls.Add(pic);
                    arrAliens.Add(pic);

                    int b = -(y - f) - 1;

                    switch (b)
                    {
                        case 0:
                            pic.Image = SpaceInvaders2017.Properties.Resources.alien1;
                            pic.Tag = "alien1";
                            pic.Size = new Size(42, 29);
                            break;
                        case 1:
                            pic.Image = SpaceInvaders2017.Properties.Resources.alien2;
                            pic.Tag = "alien2";
                            pic.Size = new Size(44, 33);
                            break;
                        default:
                            pic.Image = SpaceInvaders2017.Properties.Resources.alien3;
                            pic.Tag = "alien3";
                            pic.Size = new Size(39, 36);
                            break;
                    }

                }
            }

            waveNumber++;

        }

        // Create boss alien.
        private void createBoss()
        {
            //Temporary filler until function is made.
            createAliens(ax, ay);

        }

        // This function is called on a repeating timer and allows the
        // aliens to shoot back at the ship.
        private void shootAtShip(int a)
        {
            if (finished == false && started == true)
            {
                // Picks a random integer from a pre-set Random variable
                int x = r.Next(0, arrAliens.Count);

                // Creates a new alienbullet
                PictureBox pic = new PictureBox();
                pic.Size = new Size(2, 15);
                pic.BackColor = System.Drawing.Color.White;
                pic.Name = "alienbullet";

                switch (a)
                {
                    case 0:
                        // Places it at the base of a randomly selected alien and adds it to Controls
                        pic.Location = new Point(arrAliens[x].Left + (arrAliens[x].Width / 2), arrAliens[x].Bottom);
                        break;
                    case 1:
                        pic.Location = new Point(arrAliens[0].Left + (arrAliens[0].Width / 2), arrAliens[0].Bottom);
                        break;
                    case 2:
                        int b = arrAliens.Count - 1;
                        pic.Location = new Point(arrAliens[b].Left + (arrAliens[b].Width / 2), arrAliens[b].Bottom);
                        break;
                }


                arrBullets.Add(pic);
                Controls.Add(pic);
            }
        }

        // This function is used to generate a power up
        private void createPowerUp()
        {
            int type = r.Next(0, 3);
            int position = r.Next(60, this.Width - 90);

            powerUp.BackColor = System.Drawing.Color.White;
            powerUp.Size = new Size(30, 30);
            powerUp.Name = "powerUp";
            powerUp.Location = new Point(position, this.Height - 100);

            if (!pwrUsed) pwrUsed = true; Controls.Add(powerUp);

            switch (type)
            {
                case 0:                 
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.stun;
                    powerUp.Tag = "stun";
                    break;
                case 2:
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.laser;
                    powerUp.Tag = "laser";
                    break;
                case 1:
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.health;
                    powerUp.Tag = "health";
                    break;
            }

            powerUp.Visible = true;
            powerUp = (PictureBox)Controls["powerUp"];

        }


        //------------------------------------------------------------------------
        //-------------------------- Timers --------------------------------------
        //------------------------------------------------------------------------


        // The following  functions are called when a timer
        // reaches its interval, and call certain functions to
        // update the game
        private void tmrMove_Tick(object sender, EventArgs e)
        {
            moveAliens();
            checkHits();
            deleteDead();
        }
        private void tmrSlow_Tick(object sender, EventArgs e)
        {
            shootAtShip(0);
            allowUpdate = true;
        }
        private void tmrFast_Tick(object sender, EventArgs e)
        {
            moveShip();
            moveBullet();
            checkHits();
            deleteDead();
            updatePlayerScore();
            checkEndGame();
            if (specialFireHelper) specialFire(2);
        }
        private void tmrPowerUp_Tick(object sender, EventArgs e)
        {
            createPowerUp();
        }
        private void tmrTextFlash_Tick(object sender, EventArgs e)
        {
            if (displayText.Visible && flasherHelper > 1)
            {
                displayText.Visible = false;
                buttonInstructions.BackColor = System.Drawing.Color.Black;


                flasherHelper = 0;
            }
            else if (!displayText.Visible)
            {
                displayText.Visible = true;
                if (!playedBefore)
                {

                    buttonInstructions.BackColor = System.Drawing.Color.Red;
                }
                flasherHelper++;
            }
            else
            {
                flasherHelper++;
            }

        }
        private void debugUpdate_Tick(object sender, EventArgs e)
        {
            debugText.Text = "Debug:\nhighScore: " + highScore.ToString() + "\nBullets Flying: " + arrBullets.Count.ToString() + "\nAliens Alive:" + arrAliens.Count.ToString() + "\nPlayer Health: " + playerHealth.ToString() + "\nPControls: " + Controls.Count.ToString();
        }
        private void tmrLogoAnimation_Tick(object sender, EventArgs e)
        {
            logoBox.Top -= 5;
            buttonExpert.Top -= 10;
            buttonBeginner.Top -= 10;
            buttonInstructions.Top -= 10;
            this.Focus();

            if (logoBox.Top + logoBox.Height < 0)
            {
                logoBox.Visible = false;
                logoBox.Enabled = false;
                tmrLogoAnimation.Enabled = false;
            }
            if (buttonBeginner.Top + 40 < 0)
            {
                Controls.Remove(buttonExpert);
                Controls.Remove(buttonBeginner);
                Controls.Remove(buttonInstructions);
                powerUpTextHelper = true;
            }
        }
        private void tmrWindowAnimation_Tick(object sender, EventArgs e)
        {
            this.Focus();

            if (this.Height < scHeight)
            {
                this.Height += stretchSpeed;
            }
            if (this.Width < scWidth)
            {
                this.Width += stretchSpeed;
            }
            if (windowAnimVar)
            {
                this.Location = new Point(scWidth / 2 - this.Width / 2, scHeight / 2 - this.Height / 2);
            }

            if (this.Height > 400)
            {
                if (!started)
                {
                    setText("WELCOME TO SPACE INVADERS\n\nSELECT A BUTTON:", 0);
                }

                buttonBeginner.Top = scHeight / 2 + 75;
                buttonExpert.Top = scHeight / 2 + 75;
                buttonInstructions.Top = scHeight / 2 + 75;

                powerUpTextHelper = true;
                setText("HIGHSCORE: " + highScore.ToString(), 1);
                powerUpTextHelper = false;

                buttonExpert.Enabled = true;
                buttonBeginner.Enabled = true;
                buttonInstructions.Enabled = true;
            }
            if (this.Height > scHeight - 1 && this.Width > scWidth - 1)
            {
                tmrWindowAnimation.Enabled = false;
                buttonExpert.Enabled = true;
                buttonBeginner.Enabled = true;
                buttonInstructions.Enabled = true;
                this.Top = scHeight / 2 - this.Height / 2;
                this.Location = new Point(0, 0);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

            }
        }
        private void tmrScreenFlash_Tick(object sender, EventArgs e)
        {
            screenFlash(1, 0, "");
        }
        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void tmrStun_Tick(object sender, EventArgs e)
        {
            tmrMove.Enabled = true;
            tmrSlow.Enabled = true;
        }

        //------------------------------------------------------------------------
        //--------------------- Updating Functions -------------------------------
        //------------------------------------------------------------------------

        // This function updates the size and Color of the health bar.
        private void updateplayerHealthBar()
        {
            playerHealthBar.Width = playerHealth * (this.Width / playerAllowedHealth);

            if (playerHealth > 3)
            {
                playerHealthBar.BackColor = System.Drawing.Color.Lime;
            }
            else if (playerHealth < 2)
            {
                playerHealthBar.BackColor = System.Drawing.Color.Red;
            }
            else if (playerHealth < 3 && playerAllowedHealth > 2)
            {
                playerHealthBar.BackColor = System.Drawing.Color.Orange;
            }
            else if (playerHealth < 4 && playerAllowedHealth > 2)
            {
                playerHealthBar.BackColor = System.Drawing.Color.Yellow;
            }
            else if (playerAllowedHealth == playerHealth)
            {
                playerHealthBar.BackColor = System.Drawing.Color.Lime;
            }

            screenFlash(0, 0, playerHealth.ToString());
        }

        // This function updates the total player bullet count, and determines
        // whether or not the user can fire.
        private void updateBulletCount()
        {
            bulletcount = 0;
            foreach (PictureBox b in Controls.OfType<PictureBox>())
            {
                if (b.Name == "shipbullet")
                {
                    bulletcount++;
                }
            }

            if (bulletcount >= allowedBulletCount) allowFire = false;
            if (bulletcount < allowedBulletCount) allowFire = true;
        }

        // This function updates the player score.
        private void updatePlayerScore()
        {
            if (powerUpTextHelper)
            {
                setText("SCORE: " + playerScore.ToString(), 1);
            }
        }


        //------------------------------------------------------------------------
        //--------------------- Utility Functions --------------------------------
        //------------------------------------------------------------------------

        // Used to handle all file operations. 
        // 0 = Check exists and set high score if exists
        // 1 = Write highscore
        // 2 = Creates file
        private void fileHandler(int a)
        {
            Console.WriteLine(buttonInstructions.Font.ToString());
            string path = "C:\\Users\\Public\\Documents\\SpaceInvaders\\data.txt";
            string path2 = "C:\\Users\\Public\\Documents\\SpaceInvaders\\";
            switch (a)
            {
                case 0:
                    if (!File.Exists(path))
                    {
                        fileHandler(2);
                    }
                    else
                    {
                        playedBefore = true;
                        string readText = File.ReadAllText(path);
                        try
                        {
                            highScore = Int32.Parse(readText);
                        }
                        catch
                        {
                            File.WriteAllText(path, "0");
                            readText = File.ReadAllText(path);
                            highScore = Int32.Parse(readText);
                        }
                        //MessageBox.Show(highScore.ToString());
                    }
                    break;
                case 1:
                    File.WriteAllText(path, highScore.ToString());
                    break;
                case 2:
                    playedBefore = false;
                    buttonInstructions.Font = new Font(buttonInstructions.Font, FontStyle.Bold); ;
                    string fileText = "0";
                    Directory.CreateDirectory(path2);
                    File.WriteAllText(path, fileText);
                    break;
            }
        }

        // This function is called in many places throughout
        // the game to display text on the screen..
        private void setText(string a, int b)
        {
            Control elementId = new Control();
            switch (b)
            {
                case 0:
                    elementId = displayText;
                    powerUp.Visible = false;
                    tmrTextFlash.Enabled = true;
                    ship.Visible = false;
                    playerHealthBar.Visible = false;
                    break;
                case 1:
                    elementId = statusLabel;
                    break;
            }
            // Sets Text property of the Label element to the incoming string
            elementId.Text = a;
            if (powerUpTextHelper) elementId.Visible = true;
        }

        // This function hides the text, and stops the flashing text
        // timer from running.
        private void hideText(int b)
        {
            Control elementId = new Control();
            switch (b)
            {
                case 0:
                    elementId = displayText;
                    break;
                case 1:
                    elementId = statusLabel;
                    break;
            }

            tmrTextFlash.Enabled = false;
            elementId.Visible = false;
            ship.Visible = true;
            playerHealthBar.Visible = true;
            powerUp.Visible = true;
        }

        // This function is used by the keypress checking function
        // to tell if two are the same.
        private bool checkKeys(string b)
        {
            if (checkKeysHelper.KeyChar.ToString() == b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // This function flashes the screen.
        private void screenFlash(int mode, int color, string text)
        {
            System.Drawing.Color flashColor = new System.Drawing.Color();

            switch (color)
            {
                case 1:
                    flashColor = System.Drawing.Color.FromArgb(30, 0, 255, 0);
                    break;
                default:
                    flashColor = System.Drawing.Color.FromArgb(30, 255, 0, 0);
                    break;
            }

            if (mode == 0)
            {
                tmrScreenFlash.Enabled = true;
                screenFlashBox.BackColor = flashColor;
                screenFlashBox.Text = text;
                screenFlashBox.Visible = true;

            }
            else if (mode == 1)
            {
                screenFlashBox.Visible = false;
                //this.BackColor = System.Drawing.Color.Black;
                tmrScreenFlash.Enabled = false;
            }

        }

        // This function is used to play various sounds and is called
        // in numerous functions.
        private void playSound(int a)
        {
            switch (a)
            {
                case 0:
                    SoundPlayer sound0 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.hit);
                    sound0.Play();
                    break;
                case 1:
                    SoundPlayer sound1 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.hit);
                    sound1.Play();
                    break;
                case 2:
                    SoundPlayer sound2 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.shoot);
                    sound2.Play();
                    break;
                case 3:
                    SoundPlayer sound3 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.death);
                    sound3.Play();
                    break;

            }


        }

        // This function toggles the visibility of the debug window.
        private void toggleDebug()
        {
            if (debugText.Visible == false)
            {
                debugText.Visible = true;
                debugUpdate.Enabled = true;
            }
            else if (debugText.Visible)
            {
                debugText.Visible = false;
                debugUpdate.Enabled = false;
            }

        }

        // This function is called when aliens hit a side, and handles
        // turning around the aliens properly. It also ensures gamers cannot
        // camp in corners by shooting every time it hits a side.
        private void hitSideCheck()
        {
            foreach (PictureBox alien in arrAliens)
            {

                if (alien.Left >= this.Width - 70)
                {
                    alienUpdate(false);
                }

                if (alien.Left <= 21)
                {
                    alienUpdate(true);
                }

            }
        }

        // Used with Laser powerup.
        private void specialFire(int a)
        {
            switch (a)
            {
                case 0:
                    laserOwned = true;
                    specialFireHelper2 = true;
                    specialFireCounter = 0;
                    powerUpTextHelper = false;
                    break;
                case 1:
                    createBullet(1);
                    break;
                case 2:
                    specialFireCounter++;
                    powerUpTextHelper = true;

                    if (specialFireCounter < 11)
                    {
                        specialFire(1);
                    }
                    else
                    {
                        specialFireHelper = false;
                    }
                    break;
            }
        }

        //------------------------------------------------------------------------
        //---------------------------- Movement ----------------------------------
        //------------------------------------------------------------------------

        // This function is called on a repeating timer and updates the
        // ship's position based on the keys pressed by the user
        private void moveShip()
        {
            if (!finished)
            {
                // Moves ahead ship in direction specified by movement keys
                ship.Left += shipDir;

                // Teleports ship to other side of screen when it reaches an edge
                if (ship.Left > this.Width)
                {
                    ship.Left = -ship.Width;
                }
                else if (ship.Left < -ship.Width)
                {
                    ship.Left = this.Width;
                }

                // Keeps ship spaced 100 pixels from the bottom of the game when the window is resized
                ship.Location = new Point(ship.Left, (this.Height - 100));
                playerHealthBar.Location = new Point(0, scHeight - 10);
                playerHealthBar.Width = this.Width / playerAllowedHealth * playerHealth;
            }
        }

        // This function is called on a repeating timer and updates the
        // position of the aliens in relation to walls around them
        private void moveAliens()
        {
            // Finds moving direction of aliens and updates each aliens position accordingly
            foreach (PictureBox alien in arrAliens)
            {
                if (moveDir == true)
                {
                    alien.Left += 5;
                }
                else
                {
                    alien.Left -= 5;
                }
            }

            checkEndGame();
            hitSideCheck();

            bool gameOverHelper = false;

            foreach (PictureBox p in arrAliens)
            {
                if (p.Top > this.Height || p.Bounds.IntersectsWith(ship.Bounds))
                {
                    gameOverHelper = true;
                }
            }

            if (gameOverHelper) gameOver();
        }

        // This function works with moveAliens() and hitSide() to move aliens down and
        // modify a timer when a wall is hit.
        private void alienUpdate(bool movement)
        {
            if (allowUpdate)
            {

                if (movement)
                {
                    shootAtShip(1);
                }
                else
                {
                    shootAtShip(2);
                }

                moveDir = movement;

                // Moves all aliens down
                foreach (PictureBox a in arrAliens)
                {
                    a.Top += 40;
                }

                // Modifies timer interval to a certain point.
                if (tmrMove.Interval > 40)
                {
                    tmrMove.Interval -= 10;
                }

                allowUpdate = false;
            }
        }

        // This function is called on a repeating timer and updates the
        // position of bullets fired by both the alien and the user
        private void moveBullet()
        {
            // Cycles through PictureBoxes checking if they are above the game height, and removes
            // them or if they are not it will move them up.
            foreach (PictureBox p in arrBullets)
            {
                if (p.Name == "shipbullet")
                {
                    if (p.Top < 0)
                    {
                        Controls.Remove(p);
                    }
                    else
                    {
                        p.Top -= 10;
                    }
                }
                if (p.Name == "alienbullet")
                {
                    if (p.Top > this.Height)
                    {
                        Controls.Remove(p);
                    }
                    else
                    {
                        p.Top += 10;
                    }
                }
            }
        }

        //------------------------------------------------------------------------
        //----------------------- Collision Detection ----------------------------
        //------------------------------------------------------------------------

        // This function is called on a repeating timer and checks whether
        // any aliens have been hit by a bullet.
        private void checkHits()
        {
            // Cycles thorugh each alien
            foreach (PictureBox a in arrAliens)
            {
                // Finds shipbullets/alienbullets
                foreach (PictureBox p in arrBullets)
                {
                    // Checks intersection with alien
                    if (p.Name == "shipbullet" && p.Bounds.IntersectsWith(a.Bounds))
                    {
                        // Labels Control for deletion
                        switch (a.Tag.ToString())
                        {
                            case "alien1":
                                playerScore++;
                                break;
                            case "alien2":
                                playerScore += 2;
                                break;
                            case "alien3":
                                playerScore += 3;
                                break;
                        }

                        p.Tag = "dead";
                        a.Tag = "dead";
                        playSound(1);
                    }
                }
            }

            foreach (PictureBox p in Controls.OfType<PictureBox>())
            {
                if (p.Name == "alienbullet" && p.Bounds.IntersectsWith(ship.Bounds))
                {
                    playerHealth -= 1;
                    updateplayerHealthBar();
                    checkEndGame();
                    playSound(3);
                    p.Tag = "dead";
                }
                if (p.Name == "alienbullet" && p.Top > this.Height)
                {
                    p.Tag = "dead";
                }

            }

            if (ship.Bounds.IntersectsWith(powerUp.Bounds))
            {
                powerUp.Visible = false;

                switch (powerUp.Tag.ToString())
                {
                    case "health":
                        playerHealth += 2;
                        if (playerHealth > playerAllowedHealth) playerHealth = playerAllowedHealth;
                        updateplayerHealthBar();
                        screenFlash(0, 1, "");
                        break;
                    case "laser":
                        specialFire(0);
                        setText("PRESS B TO FIRE LASER", 1);
                        break;
                    case "stun":
                        tmrStun.Enabled = false;
                        tmrStun.Enabled = true;
                        tmrSlow.Enabled = false;
                        tmrMove.Enabled = false;
                        break;
                }

                powerUp.Tag = "null";
            }


        }
        private void deleteDead()
        {
            foreach (PictureBox m in Controls.OfType<PictureBox>())
            {
                if (m.Tag == "dead")
                {
                    switch (m.Name)
                    {
                        case "alien":
                            arrAliens.Remove(m);
                            break;
                        case "shipbullet":
                            arrBullets.Remove(m);
                            break;
                        case "alienbullet":
                            arrBullets.Remove(m);
                            break;

                    }

                    Controls.Remove(m);
                    m.Dispose();

                }
            }
            if (arrBullets.Count > 0)
            {
                foreach (PictureBox m in Controls.OfType<PictureBox>())
                {
                    if (m.Tag == "dead")
                    {
                        arrBullets.Remove(m);
                        Controls.Remove(m);
                    }

                }
            }
        }
        private static bool deleteSpecifier(PictureBox s)
        {
            return s.Name.Contains("alien");
        }

        // This function checks to see if there are any aliens left, and
        // invokes the next wave or death screen. It now also checks to
        // if the player is dead, and calls the gameOver() function.
        private void checkEndGame()
        {
            // Checks number of aliens and that game is not finished
            if (arrAliens.Count < 1 && !finished)
            {

                finished = true;

                // Adds to alien count for next level
                if (ax < 15) ax++;
                if (ay < 10) ay++;

                // Displays text
                setText("PRESS N TO START NEXT WAVE\n\nINVADERS: " + (ax * ay), 0);

                clearScreen();
            }

            if (playerHealth <= 0 && !finished)
            {
                gameOver();
            }
        }
        private void clearScreen()
        {
            foreach (PictureBox s in arrAliens.OfType<PictureBox>())
            {
                s.Tag = "dead";
            }
            foreach (PictureBox s in Controls.OfType<PictureBox>())
            {
                arrAliens.Remove(s);
            }
            arrAliens.RemoveAll(deleteSpecifier);

            foreach (PictureBox s in arrBullets.OfType<PictureBox>())
            {
                s.Tag = "dead";
            }
            foreach (PictureBox s in Controls.OfType<PictureBox>())
            {
                arrBullets.Remove(s);
            }

        }
        private void clearAliens()
        {
            if (arrAliens.Count > 0)
            {
                foreach (PictureBox p in arrAliens.OfType<PictureBox>())
                {
                    p.Tag = "dead";
                }
            }
            deleteDead();
        }

        // These function is called to end a game.
        private void gameOver()
        {

            finished = true;
            playerdied = true;
            playerHealth = playerAllowedHealth;

            playSound(3);

            string smartSentence = "";
            if (arrAliens.Count < 6) smartSentence = "ONLY ";

            // Displays text
            setText("GAME OVER. YOU " + smartSentence + "HAD " + arrAliens.Count.ToString() + " LEFT TO KILL.\n\nYOU GOT TO ROUND " + waveNumber.ToString() + " WITH A SCORE OF " + playerScore.ToString() + ".\n\nPRESS R TO RESTART.", 0);

            clearScreen();
            if (playerScore > highScore) highScore = playerScore;
            fileHandler(1);
            playerScore = 0;

            ax = 3;
            ay = 10;

        }

    }
}
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
