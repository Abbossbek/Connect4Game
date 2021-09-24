using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class GameStep
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Connect4Player Player { get; set; }
    }
}
