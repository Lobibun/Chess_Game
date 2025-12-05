using Chessboard;
namespace Chess
{
    class Screen
    {
        public static void PrintBoard(Board board)
        {
            for(int i=0; i<board.Lines; i++)
            {
                for (int n=0; n<board.Columns; n++)
                {
                    if (board.piece(i, n) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write($"{board.piece(i, n)} ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
