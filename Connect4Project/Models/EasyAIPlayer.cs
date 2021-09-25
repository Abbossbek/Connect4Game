using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Game.Models
{
    public class EasyAIPlayer:AIPlayer
    {
        public EasyAIPlayer():base(DifficultyLevel.Easy)
        {

        }
    }
}
