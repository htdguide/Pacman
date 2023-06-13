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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PacMan
{
    public partial class Pacman : Form
    {
        private Creatures player, ghost1, ghost2, ghost3;
        private Engine engine;

        public Pacman()
        {
            InitializeComponent();
            player = new Creatures("Pacman", pictureBox204, -1, 0);
            ghost1 = new Creatures("Ghost 1", pictureBox392, 2, 0);
            ghost2 = new Creatures("Ghost 2", pictureBox393, 2, 0);
            ghost3 = new Creatures("Ghost 3", pictureBox394, 2, 0);
            engine = new Engine(0, 0, 3, false, false, player, ghost1, ghost2, ghost3);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            wallcheck(player);
            player.movement();
        }

        private void Pacman_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                player.direction = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                player.direction = -1;
            }
            if (e.KeyCode == Keys.Up)
            {
                player.direction = 2;
            }
            if (e.KeyCode == Keys.Down) 
            {
                player.direction = -2;
            }
        }

        private void wallcheck (Creatures entity) //Checking for the wall collision
        {
            foreach (Control x in panel1.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "border"))  //Tag property 
                {
                    if (entity.appearance.Bounds.IntersectsWith(x.Bounds)) //If entity touches the wall
                    {
                        player.direction = -player.direction;
                        player.movement();
                        player.direction = -player.direction;
                    }
                }
            }
        }
    }
}
