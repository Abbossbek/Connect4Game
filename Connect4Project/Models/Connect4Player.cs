using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Connect4Game.Models
{
    public class Connect4Player
    {
        public string Name { get; set; }
        public string Color { get; set; }
    }
}
