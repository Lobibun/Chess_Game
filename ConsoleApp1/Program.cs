using Chess;
using Chess_Console.Chess;
using Chessboard;
class Program
{

    static void Main(string[] args)
    {
        try
        {
           ChessMatch match = new ChessMatch();

            while (!match.finished)
            {
                Console.Clear();
                Screen.PrintBoard(match.board);
                Console.WriteLine("\nOrigem: ");
                Position origen = Screen.ReadChessPosition().toPosition();
                Console.WriteLine("Destino: ");
                Position destiny = Screen.ReadChessPosition().toPosition();

                match.PerformMovement(origen, destiny);
            }

            

            Console.ReadLine();

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

