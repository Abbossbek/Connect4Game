using Caliburn.Micro;
using Connect4Game.Models;
using System;
using System.Windows;

namespace Connect4Game.ViewModels
{
    public class UserDetailsViewModel : Screen
    {
        private string _player1 { get; set; }
        public string Player1
        {
            get => _player1;
            set
            {
                _player1 = value;
            }
        }
        private string _player2 { get; set; }
        public string Player2
        {
            get => _player2;
            set
            {
                _player2 = value;
            }
        }
        public UserDetailsViewModel()
        {
        }
        public bool CanPlayGameCommand(string player1, string player2)
        {
            if (string.IsNullOrEmpty(player1) || string.IsNullOrEmpty(player2) || player1 == player2)
                return false;

            return true;
        }
        public void PlayGameCommand(string player1, string player2)
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync( new ChooseColorViewModel(new HumanPlayer(player1), new HumanPlayer(player2)));
        }
    }
}
