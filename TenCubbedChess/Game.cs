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
using System.Windows.Navigation;
using System.Data.Common;
using System.Net.NetworkInformation;
using System.Numerics;

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
    class Game
    {
        int[,] _board;
        List<Piece> _whitePieces;
        List<Piece> _darkPieces;
        Dictionary<int, List<Piece>> _pieces = new Dictionary<int, List<Piece>> { { 1, new List<Piece>() }, { 2, new List<Piece>() } };
        Position selectedLocation;
        PieceFactory pieceFactory;
        bool check;
        bool checkMate;
        //TODO:
        //3.CheckMate
        //4.Castling
        //5.Promotion
        //6.Stalemate: If a player is not in check but has no legal moves, the game is a draw
        //adica PAT


        public int[,] board
        {
            get { return _board; }
            set { _board = value; }
        }
        public bool whiteTurn;

        public Game()
        {
            _board = new int[10, 10] { {0, 0, 16, 17, 19, 18, 17, 16,0,0},
                                       {0 , 11, 12,13,15, 14, 13, 12, 11,0},
                                       {10, 10,10,10, 10, 10, 10, 10, 10,10},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {0,  0, 0, 0,  0,  0,  0,  0,  0, 0},
                                       {20, 20,20,20, 20, 20, 20, 20, 20,20},
                                       {0 , 21, 22,23,25, 24, 23, 22, 21,0},
                                       {0,  0, 26, 27, 29, 28, 27, 26,0, 0}
            };
            whiteTurn = false;
            check = false;
            checkMate = false;
            selectedLocation = new Position(-1, -1);

            pieceFactory = new PieceFactory();

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] != 0)
                        _pieces[board[i, j] / 10].Add(pieceFactory.createPiece(i, j, board[i, j]));

        }


        public List<Position> ValidMoves(int row, int column)
        {
            Piece piece = GetPieceByLocation(row, column);
            selectedLocation.SetPosition(piece.position.row, piece.position.column);
            var legalMoves = piece.LegalMoves(board);
            var filteredLegalMoves = new List<Position>(legalMoves);
            Piece king;


            foreach (Position legalMove in legalMoves)
                {
                int[,] newBoard = (int[,])_board.Clone();
                newBoard = ChangeBoard(newBoard, piece, legalMove);

                if (piece.Id % 10 == 4)
                    king = pieceFactory.createPiece(legalMove.row,legalMove.column,piece.Id);
                else
                {
                    var tempPiece = GetPieceById(piece.Id / 10 * 10 + 4);
                    king = pieceFactory.createPiece(tempPiece.position.row, tempPiece.position.column, tempPiece.Id);
                }

                if (IsCheck(piece.Id / 10, newBoard,king))
                        filteredLegalMoves.Remove(legalMove);
                }

                return filteredLegalMoves;
            

        }

        public void Move(int row, int column, int oldRow=-1, int oldCol=-1)
        {   if (oldCol == -1 && oldRow == -1)
            {
                if (selectedLocation.row == -1 && selectedLocation.column == -1)
                    throw new Exception("Missing piece to be moved");
            }
        else
            {
                selectedLocation.SetPosition(oldRow, oldCol);
            }
            Piece piece = GetPieceByLocation(selectedLocation.row, selectedLocation.column);
            _board[selectedLocation.row, selectedLocation.column] = 0;
            board[row, column] = piece.Id;
            piece.Move(row, column);
            var oppositePlayer = (piece.Id / 10) % 2 + 1;
            check = this.IsCheck(oppositePlayer, board,GetPieceById(oppositePlayer*10+4));
            selectedLocation.SetPosition(-1, -1);
            whiteTurn = !whiteTurn  ;
        }

        public bool IsCheck(int player, int[,] board, Piece king)
        {
            List<Position> attackedSquares = new List<Position>();
            foreach (Piece piece in _pieces[player % 2 + 1])
            {
                piece.LegalMoves(board).ForEach(p => attackedSquares.Add(p));
            }
            if (attackedSquares.Exists(sq => sq.Equals(king.position)))
                return true;

            return false;
        }

        private Piece GetPieceByLocation(int row, int column)
        {
            Piece? piece = _pieces[board[row, column] / 10].Find(p =>
            {
                return p.position.row == row && p.position.column == column;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }

        private Piece GetPieceById(int Id)
        {
            Piece? piece = _pieces[Id / 10].Find(p =>
            {
                return p.Id == Id;
            });
            if (piece == null)
                throw new Exception("This piece does not exist.");
            return piece;

        }
        private int EvaluateBoard(int[,] board)
        {
            int whiteSum = 0;
            int darkSum = 0;
            List<Piece> pieces = new List<Piece>(); 
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] != 0)
                        pieces.Add(pieceFactory.createPiece(i, j, board[i, j]));

            foreach(Piece piece in pieces)
            {
                if (piece.Id / 10 == 1)
                    whiteSum += piece.points;
                else
                    darkSum += piece.points;
            }
            return whiteSum - darkSum;
        }
        private List<Piece> GeneratePieces(int[,] board, int player )
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] == player)
                        pieces.Add(pieceFactory.createPiece(i, j, board[i, j]));

            return pieces;
        }
        private List<(Piece,Position)> GetMovesOfPieces(int[,] board, List<Piece> pieces)
        {
            List<(Piece, Position)> movesOfPieces = new List<(Piece, Position)>;
            foreach(Piece piece in pieces)
            {
                List<Position> moves = piece.LegalMoves(board);

                foreach(Position move in moves)
                {
                    movesOfPieces.Add((piece, move));
                }

            }
            return movesOfPieces;
        }

        private int[,] ChangeBoard(int[,] board, Piece piece, Position newPosition)
        {
            board[newPosition.row, newPosition.column] = piece.Id;
            board[piece.position.row, piece.position.column] = 0;
            return board;
        }
        private int MiniMax(int[,] board, int depth, bool isMaximizingPlayer) { 

            if(depth == 0 )//ADD || GAMEOVER(BOARD)
            {
                return EvaluateBoard(board);
            }

        if(isMaximizingPlayer)
            {
                int maxEval = int.MinValue;

                List<Piece> myPieces = GeneratePieces(board, 2);
                List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, myPieces);
                
                foreach(var pieceMove in pieceMoves)
                {
                    int [,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                    int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);
                    maxEval = Math.Max(maxEval, eval);
                }
                return maxEval;

            }
        else
            {
                int minEval = int.MaxValue;
                List<Piece> myPieces = GeneratePieces(board, 1);
                List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, myPieces);

                foreach (var pieceMove in pieceMoves)
                {
                    int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                    int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);
                    minEval = Math.Min(minEval, eval);
                }
                return minEval;
            }
        }

        private (Piece,Position) FindBestMove( int depth)
        {
            int bestEval = int.MinValue;
            (Piece piece, Position position) bestMove = (null,null);
            bool isMaximizingPlayer = true;
            List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, _darkPieces);
            foreach (var pieceMove in pieceMoves)
            {
                int[,] newBoard = ChangeBoard(board,pieceMove.piece, pieceMove.position);
                int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);

                if(eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = pieceMove;
                }

            }
            return bestMove;
        }
        public void MoveAI()
        {   int depth = 5;
            (Piece piece, Position position) bestMove = FindBestMove(depth);
            Move(bestMove.position.row, bestMove.position.column, bestMove.piece.position.row, bestMove.piece.position.column);
        }
    }
}
