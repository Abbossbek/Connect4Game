using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseColorViewModel : Screen
    {
        private Connect4Player _player1;
        private Connect4Player _player2;
        private Connect4Player currentPlayer;
        public Brush Color { get; set; }
        public string ChooseColor
        {
            get => $"Choose color for {currentPlayer.Name}";
        }

        public ChooseColorViewModel(Connect4Player player1, Connect4Player player2)
        {
            currentPlayer = _player1 = player1;
            _player2 = player2;
        }

        public async void SelectColor(object obj)
        {
            var selectedButton = (Button)obj;
            if (currentPlayer == _player1)
            {
                _player1.Color = selectedButton.Foreground;
                currentPlayer = _player2;
                NotifyOfPropertyChange("ChooseColor");
            }
            else
            {
                _player2.Color = selectedButton.Foreground;

                var conductor = this.Parent as IConductor;
                await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
            }
        }
    }
}
