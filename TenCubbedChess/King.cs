using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class King : Piece
    {
        public King(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 14;
            }
            else
            {
                Id = 24;
            }
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();
            Position[] offsets = new Position[8]
            {
                new Position(1,-1),
                new Position(1,0),
                new Position(1,1),
                new Position(0,1),
                new Position(-1,1),
                new Position(-1,0),
                new Position(-1,-1),
                new Position(0,-1)
            };
            foreach(Position offset in offsets)
            {
                 if (!this.IsOutOfBounds(position.row + offset.row, position.column + offset.column) &&
                        (IsEmpty(board[position.row + offset.row, position.column + offset.column]) || IsEnemy(board[position.row + offset.row, position.column+offset.column])))
                        moves.Add(new Position(position.row + offset.row, position.column + offset.column));
                   
                        }


                return moves;
        }
    }
}
