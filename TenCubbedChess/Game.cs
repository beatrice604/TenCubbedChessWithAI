using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Windows.Media.Animation;

namespace TenCubbedChess
{
    //TEAM + ID = ELEMENT
    //TEAM: 1= BLUE; 2=YELLOW
    //ID
    //0-pawn
    //1-Rook
    //2-Knight
    //3-Bishop
    //4-King
    //5-Queen
    //6-Champion
    //7-Wizard
    //8-Marshall
    //9-Archbishop

    //depth= input
    //pion = 1 point
    //cal nebun 3 points
    //turn 4 to 5 points la inceput 5 si merge spre 4 -> 5 points
    //regina synergy = +1 + nebun + turn
    //sinergia intre 2 la fel +0.5, diferite = 1
    //king 255 (max)
    //champion - 7
    //wizard - 8
    class Game
    {
        int[,] _board;
        List<Piece> _whitePieces;
        List<Piece> _darkPieces;
        Dictionary<int, List<Piece>> _pieces = new Dictionary<int, List<Piece>> { { 1, new List<Piece>() }, { 2, new List<Piece>() } };
        Position selectedLocation;
        PieceFactory pieceFactory;
        bool check;
        bool checkMate;
        //TODO:
        //3.CheckMate
        //4.Castling
        //5.Promotion
        //6.Stalemate: If a player is not in check but has no legal moves, the game is a draw
        //adica PAT


        public int[,] board
        {
            get { return _board; }
            set { _board = value; }
        }
        public bool whiteTurn;

        public Game()
        {
            _board = new int[10, 10] { {0, 0, 16, 17, 19, 18, 17, 16,0,0},
                                       {0 , 11, 12,13,15, 14, 13, 12, 11,0},
                                       {10, 10,10,10, 10, 10, 10, 10, 10,10},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {20, 20,20,20, 20, 20, 20, 20, 20,20},
                                       {0 , 21, 22,23,25, 24, 23, 22, 21,0},
                                       {0,  0, 26, 27, 29, 28, 27, 26,0, 0}
            };
            whiteTurn = true;
            check = false;
            checkMate = false;
            selectedLocation = new Position(-1, -1);

            pieceFactory = new PieceFactory();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] != 0)
                        _pieces[board[i, j] / 10].Add(pieceFactory.createPiece(i, j, board[i, j]));

        }
        public Game(int[,] board, int player)
        {
            _board = board;
            whiteTurn = player==1;
            check = false;
            checkMate = false;
            selectedLocation = new Position(-1, -1);

            pieceFactory = new PieceFactory();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] != 0)
                        _pieces[board[i, j] / 10].Add(pieceFactory.createPiece(i, j, board[i, j]));
        }
        public void NextTurn()
        {
            whiteTurn = !whiteTurn;
        }

        public List<Position> ValidMoves(int row, int column)
        {
            Piece piece = GetPieceByLocation(row, column);
            selectedLocation.SetPosition(piece.position.row, piece.position.column);
            var legalMoves = piece.LegalMoves(board);
            var filteredLegalMoves = new List<Position>(legalMoves);
            Piece king;


            foreach (Position legalMove in legalMoves)
                {
                int[,] newBoard = (int[,])_board.Clone();
                ChangeBoard(newBoard, piece, legalMove);

                if (piece.Id % 10 == 4)
                    king = pieceFactory.createPiece(legalMove.row,legalMove.column,piece.Id);
                else
                {
                    var tempPiece = GetPieceById(piece.Id / 10 * 10 + 4);
                    king = pieceFactory.createPiece(tempPiece.position.row, tempPiece.position.column, tempPiece.Id);
                }

                if (IsCheck(piece.Id / 10, newBoard,king))
                        filteredLegalMoves.Remove(legalMove);
                }

                return filteredLegalMoves;
            

        }
        private void ChangeBoard(int[,] board, Piece piece, Position newPosition)
        {
            board[newPosition.row, newPosition.column] = piece.Id;
            board[piece.position.row, piece.position.column] = 0;
        }

        public void Move(int row, int column, int oldRow=-1, int oldCol=-1)
        {   if (oldCol == -1 && oldRow == -1)
            {
                if (selectedLocation.row == -1 && selectedLocation.column == -1)
                    throw new Exception("Missing piece to be moved");
            }
        else
            {
                selectedLocation.SetPosition(oldRow, oldCol);
            }
            Piece piece = GetPieceByLocation(selectedLocation.row, selectedLocation.column);
            _board[selectedLocation.row, selectedLocation.column] = 0;
            board[row, column] = piece.Id;
            piece.Move(row, column);
            var oppositePlayer = (piece.Id / 10) % 2 + 1;
            check = this.IsCheck(oppositePlayer, board,GetPieceById(oppositePlayer*10+4));

            if(check)
            {
                checkMate = IsCheckMate(oppositePlayer, board, GetPieceById(oppositePlayer * 10 + 4));
            }
            selectedLocation.SetPosition(-1, -1);
            NextTurn();
        }
        //mutarile inamicului  ataca regele player-ului
        public bool IsCheck(int player, int[,] board, Piece king)
        {
            List<Position> attackedSquares = new List<Position>();
            var pieces = GeneratePieces(board, player % 2 + 1);
            foreach (Piece piece in pieces)
            {
                piece.LegalMoves(board).ForEach(p => attackedSquares.Add(p));
            }

            if (attackedSquares.Exists(sq => sq.Equals(king.position)))
                return true;

            return false;
        }

        private Piece GetPieceByLocation(int row, int column)
        {
            Piece? piece = _pieces[board[row, column] / 10].Find(p =>
            {
                return p.position.row == row && p.position.column == column;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }

        private Piece GetPieceById(int Id)
        {
            Piece? piece = _pieces[Id / 10 ].Find(p =>
            {
                return p.Id == Id;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }
        public bool IsCheckMate(int player,int[,] board,Piece king)
        {
            int a;
            List<Position> attackedSquares = new List<Position>();
            foreach (Piece piece in _pieces[player])
            {
                var moves = piece.LegalMoves(board);
                foreach(Position move in moves)
                {
                    int[,] newBoard = new int[10,10];
                    Array.Copy(board, newBoard, board.Length);

                    ChangeBoard(newBoard, piece, move);

                    if (!IsCheck(player, newBoard,king))
                        return false;
                }
                
            }


            return true;
        }

        public bool IsGameOver()
        {
            return checkMate && check;

        }
        public bool IsGameOver(int player,int[,] board)
        {
            Piece king = GetPieceById(player * 10 + 4);
            return IsCheck(player, board, king) && IsCheckMate(player, board, king);
        }

        private List<Piece> GeneratePieces(int[,] board, int player)
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] / 10 == player)
                        pieces.Add(pieceFactory.createPiece(i, j, board[i, j]));

            return pieces;
        }

        public bool ValidPiecePicked(int row,int column)
        {
            Position pickedPiece = new Position(row, column);

            return !(GetPieceByLocation(row, column).GetPlayer() == 1 ^ whiteTurn);
        }

    }
}
