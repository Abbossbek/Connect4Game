using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class TreeNode
    {
        private int score;
        private int[,] moveBoard;
        private int row;
        private int col;
        private int counterColour;
        private int depth;

        public TreeNode[] moves = new TreeNode[7];

        public int Score { get => score; set => score = value; }
        public int Col { get => col; set => col = value; }

        public TreeNode(int[,] board, int c, int r, bool yourMove, int depth, int prevCol)// Constructor for none root node
        {
            this.depth = depth;
            Col = c;
            row = r;
            copyBoard(board);
            if (row != -1)// makes sure the move  is valid
            {
                moveBoard[row, Col] = counterColour;
            }
            if (depth > 0)
            {
                createBranches(yourMove);// creates the branches which represent all the moves that can be made next turn.
            }
            Score = calculateScore();
        }

        public TreeNode(int[,] board, int depth)// constructor for root node.
        {
            Col = -1;
            this.depth = depth;
            copyBoard(board);
            createBranches(false);
        }

        public void createBranches(bool yourMove)
        {
            for (int i = 0; i < moves.Length; i++)
            {
                int r = playerMove(moveBoard, i);
                if (r != -1)
                {
                    moves[i] = new TreeNode(moveBoard, i, r, !yourMove, depth - 1, Col);// Will create a new node for every possible move afterwards.
                }
            }
        }
        public void copyBoard(int[,] board)// creates a local copy of the board for this node to use.
        {
            moveBoard = new int[6, 7];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    moveBoard[i, j] = board[i, j];
                }
            }
        }

        public int calculateScore()
        {
            double score = checkAdjacent(0, 1) + checkAdjacent(1, 0) + checkAdjacent(1, 1) + checkAdjacent(-1, 1) + checkAdjacent(1, -1) + checkAdjacent(-1, -1);// Calls all the adjacent tiles to check if they have adjacent counters in all direction.
            return (int)score;
        }

        public double checkAdjacent(int x, int y)
        {
            int count = 0;
            while (row - y >= 0 && row - y < 6 && Col - x >= 0 && Col - x < 7 && moveBoard[row - y, Col - x] == counterColour)// will shift to the left as to not miss out on counters if placed in middle.
            {
                row -= y;
                Col -= x;
            }
            while (row + y >= 0 && row + y < 6 && Col + x >= 0 && Col + x < 7 && moveBoard[row + y, Col + x] == counterColour)//will count across then how many counters of same colour are next to it.
            {
                row += y;
                Col += x;
                count++;
            }
            return Math.Pow(count, 2);
        }

        public int playerMove(int[,] board, int move)
        {
            for (int r = 0; r < 6; r++)// will find the first available spot where a move could be placed using the column otherwise will return -1.
            {
                if (board[r, move] == 0)
                {
                    return r;
                }
            }
            return -1;
        }
        public bool checkWin()// Checks if user can win in this move.
        {
            if (Board.checkWin(Col, row, counterColour, moveBoard))
            {
                return true;
            }
            return false;
        }
    }
}
