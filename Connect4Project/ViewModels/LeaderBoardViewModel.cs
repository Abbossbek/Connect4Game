using Caliburn.Micro;
using Connect4Game.Models;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Connect4Game.ViewModels
{
    class LeaderBoardViewModel : Screen
    {

        private ObservableCollection<PlayerStats> _playerStats;
        public ObservableCollection<PlayerStats> PlayerStats
        {
            get => _playerStats;
            set
            {
                _playerStats = value;
                NotifyOfPropertyChange();
            }
        }


        public LeaderBoardViewModel()
        {
        }

        public void DeleteItem(object obj)
        {
            var deletebtn = obj as Button;
            var parentContainer = (StackPanel)deletebtn.Parent;
            var BorderContainer = (Border)parentContainer.Children[0];
            var StackContainer = (StackPanel)BorderContainer.Child;
            var ListItems = (ListView)StackContainer.Children[2];
            var selected = (PlayerStats)ListItems.SelectedValue;

            if (selected != null)
            {
            }

            else
            {
                WindowManager windowManager = new WindowManager();
                windowManager.ShowDialogAsync(new CustomAlertViewModel("No User Selected!"));
            }

        }
    }
}
