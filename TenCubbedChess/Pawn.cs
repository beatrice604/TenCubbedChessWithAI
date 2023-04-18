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
        }


        public override List<Position> LegalMoves(int[,] Board)
        {
            List<Position> moves = new List<Position>();
            int direction = -1;
            if (Id / 10 == 1)
                direction = 1;
            if (this.position.row + direction >= 0 && this.position.row + direction < 10)
            { if (Board[this.position.row + direction, this.position.column] == 0)
                    moves.Add(new Position(this.position.row + direction, this.position.column));
                if (this.position.column + 1 < 10 && Board[this.position.row + direction, this.position.column + 1] / 10 != this.Id / 10)
                    moves.Add(new Position(this.position.row + direction, this.position.column + 1));
                if (this.position.column - 1 >= 0 && Board[this.position.row + direction, this.position.column - 1] / 10 != this.Id / 10)
                    moves.Add(new Position(this.position.row + direction, this.position.column - 1));
                //enPassant
                if (this.position.column - 1 >= 0 && Board[this.position.row, this.position.column - 1] / 10 != this.Id / 10)
                    moves.Add(new Position(this.position.row+direction, this.position.column-1));
                if (this.position.column + 1 < 10 && Board[this.position.row, this.position.column - 1] / 10 != this.Id / 10)
                    moves.Add(new Position(this.position.row+direction, this.position.column+1));
            }
            if (this.position.row == 3 && direction < 0)
                moves.Add(new Position(position.row + 2, position.column));
            if(this.position.row == 7 && direction > 0)
                moves.Add(new Position(position.row-2,position.column));
            return moves;
        }
    }
}

