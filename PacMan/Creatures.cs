using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    internal class Creatures //Entity class for pacman and ghosts
    {
        public string name; //Name of the creature
        public int direction; //movement direction
        public PictureBox appearance, colliderUp, colliderDown, colliderLeft, colliderRight; //appearance and collision pictureboxes
        public int stop = 0; 


        public Creatures(string name, PictureBox appearance, PictureBox colliderUp, PictureBox colliderDown, PictureBox colliderLeft, PictureBox colliderRight,  int direction,int stop) 
        {
            this.name = name;
            this.appearance = appearance;
            this.colliderUp = colliderUp;
            this.colliderDown = colliderDown;
            this.colliderLeft = colliderLeft;
            this.colliderRight = colliderRight;
            this.direction = direction;
            this.stop = stop;
        }

        public void movement()
        {
            if (stop == 0)
            {
                if (direction == 1)
                {
                    appearance.Left = appearance.Left - 1;
                    colliderUp.Left = colliderUp.Left - 1;
                    colliderDown.Left = colliderDown.Left - 1;
                    colliderLeft.Left = colliderLeft.Left - 1;
                    colliderRight.Left = colliderRight.Left - 1;
                }
                if (direction == -1)
                {
                    appearance.Left = appearance.Left + 1;
                    colliderUp.Left = colliderUp.Left + 1;
                    colliderDown.Left = colliderDown.Left + 1;
                    colliderLeft.Left = colliderLeft.Left + 1;
                    colliderRight.Left = colliderRight.Left + 1;
                }
                if (direction == 2)
                {
                    appearance.Top = appearance.Top - 1;
                    colliderUp.Top = colliderUp.Top - 1;
                    colliderDown.Top = colliderDown.Top - 1;
                    colliderLeft.Top = colliderLeft.Top - 1;
                    colliderRight.Top = colliderRight.Top -1;
                }
                if (direction == -2)
                {
                    appearance.Top = appearance.Top + 1;
                    colliderUp.Top = colliderUp.Top + 1;
                    colliderDown.Top = colliderDown.Top + 1;
                    colliderLeft.Top = colliderLeft.Top + 1;
                    colliderRight.Top = colliderRight.Top + 1;
                }
            }
        }
    }
}
