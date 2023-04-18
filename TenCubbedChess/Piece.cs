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

    }
}
