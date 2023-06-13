using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacMan
{
    internal class Engine
    {
        public int score; //Game score
        public int lives; //Player's lives
        public int ghosts; //ghosts alive
        public bool restart; //restart button
        public bool pause; //pause button
        private Creatures pacman, ghost1, ghost2, ghost3;

        public Engine (int score, int lives, int ghosts, bool restart, bool pause, Creatures pacman, Creatures ghost1, Creatures ghost2, Creatures ghost3)
        {
            this.score = score;
            this.lives = lives;
            this.ghosts = ghosts;
            this.restart = restart;
            this.pause = pause;
            this.pacman = pacman;
            this.ghost1 = ghost1;
            this.ghost2 = ghost2;
            this.ghost3 = ghost3;
        }

    }
}
