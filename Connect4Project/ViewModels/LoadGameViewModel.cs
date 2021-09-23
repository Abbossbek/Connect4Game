using Caliburn.Micro;

using Connect4Game.Models;
using Connect4Game.Utils;

using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class LoadGameViewModel : Screen
    {
        private List<GameModel> _games;

        public List<GameModel> Games
        {
            get { return _games; }
            set { _games = value; }
        }


        public LoadGameViewModel()
        {
            Games = DataHelper.GetValue<List<GameModel>>("games");
        }

        public void ItemClicked(object obj)
        {
            var selectedItem = (GameModel)((Button)obj).DataContext;
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new GamePlayViewModel(selectedItem));
        }
    }
}
