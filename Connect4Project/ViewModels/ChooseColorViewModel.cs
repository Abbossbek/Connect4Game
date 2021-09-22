using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseColorViewModel : Screen
    {
        private Connect4Player _player;
        public Brush Color { get; set; }
        public string ChooseColor
        {
            get => $"Choose color for {_player.Name}";
        }

        public ChooseColorViewModel(Connect4Player player)
        {
            _player = player;
        }

        public void SelectColor(object obj)
        {
            var selectedButton = (Button)obj;
            _player.Color = selectedButton.Foreground;
        }
    }
}
