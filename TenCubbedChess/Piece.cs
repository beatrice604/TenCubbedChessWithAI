using System.Collections.Generic;

namespace TenCubbedChess
{
    public abstract class Piece
    {
        public Position position;
        public  int Id { get; set; }

        public Piece(int row,int column, bool whitePiece)
        {
            position = new Position(row,column);
        }

        public abstract List<Position> LegalMoves(int[,] board);

        public void Move(int row,int column)
        {
            position.row = row; position.column = column;
        }
        public bool IsOutOfBounds(int row,int column)
        {
            if(row < 0 || row >= 10) return true;
            if(column < 0 || column >= 10)return true;
            return false;
        }
        protected void MoveWhileTrue(int rowOffset, int columnOffset, ref bool condition, ref List<Position> moves, int[,] board)
        {
            Position nextPosition = new Position(this.position.row + rowOffset, this.position.column + columnOffset);
            if (IsOutOfBounds(nextPosition.row, nextPosition.column) || board[nextPosition.row, nextPosition.column] / 10 == Id / 10)
                condition = false;
            else
            {
                moves.Add(new Position(nextPosition));
                if (board[nextPosition.row, nextPosition.column] != 0 && board[nextPosition.row, nextPosition.column] / 10 != Id / 10)
                    condition = false;
            }
        }

        protected bool IsEnemy(int value)
        {
            if(Id / 10 != value/10 && value!=0)
            return true;
            return false;
        }

        protected bool IsEmpty(int value)
        {
            if (value == 0)
                return true;
            return false;
        }
    }
}
