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
        private HashSet<Piece> pieces;
        private HashSet<Piece> captuded;


        public ChessMatch()
        {
            board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.Branca;
            finished = false;
            pieces = new HashSet<Piece>();
            captuded = new HashSet<Piece>();
            PutPieces();
        }

        public void PerformMovement(Position origen, Position destiny)
        {
            Piece p = board.RemovePiece(origen);
            p.increaseQuantityOfMovement();
            
            Piece CapturedPice = board.RemovePiece(destiny);
            board.PutPiece(p, destiny);
            if (CapturedPice != null)
            {
                captuded.Add(CapturedPice);
            }
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captuded)
            {
                if (x.color == color)
                { 
                aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void PutNewPiece(Char column, int line, Piece piece)
        {
            board.PutPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('a', 1, new Tower(Color.Branca, board));
            PutNewPiece('h', 1, new Tower(Color.Branca, board));
            PutNewPiece('d', 1, new King(Color.Branca, board));

            PutNewPiece('a', 8, new Tower(Color.Preta, board));
            PutNewPiece('h', 8, new Tower(Color.Preta, board));
            PutNewPiece('d', 8, new King(Color.Preta, board));
        }
    }
}
