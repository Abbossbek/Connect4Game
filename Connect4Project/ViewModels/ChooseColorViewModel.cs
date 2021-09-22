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
        private Connect4Player _player1;
        private Connect4Player _player2;
        private Connect4Player currentPlayer;
        public Brush Color { get; set; }
        public string ChooseColor
        {
            get => $"Choose color for {(currentPlayer.Name is "You"?(currentPlayer.Name+"rself"):currentPlayer.Name)}";
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
                if(_player2.Name is null)
                {
                    _player2.Name = "Computer"; 
                    do
                    {
                        Random r = new Random();
                        switch (r.Next(0,6))
                        {
                            case 0: _player2.Color = Brushes.Red; break;
                            case 1: _player2.Color = Brushes.Green; break;
                            case 2: _player2.Color = Brushes.Blue; break;
                            case 3: _player2.Color = Brushes.Pink; break;
                            case 4: _player2.Color = Brushes.Yellow; break;
                            case 5: _player2.Color = Brushes.Gray; break;
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
                _player2.Color = selectedButton.Foreground;

                var conductor = this.Parent as IConductor;
                await conductor.ActivateItemAsync(new GamePlayViewModel(_player1, _player2));
            }
        }
    }
}
