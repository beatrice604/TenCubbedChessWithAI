using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TenCubbedChess
{
    class Tile
    {
        private Position _position;
        private bool _legalNextMove;
       // private bool _occupied;

        Tile(Position position)
        {
            _position = position;
            _legalNextMove = false;
          //  _occupied = false;
        }
        public bool isLegalNextMove()
        {  return _legalNextMove; }

        public void setLegalNextMove(bool legalNextMove)
        {
            _legalNextMove = legalNextMove;
        }

 /*       public bool isOccupied()
        { return _occupied; }

        public void setOccupied(bool occupied)
        {
            _occupied = occupied;
        }*/
    }
}
