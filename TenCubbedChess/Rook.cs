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
        }

        public override List<Position> LegalMoves(int[,] board)
        {
            List<Position> moves = new List<Position>();
            for (int i = this.position.row+1; i < 10; i++)
            {

                if (board[i,this.position.column]==0)
                { 
                    moves.Add(new Position(i,this.position.column)); 
                }
                else
                if (board[i, this.position.column]/10 != Id/10 && board[i-1, this.position.column] / 10 == 0)
                {
                    moves.Add(new Position(i, this.position.column));
                }
                else { break; }
               
            }
            for (int i = this.position.row - 1; i > 0; i--)
            {

                if (board[i, this.position.column] == 0)
                {
                    moves.Add(new Position(i, this.position.column));
                }
                else
                if (board[i, this.position.column] / 10 != Id / 10 && board[i + 1, this.position.column] / 10 == 0)
                {
                    moves.Add(new Position(i, this.position.column));
                }
                else { break; }

            }
            
            for (int j = this.position.column + 1; j < 10; j++)
            {

                if (board[this.position.row, j] == 0)
                {
                    moves.Add(new Position(this.position.row, j));
                }
                else
                if (board[this.position.row,j] / 10 != Id / 10 && board[this.position.row, j-1] / 10 == 0)
                {
                    moves.Add(new Position(this.position.row, j));
                }
                else { break; }

            }
            for (int j = this.position.column - 1; j>0; j--)
            {
                if (board[this.position.row, j] == 0)
                {
                    moves.Add(new Position(this.position.row, j));
                }
                else if (board[this.position.row, j] / 10 != Id / 10 && board[this.position.row, j + 1] / 10 == 0)
                {
                    moves.Add(new Position(this.position.row, j));
                }
                else { break; }

            }

            return moves;
        }
    }
}
