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
           (Piece piece, Position newPosition) aiMove = FindBestMove(depth, board);
         //   (Piece piece, Position newPosition) aiMove = FindBestMoveAlphaBeta(depth, board);

            return aiMove;
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
                int[,] newBoard = new int[10, 10];

                newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);

                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = pieceMove;
                }

            }
            return bestMove;
        }
        private int MiniMax(int[,] board, int depth, bool isMaximizingPlayer)
        {   // ordonare dupa piesele pe care le ia

            /*            int player;
                        if(isMaximizingPlayer) { player = 1; }
                        else { player = 2; }
                        Game game = new Game(board, player);
                        if (game.IsGameOver(player,board))
                            return 0;*/
            if (depth == 0)
            {
                if(isMaximizingPlayer)
                return EvaluateBoard(board);
                else
                    return -EvaluateBoard(board);
            }

            if (isMaximizingPlayer)
            {
                int maxEval = int.MinValue;

                List<Piece> myPieces = GeneratePieces(board, 2);
                List<(Piece piece, Position position)> pieceMoves = new List<(Piece piece, Position position)>(GetMovesOfPieces(board, myPieces));

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
                List<(Piece piece, Position position)> pieceMoves =new List<(Piece piece, Position position)>(GetMovesOfPieces(board, myPieces));

                foreach (var pieceMove in pieceMoves)
                {
                    int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                    int eval = MiniMax(newBoard, depth - 1, !isMaximizingPlayer);
                    minEval = Math.Min(minEval, eval);
                }
                return minEval;
            }
        }
        private (Piece, Position) FindBestMoveAlphaBeta(int depth, int[,] board)
        {
            int bestEval = int.MinValue;
            (Piece piece, Position position) bestMove = (null, null);
            bool isMaximizingPlayer = true;
            int alpha = int.MinValue;
            int beta = int.MaxValue;
            var darkPieces = GeneratePieces(board, 2);

            List<(Piece piece, Position position)> pieceMoves = GetMovesOfPieces(board, darkPieces);
            foreach (var pieceMove in pieceMoves)
            {
                int[,] newBoard = new int[10, 10];

                newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                int eval = Alphabeta(newBoard, depth - 1, !isMaximizingPlayer,alpha,beta);

                if (eval > bestEval)
                {
                    bestEval = eval;
                    bestMove = pieceMove;
                }
                alpha = Math.Max(alpha, eval);
            }
            return bestMove;
        }
        private int Alphabeta(int[,] board, int depth, bool isMaximizingPlayer, int alpha, int beta)
        {/*
            Game game = new Game(board);
            if (depth == 0 || game.IsGameOver())
            {
                return EvaluateBoard(board);
            }*/

            if (isMaximizingPlayer)
            {
                int maxEval = int.MinValue;

                List<Piece> myPieces = GeneratePieces(board, 2);
                List<(Piece piece, Position position)> pieceMoves = new List<(Piece piece, Position position)>(GetMovesOfPieces(board, myPieces));

                foreach (var pieceMove in pieceMoves)
                {
                    int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);

                    int eval = Alphabeta(newBoard, depth - 1, !isMaximizingPlayer, alpha, beta);

                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                        break;
                }
                return maxEval;

            }
            else
            {
                int minEval = int.MaxValue;
                List<Piece> myPieces = GeneratePieces(board, 1);
                List<(Piece piece, Position position)> pieceMoves = new List<(Piece piece, Position position)>(GetMovesOfPieces(board, myPieces));

                foreach (var pieceMove in pieceMoves)
                {
                    int[,] newBoard = ChangeBoard(board, pieceMove.piece, pieceMove.position);
                    int eval = Alphabeta(newBoard, depth - 1, !isMaximizingPlayer, alpha, beta);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                        break;
                }
                return minEval;
            }
        }
        private int[,] ChangeBoard(int[,] board, Piece piece, Position newPosition)
        {
            int[,] newBoard =new int[10, 10];
            Array.Copy(board,newBoard,board.Length);
            newBoard[newPosition.row, newPosition.column] = piece.Id;
            newBoard[piece.position.row, piece.position.column] = 0;
            return newBoard;
        }


        private List<Piece> GeneratePieces(int[,] board, int player)
        {
            List<Piece> pieces = new List<Piece>();
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    if (board[i, j] /10  == player)
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
