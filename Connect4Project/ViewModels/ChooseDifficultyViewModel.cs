using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseDifficultyViewModel : Screen
    {
        public ChooseDifficultyViewModel()
        {
        }

        public void Easy()
        {
            var conductor = this.Parent as IConductor;
            var player = new HumanPlayer() { Name = "You" };
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new EasyAIPlayer()));
        }
        public void Medium()
        {
            var conductor = this.Parent as IConductor;
            var player = new HumanPlayer() { Name = "You" };
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new MediumAIPlayer()));
        }
        public void Hard()
        {
            var conductor = this.Parent as IConductor;
            var player = new HumanPlayer() { Name = "You" };
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new HardAIPlayer()));
        }
    }
}
