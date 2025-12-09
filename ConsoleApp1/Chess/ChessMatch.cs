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
        public bool Check { get; private set; }


        public ChessMatch()
        {
            board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.Branca;
            finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            captuded = new HashSet<Piece>();
            PutPieces();
        }

        public Piece PerformMovement(Position origen, Position destiny)
        {
            Piece p = board.RemovePiece(origen);
            p.increaseQuantityOfMovement();
            
            Piece CapturedPice = board.RemovePiece(destiny);
            board.PutPiece(p, destiny);
            if (CapturedPice != null)
            {
                captuded.Add(CapturedPice);
            }
            return CapturedPice;
        }

        public void MakePlay(Position origin, Position destiny)
        {
           Piece CapturedPiece = PerformMovement(origin, destiny);

            if (ByCheck(CurrentPlayer))
            {
                UndoMove(origin, destiny, CapturedPiece);
                throw new BoardException("Você não pode se colocar em xeque!");
            }
            if (ByCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }
            if (TestCheckmate(Adversary(CurrentPlayer)))
            {
                finished = true;
            }
            else
            {
                Turn++;
                ChangePlayer();
            }
        }

        public void UndoMove(Position origin, Position destiny, Piece CapturedPiece)
        {
            Piece p = board.RemovePiece(destiny);
            p.DecrementQuantityOfMovement();
            if (CapturedPiece != null)
            {
                board.PutPiece(CapturedPiece, destiny);
                captuded.Remove(CapturedPiece);
            }
            board.PutPiece(p, origin);
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
        private Color Adversary(Color color)
        {
            if (color == Color.Branca)
            {
                return Color.Preta;
            }
            else
            {
                return Color.Branca;
            }
        }

        private Piece King(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool ByCheck(Color color)
        {
            Piece K = King(color);
            if (K == null)
            {
                throw new BoardException($"Não tem rei da cor {color} no tabuleiro!");
            }
            foreach (Piece x in piecesInGame(Adversary(color)))
            {
                bool[,] mat = x.PossibleMovements();
                if (mat[K.position.Line, K.position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckmate(Color color)
        {
            if (!ByCheck(color))
            {
                return false;
            }
            foreach (Piece x in piecesInGame(color))
            {
                bool[,] mat = x.PossibleMovements();
                for (int i=0; i<board.Lines; i++)
                {
                    for (int n=0; n<board.Columns; n++)
                    {
                        if (mat[i,n])
                        {
                            Position origin = x.position;
                            Position destiny = new Position(i, n);
                            Piece CapturedPiece = PerformMovement(x.position, destiny);
                            bool CheckTest = ByCheck(color);
                            UndoMove(origin, destiny, CapturedPiece);
                            if (!CheckTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(Char column, int line, Piece piece)
        {
            board.PutPiece(piece, new ChessPosition(column, line).toPosition());
            pieces.Add(piece);
        }

        private void PutPieces()
        {
            PutNewPiece('a', 2, new Tower(Color.Branca, board));
            PutNewPiece('h', 7, new Tower(Color.Branca, board));
            PutNewPiece('d', 1, new King(Color.Branca, board));

            PutNewPiece('c', 8, new Tower(Color.Preta, board));
            PutNewPiece('e', 8, new Tower(Color.Preta, board));
            PutNewPiece('d', 8, new King(Color.Preta, board));
        }
    }
}
