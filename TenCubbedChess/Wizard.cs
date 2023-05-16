using System;
using System.Collections.Generic;

namespace TenCubbedChess
{
    public class Wizard : Piece
    {
        public Wizard(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 17;
            }
            else
            {
                Id = 27;
            }
            points = 8;
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();
            Position[] camelOffsets = new Position[8] {
                new Position(1,3),
                new Position(-1,3),
                new Position(3,1),
                new Position(3,-1),
                new Position(1,-3),
                new Position(-1,-3),
                new Position(-3,1),
                new Position(-3,-1)
            };

            Position[] ferzOffsets = new Position[4]
            {
                   new Position(2,2),
                new Position(2,-2),
                new Position(-2,2),
                new Position(-2,-2)
            };

            List<Position> offsetMoves = new List<Position>(camelOffsets);
            offsetMoves.AddRange(ferzOffsets);
            foreach (Position offset in offsetMoves)
            {
                if (!IsOutOfBounds(position.row + offset.row, position.column + offset.column))
                {
                    if (IsEmpty(board[position.row + offset.row, position.column + offset.column]) || IsEnemy(board[position.row + offset.row, position.column + offset.column]))
                        moves.Add(new Position(position.row + offset.row, position.column + offset.column));
                }
            }
            return moves;
        }
    }
}
