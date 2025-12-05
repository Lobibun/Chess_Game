
namespace Chessboard
{
    internal class Board
    {
        public int Lines { get; set; }
        public int Columns { get; set; }
        private Piece[,] Pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Pieces = new Piece[Lines, Columns];
        }
        public Piece piece(int Line, int Column)
        {
            return Pieces[Line, Column];
        }
        public Piece piece(Position position)
        {
            return piece(position.Line, position.Column);
        }
        public bool PieceExists(Position pos)
        {
            ValidPosition(pos);
            return piece(pos) != null;
        }

        public void PutPiece(Piece p, Position pos)
        {
            if (PieceExists(pos))
            {
                throw new BoardException("Já existe uma peça nessa posição!");
            }
            Pieces[pos.Line, pos.Column] = p;
            p.position = pos;
        }

        public bool ValidPosition(Position pos) 
        { 
            if (pos.Line<0 || pos.Line>=Lines || pos.Column<0 || pos.Column>=Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Posição inválida");
            }
        }
    }
}
