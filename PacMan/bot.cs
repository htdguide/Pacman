using System.Windows.Forms;

namespace PacMan
{
    internal class bot
    {
        public Creatures ghost;
        public Creatures target;
        public Panel panel;
        private int mem1, mem2, mem3;

        public bot(Creatures ghost, Panel panel, Creatures target)
        {
            this.ghost = ghost;
            this.panel = panel;
            this.target = target;
        }

        public void mind()
        {
            if (ghost.appearance.Left > target.appearance.Left && ghost.appearance.Top > target.appearance.Top) //Down right corner position relative to target
            {
                if (!wallcheck(ghost, 2)) ghost.direction = 2;
                else ghost.direction = 1;
            }
            if (ghost.appearance.Left < target.appearance.Left && ghost.appearance.Top > target.appearance.Top) //Down left corner position relative to target
            {
                if (!wallcheck(ghost, 2)) ghost.direction = 2; 
                else ghost.direction = -1;
            }
            if (ghost.appearance.Left > target.appearance.Left && ghost.appearance.Top < target.appearance.Top) //Up right corner position relative to target
            {
                if (!wallcheck(ghost, -2)) ghost.direction = -2;
                else ghost.direction = 1;
            }
            if (ghost.appearance.Left < target.appearance.Left && ghost.appearance.Top < target.appearance.Top) //Up left corner position relative to target
            {
                if (!wallcheck(ghost, -2)) ghost.direction = -2;
                else ghost.direction = -1;
            }     
            if (ghost.appearance.Left == target.appearance.Left && ghost.appearance.Top > target.appearance.Top) //Same line as a target, but lower
            {
                if (!wallcheck(ghost, 2)) ghost.direction = 2; 
                else ghost.direction = 1;
            }
            if (ghost.appearance.Left > target.appearance.Left && ghost.appearance.Top == target.appearance.Top) //Same height as a target, but more to right
            {
                if (!wallcheck(ghost, 1)) ghost.direction = 1; 
                else ghost.direction = 2;
            }
            if (ghost.appearance.Left == target.appearance.Left && ghost.appearance.Top < target.appearance.Top) //Same line as a target, but higher
            {
                if (!wallcheck(ghost, -2)) ghost.direction = -2; 
                else ghost.direction = 1;
            }
            if (ghost.appearance.Left < target.appearance.Left && ghost.appearance.Top == target.appearance.Top) //Same height as a target, but more to left
            {
                if (!wallcheck(ghost, -1)) ghost.direction = -1; 
                else ghost.direction = 2;
            }
        }

        private bool wallcheck(Creatures entity, int direction) //Checking for the wall collision
        {
            bool b = false;
            foreach (Control x in panel.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall" || x.Tag == "border" || x.Tag == "door"))  //Wall detection
                {
                    if (direction == 1)
                    {
                        if (entity.colliderLeft.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (direction == -1)
                    {
                        if (entity.colliderRight.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (direction == 2)
                    {
                        if (entity.colliderUp.Bounds.IntersectsWith(x.Bounds))
                        {
                            b = true;
                        }
                    }
                    if (direction == -2)
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
