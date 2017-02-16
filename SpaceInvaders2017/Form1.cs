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
        
        List<PictureBox> arrAliens = new List<PictureBox>();
        bool moveDir = true;
        int shipDir = 0;
        PictureBox ship = new PictureBox();
        Random r = new Random();

        public Form1()
        {
            InitializeComponent();
            this.KeyPress += Form1_KeyPress;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar.ToString() == " ")
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
        }

        private void moveShip()
        {
            ship.Left += shipDir;

            if(ship.Left > this.Width)
            {
                ship.Left = -ship.Width;
            }
            else if(ship.Left < -ship.Width)
            {
                ship.Left = this.Width;
            }
        }

        private void createBullet()
        {
            PictureBox shell = new PictureBox();
            shell.Size = new Size(3, 5);
            shell.Location = new Point(ship.Left + (ship.Width / 2), ship.Top);
            shell.Name = "shell";
            shell.BackColor = Color.DarkOrange;
            Controls.Add(shell);
        }

        private void moveBullet()
        {
            foreach (PictureBox p in Controls)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            createShip();
            ship = (PictureBox)Controls["ship"];
            //ship.Left += 500;
            createAliens();
        }

        private void createShip()
        {
            PictureBox ship = new PictureBox();
            ship.Size = new Size(20, 20);
            ship.BackColor = Color.Red;
            ship.Name = "ship";
            ship.Location = new Point(this.Width / 2, this.Height - 100);
            Controls.Add(ship);

        }

        private void createAliens()
        {
            for(int y = 0; y<=2; y++)
            {
                for(int x = 0; x <= 9; x++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(20,20);
                    pic.Location = new Point(50 + 30 * x, 50 + 30 * y);
                    pic.BackColor = Color.LawnGreen;
                    pic.Name = "alien";
                    Controls.Add(pic);
                    arrAliens.Add(pic);
                }
            }
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            moveAliens();
            moveBullet();
            moveShip();
            checkHits();
        }

        private void moveAliens()
        {
            foreach(PictureBox alien in arrAliens)
            {
                if (moveDir == true)
                {
                    alien.Left += 10;
                }
                else
                {
                    alien.Left -= 10;
                }
                
                if(alien.Left >= this.Width - 21)
                {
                    moveDir = false;
                }
                else if (alien.Left <= 21)
                {
                    moveDir = true;
                }
                
            }
        }

        private void checkHits()
        {
            foreach (PictureBox a in arrAliens)
            {
                foreach (PictureBox p in Controls.OfType<PictureBox>())
                {
               
                    if (p.Name == "shell" && p.Bounds.IntersectsWith(a.Bounds))
                    {
                        
                        p.BackColor = this.BackColor;
                        a.BackColor = this.BackColor;
                        p.Tag = "dead";
                        a.Tag = "dead";
                    }
                }
            }

            foreach(PictureBox p in Controls)
            {
                if(p.Tag ==  "dead")
                {
                    Controls.Remove(p);

                    if(p.Name == "alien")
                    {
                        arrAliens.Remove(p);
                    }
                }
            }

        }

        private void tmrSlow_Tick(object sender, EventArgs e)
        {
            shootAtShip();
        }

        private void shootAtShip()
        {
            int x = r.Next(0, arrAliens.Count);

            PictureBox pic = new PictureBox();
            pic.Size = new Size(3, 5);
            pic.BackColor = Color.YellowGreen;
            pic.Location = new Point(arrAliens[x].Left + (arrAliens[x].Width / 2), arrAliens[x].Bottom);
            pic.Name = "bullet";
            Controls.Add(pic);
        }

    }
}
