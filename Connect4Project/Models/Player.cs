using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Connect4Game.Models
{
    public abstract class Player
    {
        public string Name { get; private set; }
        public string Color { get; set; }
        public Player(string name)
        {
            Name = name;
        }
        public virtual int Move(int[,] boardM) { return -1; }
    }
}
