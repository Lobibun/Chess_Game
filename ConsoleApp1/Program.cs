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
                Console.Write("\nOrigem: ");
                Position origen = Screen.ReadChessPosition().toPosition();

                bool[,] PosiblePosition = match.board.piece(origen).PossibleMovements();

                Console.Clear();
                Screen.PrintBoard(match.board, PosiblePosition);

                Console.Write("\nDestino: ");
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

