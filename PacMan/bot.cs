using System.Windows.Forms;
using PacMan.Properties;

namespace PacMan
{
    internal class bot
    {
        public Creatures ghost;
        public Creatures target;
        public Panel panel;
        public int wallbreakCounter;
        public int maxWallbreakCounter = 100;

        public bot(Creatures ghost, Panel panel, Creatures target)
        {
            this.ghost = ghost;
            this.panel = panel;
            this.target = target;
        }

        public void mind()
        {
                if (ghost.appearance.Left > target.appearance.Left + 2 && ghost.appearance.Top > target.appearance.Top + 2) //Down right corner position relative to target
                {
                if (!wallcheck(ghost, 2)) directionChange(2);

                else directionChange(1);
                }
                if (ghost.appearance.Left < target.appearance.Left -2 && ghost.appearance.Top > target.appearance.Top + 2) //Down left corner position relative to target
                {
                    if (!wallcheck(ghost, 2)) directionChange(2); 
                    else directionChange(-1);
                }
                if (ghost.appearance.Left > target.appearance.Left + 2 && ghost.appearance.Top < target.appearance.Top - 2) //Up right corner position relative to target
                {
                    if (!wallcheck(ghost, -2)) directionChange(-2);
                    else directionChange(1);
                }
                if (ghost.appearance.Left < target.appearance.Left - 2 && ghost.appearance.Top < target.appearance.Top - 2) //Up left corner position relative to target
                {
                    if (!wallcheck(ghost, -2)) directionChange(-2);
                    else directionChange (-1);
                }     
                if ((ghost.appearance.Left <= target.appearance.Left + 2 && ghost.appearance.Left >= target.appearance.Left) && ghost.appearance.Top > target.appearance.Top) //Same line as a target, but lower
                {
                    if (ghost.direction == 2)
                    {
                        if (wallcheck(ghost, 2))
                        {
                            wallbreakCounter = wallbreakCounter + 1;
                            if (wallbreakCounter > maxWallbreakCounter) wallbreak(ghost, 2);
                        }
                    }
                    else
                    {
                        directionChange(2);
                        wallbreakCounter = 0;
                    }
                }
                if (ghost.appearance.Left > target.appearance.Left && (ghost.appearance.Top <= target.appearance.Top + 2 && ghost.appearance.Top >= target.appearance.Top)) //Same height as a target, but more to right
                {
                    if (ghost.direction == 1)
                    {
                        if (wallcheck(ghost, 1))
                        {
                            wallbreakCounter = wallbreakCounter + 1;
                            if (wallbreakCounter > maxWallbreakCounter) wallbreak(ghost, 1);
                        }
                    }
                    else
                    {
                        directionChange(1);
                        wallbreakCounter = 0;
                    }
                }
                if ((ghost.appearance.Left <= target.appearance.Left + 2 && ghost.appearance.Left >= target.appearance.Left) && ghost.appearance.Top < target.appearance.Top) //Same line as a target, but higher
                {
                    if (ghost.direction == -2)
                    {
                        if (wallcheck(ghost, -2))
                        {
                            wallbreakCounter = wallbreakCounter + 1;
                            if (wallbreakCounter > maxWallbreakCounter) wallbreak(ghost, -2);
                        }
                    }
                    else
                    {
                        directionChange(-2);
                        wallbreakCounter = 0;
                    }
                }
                if (ghost.appearance.Left < target.appearance.Left && (ghost.appearance.Top <= target.appearance.Top + 2 && ghost.appearance.Top >= target.appearance.Top)) //Same height as a target, but more to left
                {
                    if (ghost.direction == -1)
                    {
                        if (wallcheck(ghost, -1))
                        {
                            wallbreakCounter = wallbreakCounter + 1;
                            if (wallbreakCounter > maxWallbreakCounter) wallbreak(ghost, -1);
                        }
                    }
                    else
                    {
                        directionChange(-1);
                        wallbreakCounter = 0;
                    }

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
        private void directionChange(int direction)
        {
            ghost.direction = direction;
            ghost.aimMovement();
            switch (direction)
            {
                case 1:
                    ghost.appearance.Image = Resources.GhostLeft; break;
                case -1:
                    ghost.appearance.Image = Resources.GhostRight; break;
                case 2:
                    ghost.appearance.Image = Resources.GhostUp; break;
                case 3:
                    ghost.appearance.Image = Resources.GhostDown; break;
            }
           
        }
        private void wallbreak(Creatures entity, int direction)
        {
            foreach (Control x in panel.Controls) //Checking the all controls for a pictureboxes
            {
                if (x is PictureBox && (x.Tag == "wall"))  //Wall detection
                {
                    PictureBox wall = (PictureBox)x;
                    if (direction == 1)
                    {
                        if (entity.colliderLeft.Bounds.IntersectsWith(x.Bounds))
                        {   
                            wall.Image = Resources.deadwall;
                            wall.Tag = "deadwall";
                            wallbreakCounter = 0;
                        }
                    }
                    if (direction == -1)
                    {
                        if (entity.colliderRight.Bounds.IntersectsWith(x.Bounds))
                        {
                            wall.Image = Resources.deadwall;
                            wall.Tag = "deadwall";
                            wallbreakCounter = 0;
                        }
                    }
                    if (direction == 2)
                    {
                        if (entity.colliderUp.Bounds.IntersectsWith(x.Bounds))
                        {
                            wall.Image = Resources.deadwall;
                            wall.Tag = "deadwall";
                            wallbreakCounter = 0;
                        }
                    }
                    if (direction == -2)
                    {
                        if (entity.colliderDown.Bounds.IntersectsWith(x.Bounds))
                        {
                            wall.Image = Resources.deadwall;
                            wall.Tag = "deadwall";
                            wallbreakCounter = 0;
                        }
                    }
                }

            }
        }

    }
}
