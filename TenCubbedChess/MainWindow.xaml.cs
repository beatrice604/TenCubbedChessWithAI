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
        Board board;
        public MainWindow()
        {
            InitializeComponent();
            board= new Board();
            Grid[,] UIGrid = new Grid[10,10];
            Dictionary<int,string> pieces= new Dictionary<int,string>();
            pieces.Add(0, "Pawn");
            pieces.Add(1, "Rook");
            pieces.Add(2, "Knight");
            pieces.Add(3, "Bishop");
            pieces.Add(4, "King");
            pieces.Add(5, "Queen");
            pieces.Add(6, "Champion");
            pieces.Add(7, "Wizard");
            pieces.Add(8, "Marshall");
            pieces.Add(9, "Archbishop");
            Dictionary<int, string> colors = new Dictionary<int, string>();
            colors.Add(1, "Dark");
            colors.Add(2, "Light");
            for(int row=0;row<10;row++)
            {
                for(int col=0;col<10;col++)
                {
                    int squareValue = board.board[row,col]; 
                    Grid grid = new Grid();
                    grid.SetValue(Grid.RowProperty, row);
                    grid.SetValue(Grid.ColumnProperty, col);

                    if((row+col)%2==0)
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
                        path.Style = (Style)FindResource(pieces[squareValue % 10] + colors[squareValue / 10]);
                        grid.Children.Add(path);
                    }
                    UIGrid[row,col] = grid;
                    MainGrid.Children.Add(grid);
                }
            }
        }
    }
}
