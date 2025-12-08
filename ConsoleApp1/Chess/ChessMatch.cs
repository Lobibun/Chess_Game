using Chess_Console.Chess;
using Chessboard;
using System;

namespace Chess
{
    internal class ChessMatch
    {
        public Board board {  get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool finished { get; private set; }


        public ChessMatch()
        {
            board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.Branca;
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

        public void MakePlay(Position origin, Position destiny)
        {
            PerformMovement(origin, destiny);
            Turn++;
            ChangePlayer();
        }

        public void ValidadeOriginPosition(Position pos)
        {
            if (board.piece(pos) == null)
            {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != board.piece(pos).color)
            {
                throw new BoardException("A peça de origem escolhida é de outro jogador!");
            }
            if (!board.piece(pos).ThereArePosibleMovements())
            {
                throw new BoardException("Não há movimentos possiveis para a peça de origem escolhida!");
            }

        }

        public void ValidadeDestinyPosition(Position origin, Position destiny)
        {
            if (!board.piece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Posição de destino inválida");
            }
        }
        private void ChangePlayer()
        {
            if (CurrentPlayer == Color.Branca)
            {
                CurrentPlayer = Color.Preta;
            }
            else
            {
                CurrentPlayer = Color.Branca;
            }
        }
        private void PutPieces()
        {
            board.PutPiece(new Tower(Color.Branca, board), new ChessPosition('a', 1).toPosition());
            board.PutPiece(new Tower(Color.Branca, board), new ChessPosition('h', 1).toPosition());
            board.PutPiece(new King(Color.Branca, board), new ChessPosition('d', 1).toPosition());

            board.PutPiece(new Tower(Color.Preta, board), new ChessPosition('a', 8).toPosition());
            board.PutPiece(new Tower(Color.Preta, board), new ChessPosition('h', 8).toPosition());
            board.PutPiece(new King(Color.Preta, board), new ChessPosition('d', 8).toPosition());

        }
    }
}
