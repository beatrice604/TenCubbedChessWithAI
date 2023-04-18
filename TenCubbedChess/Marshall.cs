using System;
using System.Collections.Generic;

namespace TenCubbedChess
{
    public class Marshall : Piece
    {
        public Marshall(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 18;
            }
            else
            {
                Id = 28;
            }
        }

        public override List<Position> LegalMoves(int[,] board)
        {   List<Position> moves = new List<Position>();
            Rook rook = new Rook(position.row, position.column, Id / 10 == 1);
            Knight knight = new Knight(position.row, position.column, Id / 10 == 1);

            moves.AddRange(rook.LegalMoves(board));
            moves.AddRange(knight.LegalMoves(board));

            return moves;
        }
    }
}
