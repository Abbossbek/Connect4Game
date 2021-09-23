using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class HardAIPlayer:AIPlayer
    {
        public override int bestMove(int[,] board)
        {
            root = new TreeNode(board, 2,1,3); // creates the tree storing moves with the root being the board with no move
            return base.bestMove(board);
        }
    }
}
