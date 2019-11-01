using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler GameOver;

        private int cellLeft;

        private int maxWidth, maxHight;

        private GameState[,] gameStates;

        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Reset();
        }

        private void Reset()
        {
            if (board == null || NumOfMines == null || width == null || hight == null) return;
            maxWidth = width.Value.Value;
            maxHight = hight.Value.Value;
            int numOfCells = maxWidth * maxHight;
            int countMines = Math.Min(NumOfMines.Value.Value, maxWidth * maxHight / 2);

            label.Value = countMines;
            cellLeft = numOfCells - countMines;

            gameStates = new GameState[maxWidth, maxHight];
            board.Children.Clear();
            for (int i = 0; i < maxWidth; i++)
            {
                StackPanel stackPanel = new StackPanel();
                board.Children.Add(stackPanel);
                for (int j = 0; j < maxHight; j++)
                {
                    gameStates[i, j] = new GameState(i, j);

                    Cell cell = new Cell(gameStates[i, j]);
                    stackPanel.Children.Add(cell);
                    GameOver += cell.GameOver;
                    FlagToggle.Checked += (object sender, RoutedEventArgs e) => { cell.IsFlag = true; };
                    FlagToggle.Unchecked += (object sender, RoutedEventArgs e) => { cell.IsFlag = false; };
                    cell.Open += (object sender, EventArgs e) => { Open(((Cell)sender).State); };
                    cell.SetFlag += (object sender, EventArgs e) => { SetFlag(((Cell)sender).State); };
                }
            }

            Random random = new Random();
            while (countMines > 0)
            {
                int i = random.Next(0, maxWidth);
                int j = random.Next(0, maxHight);
                if (!gameStates[i, j].IsMine)
                {
                    gameStates[i, j].IsMine = true;
                    countMines--;
                }
            }
        }

        private Cell GetCell(GameState cell)
        {
            return (Cell)((StackPanel)board.Children[cell.X]).Children[cell.Y];
        }

        private void SetFlag(GameState cell)
        {
            if (cell == null) return;
            if (cell.State == GameState.StateType.Unknow)
            {
                cell.State = GameState.StateType.Flag;
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    GetCell(cell).Type = Cell.CellType.Flag;
                    label.Value--;
                    if (label.Value == 0)
                    {
                        // Game over
                        FlagToggle.IsChecked = false;
                        GameOver?.Invoke(this, null);
                        MessageBox.Show("You Win", "Good Job!", MessageBoxButton.OK);
                    }
                });
            }
            else if (cell.State == GameState.StateType.Flag)
            {
                cell.State = GameState.StateType.Unknow;
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    GetCell(cell).Type = Cell.CellType.Unknow;
                });
                label.Value++;
            }
        }

        private HashSet<GameState> Open(GameState cell)
        {
            if (cell.IsMine)
            {
                // Game over
                this.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    FlagToggle.IsChecked = false;
                    GameOver?.Invoke(this, null);
                    if (MessageBox.Show("Restart", "Game Over", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Reset();
                    }
                });
                //     Reset();
                return new HashSet<GameState>();
            }

            HashSet<GameState> numberedCells = new HashSet<GameState>();
            switch (cell.State)
            {
                case GameState.StateType.Unknow:
                    cell.Value = Value(cell);
                    cell.State = GameState.StateType.Open;
                    this.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        GetCell(cell).Value = cell.Value;
                    });
                    cellLeft--;
                    if (cellLeft == 0)
                    {
                        this.Dispatcher.BeginInvoke((Action)delegate ()
                        {
                            FlagToggle.IsChecked = false;
                            GameOver?.Invoke(this, null);
                            MessageBox.Show("You Win", "Good Job!", MessageBoxButton.OK);
                        });
                        return numberedCells;
                    }
                    if (cell.Value > 0)
                    {
                        foreach (GameState i in Neighbor(cell, GameState.StateType.Open))
                            if (i.Value > 0)
                                numberedCells.Add(i);
                    }
                    else
                    {
                        // expand
                        for (int i = Math.Max(0, cell.X - 1); i < Math.Min(maxWidth, cell.X + 2); i++)
                            for (int j = Math.Max(0, cell.Y - 1); j < Math.Min(maxHight, cell.Y + 2); j++)
                                foreach (GameState c in Open(gameStates[i, j]))
                                    numberedCells.Add(c);
                    }
                    break;
                case GameState.StateType.Open:
                    if (cell.Value <= CountFlag(cell))
                    {
                        // Auto open
                        for (int i = Math.Max(0, cell.X - 1); i < Math.Min(maxWidth, cell.X + 2); i++)
                            for (int j = Math.Max(0, cell.Y - 1); j < Math.Min(maxHight, cell.Y + 2); j++)
                                if (gameStates[i, j].State == GameState.StateType.Unknow)
                                    foreach (GameState c in Open(gameStates[i, j]))
                                        numberedCells.Add(c);
                    }
                    break;
            }
            return numberedCells;
        }

        private int Value(GameState cell)
        {
            int c = 0;
            for (int i = Math.Max(0, cell.X - 1); i < Math.Min(maxWidth, cell.X + 2); i++)
                for (int j = Math.Max(0, cell.Y - 1); j < Math.Min(maxHight, cell.Y + 2); j++)
                    c += gameStates[i, j].IsMine ? 1 : 0;
            return c;
        }

        private int CountFlag(GameState cell)
        {
            int c = 0;
            for (int i = Math.Max(0, cell.X - 1); i < Math.Min(maxWidth, cell.X + 2); i++)
                for (int j = Math.Max(0, cell.Y - 1); j < Math.Min(maxHight, cell.Y + 2); j++)
                    c += gameStates[i, j].State == GameState.StateType.Flag ? 1 : 0;
            return c;
        }

        #region auto

        private void Auto(object sender, RoutedEventArgs args)
        {
            ((Button)sender).IsEnabled = false;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += (object s, RunWorkerCompletedEventArgs e) => { ((Button)sender).IsEnabled = true; };
            worker.RunWorkerAsync();
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Queue<GameState> queue = new Queue<GameState>();
            for (int i = 0; i < maxWidth; i++)
                for (int j = 0; j < maxHight; j++)
                    if (gameStates[i, j].State == GameState.StateType.Open && gameStates[i, j].Value > 0
                        && Neighbor(gameStates[i, j], GameState.StateType.Unknow).Count > 0)
                        queue.Enqueue(gameStates[i, j]);

            while (queue.Count > 0)
            {
                GameState cell = queue.Dequeue();
                List<GameState> unknownNeighbor = Neighbor(cell, GameState.StateType.Unknow);

                if (unknownNeighbor.Count == cell.Value - CountFlag(cell))
                    foreach (GameState c in unknownNeighbor)
                    {
                        SetFlag(c);
                        foreach (GameState i in Neighbor(c, GameState.StateType.Open))
                            if (i.Value > 0 && !queue.Contains(i))
                                queue.Enqueue(i);
                        System.Threading.Thread.Sleep(100);
                    }

                if (CountFlag(cell) >= cell.Value)
                {
                    foreach (GameState i in Open(cell))
                        if (!queue.Contains(i))
                            queue.Enqueue(i);
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private List<GameState> Neighbor(GameState cell, GameState.StateType type)
        {
            List<GameState> l = new List<GameState>();
            for (int i = Math.Max(0, cell.X - 1); i < Math.Min(maxWidth, cell.X + 2); i++)
                for (int j = Math.Max(0, cell.Y - 1); j < Math.Min(maxHight, cell.Y + 2); j++)
                {
                    if (gameStates[i, j].State == type)
                        l.Add(gameStates[i, j]);
                }
            return l;
        }

        #endregion
    }
}
