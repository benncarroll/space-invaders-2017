﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

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
        List<Rectangle> arrStars = new List<Rectangle>();

        bool moveDir = true;
        bool finished = false;
        bool playerdied = false;
        bool started = false;
        bool windowAnimVar = true;
        bool allowFire = true;
        bool noWaveDupe = false;
        bool allowUpdate = true;
        bool pwrUsed = false;

        int playerHealth = 5;
        int playerAllowedHealth = 5;
        int aliensx = 3;
        int aliensy = 10;
        int shipDir = 0;
        int scHeight = 0;
        int scWidth = 0;
        int flasherHelper = 0;
        int waveNumber = 0;
        int bulletcount = 0;
        int allowedBulletCount = 5;

        PictureBox ship = new PictureBox();
        PictureBox playerHealthBar = new PictureBox();
        PictureBox powerUp = new PictureBox();
        Random r = new Random();
        KeyPressEventArgs checkKeysHelper = new KeyPressEventArgs((char)1);

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
                createBullet();
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
            if (checkKeys("O") || checkKeys ("o"))
            {
                toggleDebug();
                shootAtShip(2);
            }
            if (checkKeys("q") || checkKeys("Q"))
            {
                this.Height = 800;
                this.Width = 1000;
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

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            shipDir = 0;
        }

        // This function is called on load, and is used to generate a ship
        // and some aliens for the first wave.
        private void Form1_Load(object sender, EventArgs e)
        {
            scHeight = Screen.FromControl(this).Bounds.Height;
            scWidth = Screen.FromControl(this).Bounds.Width;

            powerUp.Visible = false;

        }

        // This function is called when the user clicks anywhere on the window
        private void Form1_Click(object sender, EventArgs e)
        {
            if (started && !finished)
            {
                createBullet();
            }
        }

        private void buttonBeginner_Click(object sender, EventArgs e)
        {
            //tmrFireRate.Interval = 400;
            allowedBulletCount = 5;		
            playerAllowedHealth = 5;
            startGame();
        }
        private void buttonExpert_Click(object sender, EventArgs e)
        {
            //tmrFireRate.Interval = 500;
            allowedBulletCount = 3;
            playerAllowedHealth = 2;
            startGame();
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
                hideText();
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
                hideText();
                tmrFast.Interval = 30;
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
                    createAliens(aliensx, aliensy);
                }
            }
        }


        //------------------------------------------------------------------------
        //---------------------------- Generation --------------------------------
        //------------------------------------------------------------------------

        // This function is called when the user presses the fire key, 
        // and spawn a bullet headed toward the aliens.
        private void createBullet()
        {
            updateBulletCount();
            if (allowFire)
            {
                // Generates bullet
                PictureBox shipbullet = new PictureBox();
                shipbullet.Size = new Size(3, 5);
                shipbullet.Name = "shipbullet";
                shipbullet.BackColor = Color.Red;
                // Places bullet within ship bounds
                shipbullet.Location = new Point(ship.Left + (ship.Width / 2) - (shipbullet.Size.Width / 2), ship.Top);
                playSound(2);
                arrBullets.Add(shipbullet);
                Controls.Add(shipbullet);
            }
        }

        // This function is called at the very start of a game and 
        // generates the ship for the user to interact with.
        private void createShip()
        {
            // Generates ship
            PictureBox ship = new PictureBox();
            ship.Size = new Size(48, 40);
            ship.BackColor = Color.Black;
            ship.Image = SpaceInvaders2017.Properties.Resources.ship;
            ship.Name = "ship";
            ship.Location = new Point(this.Width / 2, this.Height - 100);
            Controls.Add(ship);

            // Generates Health Bar
            PictureBox playerHealthBar = new PictureBox();
            playerHealthBar.Size = new Size(this.Width, 10);
            playerHealthBar.BackColor = Color.DarkGreen;
            playerHealthBar.Name = "playerHealthBar";
            playerHealthBar.Location = new Point(0, this.Height - 50);
            Controls.Add(playerHealthBar);

        }

        // This function is called every wave, and takes variables 
        // corresponding to the amount of rows and colums of aliens
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
                    pic.Size = new Size(48, 48);
                    pic.Location = new Point(55 * x + 50, 55 * y + 50);
                    pic.Image = SpaceInvaders2017.Properties.Resources.alien;
                    pic.BackColor = Color.Transparent;
		    //Color.FromArgb(r.Next(1, 256), r.Next(1, 256), r.Next(1, 256));
                    pic.Name = "alien";
                    // Adds new alien to array and Controls
                    Controls.Add(pic);
                    arrAliens.Add(pic);
                }
            }

            waveNumber++;

        }

        // Create boss alien.
        private void createBoss()
        {
            //Temporary filler until function is made.
            createAliens(aliensx, aliensy);

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
                pic.Size = new Size(3, 5);
                pic.BackColor = Color.YellowGreen;
                pic.Name = "alienbullet";

                if (a == 0)
                {
                    // Places it at the base of a randomly selected alien and adds it to Controls
                    pic.Location = new Point(arrAliens[x].Left + (arrAliens[x].Width / 2), arrAliens[x].Bottom);
                }
                else if (a == 1)
                {
                    pic.Location = new Point(arrAliens[0].Left + (arrAliens[0].Width / 2), arrAliens[0].Bottom);
                }
                else if (a == 2)
                {
                    int b = arrAliens.Count - 1;
                    pic.Location = new Point(arrAliens[b].Left + (arrAliens[b].Width / 2), arrAliens[b].Bottom);
                }

                arrBullets.Add(pic);
                Controls.Add(pic);
            }
        }

        // This function is used to generate a power up
        private void createPowerUp()
        {
            int type = r.Next(0, 2);
            int position = r.Next(20, this.Width - 20);

            powerUp.BackColor = Color.White;
            powerUp.Size = new Size(15, 15);
            powerUp.Name = "powerUp";
            powerUp.Location = new Point(position, this.Height - 100);

            if (!pwrUsed) pwrUsed = true; Controls.Add(powerUp);

            switch (type)
            {
                case 0:
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.bomb;
                    powerUp.Tag = "bomb";
                    break;
                case 1:
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.laser;
                    powerUp.Tag = "laser";
                    break;
                case 2:
                    powerUp.Image = SpaceInvaders2017.Properties.Resources.health;
                    powerUp.Tag = "health";
                    break;
            }

            powerUp.Visible = true;

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
            updateBulletCount();
        }
        private void tmrSlow_Tick(object sender, EventArgs e)
        {
            checkEndGame();
            shootAtShip(0);
            allowUpdate = true;
        }
        private void tmrFast_Tick(object sender, EventArgs e)
        {
            moveShip();
            moveBullet();
            checkHits();
            deleteDead();

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
                flasherHelper = 0;
            }
            else if (!displayText.Visible)
            {
                displayText.Visible = true;
                flasherHelper++;
            }
            else
            {
                flasherHelper++;
            }

        }
        private void debugUpdate_Tick(object sender, EventArgs e)
        {
            debugText.Text = "Debug:\nBullets Flying: " + arrBullets.Count.ToString() + "\nAliens Alive:" + arrAliens.Count.ToString() + "\nPlayer Health: " + playerHealth.ToString() + "\nTest";
        }
        private void tmrLogoAnimation_Tick(object sender, EventArgs e)
        {
            logoBox.Top -= 5;
            buttonExpert.Top -= 10;
            buttonBeginner.Top -= 10;
            if (logoBox.Top + 170 < 0)
            {
                logoBox.Visible = false;
                logoBox.Enabled = false;
            }
            if (buttonBeginner.Top + 40 < 0)
            {
                tmrLogoAnimation.Enabled = false;
                Controls.Remove(buttonExpert);
                Controls.Remove(buttonBeginner);


            }



        }
        private void tmrWindowAnimation_Tick(object sender, EventArgs e)
        {
            if (windowAnimVar)
            {
                this.Location = new Point(scWidth / 2 - this.Width / 2, this.Top);
            }
            if (this.Height < 800)
            {
                this.Top = scHeight / 2 - this.Height / 2;
                this.Height += 2;
            }
            if (this.Height > 600 && this.Width < 1000)
            {
                this.Width += 2;
            }
            if (this.Height > 400)
            {
                if (!started)
                {
                    setText("WELCOME TO SPACE INVADERS\n\nCHOOSE A DIFFICULTY LEVEL TO BEGIN:");
                }

                buttonExpert.Enabled = true;
                buttonBeginner.Enabled = true;
            }
            if (this.Height > 799 && this.Width > 999)
            {
                tmrWindowAnimation.Enabled = false;
                buttonExpert.Enabled = true;
                buttonBeginner.Enabled = true;
                this.Top = scHeight / 2 - this.Height / 2;
                this.Location = new Point(scWidth / 2 - this.Width / 2, this.Top);

            }
        }
        //private void tmrFireRate_Tick(object sender, EventArgs e)
        //{
        //    allowFire = true;

        //}
        private void tmrScreenFlash_Tick(object sender, EventArgs e)
        {
            screenFlash(1);
        }


        //------------------------------------------------------------------------
        //--------------------- Utility Fucntions --------------------------------
        //------------------------------------------------------------------------

        // This function is called in many places throughout
        // the game to display text on the screen..
        private void setText(string a)
        {
            // Sets Text property of the Label element to the incoming string
            displayText.Text = a;
            displayText.Visible = true;
            tmrTextFlash.Enabled = true;
            ship.Visible = false;
            playerHealthBar.Visible = false;
        }

        // This function hides the text, and stops the flashing text
        // timer from running.
        private void hideText()
        {
            tmrTextFlash.Enabled = false;
            displayText.Visible = false;
            ship.Visible = true;
            playerHealthBar.Visible = true;
        }

        private bool checkKeys(string b)
        {
            if (checkKeysHelper.KeyChar.ToString() == b)
            {
                return true;
            } else
            {
                return false;
            }
        }

        // This function flashes the screen.
        private void screenFlash(int mode)
        {

            if (mode == 0)
            {
                tmrScreenFlash.Enabled = true;
                screenFlashBox.BackColor = Color.FromArgb(30, 255, 0, 0);
                screenFlashBox.Visible = true;

            }
            else if (mode == 1)
            {
                screenFlashBox.Visible = false;
                //this.BackColor = Color.Black;
                tmrScreenFlash.Enabled = false;
            }

        }

        // This function is used to play various sounds and is called
        // in numerous functions.
        private void playSound(int a)
        {
            if (a == 1)
            {
                SoundPlayer sound1 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.hit);
                sound1.Play();
            }
            else if (a == 2)
            {
                SoundPlayer sound2 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.shoot);
                sound2.Play();
            }
            else if (a == 3)
            {
                SoundPlayer sound3 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.death);
                sound3.Play();

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

        // This function updates the size and colour of the health bar.
        private void updateplayerHealthBar()
        {
            playerHealthBar.Width = playerHealth * (this.Width / playerAllowedHealth);

            if (playerHealth > 3)
            {
                playerHealthBar.BackColor = Color.Green;
            }
            else if (playerHealth < 2)
            {
                playerHealthBar.BackColor = Color.Red;
            }
            else if (playerHealth < 3 && playerAllowedHealth > 2)
            {
                playerHealthBar.BackColor = Color.Orange;
            }
            else if (playerHealth < 4 && playerAllowedHealth > 2)
            {
                playerHealthBar.BackColor = Color.Yellow;
            }

            screenFlash(0);
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

            if (bulletcount > allowedBulletCount) allowFire = false;
            if (bulletcount <= allowedBulletCount) allowFire = true;
        }

        // This function is called when aliens hit a side, and handles
        // turning around the aliens properly. It also ensures gamers cannot
        // camp in corners by shooting every time it hits a side.
        private void hitSideCheck()
        {
            foreach (PictureBox alien in arrAliens)
            {

                if (alien.Left >= this.Width - 21)
                {
                     alienUpdate(false);
                }

                if (alien.Left <= 21)
                {
                    alienUpdate(true);
                }

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
                playerHealthBar.Location = new Point(0, this.Height - 50);
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

            foreach (PictureBox p in arrAliens)
            {
                if (p.Name == "alien")
                {
                    if (p.Top > this.Height || p.Bounds.IntersectsWith(ship.Bounds))
                    {

                        p.Tag = "dead";

                        gameOver();
                    }
                }
            }


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
                        // Hides alien by changing BackColor
                        p.Visible = false;
                        a.Visible = false;
                        // Labels Control for deletion
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
                    p.Tag = "dead";
                }
            }


        }
        private void deleteDead()
        {
            foreach (PictureBox m in Controls.OfType<PictureBox>())
            {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                if (m.Tag == "dead")
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
                {

                    
                    if (m.Name == "alien")
                    {
                        arrAliens.Remove(m);
                        Controls.Remove(m);
                    
                    }

                    Controls.Remove(m);

                }
            }
            if (arrBullets.Count > 0)
            {
                foreach (PictureBox m in Controls.OfType<PictureBox>())
                {
#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
                    if (m.Tag == "dead")
#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
                    {
                        arrBullets.Remove(m);
                        Controls.Remove(m);
                    }

                }
            }
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
                aliensx++;
                aliensy++;

                // Displays text
                setText("PRESS N TO START NEXT WAVE\n\nINVADERS: " + (aliensx * aliensy));
                tmrFast.Interval = 1;

                clearAliens();
                clearBullets();
            }

            if (playerHealth <= 0 && !finished)
            {
                gameOver();
            }
        }
        private void clearBullets()
        {
            foreach (PictureBox p in Controls.OfType<PictureBox>())
            {
                if (!(p.Name == "ship"))
                {
                    p.Tag = "dead";
                }
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
        }

        // These function is called to end a game.
        private void gameOver()
        {

            finished = true;
            playerdied = true;
            playerHealth = playerAllowedHealth;

            playSound(3);

            // Displays text
            setText("GAME OVER. YOU DIED ON A WAVE WITH " + (aliensx * aliensy) + " ALIENS.\n\nPRESS R TO RESTART.");

            aliensx = 3;
            aliensy = 10;

            clearAliens();

        }

    }
}
