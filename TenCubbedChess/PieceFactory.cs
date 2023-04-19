using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{   public interface IPieceFactory
    {
        Piece createPiece(int row, int column, int id);
    }
    class PieceFactory : IPieceFactory
    {   //0-pawn
        /*1-Rook
        2-Knight
        3-Bishop
        4-King
        5-Queen
        6-Champion
        7-Wizard
        8-Marshall
        9-Archbishop*/
        public Piece createPiece(int row, int column, int id)
        {   
            switch (id % 10)
            {
                case 0: return new Pawn(row,column,id / 10 ==1);
                case 1: return new Rook(row,column,id / 10 ==1);
                case 2: return new Knight(row,column,id / 10 ==1);
                case 3: return new Bishop(row,column,id / 10 ==1);
                case 4: return new King(row,column,id / 10 ==1);
                case 5: return new Queen(row,column,id / 10 ==1);
                case 6: return new Champion(row,column,id / 10 ==1);
                case 7: return new Wizard(row,column,id / 10 ==1);
                case 8: return new Marshall(row,column,id / 10 ==1);
                case 9: return new Archbishop(row,column,id / 10 ==1);

                default: throw new ArgumentException("Invalid piece ID");

                }
        }
    }
}
