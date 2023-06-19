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
        public int scoreTotal = 0;
        public int score1 = 0; //Player's score
        public int lives1 = 3; //Player's lives
        public int score2 = 0; //Second player's score
        public int lives2 = 3; //Second player's lives
        public int keys = 0;
        public int ghosts; //ghosts alive
        public bool restart; //restart button
        public bool pause; //pause button
        public Label scoreBoard1, livesBoard1, scoreBoard2, livesBoard2;
        private Creatures pacman, ghost1, ghost2, ghost3;

        public Engine (int score1, int lives1, bool restart, bool pause, Creatures pacman, Creatures ghost1, Creatures ghost2, Creatures ghost3, Label scoreBoard1, Label livesBoard1, Label scoreBoard2, Label livesBoard2)
        {
            this.score1 = score1;
            this.lives1 = lives1;
            this.restart = restart;
            this.pause = pause;
            this.pacman = pacman;
            this.ghost1 = ghost1;
            this.ghost2 = ghost2;
            this.ghost3 = ghost3;
            this.scoreBoard1 = scoreBoard1;
            this.livesBoard1 = livesBoard1;
            this.scoreBoard1 = scoreBoard2;
            this.livesBoard1 = livesBoard2;
        }
        public void scoring(Creatures player)
        {
            if (player.name == "Pacman")
            {
                score1 = score1 + 1;
                scoreBoard1.Text = score1.ToString();
                livesBoard1.Text = lives1.ToString();
                scoreTotal = score1 + score2;
            }
            else if (player.name == "Pacman2")
            {
                score2= score2++;
                scoreBoard2.Text = score2.ToString();
                livesBoard2.Text = lives2.ToString();
                scoreTotal = score1 + score2;
            }
        }
    }
}
