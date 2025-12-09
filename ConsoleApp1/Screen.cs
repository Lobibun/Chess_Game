using Chessboard;
namespace Chess
{
    class Screen
    {
        public static void PrintMatch(ChessMatch match)
        {
            PrintBoard(match.board);
            Console.WriteLine();
            PrintCapturedPieces(match);
            Console.WriteLine($"\nTurno {match.Turn}");
            
            if (!match.finished)
            {
                Console.WriteLine($"Aguardando jogada: {match.CurrentPlayer}");
                if (match.Check)
                {
                    Console.WriteLine("Você está em Xeque!");
                }
            }
            else
            {
                Console.WriteLine("Xequemate!");
                Console.WriteLine($"Vencedor: {match.CurrentPlayer} ");
            }
        }
        public static void PrintCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            PrintSets(match.CapturedPieces(Color.Branca));
            Console.Write("\nPretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            PrintSets(match.CapturedPieces(Color.Preta));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSets(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach (Piece x in set)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }
        public static void PrintBoard(Board board)
        {
            for(int i=0; i<board.Lines; i++)
            {
                Console.Write( 8 - i + " ");
                for (int n=0; n<board.Columns; n++)
                {
                    
                        PrintPiece(board.piece(i, n));
                      
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(Board board, bool[,] PosiblePosition)
        {
            ConsoleColor OriginalBackGround = Console.BackgroundColor;
            ConsoleColor ChangedBackGround = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int n = 0; n < board.Columns; n++)
                {
                    if (PosiblePosition[i, n])
                    {
                        Console.BackgroundColor = ChangedBackGround;
                    }
                    else
                    {
                        Console.BackgroundColor= OriginalBackGround;
                    }

                        PrintPiece(board.piece(i, n));
                    Console.BackgroundColor = OriginalBackGround;


                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = OriginalBackGround;
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {

                if (piece.color == Color.Branca)
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
                Console.Write(" ");
            }

        }
    }
}
