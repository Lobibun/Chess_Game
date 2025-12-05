using Chessboard;
namespace Chess
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for(int i=0; i<board.Lines; i++)
            {
                Console.Write( 8 - i + " ");
                for (int n=0; n<board.Columns; n++)
                {
                    if (board.piece(i, n) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        PrintPiece(board.piece(i, n));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece.color == Color.White)
            {
                Console.Write(piece);
            }
            else
            {
               ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(piece);
                Console.ForegroundColor = aux;

            }

        }
    }
}
