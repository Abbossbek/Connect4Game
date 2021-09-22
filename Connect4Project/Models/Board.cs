using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class Board
    {
        private int[,] _board = new int[_boardHeight, _boardWidth];// this is the representation of the board encapsulated as needs to be used by form.
        public int[,] Board1
        {
            get { return _board; }
            set { _board = value; }
        }
        private const int _boardHeight = 6;
        private const int _boardWidth = 7;

        public bool checkWin(int col, int row, int counterColour)// Will check all the possible ways a 4 in a row could be achieved and if one or more is true then true will be returned.
        {
            if (fourInRow(_board, row, col, counterColour, 1, 0) | fourInRow(_board, row, col, counterColour, -1, 1) | fourInRow(_board, row, col, counterColour, 1, -1) | fourInRow(_board, row, col, counterColour, -1, -1) | fourInRow(_board, row, col, counterColour, 0, 1) | fourInRow(_board, row, col, counterColour, 1, 1))
            {
                return true;
            }
            return false;
        }

        public static bool checkWin(int col, int row, int counterColour, int[,] board)// This needs to be accessed by the ai when board isn't encapsulated so needs to bew made static.
        {
            if (fourInRow(board, row, col, counterColour, 1, 0) | fourInRow(board, row, col, counterColour, -1, 1) | fourInRow(board, row, col, counterColour, 1, -1) | fourInRow(board, row, col, counterColour, -1, -1) | fourInRow(board, row, col, counterColour, 0, 1) | fourInRow(board, row, col, counterColour, 1, 1))
            {
                return true;
            }
            return false;
        }

        public bool boardFull()// Checks if the board is full by checking all the top row seeing if any are empty and if so then board isn't full. 
        {
            for (int i = 0; i < 7; i++)
            {
                if (_board[5, i] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool fourInRow(int[,] board, int row, int col, int counterNum, int y, int x)// Is passed x and y to determine the direction each check will make and each turn all relevant combinations will be passed to check all possible four connecting counters.
        {
            int count = 1;
            while (row - y >= 0 && row - y < 6 && col - x >= 0 && col - x < 7 && board[row - y, col - x] == counterNum)// Will shift as many possible to the left so if the counter is placed in the middle will still work out if four in a row.
            {
                row -= y;
                col -= x;
            }
            while (row + y >= 0 && row + y < 6 && col + x >= 0 && col + x < 7 && board[row + y, col + x] == counterNum)// Will then shift back as many depending on the direction to check whether there are four counters.
            {
                row += y;
                col += x;
                count++;
            }
            if (count >= 4)// If there was 4 counters in a line then true will be returned.
            {
                return true;
            }
            return false;
        }
        //public void changeUser(User u1, User u2)// Changes the user by changing the current user variable indise each user. 
        //{
        //    u1.CurrentUser = !u1.CurrentUser;
        //    u2.CurrentUser = !u2.CurrentUser;
        //}

    }
}
