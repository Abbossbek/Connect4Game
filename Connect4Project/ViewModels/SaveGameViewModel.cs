using Caliburn.Micro;

using Connect4Game.Models;
using Connect4Game.Utils;

using System.Collections.Generic;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class SaveGameViewModel : Screen
    {
        private List<GameModel> _games;
        private GameModel _game;

        public string Name
        {
            get { return _game.Name; }
            set { _game.Name = value;
            }
        }

        public SaveGameViewModel(Player player1, Player player2, int[,] gameMap)
        {
            var list = new List<int>();
            foreach (var item in gameMap)
            {
                list.Add(item);
            }
            _game = new GameModel() { Player1 = player1.Name, Player1Color = player1.Color, Player2 = player2.Name, Player2Color = player2.Color, GameMap = list };
            if (player2.GetType() == typeof(AIPlayer))
            {
                _game.Depth = (int)((AIPlayer)player2).Lavel;
            }
            _games = DataHelper.GetValue<List<GameModel>>("games")?? new List<GameModel>();
        }

        public bool CanSave(string name)
        {
            if (string.IsNullOrEmpty(name) || _games is null || _games.Exists(x=>x.Name == _game.Name))
                return false;
            return true;
        }
        public void Save(string name)
        {
            _games.Add(_game);
            DataHelper.SetValue("games", _games);
            var conductor = this.Parent as ShellViewModel;
            conductor.BackHomeCommand();
        }
    }
}
