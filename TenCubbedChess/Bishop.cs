using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TenCubbedChess
{
    public class Bishop : Piece
    {
        public Bishop(int row, int column, bool whitePiece) : base(row, column, whitePiece)
        {
            if (whitePiece)
            {
                Id = 13;
            }
            else
            {
                Id = 23;
            }
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();


            bool topLeft=true, topRight=true, bottomLeft=true, bottomRight=true;
            int offset = 1;

            while (topLeft || topRight || bottomLeft || bottomRight)
            {
                if(topLeft)     MoveWhileTrue((-1) * offset, (-1) * offset, ref topLeft, ref moves, board);
                if(topRight)    MoveWhileTrue((-1) * offset, offset, ref topRight, ref moves, board);
                if(bottomLeft)  MoveWhileTrue(offset, (-1)*offset, ref bottomLeft, ref moves, board);
                if(bottomRight) MoveWhileTrue(offset, offset, ref bottomRight, ref moves, board);

                offset++;

            }

                return moves;
        }
       
    }
}
