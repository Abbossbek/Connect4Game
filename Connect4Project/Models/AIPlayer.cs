using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
   public abstract class AIPlayer : Connect4Player
    {
        readonly int maximumDepth;
        readonly Random random;

        /// <summary>
        /// This instantiates <see cref="maximumDepth"/> based from <see cref="Form1.difficultyLevel"/>.
        /// </summary>
        /// <param name="difficultyLevel"></param>
        public AIPlayer(DifficultyLevel difficultyLevel)
        {
            this.maximumDepth = (int)difficultyLevel;

            if (maximumDepth < (int)DifficultyLevel.Easy ||
                maximumDepth > (int)DifficultyLevel.Hard)
                throw new ArgumentOutOfRangeException("difficultyLevel");

            this.random = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// This chooses a random element from a list of good, possible moves.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public int GetBestMove(int[,] boardM, ActivePlayer player)
        {
            var board = new Board(boardM);
            var node = new Node(board);
            var possibleMoves = getPossibleMoves(node);
            var scores = new double[possibleMoves.Count];
            Board updatedBoard;

            for (int i = 0; i < possibleMoves.Count; i++)
            {
                board.MakeMove(player, possibleMoves[i], out updatedBoard);
                var variant = new Node(updatedBoard);
                createTree(getOpponent(player), variant, 0);
                scores[i] = scoreNode(variant, player, 0);
            }

            double maximumScore = double.MinValue;
            var goodMoves = new List<int>();

            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > maximumScore)
                {
                    goodMoves.Clear();
                    goodMoves.Add(i);
                    maximumScore = scores[i];
                }
                else if (scores[i] == maximumScore)
                {
                    goodMoves.Add(i);
                }
            }

            return possibleMoves[goodMoves[random.Next(0, goodMoves.Count)]];
        }

        /// <summary>
        /// This returns a list of possible moves based from the <see cref="Board.GetCellState(int, int)"/> of
        /// <see cref="node"/>.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<int> getPossibleMoves(Node node)
        {
            var moves = new List<int>();

            for (int i = 0; i < BoardUtils.COLS; i++)
            {
                if (node.Board.GetCellState(0, i) == CellStates.EMPTY)
                {
                    moves.Add(i);
                }
            }
            return moves;
        }

        /// <summary>
        /// This creates a recursive tree based from Minimax Algorithm. The depth of the
        /// tree is based from <see cref="maximumDepth"/>.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="rootNode"></param>
        /// <param name="depth"></param>
        private void createTree(ActivePlayer player, Node rootNode, int depth)
        {
            if (depth >= maximumDepth)
                return;

            var moves = getPossibleMoves(rootNode);

            foreach (var move in moves)
            {
                Board updatedBoard;
                rootNode.Board.MakeMove(player, move, out updatedBoard);
                var variantNode = new Node(updatedBoard);
                createTree(getOpponent(player), variantNode, depth + 1);
                rootNode.Variants.Add(variantNode);
            }
        }


        private double scoreNode(Node node, ActivePlayer player, int depth)
        {
            double score = 0;

            if (CheckForVictory(player, node.Board))
            {
                if (depth == 0)
                {
                    score = double.PositiveInfinity;
                }
                else
                {
                    score += Math.Pow(10.0, maximumDepth - depth);
                }
            }
            else if (CheckForVictory(getOpponent(player), node.Board))
            {
                score += -Math.Pow(100
                    , maximumDepth - depth);
            }
            else
            {
                foreach (var opponentMove in node.Variants)
                {
                    score += scoreNode(opponentMove, player, depth + 1);
                }
            }

            return score;
        }
        public static bool CheckForVictory(ActivePlayer player, Board board)
        {
            if (board == null)
                throw new ArgumentNullException("board null");

            for (int i = 0; i < BoardUtils.ROWS; i++)
            {
                for (int j = 0; j < BoardUtils.COLS; j++)
                {
                    if (board.GetCellState(i, j) == (CellStates)player)
                    {
                        if (SearchVictory(board, i, j))
                            return true;
                    }
                }
            }

            return false;
        }

        private static bool SearchVictory(Board board, int row, int column)
        {
            bool searchRight, searchLeft, searchUp, searchDown;

            searchRight = column <= BoardUtils.COLS - BoardUtils.WIN_NUM;
            searchLeft = column >= BoardUtils.WIN_NUM - 1;
            searchUp = row > BoardUtils.ROWS - BoardUtils.WIN_NUM;
            searchDown = row <= BoardUtils.ROWS - BoardUtils.WIN_NUM;

            if (searchRight)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row, column + 1),
                                    board.GetCellState(row, column + 2),
                                    board.GetCellState(row, column + 3)))
                    return true;
            }

            if (searchLeft)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row, column - 1),
                                    board.GetCellState(row, column - 2),
                                    board.GetCellState(row, column - 3)))
                    return true;
            }

            if (searchUp)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row - 1, column),
                                    board.GetCellState(row - 2, column),
                                    board.GetCellState(row - 3, column)))
                    return true;
            }

            if (searchDown)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row + 1, column),
                                    board.GetCellState(row + 2, column),
                                    board.GetCellState(row + 3, column)))
                    return true;
            }

            if (searchLeft && searchUp)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row - 1, column - 1),
                                    board.GetCellState(row - 2, column - 2),
                                    board.GetCellState(row - 3, column - 3)))
                    return true;
            }

            if (searchLeft && searchDown)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row + 1, column - 1),
                                    board.GetCellState(row + 2, column - 2),
                                    board.GetCellState(row + 3, column - 3)))
                    return true;
            }

            if (searchRight && searchUp)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row - 1, column + 1),
                                    board.GetCellState(row - 2, column + 2),
                                    board.GetCellState(row - 3, column + 3)))
                    return true;
            }

            if (searchRight && searchDown)
            {
                if (CheckCells(board.GetCellState(row, column),
                                    board.GetCellState(row + 1, column + 1),
                                    board.GetCellState(row + 2, column + 2),
                                    board.GetCellState(row + 3, column + 3)))

                    return true;
            }

            return false;
        }
        private static bool CheckCells(params CellStates[] cells)
        {

            for (int i = 1; i < BoardUtils.WIN_NUM; i++)
            {
                if (cells[i] != cells[0])
                    return false;
            }

            return true;
        }
        private static ActivePlayer getOpponent(ActivePlayer opponentPlayer)
        {
            return opponentPlayer == ActivePlayer.FIRST ? ActivePlayer.SECOND : ActivePlayer.FIRST;
        }

        private class Node
        {
            readonly Board board;
            readonly List<Node> variants;

            public Board Board { get { return board; } }
            public List<Node> Variants { get { return variants; } }

            public Node(Board board)
            {
                this.board = board;
                this.variants = new List<Node>();
            }
        }
    }
}
