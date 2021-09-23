using Caliburn.Micro;

using Connect4Game.Models;
using Connect4Game.Utils;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class LoadGameViewModel : Screen
    {
        private ObservableCollection<GameModel> _games = new();

        public ObservableCollection<GameModel> Games
        {
            get { return _games; }
            set { _games = value; }
        }


        public LoadGameViewModel()
        {
            var savedGames = DataHelper.GetValue<List<GameModel>>("games")??new();
            foreach (var item in savedGames)
            {
                Games.Add(item);
            }
        }

        public void ItemClicked(object obj)
        {
            var selectedItem = (GameModel)((Button)obj).DataContext;
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new GamePlayViewModel(selectedItem));
        }
        public void Clear()
        {
            DataHelper.Clear();
            Games.Clear();
        }
    }
}
