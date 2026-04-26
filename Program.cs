using System;

namespace Connect4
{
    class Program
    {
        // Using a 2D array (multidimensional) to represent the physical board.
        // 6 rows represent vertical height, 7 columns represent horizontal width.
        static char[,] board = new char[6, 7];
        
        // 'X' usually goes first. Player 'O' is either the Robot or Friend.
        static char currentPlayer = 'X';
        static bool isVsAI = false;
        
        // Track total moves to identify a 'Tie/Draw' state (max moves = 42).
        static int totalMoves = 0;

        static void Main(string[] args)
        {
            // Set up the initial state of the 2D array placeholders.
            InitializeBoard();
            
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== WELCOME TO CONNECT 4: STRATEGY ENGINE ===");
            Console.ResetColor();

            // Menu Validation: This loop ensures the user only enters '1' or '2'.
            // Using '?? ""' handles the nullable warning from Console.ReadLine.
            string choice = "";
            while (choice != "1" && choice != "2")
            {
                Console.WriteLine("Press '1' to challenge the AI Robot");
                Console.WriteLine("Press '2' to play Local Multiplayer");
                Console.Write("Selection: ");
                choice = Console.ReadLine() ?? "";

                if (choice != "1" && choice != "2")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(">> Invalid selection. Please enter 1 or 2.\n");
                    Console.ResetColor();
                }
            }
            
            isVsAI = (choice == "1");

            // PRIMARY GAME LOOP 
            while (true)
            {
                Console.Clear();
                DrawBoard();
                
                if (isVsAI && currentPlayer == 'O')
                {
                    // If Solo mode is active, the computer takes its turn automatically.
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Robot is calculating move...");
                    System.Threading.Thread.Sleep(1000); 
                    RobotMove();
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer}'s turn.");
                    Console.WriteLine("Enter column (1-7) or 'Q' to Quit:");
                    string input = Console.ReadLine() ?? "";

                    // User Story: Allow the user to terminate the session gracefully.
                    if (input.ToUpper() == "Q")
                    {
                        Console.WriteLine("Game terminated by user. Goodbye!");
                        return; 
                    }

                    // Validate that the input is a number and within the 1-7 range.
                    if (int.TryParse(input, out int column) && column >= 1 && column <= 7)
                    {
                        // Array indices are 0-based, so we subtract 1 from the user's input.
                        if (!PlacePiece(column - 1)) 
                        { 
                            Console.WriteLine("Column is full! Press any key to try again..."); 
                            Console.ReadKey(); 
                            continue; 
                        }
                    }
                    else 
                    { 
                        Console.WriteLine("Invalid input! Please enter a number between 1-7, or 'Q'."); 
                        Console.ReadKey();
                        continue; 
                    }
                }

                totalMoves++;

                // Win/Draw Evaluation Phase
                if (CheckWin()) { EndGame(false); break; }
                if (totalMoves == 42) { EndGame(true); break; }

                // Swap turns using a ternary operator for concise logic.
                currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
            }
        }

        // CORE GAME MECHANICS 

        // Simple AI: Picks a random column and checks if it's available.
        static void RobotMove() {
            Random rand = new Random();
            while (true) { if (PlacePiece(rand.Next(0, 7))) break; }
        }

        // Nested loops are used here to visit every 'cell' in the 2D array.
        static void InitializeBoard() {
            for (int r = 0; r < 6; r++)
                for (int c = 0; c < 7; c++) board[r, c] = '.';
        }

        // Renders the board to the console with color-coded tokens for UX.
        static void DrawBoard() {
            for (int r = 0; r < 6; r++) {
                for (int c = 0; c < 7; c++) {
                    if (board[r, c] == 'X') Console.ForegroundColor = ConsoleColor.Red;
                    else if (board[r, c] == 'O') Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(board[r, c] + " ");
                    Console.ResetColor();
                }
                Console.WriteLine(); // New line after each row
            }
            Console.WriteLine("1 2 3 4 5 6 7\n");
        }

        // Gravity Logic: Iterates from bottom (row 5) to top (row 0).
        // The piece 'lands' in the first empty ('.') slot found.
        static bool PlacePiece(int col) {
            for (int r = 5; r >= 0; r--) {
                if (board[r, col] == '.') { board[r, col] = currentPlayer; return true; }
            }
            return false;
        }

        // Win Condition Logic: Scans for four identical pieces in a row.
        static bool CheckWin() {
            //  Horizontal Scanning
            for (int r = 0; r < 6; r++)
                for (int c = 0; c < 4; c++) // Only need to check up to col 4 to find a row of 4
                    if (board[r, c] != '.' && board[r, c] == board[r, c+1] && board[r, c] == board[r, c+2] && board[r, c] == board[r, c+3]) return true;

            //  Vertical Scanning
            for (int r = 0; r < 3; r++) // Only need to check up to row 3 to find a stack of 4
                for (int c = 0; c < 7; c++)
                    if (board[r, c] != '.' && board[r, c] == board[r+1, c] && board[r, c] == board[r+2, c] && board[r, c] == board[r+3, c]) return true;

            // Ascending Diagonal (Bottom-Left to Top-Right)
            for (int r = 3; r < 6; r++)
                for (int c = 0; c < 4; c++)
                    if (board[r, c] != '.' && board[r, c] == board[r-1, c+1] && board[r, c] == board[r-2, c+2] && board[r, c] == board[r-3, c+3]) return true;

            // Descending Diagonal (Top-Left to Bottom-Right)
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 4; c++)
                    if (board[r, c] != '.' && board[r, c] == board[r+1, c+1] && board[r, c] == board[r+2, c+2] && board[r, c] == board[r+3, c+3]) return true;

            return false;
        }

        // Final UI output sequence.
        static void EndGame(bool isTie) {
            Console.Clear();
            DrawBoard();
            if (isTie) {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\n!!! GAME OVER: IT'S A TIE !!!");
                Console.WriteLine("Strategic deadlock reached—the board is full.");
            } else {
                string winnerName = (isVsAI && currentPlayer == 'O') ? "THE ROBOT" : $"PLAYER {currentPlayer}";
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n*******************************");
                Console.WriteLine($"   CONGRATULATIONS {winnerName}!!   ");
                Console.WriteLine("       YOU ARE THE CHAMPION!     ");
                Console.WriteLine("*******************************");
            }
            Console.ResetColor();
            Console.WriteLine("\nPress any key to close the application...");
            Console.ReadKey();
        }
    }
}