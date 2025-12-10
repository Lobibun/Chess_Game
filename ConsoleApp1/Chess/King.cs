using Chess_Console.Chess;
using Chessboard;

namespace Chess
{
    internal class King : Piece
    {
        private ChessMatch Match;
        public King(PieceColor color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        private bool RookTestForTower(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Tower && p.color == color && p.QttMovements == 0;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[board.Lines, board.Columns];
            Position pos = new Position(0, 0);

            pos.SetValues(position.Line - 1, position.Column);
            if (board.ValidPosition(pos) && CanMove(pos)) //cima
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line - 1, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //nordeste
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line , position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //direita
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line + 1, position.Column + 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //sudeste
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line + 1, position.Column);
            if (board.ValidPosition(pos) && CanMove(pos)) //baixo
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line + 1, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //sudoeste
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //esquerda
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.SetValues(position.Line - 1, position.Column - 1);
            if (board.ValidPosition(pos) && CanMove(pos)) //noroeste
            {
                mat[pos.Line, pos.Column] = true;
            }

            //#jogada especial roque 
            if (QttMovements == 0 && !Match.Check)
            {
                //#jogada especial roque pequeno
                Position T1Pos = new Position(position.Line, position.Column +3);
                if (RookTestForTower(T1Pos))
                {
                    Position p1 = new Position(position.Line, position.Column + 1);
                    Position p2 = new Position(position.Line, position.Column + 2);
                    if (board.piece(p1)==null && board.piece(p2)==null)
                    {
                        mat[position.Line, position.Column +2] = true;
                    }
                }
                //#jogada especial roque grande
                Position T2Pos = new Position(position.Line, position.Column - 4);
                if (RookTestForTower(T2Pos))
                {
                    Position p1 = new Position(position.Line, position.Column - 1);
                    Position p2 = new Position(position.Line, position.Column - 2);
                    Position p3 = new Position(position.Line, position.Column - 3);
                    if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.Line, position.Column - 2] = true;
                    }
                }
            }


            return mat;

        }
    }
}
