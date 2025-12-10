using Chessboard;

namespace Chess
{
    internal class Pawn : Piece
    {
        private ChessMatch Match;

        public Pawn(Color color, Board board, ChessMatch match) : base(color, board)
        {
            Match = match;
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

                pos.SetValues(position.Line - 1, position.Column - 1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                pos.SetValues(position.Line - 1, position.Column + 1);
                if (board.ValidPosition(pos) && ThereIsAnEnemy(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }

                //jogada especial in passant
                if (position.Line == 3)
                {
                    Position left = new Position(position.Line, position.Column - 1);
                    if (board.ValidPosition(left) && ThereIsAnEnemy(left) && board.piece(left) == Match.VulnerableInPassant)
                    {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position right = new Position(position.Line, position.Column + 1);
                    if (board.ValidPosition(right) && ThereIsAnEnemy(right) && board.piece(right) == Match.VulnerableInPassant)
                    {
                        mat[right.Line - 1, right.Column] = true;
                    }
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

                //jogada especial in passant
                if (position.Line == 4)
                {
                    Position left = new Position(position.Line, position.Column - 1);
                    if (board.ValidPosition(left) && ThereIsAnEnemy(left) && board.piece(left) == Match.VulnerableInPassant)
                    {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position right = new Position(position.Line, position.Column + 1);
                    if (board.ValidPosition(right) && ThereIsAnEnemy(right) && board.piece(right) == Match.VulnerableInPassant)
                    {
                        mat[right.Line + 1, right.Column] = true;
                    }
                }

            }
            return mat;
        }
    }
}