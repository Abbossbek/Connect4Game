using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class GameModel
    {
        public string Name { get; set; }
        public int Depth { get; set; }
        public Connect4Player Player1 { get; set; }
        public Connect4Player Player2 { get; set; }
        public List<int> GameMap { get; set; }
    }
}
