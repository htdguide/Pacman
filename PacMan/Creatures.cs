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
        public string name;
        public int direction;
        public PictureBox appearance;
        public int stop = 0;


        public Creatures(string name, PictureBox appearance,int direction,int stop) 
        {
            this.name = name;
            this.appearance = appearance;
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
                }
                if (direction == -1)
                {
                    appearance.Left = appearance.Left + 1;
                }
                if (direction == 2)
                {
                    appearance.Top = appearance.Top - 1;
                }
                if (direction == -2)
                {
                    appearance.Top = appearance.Top + 1;
                }
            }
        }
    }
}
