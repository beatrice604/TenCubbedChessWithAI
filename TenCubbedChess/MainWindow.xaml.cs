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

namespace TenCubbedChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Game game;
        private Dictionary<int,string> _pieces= new Dictionary<int, string>() {
                {0,"Pawn"},
                {1, "Rook"},
                {2, "Knight"},
                {3, "Bishop"},
                {4, "King"},
                {5, "Queen"},
                {6, "Champion"},
                {7, "Wizard"},
                {8, "Marshall"},
                {9, "Archbishop"}
                };
        private Dictionary<int, string> _colors = new Dictionary<int, string>() {
                {1, "Dark" },
                {2, "Light" }
            };
        public MainWindow(int gameType)
        {
            InitializeComponent();
            game= new Game();
            DisplayBoard(game.board);
        }

        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            DisplayBoard(game.board);
        }
        private void Grid_Click(object sender, MouseButtonEventArgs e, int row, int col)
        {
            // Call the DisplayMoves method with the row and column of the clicked grid
            DisplayMoves(row, col);
        }
        public void DisplayMoves(int row, int col)
        {
            List<Position> validMoves = game.ValidMoves(row, col);
        }
        public void DisplayBoard(int[,] board)
        {
            Grid[,] UIGrid = new Grid[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    int squareValue = game.board[row,col]; 

                    Grid grid = new Grid();
                    grid.SetValue(Grid.RowProperty, row);
                    grid.SetValue(Grid.ColumnProperty, col);

                    if ((row + col) % 2 == 0)
                    {
                        grid.Background = (Brush)FindResource("EvenTile");
                    }
                    else
                    {
                        grid.Background = (Brush)FindResource("OddTile");
                    }
                    if (squareValue != 0)
                    {
                        Path path = new Path();
                        path.Style = (Style)FindResource(_pieces[squareValue % 10] + _colors[squareValue / 10]);
                        grid.Children.Add(path);
                    }
                    UIGrid[row, col] = grid;
                    int currentRow = row;
                    int currentCol = col;
                    grid.MouseDown += (sender, e) => Grid_Click(sender, e, currentRow, currentCol);
                    MainGrid.Children.Add(grid);
                }
            }
        }
    }
}
