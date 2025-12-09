

namespace Chessboard
{
    abstract class Piece
    {
        public Position position {  get; set; }
        public Color color { get; protected set; }
        public int QttMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Color color, Board board)
        {
            this.position = null;
            this.color = color;
            this.board = board;
            QttMovements = 0;
        }

        public void increaseQuantityOfMovement()
        {
            QttMovements++;
        }

        public void DecrementQuantityOfMovement()
        {
            QttMovements--;
        }

        public bool ThereArePosibleMovements()
        {
            bool[,] mat = PossibleMovements();
            for (int i = 0; i < board.Lines; i++)
            {
                for (int n = 0; n < board.Columns; n++)
                {
                    if (mat[i, n])
                    {
                        return true;
                    }


                }
            }
            return false;
        }
        
        public bool PossibleMovement(Position pos)
        {
            return PossibleMovements()[pos.Line, pos.Column];
        }

        public abstract bool[,] PossibleMovements();
    }
}
