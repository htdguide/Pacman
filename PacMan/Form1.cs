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
        private bool c = true; //debugging
        private bool gates = false; //gates open
        private int gatesCounter = 0;
        private int doorsCounter = 0;
        private int shotAbility = -1;
        private int shells = 0;
        private int appearanceCount = 0;
        private int ghostJump = 300;
        private bot bot;
        public Pacman()
        {
            InitializeComponent();
            player = new Creatures("Pacman", pictureBox204, collider1, collider2, collider3, collider4, vision1, aimBox, -1, 0, 2);
            ghost = new Creatures("ghost", ghostAppearance, ghostColliderUp, ghostColliderDown, ghostColliderLeft, ghostColliderRight, ghostVision, ghostAim, -1, 0, 1);
            bot = new bot(ghost, panel1, player);
            engine = new Engine(0, 0, player, ghost, label7,label9);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bot.mind();
            ghostUnhide();
            if (panel1.Width > 500 && engine.scoreTotal < 1)
            {
                panel1.Width = 380;
                Pacman.ActiveForm.Width = 410;
                Pacman.ActiveForm.Height = 570;
                panel2.Width = 360;
                panel3.Width = 150;
            }
            if (!wallcheck(player)) player.movement();
            if (!wallcheck(ghost)) ghost.movement();
            if ((engine.scoreTotal > 170 && gatesCounter < 20) || (gatesCounter < 20 && gates == true)) gatesopen(pictureBox41,4);
        }

        private void Pacman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.direction = 1;
                player.aimMovement();
                player.appearance.Image = Resources.pacmanLeft;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.direction = -1;
                player.aimMovement();
                player.appearance.Image = Resources.pacmanRight;
            }
            if (e.KeyCode == Keys.Up)
            {
                player.direction = 2;
                player.aimMovement();
                player.appearance.Image = Resources.pacmanUp;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.direction = -2;
                player.aimMovement();
                player.appearance.Image = Resources.pacmanDown;
            }
            if (e.KeyCode == Keys.Space)
            {
                if (shotAbility == 1) shot();
                else if (shotAbility == 0 && shells > 0) reload();
                
            }
            if (e.KeyCode == Keys.X) //Debug mode
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
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "wall2" || x.Tag == "border" || x.Tag =="door"))  //Wall detection
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
                    if (x is PictureBox && (x.Tag == "kibble"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            engine.scoring(entity);
                            panel1.Controls.Remove(x);
                        }
                    }
                    if (x is PictureBox && (x.Tag == "shell"))  //Kibble detection
                    {
                        if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                        {
                            shells = shells + 5;
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
                            SoundPlayer pick = new SoundPlayer(Resources.gunPick);
                            pick.Play();
                            panel3.Visible = true;
                            pictureBox378.Visible = true;
                            pictureBox393.Visible = true;
                            aimBox.Visible = true;
                            label3.Visible = true;
                            shotAbility = 1;
                            panel1.Controls.Remove(x);
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
                    if (x is PictureBox && (x.Tag == "kibble" || x.Tag == "wall" || x.Tag == "wall2" || x.Tag == "key" || x.Tag == "door" || x.Tag == "straw" || x.Tag == "shotgun" || x.Tag == "shell" || x.Tag == "cherry"))  //Vision detection
                    {
                        if (entity.vision.Bounds.IntersectsWith(x.Bounds))
                        {
                            x.Visible = true;
                        }
                    }
                    if ((entity.appearance.Right + 25) > panel1.Right) //Level extender
                    {
                        panel1.Width = panel1.Width + player.speed;
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
            if (timer1.Enabled == true)
            {
                timer1.Enabled = false;
                label11.Text = "Start!";
            }
            else
            {
                timer1.Enabled = true;
                label11.Text = "Pause";
            }
        }

        private void gatesopen(PictureBox door, int direction) //Gates for level extension
        {
            SoundPlayer gates = new SoundPlayer(Resources.gates);
            gates.Play();
            switch (direction)
            {
                case 1: //Left
                    door.Left = door.Left - 1; 
                    break;
                case 2:
                    door.Left = door.Left + 1;
                    break;
                case 3:
                    door.Top = door.Top - 1;
                    break;
                case 4:
                    door.Top = door.Top + 1;
                    break;
            }
            gatesCounter = gatesCounter + 1;   
        }
        private void shot()
        {
            SoundPlayer shot = new SoundPlayer(Resources.shot);
            shot.Play();
            shotAbility = 0;
            foreach (PictureBox v in panel1.Controls)
            {
                if (v.Tag == "wall")
                {
                    if (aimBox.Bounds.IntersectsWith(v.Bounds))
                    {
                        v.Image = Resources.deadwall;
                        v.Tag = "deadwall";
                        v.Visible = true;
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
            if (player.vision.Bounds.IntersectsWith(ghost.appearance.Bounds) || player.aimbox.Bounds.IntersectsWith(ghost.appearance.Bounds))
            {
                ghost.appearance.Visible = true;
                if (ghostJump == 300) ghost.speed = 2;
                if (appearanceCount == 0)
                {
                    pursuit.Play();
                }
                appearanceCount = 100;
                if (ghostJump > 0) ghostJump = ghostJump - 1;
                if (ghostJump == 0) ghost.speed = 1;
            }
            else if (ghost.appearance.Visible && appearanceCount == 0)
            {
                ghost.appearance.Visible = false;
                ghost.speed = 1;
                pursuit.Stop();
                ghostJump = 300;
            }
            if (appearanceCount > 0) appearanceCount = appearanceCount - 1;
        }
    }
}