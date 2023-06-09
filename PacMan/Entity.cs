using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    internal class Entity //Entity class for pacman and ghosts
    {
        public string name;
        public PictureBox appearance;

        public Entity(string name, PictureBox appearance) 
        {
            this.name = name;
            this.appearance = appearance;
        }

    }
}
