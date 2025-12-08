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
                try
                {
                    Console.Clear();
                    Screen.PrintBoard(match.board);
                    Console.WriteLine($"\nTurno {match.Turn}");
                    Console.WriteLine($"Aguardando jogada: {match.CurrentPlayer}");


                    Console.Write("\nOrigem: ");
                    Position origin = Screen.ReadChessPosition().toPosition();
                    match.ValidadeOriginPosition(origin);
                    bool[,] PosiblePosition = match.board.piece(origin).PossibleMovements();

                    Console.Clear();
                    Screen.PrintBoard(match.board, PosiblePosition);

                    Console.Write("\nDestino: ");
                    Position destiny = Screen.ReadChessPosition().toPosition();
                    match.ValidadeDestinyPosition(origin, destiny);

                    match.MakePlay(origin, destiny);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }    

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

