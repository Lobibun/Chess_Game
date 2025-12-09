using Chessboard;

namespace Chess_Console.Chess
{
    internal class Pawn : Piece
    {
        public Pawn(Color color, Board board) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "P";
        }

        private bool ThereIsAnEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        public bool Free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[board.Lines, board.Columns];
            Position pos = new Position(0, 0);

            if (color == Color.Branca)
            {
                pos.SetValues(position.Line - 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line - 2, position.Column);
                Position p2 = new Position(position.Line - 1, position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && board.ValidPosition(pos) && QttMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line - 1, position.Column -1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line - 1, position.Column + 1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }

            else
            {
                pos.SetValues(position.Line + 1, position.Column);
                if (board.ValidPosition(pos) && Free(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line + 2, position.Column);
                Position p2 = new Position(position.Line + 1, position.Column);
                if (board.ValidPosition(p2) && Free(p2) && board.ValidPosition(pos) && Free(pos) && QttMovements == 0)
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line + 1, position.Column - 1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line + 1, position.Column + 1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
            }

                return mat;

        }
    }
}
