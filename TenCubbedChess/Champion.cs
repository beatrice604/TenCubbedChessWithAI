using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class Champion : Piece
    {
        public Champion(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 16;
            }
            else
            {
                Id = 26;
            }
            points = 7;
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();
            Position[] wazirOffsets = new Position[4] {
                new Position(0,1),
                new Position(1,0),
                new Position(0,-1),
                new Position(-1,0)
            };
            Position[] alfilOffsets = new Position[4] {
                new Position(2,2),
                new Position(2,-2),
                new Position(-2,2),
                new Position(-2,-2),
            };
            Position[] dabbabahOffsets = new Position[4] {
                new Position(0,2),
                new Position(2,0),
                new Position(0,-2),
                new Position(-2,0)
            };
            List<Position> offsetMoves = new List<Position>(wazirOffsets);
            offsetMoves.AddRange(alfilOffsets);
            offsetMoves.AddRange(dabbabahOffsets);

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
