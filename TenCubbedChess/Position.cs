using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    public class Position
    {   
        public int row;
        public int column;

        public Position(int row, int column)
        {
            this.row =row ;
            this.column = column;
        }
        public Position(Position newPosition) {
            this.row = newPosition.row;
            this.column = newPosition.column;
        }
        public void SetPosition(int row, int column) { this.row = row ; this.column = column ; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Position other = (Position)obj;
            return (row == other.row && column == other.column);
        }

        public override int GetHashCode()
        {
            return row ^ column;
        }
    }
}
