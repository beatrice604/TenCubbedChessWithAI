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
    class Game
    {
        int[,] _board;
        List<Piece> _whitePieces;
        List<Piece> _darkPieces;
        Dictionary<int, List<Piece>> _pieces = new Dictionary<int, List<Piece>> { { 1, new List<Piece>() }, { 2, new List<Piece>() } };
        Position selectedLocation;
        bool check;
        bool checkMate;
        //TODO:
        //1.Id is not unique
        //2.Check
        //2.1 Verific sah
        //2.2 filtreaza dupa sah mutarile
        //3.CheckMate
        //4.Castling
        //5.Promotion
        //6.Stalemate: If a player is not in check but has no legal moves, the game is a draw


        public int[,] board
         { get { return _board; }
            set { _board = value; } 
          }
        public Game()
        {
            _board = new int[10, 10] { {0, 0, 16, 17, 19, 18, 17, 16,0,0},
                                       {0 , 18, 12,13,15, 14, 13, 12, 18,0},
                                       {10, 10,10,10, 10, 10, 10, 10, 10,10},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {20, 20,20,20, 20, 20, 20, 20, 20,20},
                                       {0 , 28, 22,23,25, 24, 23, 22, 28,0},
                                       {0,  0, 26, 27, 29, 28, 27, 26,0, 0}
            };
            check = false;
            checkMate = false;
            selectedLocation= new Position(-1,-1);
            PieceFactory pieceFactory = new PieceFactory();

            for(int i = 0;i<10;i++)
                for(int j=0;j<10;j++)
                    if(board[i,j]!=0)
                        _pieces[board[i, j] / 10].Add(pieceFactory.createPiece(i,j,board[i,j]));          

                            
        }

        //onClick
        public List<Position> ValidMoves(int row, int column)
        {
            Piece piece = GetPieceByLocation(row,column);
            selectedLocation = piece.position;
            return piece.LegalMoves(board);
          
;        }

        public void Move(int row, int column )
        {
            if (selectedLocation.row == -1 && selectedLocation.column == -1)
                throw new Exception("Missing piece to be moved");

            Piece piece= GetPieceByLocation(selectedLocation.row,selectedLocation.column);
            piece.Move(row, column);
              check= this.setCheck(piece.Id / 10);
            selectedLocation.SetPosition(-1, -1);
        }

        public bool setCheck(int player)
        {//verific daca regele e in sah
            HashSet<Position> attackedSquares = new HashSet<Position>();
            foreach(Piece piece in _pieces[player%2+1])
            {
                piece.LegalMoves(board).ForEach(p => attackedSquares.Add(p));
            }
            var king = GetPieceById(player * 10 + 4);
            foreach (Position position in attackedSquares)
                if (king.position == position)
                    return true;
            return false;
        }
/*        public bool isCheckMate(int player)
        {//verific daca regele e in sah mat
            HashSet<Position> attackedSquares = new HashSet<Position>();
            foreach (Piece piece in _pieces[(player + 1) % 2])
            {
                piece.LegalMoves(board).ForEach(p => attackedSquares.Add(p));
            }
            var king = GetPieceById(player * 10 + 4);
            foreach (Position position in attackedSquares)
                if (king.position == position)
                    return true;
            return false;
        }*/


        private Piece GetPieceByLocation(int row,int column)
        {
            Piece? piece = _pieces[board[row,column] / 10].Find(p =>
            {
                return p.position.row == row && p.position.column == column;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }

        private Piece GetPieceById(int Id)
        {
            Piece? piece = _pieces[Id / 10].Find(p =>
            {
                return p.Id == Id;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }

    }
}
