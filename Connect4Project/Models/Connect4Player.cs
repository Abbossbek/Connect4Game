using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Connect4Game.Models
{
    public abstract class Connect4Player
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public int Move(int col, int[,] board)
        {
            for (int row = 0; row < 6; row++)
            {
                if (board[row, col] == 0)
                {
                    return row;
                }
            }
            return -1;
        }
    }
}
