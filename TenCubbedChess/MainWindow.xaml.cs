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
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Markup;
using System.Security.Cryptography;

namespace TenCubbedChess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Game game;
        Grid[,] UIGrid = new Grid[10, 10];
        TcpListener server;
        TcpClient client;
        NetworkStream stream;
        int oldRow, oldCol;
        bool turn = true;
        Thread thread;
        ChessAI chessAI;
        bool isAI=false;

        private Dictionary<int, string> _pieces = new Dictionary<int, string>() {
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
                {2, "Dark" },
                {1, "Light" }
            };

        public MainWindow(int gameType)
        {
            InitializeComponent();
            game = new Game();
            IPAddress localHost = IPAddress.Parse("127.0.0.1");
            Int32 port = 5000;
            switch (gameType)
            {
                case 0:
                    {
                        chessAI = new ChessAI(5);
                        isAI = true;
                    }
                    break;
                case 1:
                    {
                        createServer();
                        thread = new Thread(Listen);
                        thread.Start();
                    }
                    break;
                case 2:
                    {
                        createClient("127.0.0.1","Created Client");
                        thread = new Thread(Listen);
                        thread.Start();
                    }
                    break;
            }
            DisplayBoard(game.board);

        }
        public MainWindow()
        {
            InitializeComponent();
            game = new Game();
            DisplayBoard(game.board);

        }
        private void createServer()
        {
            server = null;

            try
            {
                Int32 port = 50516;
                IPAddress localHost = IPAddress.Parse("127.0.0.1");

                server = new TcpListener(localHost, port);

                server.Start();



                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    client = server.AcceptTcpClient();
                    stream = client.GetStream();
                    break;
                    //MessageBox.Show("Connected!");


                }

            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            //Console.WriteLine("\n Hit enter to continue...");
            //Console.Read();
        }
        private void createClient(String server, String message)
        {
            try
            {
                Int32 port = 50516;

                client = new TcpClient(server, port);
                stream = client.GetStream();

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);

            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException: {0}", se);
            }

            //Console.WriteLine("\n Press Enter to continue...");
            //Console.Read();
        }

        public MainWindow(int gameType, string depth="")
        {
            InitializeComponent();
            game = new Game();
            IPAddress localHost = IPAddress.Parse("127.0.0.1");
            Int32 port = 5000;
            switch (gameType)
            {
                case 0:
                    {
                        chessAI = new ChessAI(Convert.ToInt32(depth));
                        isAI = true;
                    }
                    break;
                case 1:
                    {
                        createServer();
                        thread = new Thread(Listen);
                        thread.Start();
                    }
                    break;
                case 2:
                    {
                        createClient("127.0.0.1","Created Client");
                        thread = new Thread(Listen);
                        thread.Start();
                    }
                    break;
            }
            DisplayBoard(game.board);

        }


        public void Listen()
        {
            Byte[] bytes = new Byte[256];
            String responseData = String.Empty;

            while (true)
            {
                while (true)
                {
                    Int32 data = stream.Read(bytes, 0, bytes.Length);
                    if (data != 0)
                    {
                        responseData = System.Text.Encoding.ASCII.GetString(bytes, 0, data);
                        break;
                    }

                }
                string[] parsedMessage = responseData.Split(";");
                string[] newPos = parsedMessage[1].Split(",");
                string[] oldPos = parsedMessage[0].Split(",");
                Dispatcher.Invoke(() => game.Move(Convert.ToInt32(newPos[0]), Convert.ToInt32(newPos[1]), Convert.ToInt32(oldPos[0]), Convert.ToInt32(oldPos[1])));
                Dispatcher.Invoke(() => DisplayBoard(game.board));
            }

        }

     

        public void SendData(int oldRow, int oldCol, int newRow, int newCol)
        {
            string message = String.Format("{0},{1};{2},{3}", oldRow, oldCol, newRow, newCol);


            Byte[] bytes = new Byte[256];

            stream = client.GetStream();

            string serverMessage = message;
            byte[] initialMessageBytes = Encoding.ASCII.GetBytes(serverMessage);
            stream.Write(initialMessageBytes, 0, initialMessageBytes.Length);
           
           
        }
        private void Grid_Click(object sender, MouseButtonEventArgs e, int row, int col)
        {
            #region old
            //bool movedPiece = false;
            //if (game.board[row, col] == 0 && UIGrid[row,col].Background!=Brushes.Green && anyValidMovesDisplayed()) return;

            //if (UIGrid[row,col].Background==Brushes.Green)
            //{
            //    game.Move(row, col);
            //    movedPiece = true;

            //} 
            //DisplayBoard(game.board);
            //if (movedPiece==false)
            //{
            //    DisplayMoves(row, col);
            //}
            #endregion
            
            if (UIGrid[row, col].Background == Brushes.Green)
            {
                game.Move(row, col,oldRow,oldCol);
                Dispatcher.Invoke(()=>DisplayBoard(game.board));
                if (game.IsGameOver())
                    this.Close();
                if(!isAI)
                { 
                    SendData(oldRow, oldCol, row, col);
                }
                else
                {   
                    (Piece piece,Position newPosition) aiMove = chessAI.MoveAI(game.board);
                    //change Game
                    game.Move(aiMove.newPosition.row,aiMove.newPosition.column, aiMove.piece.position.row, aiMove.piece.position.column);
                    //display moves
                    DisplayBoard(game.board);
                }

            }
            else
            {
                if (game.board[row, col] != 0)
                {if (game.ValidPiecePicked(row, col))
                    {
                        DisplayBoard(game.board);
                        DisplayMoves(row, col);
                        oldRow = row;
                        oldCol = col;
                    }
                }
            }
        }

        public void DisplayMoves(int row, int col)
        {
            List<Position> validMoves = game.ValidMoves(row, col);
            foreach (Position move in validMoves)
            {
                int moveRow = move.row;
                int moveCol = move.column;
                UIGrid[moveRow, moveCol].Background = Brushes.Green;
                //Border myBorder = new Border();
                //myBorder.BorderBrush = Brushes.Pink;
                //myBorder.BorderThickness = new Thickness(1);
                //Grid.SetRow(myBorder, moveRow);
                //Grid.SetColumn(myBorder, moveCol);
                //MainGrid.Children.Add(myBorder);
            }
        }
        public void DisplayBoard(int[,] board)
        {
            //Grid[,] UIGrid = new Grid[10, 10];

            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    int squareValue = game.board[row, col];

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
                    Border gridBorder = new Border();
                    gridBorder.BorderBrush = Brushes.Black;
                    gridBorder.BorderThickness = new Thickness(1);
                    grid.Children.Add(gridBorder);
                    UIGrid[row, col] = grid;
                    int currentRow = row;
                    int currentCol = col;
                    grid.MouseDown += (sender, e) => Grid_Click(sender, e, currentRow, currentCol);
                    //sendData(sender.row,sender.col,row, col);
                    MainGrid.Children.Add(grid);
                }
            }
        }
    }
}
