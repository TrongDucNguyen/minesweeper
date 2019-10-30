using System;
using System.Collections.Generic;
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
            int x = width.Value.Value;
            int y = hight.Value.Value;
            int numOfCells = x * y;
            board.Children.Clear();
            for (int i = 0; i < width.Value.Value; i++)
            {
                StackPanel stackPanel = new StackPanel();
                board.Children.Add(stackPanel);
                for (int j = 0; j < hight.Value.Value; j++)
                {
                    Cell cell = new Cell(i, j);
                    stackPanel.Children.Add(cell);
                    this.GameOver += cell.GameOver;
                    FlagToggle.Checked += (object sender, RoutedEventArgs e) => { cell.IsFlag = true; };
                    FlagToggle.Unchecked += (object sender, RoutedEventArgs e) => { cell.IsFlag = false; };
                    cell.Open += (object sender, EventArgs e) => { Open((Cell)sender); };
                }
            }

            int count = NumOfMines.Value.Value;
            Random random = new Random();
            while (count > 0)
            {
                int i = random.Next(1, x);
                int j = random.Next(1, y);
                Cell cell = GetCell(i, j);
                if (!cell.IsMine)
                {
                    cell.IsMine = true;
                    count--;
                }
            }
        }

        private Cell GetCell(int x, int y)
        {
            if (x < 0 || x >= width.Value.Value || y < 0 || y >= hight.Value.Value) return null;
            return (Cell)((StackPanel)board.Children[x]).Children[y];
        }

        private void Open(int x, int y)
        {
            Cell cell = GetCell(x, y);
            if (cell == null || cell.Type != Cell.CellType.Unknow) return;
            Open(cell);
        }

        private void Open(Cell cell)
        {
            if (cell.IsMine)
            {
                // Game over
                FlagToggle.IsChecked = false;
                GameOver?.Invoke(this, null);
                MessageBox.Show("Restart", "Game Over", MessageBoxButton.OK);
                Reset();
                return;
            }

            switch (cell.Type)
            {
                case Cell.CellType.Unknow:
                    cell.Value = Value(cell);
                    if (cell.Value == 0)
                    {
                        // expand
                        for (int i = cell.X - 1; i <= cell.X + 1; i++)
                            for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                                Open(i, j);
                    }
                    break;
                case Cell.CellType.Open:
                    if (cell.Value <= CountFlag(cell))
                    {
                        // Auto open
                        for (int i = cell.X - 1; i <= cell.X + 1; i++)
                            for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                                if (GetCell(i, j) != null && GetCell(i, j).Type == Cell.CellType.Unknow)
                                    Open(i, j);
                    }
                    break;
            }
        }

        private int Value(Cell cell)
        {
            int c = 0;
            for (int i = cell.X - 1; i <= cell.X + 1; i++)
                for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                    c += GetCell(i, j) != null && GetCell(i, j).IsMine ? 1 : 0;
            return c;
        }

        private int CountFlag(Cell cell)
        {
            int c = 0;
            for (int i = cell.X - 1; i <= cell.X + 1; i++)
                for (int j = cell.Y - 1; j <= cell.Y + 1; j++)
                    c += GetCell(i, j) != null && GetCell(i, j).Type == Cell.CellType.Flag ? 1 : 0;
            return c;
        }
    }
}
