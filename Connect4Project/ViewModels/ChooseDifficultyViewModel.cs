using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{
    class ChooseDifficultyViewModel : Screen
    {
        private Connect4Player player;
        public ChooseDifficultyViewModel()
        {
            player = new Connect4Player() { Name = "You" };
        }

        public void Easy()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new EasyAIPlayer()));
        }
        public void Medium()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new MediumAIPlayer()));
        }
        public void Hard()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new ChooseColorViewModel(player, new HardAIPlayer()));
        }
    }
}
