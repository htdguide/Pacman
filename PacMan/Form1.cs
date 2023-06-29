using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacMan;
using PacMan.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PacMan
{
    public partial class Pacman : Form
    {
        private Creatures player, ghost;
        private Engine engine;
        private bool c = true; //debugging switch
        private bool gates = false; //gates open
        private int doorsCounter = 0; //door open for strawberry
        private int shotAbility = -1; //Shotgun
        private int shells = 0;
        private int gatesSound = 0; 
        private int appearanceCount = 0; //appearance for the ghost
        private int ghostJump = 30; //timing for ghosts fast moving
        private int ghostAlive = 1; //Dead or alive ghost
        private int shotgunMenu = 0;
        private int begin = 0;
        private int deathDirection;
        private int animCounter = 0;
        private int gameend = 0;
        private int teleportMode = 0;
        
        private bot bot;
        public Pacman()
        {
            InitializeComponent();
            player = new Creatures("Pacman", pictureBox204, collider1, collider2, collider3, collider4, vision1, aimBox, -1, 0, 2);
            ghost = new Creatures("ghost", ghostAppearance, ghostColliderUp, ghostColliderDown, ghostColliderLeft, ghostColliderRight, ghostVision, ghostAim, -1, 0, 1);
            bot = new bot(ghost, panel1, player);
            engine = new Engine(0, 3, player, ghost, label7,label9);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ghostAlive == 1) bot.mind(); //ghost controlling
            ghostUnhide();
            if (!wallcheck(player)) player.movement();
            if (!wallcheck(ghost) && ghostAlive == 1) ghost.movement();
            if (engine.scoreTotal > 150 || gates == true) gatesopen();
            if (gatesSound == 1)
            {
                SoundPlayer gates = new SoundPlayer(Resources.gates);
                gates.Play();
                gatesSound = 2;
            }
            kill();
        }

        private void Pacman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                Application.Restart();
                Environment.Exit(0);
            }
            if (e.KeyCode == Keys.Left && timer1.Enabled)
            {
                player.direction = 1;
                player.aimMovement(); //changing the direction of the aimbox
                player.appearance.Image = Resources.pacmanLeft;
            }
            if (e.KeyCode == Keys.Right && timer1.Enabled)
            {
                player.direction = -1;
                player.aimMovement(); //changing the direction of the aimbox
                player.appearance.Image = Resources.pacmanRight;
            }
            if (e.KeyCode == Keys.Up && timer1.Enabled)
            {
                player.direction = 2;
                player.aimMovement(); //changing the direction of the aimbox
                player.appearance.Image = Resources.pacmanUp;
            }
            if (e.KeyCode == Keys.Down && timer1.Enabled)
            {
                player.direction = -2;
                player.aimMovement(); //changing the direction of the aimbox
                player.appearance.Image = Resources.pacmanDown;
            }
            if (e.KeyCode == Keys.Space && timer1.Enabled && gameend == 0)
            {
                if (shotAbility == 1) shot();
                else if (shotAbility == 0 && shells > 0) reload();
            }
            if (e.KeyCode == Keys.Space && !timer1.Enabled && shotgunMenu == 1) //shotgun help menu handler
            {
                shotgunMenu = 0;
                SoundPlayer pick = new SoundPlayer(Resources.gunPick);
                pick.Play();
                panel6.Enabled = false;
                panel6.Visible = false;
                timer1.Enabled = true;
            }
            if (e.KeyCode == Keys.Q)
            {
                this.Close();
            }
            if (e.KeyCode == Keys.Enter && engine.lives != 0 && begin != 0 && gameend == 0) //Press enter to continue playing
            {
                if (!timer1.Enabled)
                {
                    panel4.Visible = false;
                    panel4.Enabled = false;
                    player.appearance.Visible = false;
                    ghost.appearance.Visible = false;                   
                    for (int x = player.colliderLeft.Left; x > pictureBox3.Right; x--) //Moviing the pacman to the beginning
                    {
                        player.movementLeft();
                    }
                    for (int x = player.colliderDown.Bottom; x < pictureBox76.Top; x++) //Moving the pacman to the beginning
                    {
                        player.movementDown();
                    }
                    for (int x = ghost.colliderRight.Right; x < pictureBox31.Right; x++) //Moviing the pacman to the beginning
                    {
                        ghost.movementRight();
                    }
                    for (int x = ghost.colliderUp.Top; x > pictureBox31.Bottom; x--) //Moving the pacman to the beginning
                    {
                        ghost.movementUp();
                    }
                    switch (player.direction)
                    {
                        case 1:
                            player.appearance.Image = Resources.pacmanLeft;
                            break;
                        case -1:
                            player.appearance.Image = Resources.pacmanRight;
                            break;
                        case 2:
                            player.appearance.Image = Resources.pacmanUp;
                            break;
                        case -2:
                            player.appearance.Image = Resources.pacmanDown;
                            break;

                    }
                    teleportMode = 0;
                    player.appearance.Visible = true;
                    timer1.Enabled = true;
                }
            }
            if (e.KeyCode == Keys.Enter && timer1.Enabled == false && begin == 0 && gameend == 0)
            {
                label11.Text = "Pause";
                timer1.Enabled = true;
                panel7.Left = 714;
                panel7.Top = 728;
                begin = 1;
            }
            if (e.KeyCode == Keys.X && timer1.Enabled && gameend == 0) //Debug mode
            {
                ghost.appearance.Visible = !c;
                pictureBox204.Visible = !c;
                ghost.colliderLeft.Visible = c;
                ghost.colliderRight.Visible = c;
                ghost.colliderUp.Visible = c;
                ghost.colliderDown.Visible = c;
                ghost.aimbox.Visible = c;
                ghost.vision.Visible = c;
                collider1.Visible = c;
                collider2.Visible = c;
                collider3.Visible = c;
                collider4.Visible = c;
                vision1.Visible = c;
                pictureBox202.Visible = c;
                pictureBox205.Visible = c;
                pictureBox300.Visible = c;
                gates = true;
                engine.keys = 3;
                aimBox.Visible = c;
                c = !c;
            }
        }

        private void Pacman_Load(object sender, EventArgs e)
        {
            panel1.Width = 500;
            Pacman.ActiveForm.Width = 540;
            Pacman.ActiveForm.Height = 700;
        }

        private bool wallcheck(Creatures entity) //Checking for the wall collision
        {
            bool b = false;
            foreach (Control x in panel1.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "wall2" || x.Tag == "wall3" || x.Tag == "border" || x.Tag =="door"))  //Wall detection
                {
                    if (entity.name == "Pacman") aligning(entity, x); //Alligning of the creature
                    if (entity.direction == 1)
                    {
                        if (entity.colliderLeft.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (entity.direction == -1)
                    {
                        if (entity.colliderRight.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (entity.direction == 2)
                    {
                        if (entity.colliderUp.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (entity.direction == -2)
                    {
                        if (entity.colliderDown.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                }
                if (entity.name == "Pacman")
                {
                    if (x is PictureBox && (x.Tag == "kibble" || x.Tag == "kibble2"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            engine.scoring();
                            panel1.Controls.Remove(x);
                        }
                    }
                    if (x is PictureBox && (x.Tag == "shell"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            shells = shells + 1;
                            SoundPlayer pick = new SoundPlayer(Resources.gunPick);
                            pick.Play();
                            label3.Text = ("x" + shells);
                            panel1.Controls.Remove(x);
                        }
                    }
                    if (x is PictureBox && (x.Tag == "shotgun"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            SoundPlayer player = new SoundPlayer(Resources.duke);
                            player.Play();
                            panel3.Visible = true;
                            pictureBox378.Visible = true;
                            pictureBox393.Visible = true;
                            aimBox.Visible = true;
                            label3.Visible = true;
                            shotAbility = 1;
                            panel1.Controls.Remove(x);
                            timer1.Enabled = false;
                            panel6.Left = (panel1.Left + panel1.Width / 2) - (panel6.Width / 2);
                            panel6.Top = (panel1.Top + panel1.Height / 2) - (panel6.Height / 2);
                            shotgunMenu = 1;
                        }
                    }
                    if (x is PictureBox && (x.Tag == "straw"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            strawIcon.Visible = true;
                            foreach (Control z in panel1.Controls)
                            {
                                if (z is PictureBox && z.Tag == "kibble") z.Visible = true; //Ability to see the kibbles
                            }
                            SoundPlayer collect = new SoundPlayer(Resources.keys);
                            collect.Play();
                            panel1.Controls.Remove(x);
                        }
                    }
                    if (x is PictureBox && (x.Tag == "cherry"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            strawIcon.Visible = true;
                            foreach (Control z in panel1.Controls)
                            {
                                if (z is PictureBox && z.Tag == "kibble2") z.Visible = true; //Ability to see the kibbles
                            }
                            SoundPlayer collect = new SoundPlayer(Resources.keys);
                            cherryIcon.Visible = true;
                            collect.Play();
                            panel1.Controls.Remove(x);
                        }
                    }

                    if (x is PictureBox && (x.Tag == "key"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            panel3.Visible = true;
                            engine.keys = engine.keys + 1;
                            SoundPlayer collect = new SoundPlayer(Resources.keys);
                            collect.Play();
                            panel1.Controls.Remove(x);
                            switch (engine.keys)
                            {
                                case 1:
                                    pictureBox202.Visible = true;
                                    break;
                                case 2:
                                    pictureBox205.Visible = true;
                                    pictureBox202.Visible = true;
                                    break;
                                case 3:
                                    pictureBox202.Visible = true;
                                    pictureBox205.Visible = true;
                                    pictureBox300.Visible = true;
                                    break;
                            }
                        }
                    }
                    if (x is PictureBox && (x.Tag == "door" && engine.keys > 2))  //Door open detection
                    {
                        if (entity.colliderRight.Bounds.IntersectsWith(x.Bounds))
                        {
                            SoundPlayer collect = new SoundPlayer(Resources.doorSound);
                            collect.Play();
                            panel1.Controls.Remove(x);
                            engine.keys = 0;
                        }
                    }
                    if (x is PictureBox && (x.Tag == "kibble" || x.Tag == "kibble2" || x.Tag == "wall" || x.Tag == "wall2" || x.Tag == "wall3"|| x.Tag == "key" || x.Tag == "door" || x.Tag == "straw" || x.Tag == "shotgun" || x.Tag == "shell" || x.Tag == "cherry"))  //Vision detection
                    {
                        if (entity.vision.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = true;
                        }
                    }
                    if ((entity.appearance.Right + 25) > panel1.Right) //Level extender
                    {
                        panel1.Width = panel1.Width + player.speed;
                        Pacman.ActiveForm.MaximumSize = new System.Drawing.Size(Pacman.ActiveForm.Width + player.speed, 570);
                        Pacman.ActiveForm.Width = Pacman.ActiveForm.Width + player.speed;
                        panel2.Width = panel2.Width + player.speed;
                        Pause.Left = Pause.Left + player.speed;
                        label11.Left = label11.Left + player.speed;
                        if (Pause.Left > label1.Right && Pause.Bottom != label2.Bottom)
                        {
                            Pause.Top = Pause.Top + 1;
                            label11.Top = label11.Top + 1;
                        }
                        if (panel2.Right - 35 > panel3.Right) panel3.Width = panel3.Width + player.speed;
                    }
                    if (engine.scoreTotal > 100 && doorsCounter == 0)
                    {
                        SoundPlayer collect = new SoundPlayer(Resources.doorSound);
                        collect.Play();
                        panel1.Controls.Remove(doorS);
                        doorsCounter = 1;
                    }
                }
            }
            return b;
        }
        private void aligning(Creatures entity, Control x) //Alligning of the creature in the case when creature changes direction not in the right time, but still fits.
        {
            if (entity.direction == 1 || entity.direction == -1)
            {
                if (entity.colliderUp.Bounds.IntersectsWith(x.Bounds))
                {
                    entity.movementDown();
                }
                if (entity.colliderDown.Bounds.IntersectsWith(x.Bounds))
                {
                    entity.movementUp();
                }
            }
            if (entity.direction == 2 || entity.direction == -2)
            {
                if (entity.colliderLeft.Bounds.IntersectsWith(x.Bounds))
                {
                    entity.movementRight();
                }
                if (entity.colliderRight.Bounds.IntersectsWith(x.Bounds))
                {
                    entity.movementLeft();
                }
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {
            if (gameend == 0 && teleportMode == 0)
            {
                panel7.Left = 714;
                panel7.Top = 728;
                if (timer1.Enabled == true)
                {
                    timer1.Enabled = false;
                    label11.Text = "Start!";
                }
                else if (engine.lives != 0 && shotgunMenu != 1)
                {
                    timer1.Enabled = true;
                    label11.Text = "Pause";
                }
            }
        }

        private void gatesopen() //Gates for level extension
        {
            if (gatesSound != 2) gatesSound = 1;
            foreach (Control x in panel1.Controls)
            {
                if (x is PictureBox && x.Tag == "wall2")
                {
                    if (x.Top != 0) x.Top = x.Top + 1;
                }
            }
        }
        private void shot()
        {
            SoundPlayer shot = new SoundPlayer(Resources.shot);
            shot.Play();
            shotAbility = 0;
            foreach (PictureBox v in panel1.Controls)
            {
                if (v.Tag == "wall" || v.Tag == "wall3")
                {
                    if (aimBox.Bounds.IntersectsWith(v.Bounds))
                    {
                        v.Image = Resources.deadwall;
                        v.Tag = "deadwall";
                        v.Visible = true;
                    }
                }
                if (v.Tag == "ghost")
                {
                    if (aimBox.Bounds.IntersectsWith(v.Bounds))
                    {
                        ghostAlive = 0;
                        deathDirection = player.direction;
                        timer3.Enabled = true;
                        ghost.appearance.Image = Resources.GhostDead;
                    }
                }
            }
        }
        private void reload()
        {
            SoundPlayer reload = new SoundPlayer(Resources.reload);
            reload.Play();
            shotAbility = 1;
            shells = shells - 1;
            label3.Text = ("x" + shells);
        }
        private void ghostUnhide()
        {
            SoundPlayer pursuit = new SoundPlayer(Resources.pursuit);
            SoundPlayer dissapear = new SoundPlayer(Resources.dissapearing);
            if (player.vision.Bounds.IntersectsWith(ghost.appearance.Bounds) || player.aimbox.Bounds.IntersectsWith(ghost.appearance.Bounds))
            {
                ghost.appearance.Visible = true;
                if (ghostJump == 30) ghost.speed = 2;
                if (appearanceCount == 0)
                {
                    pursuit.Play();
                }
                appearanceCount = 100;
            }
            else if (ghost.appearance.Visible && appearanceCount == 0)
            {
                ghost.appearance.Visible = false;
                ghost.speed = 1;
                pursuit.Stop();
                dissapear.Play();
                ghostJump = 30;
            }
            if (appearanceCount > 0) appearanceCount = appearanceCount - 1;
            if (ghostJump > 0 && ghost.appearance.Visible) ghostJump = ghostJump - 1;
            if (ghostJump < 1) ghost.speed = 1;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (panel1.Width > 500 && engine.scoreTotal < 1)
            {
                panel1.Width = 380;
                Pacman.ActiveForm.MaximumSize = new System.Drawing.Size(410, 570);
                Pacman.ActiveForm.Width = 410;
                Pacman.ActiveForm.Height = 570;
                panel2.Width = 360;
                panel3.Width = 150;
                panel7.Left = (panel1.Left + panel1.Width / 2) - (panel7.Width / 2);
                panel7.Top = (panel1.Top + panel1.Height / 2) - (panel7.Height / 2);
                timer2.Enabled = false;
            }
        }

        private void kill()
        {
            if (ghost.appearance.Bounds.IntersectsWith(player.appearance.Bounds) && engine.lives > 0 && ghostAlive == 1 && teleportMode == 0)
            {
                timer1.Enabled = false;
                engine.lives = engine.lives - 1;
                engine.scoring();
                label12.Text = engine.lives.ToString();
                SoundPlayer death = new SoundPlayer(Resources.kill);
                death.Play();
                player.appearance.Image = Resources.dead;
                ghost.appearance.Visible = false;
                panel4.Enabled = true;
                panel4.Visible = true;
                panel4.Left = (panel1.Left + panel1.Width / 2) - (panel4.Width /2);
                panel4.Top = (panel1.Top + panel1.Height / 2) - (panel4.Height/2);
                teleportMode = 1;
            }
            if (ghost.appearance.Bounds.IntersectsWith(player.appearance.Bounds) && engine.lives == 0 && ghostAlive == 1)
            {
                timer1.Enabled = false;
                engine.scoring();
                SoundPlayer death = new SoundPlayer(Resources.kill);
                death.Play();
                player.appearance.Image = Resources.dead;
                ghost.appearance.Visible = false;
                panel5.Left = (panel1.Left + panel1.Width / 2) - (panel5.Width / 2);
                panel5.Top = (panel1.Top + panel1.Height / 2) - (panel5.Height / 2);
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/htdguide");
            this.Close();
        }

        private void label23_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.linkedin.com/in/htdguide");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            ghost.direction = deathDirection;
            animCounter = animCounter + 1;
            switch (deathDirection)
            {
                case 1:
                    if (animCounter < 10 && !wallcheck(ghost))
                    {
                        ghost.movementLeft();
                        ghost.movementLeft();
                        ghost.movementLeft();
                    }
                    if (animCounter > 10 && animCounter < 20 && !wallcheck(ghost))
                    {
                        ghost.movementLeft();
                        ghost.movementLeft();
                    }
                    if (animCounter > 20 && animCounter < 30 && !wallcheck(ghost))
                    {
                        ghost.movementLeft();
                    }
                break;
                case -1:
                    if (animCounter < 10)
                    {
                        ghost.movementRight();
                        ghost.movementRight();
                        ghost.movementRight();
                    }
                    if (animCounter > 10 && animCounter < 20 && !wallcheck(ghost))
                    {
                        ghost.movementRight();
                        ghost.movementRight();
                    }
                    if (animCounter > 20 && animCounter < 30 && !wallcheck(ghost))
                    {
                        ghost.movementRight();
                    }
                break;
                case 2:
                    if (animCounter < 10 && !wallcheck(ghost))
                    {
                        ghost.movementUp();
                        ghost.movementUp();
                        ghost.movementUp();
                    }
                    if (animCounter > 10 && animCounter < 20 && !wallcheck(ghost))
                    {
                        ghost.movementUp();
                        ghost.movementUp();
                    }
                    if (animCounter > 20 && animCounter < 30 && !wallcheck(ghost))
                    {
                        ghost.movementUp();
                    }
                break;
                case -2:
                    if (animCounter < 10 && !wallcheck(ghost))
                    {
                        ghost.movementDown();
                        ghost.movementDown();
                        ghost.movementDown();
                    }
                    if (animCounter > 10 && animCounter < 20 && !wallcheck(ghost))
                    {
                        ghost.movementDown();
                        ghost.movementDown();
                    }
                    if (animCounter > 20 && animCounter < 30 && !wallcheck(ghost))
                    {
                        ghost.movementDown();
                    }
                break;
            }  
            if (animCounter == 25)
            {
                SoundPlayer death = new SoundPlayer(Resources.ghostDeadSound);
                death.Play();
            }
            if (animCounter == 120)
            {
                panel8.Left = (panel1.Left + panel1.Width / 2) - (panel8.Width / 2);
                panel8.Top = (panel1.Top + panel1.Height / 2) - (panel8.Height / 2);
                timer1.Enabled = false;
                gameend = 1;
                label21.Text = engine.scoreTotal.ToString();
                SoundPlayer death = new SoundPlayer(Resources.ending);
                death.Play();
            }
        }
    }
}