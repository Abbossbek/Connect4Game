using Caliburn.Micro;

using Connect4Game.Models;

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Connect4Game.ViewModels
{

    #region CustomStruct
    public struct Field
    {
        public int rol;
        public int col;

        public Field(int r, int c)
        {
            rol = r;
            col = c;
        }
    }
    #endregion

    public class GamePlayViewModel : Screen
    {

        private Player _player1;
        private Player _player2;
        private StackPanel ColumnStacks;
        private Stack<GameStep> _steps;

        private const int MaxRow = 6;
        private const int MaxColumn = 7;
        private int[,] _gameMap = new int[MaxRow, MaxColumn] {
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 },
                                                     {0, 0, 0, 0,0,0,0 }
                                                   };

        private bool _switchPlayers { get; set; }
        private bool _canPlay { get; set; } = true;
        public bool CanUndo => _steps.Count > 0 && _canPlay;
        public Brush Player1Color
        {
            get => (Brush)new BrushConverter().ConvertFromString(_player1?.Color);
        }
        public Brush Player2Color
        {
            get => (Brush)new BrushConverter().ConvertFromString(_player2?.Color);
        }
        public string Player1Name
        {
            get => _player1?.Name;
        }

        public string Player2Name
        {
            get => _player2.Name;
        }

        private string _gameState { get; set; }
        public string GameState
        {
            get => _gameState;
            set
            {
                _gameState = value;
                NotifyOfPropertyChange();
            }
        }

        private Dictionary<string, int> _gameMapIndexDictionary;

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            var frameworkElement = view as FrameworkElement;

            if (frameworkElement == null)
            {
                return;
            }

            var control = frameworkElement.FindName("ColumnStacks") as StackPanel;

            if (control == null)
            {
                return;
            }
            else
            {
                ColumnStacks = control;
                for (int i = 0; i < ColumnStacks.Children.Count; i++)
                {
                    var buttons = ((StackPanel)ColumnStacks.Children[i]).Children;
                    for (int j = 0; j < buttons.Count; j++)
                    {
                        ((Button)buttons[j]).Foreground = _gameMap[j, i] switch
                        {
                            1 => Player1Color,
                            2 => Player2Color,
                            _ => ((Button)buttons[j]).Foreground,
                        };
                    }
                }
            }
        }
        public GamePlayViewModel(Player player1, Player player2)
        {
            _player1 = player1;
            _player2 = player2;

            _switchPlayers = true;
            _gameMapIndexDictionary = new Dictionary<string, int>();

            _gameMapIndexDictionary.Add("col1", 0);
            _gameMapIndexDictionary.Add("col2", 1);
            _gameMapIndexDictionary.Add("col3", 2);
            _gameMapIndexDictionary.Add("col4", 3);
            _gameMapIndexDictionary.Add("col5", 4);
            _gameMapIndexDictionary.Add("col6", 5);
            _gameMapIndexDictionary.Add("col7", 6);

            GameState = Player1Name + (Player1Name is "You" ? "r Turn" : "'s Turn");
            _canPlay = true;
            _steps = new();
        }
        public GamePlayViewModel(GameModel gameModel) 
            : this(
                  new HumanPlayer(gameModel.Player1) { Color = gameModel.Player1Color },
                  gameModel.Depth > 0 ? new AIPlayer(gameModel.Player2, (DifficultyLevel)gameModel.Depth) { Color = gameModel.Player2Color }
                  : new HumanPlayer(gameModel.Player1) { Color = gameModel.Player1Color })
        {
            for (int i = 0; i < gameModel.GameMap.Count; i++)
            {
                _gameMap[i / 7, i % 7] = gameModel.GameMap[i]; // 
            }
        }

        public void RunOperation(object obj)
        {
            if (_canPlay == false)
                return;
            var colIndex = -1;
            StackPanel stackContainer;
            var selectedButton = (Button)obj;
        Play:
            if (colIndex == -1)
            {
                stackContainer = (StackPanel)selectedButton.Parent;
                colIndex = _gameMapIndexDictionary[stackContainer.Name];
            }
            else
            {
                stackContainer = (StackPanel)ColumnStacks.Children[colIndex];
            }
            //var rowIndex = 0;
            //if (_gameMap[rowIndex, colIndex] == 2 || _gameMap[rowIndex, colIndex] == 5)
            //    return;

            for (int i = 0; i < MaxRow; i++)     // check for unfilled in seleceted position
            {
                if (_gameMap[MaxRow - i - 1, colIndex] == 0)
                {
                    var calButton = (Button)stackContainer.Children[MaxRow - i - 1];

                    if (_switchPlayers)
                    {
                        calButton.Foreground = (Brush)new BrushConverter().ConvertFromString(_player1.Color);
                        _gameMap[MaxRow - i - 1, colIndex] = 1;
                        var result = checkWin(1, stackContainer);
                        if (result)
                        {
                            _canPlay = false;
                            GameState = "Game Over";
                            WindowManager windowManager = new WindowManager();
                            windowManager.ShowDialogAsync(new CustomAlertViewModel($"{Player1Name} Won!"));
                        }
                    }
                    else
                    {
                        calButton.Foreground = (Brush)new BrushConverter().ConvertFromString(_player2.Color);
                        _gameMap[MaxRow - i - 1, colIndex] = 2;
                        var result = checkWin(2, stackContainer);
                        if (result)
                        {
                            _canPlay = false;
                            GameState = "Game Over";
                            WindowManager windowManager = new WindowManager();
                            windowManager.ShowDialogAsync(new CustomAlertViewModel($"{Player2Name} Won!"));
                        }
                    }

                    _steps.Push(new() { Player = _switchPlayers ? _player1 : _player2, X = i, Y = colIndex });
                    NotifyOfPropertyChange(nameof(CanUndo));
                    break;
                }
            }

            _switchPlayers = !_switchPlayers;

            if (_switchPlayers && _canPlay)
                GameState = Player1Name + (Player1Name is "You" ? "r Turn" : "'s Turn");

            else if (_switchPlayers == false && _canPlay)
            {
                if (_player2.GetType() == typeof(AIPlayer))
                {
                    colIndex = ((AIPlayer)_player2).Move(_gameMap);
                    goto Play;
                }
                GameState = $"{Player2Name}'s Turn";
            }

        }

        public void SaveGame()
        {
            var conductor = this.Parent as IConductor;
            conductor.ActivateItemAsync(new SaveGameViewModel(_player1, _player2, _gameMap));
        }

        public void Undo()
        {
            var lastStep = _steps.Pop();
            var buttonsStack = ColumnStacks.Children[lastStep.Y];
            ((Button)((StackPanel)buttonsStack).Children[MaxRow - lastStep.X - 1]).Foreground = Brushes.White;
            _gameMap[MaxRow - lastStep.X - 1, lastStep.Y] = 0;
            if (lastStep.Player.Name is "Computer")
            {
                Undo();
            }
            else
            {
                _switchPlayers = lastStep.Player.Name == _player1.Name;
                GameState = (_switchPlayers ? Player1Name : Player2Name) + (_switchPlayers && Player1Name is "You" ? "r Turn" : "'s Turn");
            }
            NotifyOfPropertyChange(nameof(CanUndo));
        }

        private bool checkWin(int player, StackPanel container)
        {
            int count = 0;
            List<Field> points = new List<Field>();
            bool foundPattern = false;

            #region HorizontalCheck

            for (int i = 0; i < MaxRow; ++i)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int j = 0; j < MaxColumn; ++j)
                {
                    if (_gameMap[MaxRow - i - 1, j] == player)
                    {
                        ++count;
                        points.Add(new Field(MaxRow - i - 1, j));
                        if (count == 4) break;
                    }

                    else if (_gameMap[MaxRow - i - 1, j] != player)
                    {
                        count = 0;
                        points.Clear();
                    }
                }//

                if (count == 4)
                {
                    var containerParent = (StackPanel)container.Parent;
                    foundPattern = true;

                    for (int p = 0; p < points.Count; ++p)
                    {
                        var col = points[p].col;
                        var innerContainer = (StackPanel)containerParent.Children[col];
                        var btn = (Button)innerContainer.Children[points[p].rol];
                        btn.Content = "X";
                    }
                }
            }//
            #endregion

            if (foundPattern)
                return foundPattern;


            #region VerticalCheck
            count = 0;
            points.Clear();

            for (int i = 0; i < MaxColumn; ++i)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int j = 0; j < MaxRow; ++j)
                {
                    if (_gameMap[j, i] == player)
                    {
                        ++count;
                        points.Add(new Field(j, i));
                        if (count == 4) break;
                    }

                    else if (_gameMap[j, i] != player)
                    {
                        count = 0;
                        points.Clear();
                    }
                }//

                if (count == 4)
                {
                    var containerParent = (StackPanel)container.Parent;
                    foundPattern = true;

                    for (int p = 0; p < points.Count; ++p)
                    {
                        var col = points[p].col;
                        var innerContainer = (StackPanel)containerParent.Children[col];
                        var btn = (Button)innerContainer.Children[points[p].rol];
                        btn.Content = "X";
                    }
                }
            }//
            #endregion

            if (foundPattern)
                return foundPattern;

            #region DiagonalWin_1

            count = 0;
            points.Clear();

            for (int col = 0; col < MaxColumn; ++col)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int rol = 3; rol < MaxRow; ++rol)
                {
                    count = 0;
                    points.Clear();

                    if (foundPattern)
                        break;

                    int i = rol;

                    for (int j = col; j < MaxColumn; ++j, --i)
                    {
                        if (i == -1)
                            break;
                        if (_gameMap[MaxRow - i - 1, j] == player)
                        {
                            points.Add(new Field(MaxRow - i - 1, j));
                            ++count;
                            if (count == 4)
                                break;
                        }

                        else if (_gameMap[MaxRow - i - 1, j] != player)
                        {
                            count = 0;
                            points.Clear();
                        }
                    }

                    if (count == 4)
                    {
                        var containerParent = (StackPanel)container.Parent;
                        foundPattern = true;

                        for (int p = 0; p < points.Count; ++p)
                        {
                            var co = points[p].col;
                            var innerContainer = (StackPanel)containerParent.Children[co];
                            var btn = (Button)innerContainer.Children[points[p].rol];
                            btn.Content = "X";
                        }
                    }//

                }//
            }//

            #endregion

            if (foundPattern)
                return foundPattern;


            #region DiagonalWin_2

            count = 0;
            points.Clear();

            for (int col = 0; col < MaxColumn; ++col)
            {
                count = 0;
                points.Clear();

                if (foundPattern)
                    break;

                for (int rol = 3; rol < MaxRow; ++rol)
                {
                    count = 0;
                    points.Clear();

                    if (foundPattern)
                        break;

                    int i = rol;

                    for (int j = col; j >= 0; --j, --i)
                    {
                        if (i == -1)
                            break;
                        if (_gameMap[MaxRow - i - 1, j] == player)
                        {
                            points.Add(new Field(MaxRow - i - 1, j));
                            ++count;
                            if (count == 4)
                                break;
                        }

                        else if (_gameMap[MaxRow - i - 1, j] != player)
                        {
                            count = 0;
                            points.Clear();
                        }
                    }

                    if (count == 4)
                    {
                        var containerParent = (StackPanel)container.Parent;
                        foundPattern = true;

                        for (int p = 0; p < points.Count; ++p)
                        {
                            var co = points[p].col;
                            var innerContainer = (StackPanel)containerParent.Children[co];
                            var btn = (Button)innerContainer.Children[points[p].rol];
                            btn.Content = "X";
                        }
                    }//
                }//
            }//

            #endregion

            if (foundPattern)
                return foundPattern;


            return false;
        }//
    }
}
