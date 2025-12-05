using Chess;
using Chess_Console.Chess;
using Chessboard;
class Program
{

    static void Main(string[] args)
    {
        try
        {
            PositionChess pos = new PositionChess('c', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.toPosition());
            //Screen.PrintBoard(board);

            Console.ReadLine();

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

