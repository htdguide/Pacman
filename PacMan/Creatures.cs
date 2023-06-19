using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacMan.Properties;

namespace PacMan
{
    internal class Creatures //Entity class for pacman and ghosts
    {
        public string name; //Name of the creature
        public int direction; //movement direction
        public int speed = 1; //speed
        public PictureBox appearance, colliderUp, colliderDown, colliderLeft, colliderRight, vision, aimbox; //appearance and collision pictureboxes
        public int stop = 0;


        public Creatures(string name, PictureBox appearance, PictureBox colliderUp, PictureBox colliderDown, PictureBox colliderLeft, PictureBox colliderRight, PictureBox vision, PictureBox aimbox, int direction, int stop) 
        {
            this.name = name;
            this.appearance = appearance;
            this.colliderUp = colliderUp;
            this.colliderDown = colliderDown;
            this.colliderLeft = colliderLeft;
            this.colliderRight = colliderRight;
            this.direction = direction;
            this.stop = stop;
            this.vision = vision;
            this.aimbox = aimbox;
        }

        public void movement()
        {
            if (direction == 1) //Left
            {
                appearance.Left = appearance.Left - speed;
                colliderUp.Left = colliderUp.Left - speed;
                colliderDown.Left = colliderDown.Left - speed;
                colliderLeft.Left = colliderLeft.Left - speed;
                colliderRight.Left = colliderRight.Left - speed;
                vision.Left = vision.Left - speed;
                aimbox.Left = aimbox.Left - speed;
            }
            if (direction == -1) //Right
            {
                appearance.Left = appearance.Left + speed;
                colliderUp.Left = colliderUp.Left + speed;
                colliderDown.Left = colliderDown.Left + speed;
                colliderLeft.Left = colliderLeft.Left + speed;
                colliderRight.Left = colliderRight.Left + speed;
                vision.Left = vision.Left + speed;
                aimbox.Left = aimbox.Left + speed;
            }
            if (direction == 2) //Up
            {
                appearance.Top = appearance.Top - speed;
                colliderUp.Top = colliderUp.Top - speed;
                colliderDown.Top = colliderDown.Top - speed;
                colliderLeft.Top = colliderLeft.Top - speed;
                colliderRight.Top = colliderRight.Top - speed;
                vision.Top = vision.Top - speed;
                aimbox.Top = aimbox.Top - speed;
            }
            if (direction == -2) //Down
            {
                appearance.Top = appearance.Top + speed;
                colliderUp.Top = colliderUp.Top + speed;
                colliderDown.Top = colliderDown.Top + speed;
                colliderLeft.Top = colliderLeft.Top + speed;
                colliderRight.Top = colliderRight.Top + speed;
                vision.Top = vision.Top + speed;
                aimbox.Top = aimbox.Top + speed;
            }
        }
        public void aimMovement()
        {
            if (direction == 1) //Left
            {
                aimbox.Location = appearance.Location;
                aimbox.Top = aimbox.Top + 2;
                aimbox.Left = appearance.Left - 75;
                aimbox.Height = 15;
                aimbox.Width = 75;
            }
            if (direction == -1) //Right
            {
                aimbox.Location = appearance.Location;
                aimbox.Left = appearance.Right;
                aimbox.Top = aimbox.Top + 2;
                aimbox.Height = 15;
                aimbox.Width = 75;
            }
            if (direction == 2) //Up
            {
                aimbox.Location = appearance.Location;
                aimbox.Top = appearance.Top - 75;
                aimbox.Left = aimbox.Left;
                aimbox.Height = 75;
                aimbox.Width = 15;
            }
            if (direction == -2) //Down
            {
                aimbox.Location = appearance.Location;
                aimbox.Top = appearance.Bottom;
                aimbox.Left = aimbox.Left;
                aimbox.Height = 75;
                aimbox.Width = 15;
            }
        }
        public void movementLeft() //just 1 step movement 
        {
                    appearance.Left = appearance.Left - 1;
                    colliderUp.Left = colliderUp.Left - 1;
                    colliderDown.Left = colliderDown.Left - 1;
                    colliderLeft.Left = colliderLeft.Left - 1;
                    colliderRight.Left = colliderRight.Left - 1;
                    vision.Left = vision.Left - 1;
                    aimbox.Left = aimbox.Left - 1;
        }
        public void movementRight()
        {

                    appearance.Left = appearance.Left + 1;
                    colliderUp.Left = colliderUp.Left + 1;
                    colliderDown.Left = colliderDown.Left + 1;
                    colliderLeft.Left = colliderLeft.Left + 1;
                    colliderRight.Left = colliderRight.Left + 1;
                    vision.Left = vision.Left + 1;
                    aimbox.Left = aimbox.Left + 1;            
        }
        public void movementUp()
        {
                    appearance.Top = appearance.Top - 1;
                    colliderUp.Top = colliderUp.Top - 1;
                    colliderDown.Top = colliderDown.Top - 1;
                    colliderLeft.Top = colliderLeft.Top - 1;
                    colliderRight.Top = colliderRight.Top - 1;
                    vision.Top = vision.Top - 1;
                    aimbox.Top = aimbox.Top - 1;
        }
        public void movementDown()
        {
                    appearance.Top = appearance.Top + 1;
                    colliderUp.Top = colliderUp.Top + 1;
                    colliderDown.Top = colliderDown.Top + 1;
                    colliderLeft.Top = colliderLeft.Top + 1;
                    colliderRight.Top = colliderRight.Top + 1;
                    vision.Top = vision.Top + 1;
                    aimbox.Top = aimbox.Top + 1;
        }
    }
}
