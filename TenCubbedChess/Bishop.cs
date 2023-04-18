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
            int size = 10;
            for (int i = this.position.row; i < 10; i++)
            {
                if (board[i, i] == 0)
                    moves.Add(new Position(i, i));
                else if (board[i, i] / 10 != Id / 10 && board[i - 1, i - 1] == 0)
                    moves.Add(new Position(i, i));
                else { break; }
                    }
            for (int i = this.position.row; i < 10; i++)
            {
                if (board[i, size - i - 1] == 0)
                    moves.Add(new Position(i, size-i-1));
                else if (board[i, size - i - 1] / 10 != Id / 10 && board[i - 1, size - i - 1 - 1] == 0)
                    moves.Add(new Position(i, size - i - 1));
                else { break; }
            }
            for(int i = this.position.row;i>0;i--)
            {
                if (board[i, i] == 0)
                    moves.Add(new Position(i, i));
                else if (board[i, i] / 10 != Id / 10 && board[i + 1, i + 1] == 0)
                    moves.Add(new Position(i, i));
                else { break; }
            }

            for (int i = this.position.row; i > 0; i--)
            {
                if (board[i, size - i - 1] == 0)
                    moves.Add(new Position(i, size - i - 1));
                else if (board[i, size - i - 1] / 10 != Id / 10 && board[i + 1, size - i - 1 + 1] == 0)
                    moves.Add(new Position(i, size - i - 1));
                else { break; }
            }

            moves.RemoveAll(p => p==this.position);
            
            return moves;
        }
    }
}
