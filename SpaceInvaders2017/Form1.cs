using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpaceInvaders2017
{
    public partial class Form1 : Form
    {
        
        // Defines alien array, and other important global variables 
        // needed in various parts of the game
        List<PictureBox> arrAliens = new List<PictureBox>();
        bool moveDir = true;
        bool finished = false;
        int aliensx = 3;
        int aliensy = 10;
        int shipDir = 0;
        PictureBox ship = new PictureBox();
        Random r = new Random();

        public Form1()
        {
            InitializeComponent();
            this.KeyPress += Form1_KeyPress;
        }

        // Is called when any key pressed, and checks it against a number
        // of different keys which proceed to call other functions
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.ToString() == "w")
            {
                createBullet();
            }

            if(e.KeyChar.ToString() == "a")
            {
                shipDir = -20;
            }
            else if(e.KeyChar.ToString() == "d")
            {
                shipDir = 20;
            }
            else if(e.KeyChar.ToString() == "s")
            {
                shipDir = 0;
            }

            if (e.KeyChar.ToString() == "n")
            {
                if (finished)
                {
                    newWave();
                }
            }
            if (e.KeyChar.ToString() == "")
            {

            }


        }

        // Function to compare Keys to what was pressed and what is
        // wanted, NOT WORKING (unable to return true or false)

        //private void compareKeys(KeyPressEventArgs a, string b)
        //{
        //    if (a.KeyChar.ToString() == b)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        // This function is called on a repeating timer and updates the
        // ship's position based on the keys pressed by the user

        private void moveShip()
        {
            // Moves ahead ship in direction specified by movement keys
            ship.Left += shipDir;

            // Teleports ship to other side of screen when it reaches an edge
            if(ship.Left > this.Width)
            {
                ship.Left = -ship.Width;
            }
            else if(ship.Left < -ship.Width)
            {
                ship.Left = this.Width;
            }

            // Keeps ship spaced 100 pixels from the bottom of the game when the window is resized
            ship.Location = new Point(ship.Left, (this.Height - 100));

        }

        // This function is called when the user presses the fire key, 
        // and spawn a bullet headed toward the aliens.
        private void createBullet()
        {
            // Generates bullet
            PictureBox shell = new PictureBox();
            shell.Size = new Size(3, 5);
            shell.Name = "shell";
            shell.BackColor = Color.DarkOrange;
            // Places bullet within ship bounds
            shell.Location = new Point(ship.Left + (ship.Width / 2), ship.Top);
            Controls.Add(shell);
        }

        // This function is called on a repeating timer and updates the
        // position of bullets fired by both the alien and the user
        private void moveBullet()
        {
            // Cycles through PictureBoxes checking if they are above the game height, and removes
            // them or if they are not it will move them up.
            foreach (PictureBox p in Controls.OfType<PictureBox>())
            {
                if (p.Name == "shell") { 
                    if (p.Top < 0)
                    {
                        Controls.Remove(p);
                    }
                    else
                    {
                        p.Top -= 20;
                    }
                }
                if (p.Name == "bullet")
                {
                    if (p.Top < 0)
                    {
                        Controls.Remove(p);
                    }
                    else
                    {
                        p.Top += 20;
                    }
                }
            }
        }

        // This function is called on load, and is used to generate a ship
        // and some aliens for the first wave.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Generates ship and makes it a global variable to be called in other functions
            createShip();
            ship = (PictureBox)Controls["ship"];
            // Generates first wave
            createAliens(3,10);
        }

        // This function is called at the very start of a game and 
        // generates the ship for the user to interact with.
        private void createShip()
        {
            // Generates ship
            PictureBox ship = new PictureBox();
            ship.Size = new Size(20, 20);
            ship.BackColor = Color.Red;
            ship.ImageLocation = @"C:\Users\Ben Carroll\BitBucket\Ben's Space Invaders\SpaceInvaders2017\img\alien.ico";
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
                    pic.Size = new Size(20,20);
                    pic.Location = new Point( 30 * x+50,30 * y +50);
                    pic.BackColor = Color.DarkGreen;
                    pic.Name = "alien";
                    // Adds new alien to array and Controls
                    Controls.Add(pic);
                    arrAliens.Add(pic);
                }
            }
        }

        // The following three functions are called when a timer
        // reaches its interval, and call certain functions to
        // update the game
        private void tmrGame_Tick(object sender, EventArgs e)
        {
            moveShip();
            checkHits();
        }
        private void tmrMove_Tick(object sender, EventArgs e)
        {
            moveAliens();
            moveBullet();
            checkHits();
        }
        private void tmrSlow_Tick(object sender, EventArgs e)
        {
            checkEndGame();
            shootAtShip();
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

            }

        // This function works with moveAliens() to move aliens down and
        // modify a timer when a wall is hit.
        private void alienUpdate()
        {
            // Moves all aliens down
            foreach (PictureBox a in arrAliens)
            {
                a.Top += 10;
            }

            // Modifies timer interval to a certain point.
            if (tmrMove.Interval > 40)
            {
                tmrMove.Interval -= 10;
            }

        }

        // This function is called on a repeating timer and checks whether
        // any aliens have been hit by a bullet.
        private void checkHits()
        {
            // Cycles thorugh each alien
            foreach (PictureBox a in arrAliens)
            {
                // Finds shells/bullets
                foreach (PictureBox p in Controls.OfType<PictureBox>())
                {
                    // Checks intersection with alien
                    if (p.Name == "shell" && p.Bounds.IntersectsWith(a.Bounds))
                    {
                        // Hides alien by changing BackColor
                        p.BackColor = this.BackColor;
                        a.BackColor = this.BackColor;
                        // Labels Control for deletion
                        p.Tag = "dead";
                        a.Tag = "dead";
                    }
                }
            }

            // Finds "dead" Controls and removes them from Controls and the array
            foreach(PictureBox p in Controls.OfType<PictureBox>())
            {
                if (p.Tag.ToString() ==  "dead")
                {
                    Controls.Remove(p);

                    if(p.Name == "alien")
                    {
                        arrAliens.Remove(p);
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
                setText("PRESS N TO START NEXT WAVE\nINVADERS: " + (aliensx * aliensy));
            }
        }

        // This function works with checkEndGame() to modify a Label
        // element's contents.
        private void setText(string a)
        {
            // Sets Text property of the Label element to the incoming string
            displayText.Text = a;
            displayText.Visible = true;
        }

        // This function calls a new wave of aliens after the user has 
        // completed a level.
        private void newWave()
        {
            // Disables text
            displayText.Visible = false;
            finished = false;

            // Generates aliens based on current level.
            createAliens(aliensx,aliensy);
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
                Controls.Add(pic);
            }
        }


    }
}
