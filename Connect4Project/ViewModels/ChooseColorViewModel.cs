using Caliburn.Micro;

using Connect4Game.Models;

using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseColorViewModel : Screen
    {
        private Player _player1;
        private Player _player2;
        private Player currentPlayer;
        public Brush Color { get; set; }
        public string ChooseColor
        {
            get => $"Choose color for {(currentPlayer.Name is "You"?(currentPlayer.Name+"rself"):currentPlayer.Name)}";
        }

        public ChooseColorViewModel(Player player1, Player player2)
        {
            currentPlayer = _player1 = player1;
            _player2 = player2;
        }

        public async void SelectColor(object obj)
        {
            var selectedButton = (Button)obj;
            selectedButton.Visibility = System.Windows.Visibility.Collapsed;
            if (currentPlayer == _player1)
            {
                _player1.Color = selectedButton.Foreground.ToString();
                if(_player2.Name is "Computer")
                {
                    do
                    {
                        Random r = new Random();
                        switch (r.Next(0,6))
                        {
                            case 0: _player2.Color = Brushes.Red.ToString(); break;
                            case 1: _player2.Color = Brushes.Green.ToString(); break;
                            case 2: _player2.Color = Brushes.Blue.ToString(); break;
                            case 3: _player2.Color = Brushes.Pink.ToString(); break;
                            case 4: _player2.Color = Brushes.Yellow.ToString(); break;
                            case 5: _player2.Color = Brushes.Gray.ToString(); break;
                        }
                    } while (_player2.Color == _player1.Color);// Will choose a random colour for the ai making sure it doesn't clash with the users.
                    var conductor = this.Parent as IConductor;
                    await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
                }
                currentPlayer = _player2;
                NotifyOfPropertyChange("ChooseColor");
            }
            else
            {
                _player2.Color = selectedButton.Foreground.ToString();

                var conductor = this.Parent as IConductor;
                await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
            }
        }
    }
}
