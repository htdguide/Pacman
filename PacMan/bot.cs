using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PacMan.Properties;

namespace PacMan
{
    internal class bot
    {
        public Creatures ghost;
        public Creatures target;
        public Panel panel;
        private int mem1, mem2, mem3;

        public bot (Creatures ghost, Panel panel, Creatures target)
        {
            this.ghost = ghost;
            this.panel = panel;
            this.target = target;
        }

        private void mind()
        {
            if (ghost.appearance.Left > target.appearance.Left && ghost.appearance.Top > target.appearance.Top) //Down right corner position relative to target
            { 
                
            }
            if (ghost.appearance.Left < target.appearance.Left && ghost.appearance.Top > target.appearance.Top) //Down left corner position relative to target
            {

            }
            if (ghost.appearance.Left > target.appearance.Left && ghost.appearance.Top < target.appearance.Top) //Up right corner position relative to target
            {

            }
            if (ghost.appearance.Left < target.appearance.Left && ghost.appearance.Top < target.appearance.Top) //Up left corner position relative to target
            {

            }
        }

        private bool wallcheck(Creatures entity) //Checking for the wall collision
        {
            bool b = false;
            foreach (Control x in panel.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "border" || x.Tag == "door"))  //Wall detection
                {
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
                
            }
            return b;
        }

    }
}
