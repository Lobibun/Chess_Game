using Chess;
using Chess_Console.Chess;
using Chessboard;
class Program
{

    static void Main(string[] args)
    {
        try
        {
            Board board = new Board(8, 8);
            board.PutPiece(new Tower(Color.Black, board), new Position(0, 0));
            board.PutPiece(new Tower(Color.Black, board), new Position(1, 3));
            board.PutPiece(new King(Color.Black, board), new Position(0, 4));

            Screen.PrintBoard(board);

            Console.ReadLine();

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

