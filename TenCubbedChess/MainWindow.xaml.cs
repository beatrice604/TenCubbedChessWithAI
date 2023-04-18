﻿using System;
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
            board= new Board();
            DisplayBoard(board.board);
        }

        public MainWindow()
        {
            InitializeComponent();
            board = new Board();
            DisplayBoard(board.board);
        }

        public void DisplayBoard(int[,] board)
        {
            Grid[,] UIGrid = new Grid[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    int squareValue = board[row, col];
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
                    MainGrid.Children.Add(grid);
                }
            }
        }
    }
}
