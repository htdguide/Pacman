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
        public Label scoreBoard, livesBoard;
        private Creatures pacman, ghost;

        public Engine (int scoreTotal, int lives, Creatures pacman, Creatures ghost, Label scoreBoard, Label livesBoard)
        {
            this.scoreTotal = scoreTotal;
            this.lives = lives;
            this.pacman = pacman;
            this.ghost = ghost;
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
