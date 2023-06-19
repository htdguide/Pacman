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
        private Creatures player, ghost1, ghost2, ghost3;
        private Engine engine;
        private bool c = true; //debugging
        private bool gates = false; //gates open
        private int gatesCounter = 0;
        private int doorsCounter = 0;
        public Pacman()
        {
            InitializeComponent();
            player = new Creatures("Pacman", pictureBox204, collider1, collider2, collider3, collider4, vision1, -1, 0);
            // ghost1 = new Creatures("Ghost 1", pictureBox392, 2, 0);
            // ghost2 = new Creatures("Ghost 2", pictureBox393, 2, 0);
            // ghost3 = new Creatures("Ghost 3", pictureBox394, 2, 0);
            engine = new Engine(0, 0, false, false, player, ghost1, ghost2, ghost3, label5, label6, label7,label9);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Width > 500 && engine.scoreTotal < 1)
            {
                panel1.Width = 380;
                Pacman.ActiveForm.Width = 410;
                Pacman.ActiveForm.Height = 570;
            }
            if (!wallcheck(player)) player.movement();
            if ((engine.scoreTotal > 187 && gatesCounter < 20) || (gatesCounter < 20 && gates == true)) gatesopen(pictureBox41,4);
        }

        private void Pacman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.direction = 1;
                player.appearance.Image = Resources.pacmanLeft;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.direction = -1;
                player.appearance.Image = Resources.pacmanRight;
            }
            if (e.KeyCode == Keys.Up)
            {
                player.direction = 2;
                player.appearance.Image = Resources.pacmanUp;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.direction = -2;
                player.appearance.Image = Resources.pacmanDown;
            }
            if (e.KeyCode == Keys.Space)
            {

            }
            if (e.KeyCode == Keys.X) //Debug mode
            {
               // firstGhost.Visible = !c;
               // secondGhost.Visible = !c;
               // thirdGhost.Visible = !c;
                pictureBox204.Visible = !c;
               // firstCollisionLeft.Visible = c;
               // firstColliderRight.Visible = c;
               // firstColliderUp.Visible = c;
               // firstColliderDown.Visible = c;
               // secondColliderLeft.Visible = c;
               // secondColliderRight.Visible = c;
               // secondColliderUp.Visible = c;
               // secondColliderDown.Visible = c;
               // thirdColliderLeft.Visible = c;
               // thirdColliderRight.Visible = c;
               // thirdColliderUp.Visible = c;
               // thirdColliderDown.Visible = c;
                collider1.Visible = c;
                collider2.Visible = c;
                collider3.Visible = c;
                collider4.Visible = c;
                vision1.Visible = c;
                gates = true;
                engine.keys = 3;
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
                    aligning(entity, x); //Alligning of the creature
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
                if (x is PictureBox && (x.Tag == "kibble"))  //Kibble detection
                {
                    if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                    {
                        engine.scoring(entity);
                        panel1.Controls.Remove(x);
                    }
                }
                if (x is PictureBox && (x.Tag == "straw"))  //Kibble detection
                {
                    if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                    {
                        foreach (Control z in panel1.Controls)
                        {
                            if (z is PictureBox && z.Tag == "kibble") z.Visible = true;
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
                        engine.keys = engine.keys + 1;
                        SoundPlayer collect = new SoundPlayer(Resources.keys);
                        collect.Play();
                        panel1.Controls.Remove(x);
                    }
                }
                if (x is PictureBox && (x.Tag == "door" && engine.keys == 3))  //Door open detection
                {
                    if (entity.vision.Bounds.IntersectsWith(x.Bounds))
                    {
                        SoundPlayer collect = new SoundPlayer(Resources.doorSound);
                        collect.Play();
                        panel1.Controls.Remove(x);
                        engine.keys = 0;
                    }
                }
                if (x is PictureBox && (x.Tag == "kibble" || x.Tag == "wall" || x.Tag == "wall2"|| x.Tag == "key" || x.Tag == "door" || x.Tag == "straw"))  //Vision detection
                {
                    if (entity.vision.Bounds.IntersectsWith(x.Bounds))
                    {
                        x.Visible = true;
                    }
                }
                if ((entity.vision.Right + 5) == panel1.Right && panel1.Right < 1005) //Level extender
                {
                    panel1.Width = panel1.Width + player.speed;
                    Pacman.ActiveForm.Width = Pacman.ActiveForm.Width + player.speed;
                }
                if (engine.scoreTotal > 100 && doorsCounter == 0)
                {
                    SoundPlayer collect = new SoundPlayer(Resources.doorSound);
                    collect.Play();
                    panel1.Controls.Remove(doorS);
                    doorsCounter = 1;
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
        private void gatesopen(PictureBox door, int direction) //Gates for level extension
        {
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
    }
}