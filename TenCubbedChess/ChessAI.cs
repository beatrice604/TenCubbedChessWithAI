using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TenCubbedChess
{
    internal class ChessAI
    {

        PieceFactory pieceFactory;
        int depth;
        public ChessAI(int depth) {
            this.depth = depth;
            pieceFactory = new PieceFactory();
        }
        public (Piece,Position) MoveAI(int[,] board)
        { 
            return FindBestMove(depth,board);
        }
        private (Piece, Position) FindBestMove(int depth, int[,] board)
        {
            int bestEval = int.MinValue;
            (Piece piece, Position position) bestMove = (null, null);
            bool isMaximizingPlayer = true;
            var darkPieces = GeneratePieces(board, 2);
            List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, darkPieces);
            foreach (var pieceMove in pieceMoves)
            {
                int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);

                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = pieceMove;
                }

            }
            return bestMove;
        }
        private int[,] ChangeBoard(int[,] board, Piece piece, Position newPosition)
        {
            board[newPosition.row, newPosition.column] = piece.Id;
            board[piece.position.row, piece.position.column] = 0;
            return board;
        }
        private int MiniMax(int[,] board, int depth, bool isMaximizingPlayer)
        {

            if (depth == 0)//ADD || GAMEOVER(BOARD)
            {
                return EvaluateBoard(board);
            }

            if (isMaximizingPlayer)
            {
                int maxEval = int.MinValue;

                List<Piece> myPieces = GeneratePieces(board, 2);
                List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, myPieces);

                foreach (var pieceMove in pieceMoves)
                {
                    int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
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
        private List<Piece> GeneratePieces(int[,] board, int player)
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] == player)
                        pieces.Add(pieceFactory.createPiece(i, j, board[i, j]));

            return pieces;
        }
        private List<(Piece, Position)> GetMovesOfPieces(int[,] board, List<Piece> pieces)
        {
            List<(Piece, Position)> movesOfPieces = new List<(Piece, Position)>();
            foreach (Piece piece in pieces)
            {
                List<Position> moves = piece.LegalMoves(board);

                foreach (Position move in moves)
                {
                    movesOfPieces.Add((piece, move));
                }

            }
            return movesOfPieces;
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

            foreach (Piece piece in pieces)
            {
                if (piece.Id / 10 == 1)
                    whiteSum += piece.points;
                else
                    darkSum += piece.points;
            }
            return whiteSum - darkSum;
        }
    }
}
