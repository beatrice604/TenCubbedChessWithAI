using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TenCubbedChess
{
    public class Rook : Piece
    {
        public Rook(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 11;
            }
            else
            {
                Id = 21;
            }
            points = 5;
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();


            bool top = true, right= true, bottom= true, left= true;
            int offset = 1;

            while (top || right || bottom || left)
            {
                if (top) MoveWhileTrue((-1) * offset,0, ref top, ref moves, board);
                if (right) MoveWhileTrue(0, offset, ref right, ref moves, board);
                if (bottom) MoveWhileTrue(offset, 0, ref bottom, ref moves, board);
                if (left) MoveWhileTrue(0, (-1)*offset, ref left, ref moves, board);

                offset++;

            }

            return moves;
        }

    }
}
