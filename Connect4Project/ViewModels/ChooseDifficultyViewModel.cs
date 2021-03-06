using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseDifficultyViewModel : Screen
    {
        private Player player;
        public ChooseDifficultyViewModel()
        {
            player = new HumanPlayer("You");
        }

        public void Easy()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Easy)));
        }
        public void Medium()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Normal)));
        }
        public void Hard()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new AIPlayer("Computer", DifficultyLevel.Hard)));
        }
    }
}
