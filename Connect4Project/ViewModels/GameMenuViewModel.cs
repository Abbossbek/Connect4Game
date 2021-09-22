using Caliburn.Micro;

using Connect4Game.Models;

using System.Windows.Input;

namespace Connect4Game.ViewModels
{
    class GameMenuViewModel : Screen
    {

        private System.Action _closeWindow;

        public GameMenuViewModel(System.Action closeWindow)
        {
            _closeWindow = closeWindow;
        }

        public void SinglePlayer()
        {
            var conductor = this.Parent as IConductor;
            var gamePlayInput = new GamePlayInput("You", "Computer");
            var player = new HumanPlayer();
            conductor.ActivateItemAsync(new ChooseColorViewModel(player));

        }

        public void QuitCommand()
        {
            _closeWindow?.Invoke();
        }

        public void LeaderBoard()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new LeaderBoardViewModel());
        }

        public void Register()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new RegisterViewModel());
        }

    }
}
