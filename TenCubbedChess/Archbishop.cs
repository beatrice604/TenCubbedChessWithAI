using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class Archbishop : Piece
    {
        public Archbishop(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 19;
            }
            else
            {
                Id = 29;
            }
            points = 7;
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();
            Bishop bishop = new Bishop(position.row, position.column, Id / 10 == 1);
            Knight knight = new Knight(position.row, position.column, Id / 10 == 1);

            moves.AddRange(bishop.LegalMoves(board));
            moves.AddRange(knight.LegalMoves(board));

            return moves;
        }
    }
}
