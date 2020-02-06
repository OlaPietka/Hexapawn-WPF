using System;
using System.Collections.Generic;
using HAI.Pages.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HAI.Models;
using Point = HAI.Models.Point;
using System.Threading;
using System.Threading.Tasks;
using HAI.Windows;
using LiveCharts;

namespace HAI.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : Page, INotifyPropertyChanged
    {
        private string Empty;
        private string Ai;
        private string Player;
        private string Selected;
        private string AiSelected;
        private string PlayerSelected;

        private bool _firstClick = false;

        private State _currentState = new State(new Board(), new List<Move>());
        private Point _clickedPoint;

        private AI ai = new AI();

        public GamePage()
        {
            InitializeComponent();

            InitSourceImages();
            DataContext = this;

            for (var i = StatsData.Values.Count - 1; i > 0; i--)
                StatsData.Values.RemoveAt(i);
        }

        private void MainPageButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            NavigationService.Navigate(new MainPage());
        }

        private void TutorialButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.PlayClickSound();

            var tutorialWindow = new TutorialWindow();
            tutorialWindow.Show();
        }

        private void Button_MouseEnter(object sender, RoutedEventArgs e)
            => SoundManager.PlayHoverSound();
        private void Cell_MouseEnter(object sender, MouseEventArgs e)
        {
            var cell = sender as Rectangle;

            var y = (int)cell.GetValue(Grid.RowProperty);
            var x = (int)cell.GetValue(Grid.ColumnProperty);

            var enterPoint = new Point(x, y);

            if(_currentState.Board.IsPlayer(enterPoint) || _currentState.PossibleMoves.Any(m => m.To == enterPoint))
                cell.Cursor = Cursors.Hand;
        }

        private void Cell_Click(object sender, MouseButtonEventArgs e)
        {
            var cell = sender as Rectangle;

            var y = (int) cell.GetValue(Grid.RowProperty);
            var x = (int) cell.GetValue(Grid.ColumnProperty);

            var clickedPoint = new Point(x, y);

            if (_currentState.Board.IsPlayer(clickedPoint))
            {
                _clickedPoint = new Point(x, y);

                _currentState.PossibleMoves = _currentState.Board.PossibleMoves(_clickedPoint, Field.Player);
                SeePossibleMoves();

                _firstClick = true;
            }
            else
            {
                var move = new Move(_clickedPoint, new Point(x, y));

                if (!_firstClick || !_currentState.PossibleMoves.Any(m => m == move)) return;

                _currentState.Board.MovePawn(move);
                SeeMove(move, Field.Player);
                CleanSelected();

                //Player end move
                if (_currentState.Board.IsWin(Field.Player))
                {
                    NewGame();
                    ai.Update();
                    PlayerScore++;
                    StatsData.PlayerVictories++;
                    return;
                }

                //AI Move
                move = ai.GetMove(_currentState);

                _currentState.Board.MovePawn(move);
                SeeMove(move, Field.AI);

                if (_currentState.Board.IsWin(Field.AI))
                {
                    NewGame();
                    AiScore++;
                    StatsData.AiVictories++;
                    return;
                }

                _firstClick = false;
            }
        }

        private async void NewGame()
        {
            CleanSelected();

            this.IsHitTestVisible = false;
            await Task.Delay(2000);
            this.IsHitTestVisible = true;

            _currentState = new State(new Board(), new List<Move>());
            CleanBoard();

            AverageWins = Math.Round(StatsData.GetAverageWins(), 2);
            AiKnowledge = StatsData.Values.Last();

            _firstClick = false;
        }

        private void SeeMove(Move move, Field field)
        {
            Sources[move.From.Y][move.From.X] = Empty;
            Sources[move.To.Y][move.To.X] = field is Field.Player ? Player : Ai;
        }

        private void SeePossibleMoves()
        {
            CleanSelected();

            foreach (var possibleMove in _currentState.PossibleMoves)
            {
                if (Sources[possibleMove.To.Y][possibleMove.To.X] == Ai)
                    Sources[possibleMove.To.Y][possibleMove.To.X] = AiSelected;
                else if (Sources[possibleMove.To.Y][possibleMove.To.X] == Empty)
                    Sources[possibleMove.To.Y][possibleMove.To.X] = Selected;
               
                Sources[possibleMove.From.Y][possibleMove.From.X] = PlayerSelected;
            }
        }

        private void CleanBoard()
        {
            for (var i = 0; i < 3; i++)
                Sources[0][i] = Ai;
            for (var i = 0; i < 3; i++)
                Sources[1][i] = Empty;
            for (var i = 0; i < 3; i++)
                Sources[2][i] = Player;
        }

        private void CleanSelected()
        {
            for (var i = 0; i < 3; i++)
                for (var j = 0; j < 3; j++)
                    if (Sources[i][j] == Selected)
                        Sources[i][j] = Empty;
                    else if (Sources[i][j] == PlayerSelected)
                        Sources[i][j] = Player;
                    else if (Sources[i][j] == AiSelected)
                        Sources[i][j] = Ai;
        }

        private void InitSourceImages()
        {
            (Empty, Ai, Player, Selected, AiSelected, PlayerSelected) = SourceHelper.Source;

            var row1 = new ObservableCollection<string>() { Ai, Ai, Ai };
            var row2 = new ObservableCollection<string>() { Empty, Empty, Empty };
            var row3 = new ObservableCollection<string>() { Player, Player, Player };

            Sources = new ObservableCollection<ObservableCollection<string>>() { row1, row2, row3 };
        }

        #region Biding properties
        public int PlayerScore
        {
            get { return playerScore; }
            set { playerScore = value; NotifyPropertyChanged("PlayerScore"); }
        }
        public int AiScore
        {
            get { return aiScore; }
            set { aiScore = value; NotifyPropertyChanged("AiScore"); }
        }
        public double AverageWins
        {
            get { return averageWins; }
            set { averageWins = value; NotifyPropertyChanged("AverageWins"); }
        }
        public double AiKnowledge
        {
            get { return aiKnowledge; }
            set { aiKnowledge = value; NotifyPropertyChanged("AiKnowledge"); }
        }

        private int playerScore;
        private int aiScore;
        private double averageWins;
        private double aiKnowledge;

        public ObservableCollection<ObservableCollection<string>> Sources
        {
            get { return sources; }
            set { sources = value; NotifyPropertyChanged("BoardSources"); }
        }

        private ObservableCollection<ObservableCollection<string>> sources;
        #endregion

        #region Properties Change Notification
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string property)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        #endregion
    }
}
