using Chessboard;

namespace Chess
{
    internal class PositionChess
    {
        public char Column {  get; set; }
        public int Line { get; set; }

        public PositionChess(char column, int line)
        {
            Column = column;
            Line = line;
        }
        public Position toPosition()
        {
            return new Position(8- Line, Column - 'a');
        }

        public override string ToString()
        {
            return $"{Column}{Line}";
        }
    }
}
