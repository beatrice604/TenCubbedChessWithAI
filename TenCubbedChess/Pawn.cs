using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenCubbedChess;

namespace TenCubbedChess
{
    public class Pawn : Piece
    {
        public Pawn(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 10;
            }
            else
            {
                Id = 20;
            }
            points = 1;
        }


        public override List<Position> LegalMoves(int[,] Board)
        {
            List<Position> moves = new List<Position>();
            Position[] offsets = new Position[3]
        {
                new Position(1,0),
                new Position(1,1),
                new Position(1,-1)
        };

            int direction = -1;
            if (Id / 10 == 2)
                direction = 1;
            foreach (Position offset in offsets) {
                Position nextMove = new Position(this.position.row + direction * offset.row, this.position.column + direction * offset.column);
                if (!IsOutOfBounds(nextMove.row, nextMove.column))
                    if (offset.column == 0)
                    {
                        if (IsEmpty(Board[nextMove.row, nextMove.column]))
                            moves.Add(nextMove);
                    }
                    else if (IsEnemy(Board[nextMove.row, nextMove.column]))
                        moves.Add(nextMove);
            }
            if (this.position.row == 2 && direction > 0 && IsEmpty(Board[position.row + 2, position.column]))
                moves.Add(new Position(position.row + 2, position.column));
            if (this.position.row == 7 && direction < 0 && IsEmpty(Board[position.row - 2, position.column]))
                moves.Add(new Position(position.row - 2, position.column));

            //enPassant -> nu ia bine piesa, treubie caz separat tratat pt pion in game 
            
            if (!IsOutOfBounds(this.position.row, this.position.column - 1) && IsEnemy(Board[this.position.row, this.position.column - 1]))
                moves.Add(new Position(this.position.row + direction, this.position.column - 1));
            if (!IsOutOfBounds(this.position.row, this.position.column + 1) && IsEnemy(Board[this.position.row, this.position.column + 1]))
                moves.Add(new Position(this.position.row + direction, this.position.column + 1));


            return moves;
        }
    }
}

