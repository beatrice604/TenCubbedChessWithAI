using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class Knight : Piece
    {
        public Knight(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 12;
            }
            else
            {
                Id = 22;
            }
            points = 3;
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();

            Position[] offsets = new Position[8]
         {
                new Position(2,1),
                new Position(2,-1),
                new Position(-1,2),
                new Position(1,2),
                new Position(1,-2),
                new Position(-1,-2),
                new Position(-2,1),
                new Position(-2,-1)
         };

            foreach(Position offset in offsets)
            {
                if (!IsOutOfBounds(position.row + offset.row,position.column + offset.column))
                        if (IsEmpty(board[position.row + offset.row, position.column + offset.column]) ||
                            IsEnemy(board[position.row + offset.row, position.column + offset.column]))
                            moves.Add(new Position(position.row + offset.row, position.column + offset.column));
            }

            return moves;
        }
    }
}
