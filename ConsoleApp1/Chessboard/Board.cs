
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

    }
}
