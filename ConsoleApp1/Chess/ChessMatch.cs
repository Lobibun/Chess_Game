using Chess_Console.Chess;
using Chessboard;
using System;

namespace Chess
{
    internal class ChessMatch
    {
        public Board board {  get; private set; }
        private int Turn;
        private Color CurrentPlayer;
        public bool finished { get; private set; }


        public ChessMatch()
        {
            board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            PutPieces();
            finished = false;
        }

        public void PerformMovement(Position origen, Position destiny)
        {
            Piece p = board.RemovePiece(origen);
            p.increaseQuantityOfMovement();
            board.RemovePiece(destiny);
            Piece CapturedPice = board.RemovePiece(destiny);
            board.PutPiece(p, destiny);
        }
        private void PutPieces()
        {
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('a', 1).toPosition());
            board.PutPiece(new Tower(Color.White, board), new ChessPosition('h', 1).toPosition());
            board.PutPiece(new King(Color.White, board), new ChessPosition('d', 1).toPosition());

            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('a', 8).toPosition());
            board.PutPiece(new Tower(Color.Black, board), new ChessPosition('h', 8).toPosition());
            board.PutPiece(new King(Color.Black, board), new ChessPosition('d', 8).toPosition());

        }
    }
}
