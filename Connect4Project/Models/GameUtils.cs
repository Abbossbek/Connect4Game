using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    /// <summary>
    /// This represents player color.
    /// </summary>
    public enum ActivePlayer
    {
        FIRST = 1,
        SECOND = 2
    }

    /// <summary>
    /// This represents a cell that can be unoccupied or occupid by <see cref="ActivePlayer.FIRST"/>
    /// or <see cref="ActivePlayer.SECOND"/>
    /// </summary>
    public enum CellStates
    {
        EMPTY = 0,
        FIRST = ActivePlayer.FIRST,
        SECOND = ActivePlayer.SECOND
        
    }

    /// <summary>
    /// This represents difficulty level for the <see cref="Player.ComputerPlayer"/>.
    /// </summary>
    public enum DifficultyLevel
    {
        Easy = 1,
        Normal = 3,
        Hard = 5,
        None = 0
    }

    public static class BoardUtils
    {
        /// <summary>
        /// This represents number of columns of board grid.
        /// </summary>
        public static int COLS = 7;
        /// <summary>
        /// This represents number of rows of board grid.
        /// </summary>
        public static int ROWS = 6;
        /// <summary>
        /// This represents number of consecutive cells to win.
        /// </summary>
        public static int WIN_NUM = 4;
    }
}
