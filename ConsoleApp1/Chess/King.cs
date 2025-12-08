using Chessboard;

namespace Chess_Console.Chess
{
    internal class King : Piece
    {
        public King(Color color, Board board) : base(color, board)
        {
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

            return mat;

        }
    }
}
