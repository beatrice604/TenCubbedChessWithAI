using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace TenCubbedChess
{
    //TEAM + ID = ELEMENT
    //TEAM: 1= BLUE; 2=YELLOW
    //ID
    //0-pawn
    //1-Rook
    //2-Knight
    //3-Bishop
    //4-King
    //5-Queen
    //6-Champion
    //7-Wizard
    //8-Marshall
    //9-Archbishop
    //    TenCubed Chess uses ten different piece-types on a 10x10 board.The Queen combines the moves of rook and bishop. The Marshall combines the moves of rook and knight.The Archbishop combines the moves of bishop and knight.The Champion combines the moves of dabbabah and alfil and wazir.The Wizard combines the moves of camel and ferz.
    class Board
    {
        int[,] _board;
         public int[,] board
         { get { return _board; }
            set { _board = value; } 
          }
        public Board()
        {
            _board = new int[10, 10] { {0, 0, 16, 17, 19, 18, 17, 16,0,0},
                                       {0 , 18, 12,13,15, 14, 13, 12, 18,0},
                                       {10, 10,10,10, 10, 10, 10, 10, 10,10},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {20, 20,20,20, 20, 20, 20, 20, 20,20},
                                       {0 , 28, 22,23,25, 24, 23, 22, 28,0},
                                       {0,  0, 26, 27, 29, 28, 27, 26,0, 0}
                                       
                                       
            };
        }
    }
}
