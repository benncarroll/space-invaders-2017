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
        int aliensx = 3;
        int aliensy = 10;
        int shipDir = 0;
        int scHeight = 0;
        int scWidth = 0;
        int flasherHelper = 0;
        PictureBox ship = new PictureBox();
        Random r = new Random();

        //------------------------------------------------------------------------
        //----------------------- Event Listeners --------------------------------
        //------------------------------------------------------------------------


        public Form1()
        {
            InitializeComponent();
            this.Click += Form1_Click;
            this.KeyPress += Form1_KeyPress;
        }

        // Is called when any alphanumeric key pressed, and checks it against a number
        // of different keys which proceed to call other functions
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.ToString() == " " && !finished && started)
            {
                createBullet();
                allowFire = false;
            }

            if (e.KeyChar.ToString() == "n" && finished && !playerdied && started)
            {
                newWave();    
            }
            if (e.KeyChar.ToString() == "r" && finished && playerdied && started)
            {
                newWave();
                playerdied = false;
            }
            if (e.KeyChar.ToString() == "o")
            {
                toggleDebug();
            }

        }

        // Is called when system keys are pressed, and is used to
        // process arrow keys to move the ship.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Left)
            {
                shipDir = -10;
            } else if (keyData == Keys.Right)
            {
                shipDir = 10;
            } else if (keyData == Keys.Left)
            {
                shipDir = 0;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // This function is called on load, and is used to generate a ship
        // and some aliens for the first wave.
        private void Form1_Load(object sender, EventArgs e)
        {
            scHeight = Screen.FromControl(this).Bounds.Height;
            scWidth = Screen.FromControl(this).Bounds.Width;
        }

        // This function is called when the user clicks anywhere on the window
        private void Form1_Click(object sender, EventArgs e)
        {
            if (started && !finished)
            {
                createBullet();
            }
        }

        private void beginnerButton_Click(object sender, EventArgs e)
        {
            tmrFireRate.Interval = 5;
            startGame();
        }
        private void expertButton_Click(object sender, EventArgs e)
        {
            tmrFireRate.Interval = 500;
            startGame();
        }


        //------------------------------------------------------------------------
        //------------------------ Game Initiation -------------------------------
        //------------------------------------------------------------------------

        // This function is called at the very start of a game, and
        // starts the timers and spawns the aliens and ship.
        private void startGame()
        {
            started = true;
            // Generates ship and makes it a global variable to be called in other functions
            createShip();
            ship = (PictureBox)Controls["ship"];
            createAliens(3, 10);
            hideText();
            tmrLogoAnimation.Enabled = true;
            tmrFast.Enabled = true;
            tmrSlow.Enabled = true;
            tmrPowerUp.Enabled = true;
            tmrMove.Enabled = true;

        }

        // This function calls a new wave of aliens after the user has 
        // completed a level.
        private void newWave()
        {
            // Disables text
            hideText();
            finished = false;
            shipDir = 0;
            ship.Location = new Point(this.Width / 2, this.Height - 100);

            // Generates aliens based on current level.
            createAliens(aliensx,aliensy);
        }


        //------------------------------------------------------------------------
        //---------------------------- Generation --------------------------------
        //------------------------------------------------------------------------

        // This function is used to create parrallax stars
        private void createStars(int starwidth, int starc)
        {
            // Sets vars
            Color starcolour = new Color();
            if (starc == 1)
            {
                starcolour = Color.White;
            } else if (starc == 2)
            {
                starcolour = Color.LightGray;
            } else if (starc == 3)
            {
                starcolour = Color.DarkGray;
            }

            // Loops based on amount of stars specified
            for (int y = 0; y < 21; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    // Creates new star
                    Random rDistance = new Random();
                    int starDistance = rDistance.Next(0, 50);
                    Rectangle pic = new Rectangle();
                    pic.Size = new Size(starwidth,starwidth);
                    pic.Location = new Point((this.Width/20) * x + starDistance, (this.Height/10) * y + starDistance);

                    // Adds new star to array
                    arrStars.Add(pic);
                }
            }
        

    }

        // This function is called when the user presses the fire key, 
        // and spawn a bullet headed toward the aliens.
        private void createBullet()
        {
            if (allowFire)
            {
                // Generates bullet
                PictureBox shell = new PictureBox();
                shell.Size = new Size(2, 4);
                shell.Name = "shell";
                shell.BackColor = Color.Red;
                // Places bullet within ship bounds
                shell.Location = new Point(ship.Left + 9, ship.Top);
                playSound(2);
                arrBullets.Add(shell);
                Controls.Add(shell);
            }
        }

        // This function is called at the very start of a game and 
        // generates the ship for the user to interact with.
        private void createShip()
        {
            // Generates ship
            PictureBox ship = new PictureBox();
            ship.Size = new Size(20, 19);
            ship.BackColor = Color.Black;
            ship.Image = SpaceInvaders2017.Properties.Resources.ship;
            ship.Name = "ship";
            ship.Location = new Point(this.Width / 2, this.Height - 100);
            Controls.Add(ship);
        }

        // This function is called every wave, and takes variables 
        // corresponding to the amount of rows and colums of aliens
        // requested.
        private void createAliens(int f, int g)
        {
            // Loops based on amount of aliens specified
            for(int y = 0; y < f; y++)
            {
                for(int x = 0; x < g; x++)
                {
                    // Creates new alien
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(15,15);
                    pic.Location = new Point( 30 * x+50,30 * y +50);
                    pic.Image = SpaceInvaders2017.Properties.Resources.alien;
                    pic.BackColor = Color.DarkGreen;
                    pic.Name = "alien";
                    // Adds new alien to array and Controls
                    Controls.Add(pic);
                    arrAliens.Add(pic);
                }
            }
        }

        // This function is called on a repeating timer and allows the
        // aliens to shoot back at the ship.
        private void shootAtShip()
        {
            if (finished == false)
            {
                // Picks a random integer from a pre-set Random variable
                int x = r.Next(0, arrAliens.Count);

                // Creates a new bullet
                PictureBox pic = new PictureBox();
                pic.Size = new Size(3, 5);
                pic.BackColor = Color.YellowGreen;
                pic.Name = "bullet";

                // Places it at the base of a randomly selected alien and adds it to Controls
                pic.Location = new Point(arrAliens[x].Left + (arrAliens[x].Width / 2), arrAliens[x].Bottom);
                arrBullets.Add(pic);
                Controls.Add(pic);
            }
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
            checkEndGame();
            shootAtShip();
        }
        private void tmrFast_Tick(object sender, EventArgs e)
        {
            moveShip();
            moveBullet();
        }
        private void tmrPowerUp_Tick(object sender, EventArgs e)
        {

        }
        private void tmrTextFlash_Tick(object sender, EventArgs e)
        {
            if (displayText.Visible && flasherHelper > 1)
            {
                displayText.Visible = false;
                flasherHelper = 0;
            } else if (!displayText.Visible)
            {
                displayText.Visible = true;
                flasherHelper++;
            } else
            {
                flasherHelper++;
            }
        }
        private void debugUpdate_Tick(object sender, EventArgs e)
        {
            debugText.Text = "Debug:\nBullets Flying: " + arrBullets.Count.ToString() + "\nAliens Alive:" + arrAliens.Count.ToString();
        }
        private void tmrLogoAnimation_Tick(object sender, EventArgs e)
        {
            logoBox.Top -= 5;
            expertButton.Top -= 10;
            beginnerButton.Top -= 10;
            if (logoBox.Top + 170 < 0)
            {
                logoBox.Visible = false;
                logoBox.Enabled = false;
            }
            if (expertButton.Top + 40 < 0)
            {
                tmrLogoAnimation.Enabled = false;
                expertButton.Visible = false;
                expertButton.Enabled = false;
                Controls.Remove(expertButton);
            }
            if (beginnerButton.Top + 40 < 0)
            {
                expertButton.Visible = false;
                expertButton.Enabled = false;
                Controls.Remove(beginnerButton);

            }



        }
        private void tmrWindowAnimation_Tick(object sender, EventArgs e)
        {
            if (windowAnimVar)
            {
                this.Location = new Point(scWidth/2 - this.Width/2, this.Top);
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
            if (this.Height > 799 && this.Width > 999)
            {
                if (!started)
                {
                    setText("WELCOME TO SPACE INVADERS\n\nCHOOSE A DIFFICULTY LEVEL TO BEGIN:");
                }
                tmrWindowAnimation.Enabled = false;
            }
        }
        private void tmrFireRate_Tick(object sender, EventArgs e)
        {
            allowFire = true;
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
        }

        private void hideText()
        {
            tmrTextFlash.Enabled = false;
            displayText.Visible = false;
            ship.Visible = true;
        }

        // This function is used to play various sounds and is called
        // in numerous functions.
        private void playSound(int a)
        {
            if (a==1)
            {
                SoundPlayer sound1 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.hit);
                sound1.Play();
            } else if (a==2)
            {
                SoundPlayer sound2 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.shoot);
                sound2.Play();
            }
            else if (a==3)
            {
                SoundPlayer sound3 = new SoundPlayer(SpaceInvaders2017.Properties.Resources.death);
                sound3.Play();

            }
        }

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
                    alien.Left += 10;
                }
                else
                {
                    alien.Left -= 10;
                }
            }

            // Checks each alien against walls of game, modifies move direction and calls alienUpdate()
            foreach (PictureBox alien in arrAliens)
            {

                if (alien.Left >= this.Width - 21)
                {
                    moveDir = false;
                    alienUpdate();
                }

                else if (alien.Left <= 21)
                {
                    moveDir = true;
                    alienUpdate();
                }
            }

            checkEndGame();

            foreach (PictureBox p in arrAliens)
            {
                if (p.Name == "alien")
                {
                    if (p.Top > this.Height)
                    {

                        p.Tag = "dead";

                        gameOver();
                    }
                }
            }


        }

        // This function works with moveAliens() to move aliens down and
        // modify a timer when a wall is hit.
        private void alienUpdate()
        {
            // Moves all aliens down
            foreach (PictureBox a in arrAliens)
            {
                a.Visible = false;
                a.Top += 10;
                a.Visible = true;

            }

            // Modifies timer interval to a certain point.
            if (tmrMove.Interval > 40)
            {
                tmrMove.Interval -= 10;
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
                if (p.Name == "shell") { 
                    if (p.Top < 0)
                    {
                        Controls.Remove(p);
                    }
                    else
                    {
                        p.Top -= 10;
                    }
                }
                if (p.Name == "bullet")
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
                // Finds shells/bullets
                foreach (PictureBox p in arrBullets)
                {
                    // Checks intersection with alien
                    if (p.Name == "shell" && p.Bounds.IntersectsWith(a.Bounds))
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
                if (p.Name == "bullet" && p.Bounds.IntersectsWith(ship.Bounds)) {
                    gameOver();
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
                    if (m.Name == "shell" || m.Name == "bullet")
                    {
                        arrBullets.Remove(m);
                        Controls.Remove(m);
                    }
                    else if (m.Name == "alien")
                    {
                        arrAliens.Remove(m);
                        Controls.Remove(m);
                    }
                }
            }
        }

        // This function checks to see if there are any aliens left, and
        // invokes the next wave or death screen.
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

                clearAliens();
                clearBullets();
            }
        }
        private void clearBullets()
        {
            foreach (PictureBox p in Controls.OfType<PictureBox>())
            {
                if (!(p.Name == "ship"))
                {
                    p.Tag = "dead";                }
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
        

        private void gameOver()
        {

            finished = true;
            playerdied = true;

            playSound(3);   

                // Displays text
            setText("GAME OVER. YOU DIED ON A WAVE WITH " + (aliensx * aliensy) +  " ALIENS.\n\nPRESS R TO RESTART.");

            aliensx = 3;
            aliensy = 10;

            clearAliens();

        }

    }
}
