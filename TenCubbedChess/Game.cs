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
    //    TenCubed Chess uses ten different piece-types on a 10x10 game.The Queen combines the moves of rook and bishop. The Marshall combines the moves of rook and knight.The Archbishop combines the moves of bishop and knight.The Champion combines the moves of dabbabah and alfil and wazir.The Wizard combines the moves of camel and ferz.
    class Game
    {
        int[,] _board;
        List<Piece> _whitePieces;
        List<Piece> _darkPieces;
        Dictionary<int, List<Piece>> _pieces = new Dictionary<int, List<Piece>> { { 1, new List<Piece>() }, { 2, new List<Piece>() } };
        Position selectedLocation;
        //TODO:
        //1.Id is not unique
        //2.Check
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

        public void Move(int row, int column)
        {

            if (board[row, column] != 0)
            {
               Piece piece= GetPieceByLocation(row,column);
                piece.Move(row, column);    
            }
        }

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
    }
}
