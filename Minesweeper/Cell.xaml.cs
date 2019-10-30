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
    /// Interaction logic for Cell.xaml
    /// </summary>
    public partial class Cell : UserControl
    {
        public enum CellType { Unknow, Flag, Mine, Open, GameOver }

        public event EventHandler Open;

        public event EventHandler SetFlag;

        public Cell(int x, int y)
        {
            InitializeComponent();
            X = x;
            Y = y;
            IsMine = false;
        }

        public int X { get; set; }

        public int Y { get; set; }

        private bool isFlag;
        public bool IsFlag
        {
            get
            {
                return isFlag;
            }
            set
            {
                isFlag = value;
                DotLabel.Visibility = value && Type == CellType.Unknow
                    ? Visibility.Visible
                    : Visibility.Hidden;
            }
        }

        private int? value;
        public int? Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                Type = CellType.Open;
            }
        }

        public bool IsMine { get; set; }

        private CellType type = CellType.Unknow;
        public CellType Type
        {
            get { return type; }
            set
            {
                type = value;
                switch (value)
                {
                    case CellType.Unknow:
                        DotLabel.Visibility = IsFlag ? Visibility.Visible : Visibility.Hidden;
                        Flag.Visibility = Visibility.Hidden;
                        break;
                    case CellType.Flag:
                        DotLabel.Visibility = Visibility.Hidden;
                        Flag.Visibility = Visibility.Visible;
                        break;
                    case CellType.Mine:
                        DotLabel.Visibility = Visibility.Hidden;
                        if (Flag.Visibility != Visibility.Visible)
                            Mine.Visibility = Visibility.Visible;
                        break;
                    case CellType.Open:
                        DotLabel.Visibility = Visibility.Hidden;
                        Flag.Visibility = Visibility.Hidden;
                        ValueLabel.Content = Value == 0 ? "" : Value.ToString();
                        cell.Background = Brushes.LightGray;
                        break;
                }
            }
        }

        internal void GameOver(object sender, EventArgs e)
        {
            Type = CellType.GameOver;
            if (Flag.Visibility == Visibility.Visible && !IsMine)
            {
                ValueLabel.Content = "X";
            }
            if (Flag.Visibility == Visibility.Hidden && IsMine)
            {
                Type = CellType.Mine;
            }
        }

        private void Click(object sender, MouseButtonEventArgs e)
        {
            switch (Type)
            {
                case CellType.Unknow:
                    (IsFlag == (e.ChangedButton == MouseButton.Left) ? SetFlag : Open)?.Invoke(this, e);
                    break;
                case CellType.Flag:
                    SetFlag?.Invoke(this, e);
                    break;
                case CellType.Open:
                    Open?.Invoke(this, e);
                    break;
            }
        }
    }
}
