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
                    Screen.PrintMatch(match);


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

                    if (match.PieceToPromote != null)
                    {
                        Console.Clear();
                        Screen.PrintBoard(match.board);

                        Console.Write("Promoção! Escolha a peça (B=Bispo, C=Cavalo, T=Torre, R=Rainha): ");
                        string chosenPiece = Console.ReadLine().ToUpper();
                        while (chosenPiece != "B" && chosenPiece != "C" && chosenPiece != "T"  && chosenPiece != "R")
                        {
                            Console.Write("Valor inválido! Digite novamente (B, C, T, R): ");
                            chosenPiece = Console.ReadLine().ToUpper();
                        }
                            match.PromotePiece(chosenPiece);
                    }
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nERRO DE ENTRADA: O formato da posição digitada é inválido");
                    Console.WriteLine("Pressione ENTER para tentar novamente...");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Screen.PrintMatch(match);

        }
        catch (BoardException e)
        {
            Console.WriteLine(e.Message);
        }
    }
}

