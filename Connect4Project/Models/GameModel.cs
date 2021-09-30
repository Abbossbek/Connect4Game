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
        public string Player1 { get; set; }
        public string Player1Color { get; set; }
        public string Player2 { get; set; }
        public string Player2Color { get; set; }
        public List<int> GameMap { get; set; }
    }
}
