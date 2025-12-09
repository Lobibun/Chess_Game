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

        public Piece PerformMovement(Position origin, Position destiny)
        {
            Piece p = board.RemovePiece(origin);
            p.increaseQuantityOfMovement();
            
            Piece CapturedPice = board.RemovePiece(destiny);
            board.PutPiece(p, destiny);
            if (CapturedPice != null)
            {
                captuded.Add(CapturedPice);
            }

            //#jogada especial roque pequeno
            if (p is King && destiny.Column == origin.Column +2)
            {
                Position TOrigin = new Position(origin.Line, origin.Column + 3);
                Position TDestiny = new Position(origin.Line, origin.Column + 1);
                Piece T = board.RemovePiece(TOrigin);
                T.increaseQuantityOfMovement();
                board.PutPiece(T, TDestiny);
            }

            //#jogada especial roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position TOrigin = new Position(origin.Line, origin.Column - 4);
                Position TDestiny = new Position(origin.Line, origin.Column - 1);
                Piece T = board.RemovePiece(TOrigin);
                T.increaseQuantityOfMovement();
                board.PutPiece(T, TDestiny);
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

            //#jogada especial roque pequeno
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position TOrigin = new Position(origin.Line, origin.Column + 3);
                Position TDestiny = new Position(origin.Line, origin.Column + 1);
                Piece T = board.RemovePiece(TDestiny);
                T.DecrementQuantityOfMovement();
                board.PutPiece(T, TOrigin);
            }

            //#jogada especial roque grande
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position TOrigin = new Position(origin.Line, origin.Column - 4);
                Position TDestiny = new Position(origin.Line, origin.Column - 1);
                Piece T = board.RemovePiece(TDestiny);
                T.DecrementQuantityOfMovement();
                board.PutPiece(T, TOrigin);
            }
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
            if (!board.piece(origin).PossibleMovement(destiny))
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
            PutNewPiece('a', 1, new Tower(Color.Branca, board));
            PutNewPiece('h', 1, new Tower(Color.Branca, board));
            PutNewPiece('e', 1, new King(Color.Branca, board, this));
            //PutNewPiece('c', 1, new Bishop(Color.Branca, board));
            //PutNewPiece('f', 1, new Bishop(Color.Branca, board));
            //PutNewPiece('b', 1, new Horse(Color.Branca, board));
            //PutNewPiece('g', 1, new Horse(Color.Branca, board));
            //PutNewPiece('d', 1, new Queen(Color.Branca, board));
            PutNewPiece('a', 2, new Pawn(Color.Branca, board));
            PutNewPiece('b', 2, new Pawn(Color.Branca, board));
            PutNewPiece('c', 2, new Pawn(Color.Branca, board));
            PutNewPiece('d', 2, new Pawn(Color.Branca, board));
            PutNewPiece('e', 2, new Pawn(Color.Branca, board));
            PutNewPiece('f', 2, new Pawn(Color.Branca, board));
            PutNewPiece('g', 2, new Pawn(Color.Branca, board));
            PutNewPiece('h', 2, new Pawn(Color.Branca, board));

            PutNewPiece('a', 8, new Tower(Color.Preta, board));
            PutNewPiece('h', 8, new Tower(Color.Preta, board));
            PutNewPiece('e', 8, new King(Color.Preta, board, this));
            PutNewPiece('f', 8, new Bishop(Color.Preta, board));
            PutNewPiece('c', 8, new Bishop(Color.Preta, board));
            PutNewPiece('b', 8, new Horse(Color.Preta, board));
            PutNewPiece('g', 8, new Horse(Color.Preta, board));
            PutNewPiece('d', 8, new Queen(Color.Preta, board));
            PutNewPiece('a', 7, new Pawn(Color.Preta, board));
            PutNewPiece('b', 7, new Pawn(Color.Preta, board));
            PutNewPiece('c', 7, new Pawn(Color.Preta, board));
            PutNewPiece('d', 7, new Pawn(Color.Preta, board));
            PutNewPiece('e', 7, new Pawn(Color.Preta, board));
            PutNewPiece('f', 7, new Pawn(Color.Preta, board));
            PutNewPiece('g', 7, new Pawn(Color.Preta, board));
            PutNewPiece('h', 7, new Pawn(Color.Preta, board));
        }
    }
}
