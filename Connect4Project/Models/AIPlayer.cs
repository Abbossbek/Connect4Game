using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public abstract class AIPlayer : Connect4Player
    {
        protected TreeNode root;
        private int[] moveScores;
        private int depth;

        public int Depth
        {
            get => depth;
            set => depth = value;
        }

        public virtual int bestMove(int[,] board)
        {
            return collectScores();
        }

        public int collectScores()// Called for first layers of branches so can be stored in array.
        {
            moveScores = new int[7];
            TreeNode[] childNodes = root.moves;
            for (int i = 0; i < childNodes.Length; i++)
            {
                if (childNodes[i] != null)
                {
                    if (childNodes[i].checkWin())// Override added to make sure they place to win if they can.
                    {
                        return i;
                    }
                    else
                    {
                        moveScores[i] = childNodes[i].Score + getScores(childNodes[i], false);
                    }
                }
                else
                {
                    moveScores[i] = -120000;
                }
            }
            foreach (int i in moveScores)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();
            return Array.IndexOf(moveScores, moveScores.Max());// returns max in the array of scores meaning it will be optimum score.
        }

        public int getScores(TreeNode node, bool yourMove)// collects the scores recursively 
        {
            int score = 0;
            TreeNode[] childNodes = node.moves;
            for (int i = 0; i < childNodes.Length; i++)
            {
                if (childNodes[i] != null)
                {
                    if (yourMove)
                    {
                        score += childNodes[i].Score + getScores(childNodes[i], !yourMove);
                    }
                    else
                    {
                        if (childNodes[i].checkWin())
                        {
                            if (childNodes[i].Col == i)
                            {
                                score -= 3000;
                            }
                            else
                            {
                                score += 3000;
                            }
                        }
                        else
                        {
                            score += (-childNodes[i].Score) + getScores(childNodes[i], !yourMove);// recursively calls method so scores for each are added upwards.
                        }
                    }
                }
            }
            Console.WriteLine(score);
            return score;
        }
    }
}
