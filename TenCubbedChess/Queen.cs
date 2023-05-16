using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class Queen : Piece
    {
        public Queen(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 15;
            }
            else
            {
                Id = 25;
            }
            points = 9;
        }

        public override List<Position> LegalMoves(int[,] board)
        {   List<Position> moves = new List<Position>();
            
            Bishop bishop = new Bishop(this.position.row, this.position.column, Id / 10 == 1);
            Rook rook = new Rook(this.position.row, this.position.column, Id / 10 == 1);

            moves.AddRange(bishop.LegalMoves(board));
            moves.AddRange(rook.LegalMoves(board));

            return moves;
        }
    }
}
