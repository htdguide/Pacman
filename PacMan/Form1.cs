using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        private bool c = true;

        public Pacman()
        {
            InitializeComponent();
            player = new Creatures("Pacman", pictureBox204, collider1, collider2, collider3, collider4, -1, 0);
            // ghost1 = new Creatures("Ghost 1", pictureBox392, 2, 0);
            // ghost2 = new Creatures("Ghost 2", pictureBox393, 2, 0);
            // ghost3 = new Creatures("Ghost 3", pictureBox394, 2, 0);
            engine = new Engine(0, 0, false, false, player, ghost1, ghost2, ghost3, label5, label6, label7,label9);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!wallcheck(player)) player.movement();
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
                panel1.Width = 1005;
                Pacman.ActiveForm.Width = 1040;
                pictureBox49.Top = pictureBox49.Top + 25;
            }
            if (e.KeyCode == Keys.X) //Collision debug
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
                c = !c;
            }
        }

        private bool wallcheck(Creatures entity) //Checking for the wall collision
        {
            bool b = false;
            foreach (Control x in panel1.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "border"))  //Tag property 
                {
                    aligning(entity, x);
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
                if (x is PictureBox && (x.Tag == "kibble"))  //Tag property 
                {
                    if (entity.appearance.Bounds.IntersectsWith(x.Bounds))
                    {
                        engine.scoring(entity);
                        panel1.Controls.Remove(x);
                    }
                }
            }
            return b;
        }
        private void aligning(Creatures entity, Control x)
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
    }
}