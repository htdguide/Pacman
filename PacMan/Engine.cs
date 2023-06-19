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
        public int scoreTotal;
        public int lives = 3; //Player's lives
        public int keys = 0;
        public int ghosts; //ghosts alive
        public bool restart; //restart button
        public bool pause; //pause button
        public Label scoreBoard, livesBoard;
        private Creatures pacman, ghost1, ghost2, ghost3;

        public Engine (int scoreTotal, int lives, bool restart, bool pause, Creatures pacman, Creatures ghost1, Creatures ghost2, Creatures ghost3, Label scoreBoard, Label livesBoard)
        {
            this.scoreTotal = scoreTotal;
            this.lives = lives;
            this.restart = restart;
            this.pause = pause;
            this.pacman = pacman;
            this.ghost1 = ghost1;
            this.ghost2 = ghost2;
            this.ghost3 = ghost3;
            this.scoreBoard = scoreBoard;
            this.livesBoard = livesBoard;
        }
        public void scoring(Creatures player)
        {
                scoreTotal = scoreTotal + 1;
                scoreBoard.Text = scoreTotal.ToString();
                livesBoard.Text = lives.ToString();
        }
    }
}
