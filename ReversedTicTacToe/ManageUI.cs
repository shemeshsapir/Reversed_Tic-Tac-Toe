using System.Text;

namespace ReversedTicTacToe
{
    public class ManageUI
    {
        public static void WelcomeMessage()
        {
            string welcomeMessage = string.Format(
 @"Hello! Welcom to the Reversed-Tic-Tac-Toe game!
In order to win this game, you need to PREVENT your symbol of making a series of it (by row/column/diagnoal).
Each round you will be asked to pick any cell in the board by choosing row and column indexes.
You can press the 'Q' sign at any time to end the game.
Have fun, and the best will win!");
            System.Console.WriteLine(welcomeMessage);
            System.Console.WriteLine();
        }

        public static string GetPlayerName()
        {
            System.Console.WriteLine("Please enter the player's name:");
            return System.Console.ReadLine();
        }

        public static int ChooseGameMode()
        {
            int gameMode = 0;
            bool isValidInput = false;
            string userChoice;

            System.Console.WriteLine("Please enter the game mode: 1. VS another Player 2. VS AI Computer:");
            userChoice = System.Console.ReadLine();
            isValidInput = int.TryParse(userChoice, out gameMode);

            while (!isValidInput || (gameMode != 1 && gameMode != 2))
            {
                System.Console.WriteLine("Wrong Input! Please choose 1 or 2 only!");
                userChoice = System.Console.ReadLine();
                isValidInput = int.TryParse(userChoice, out gameMode);
            }

            return gameMode;
        }

        public static int GetBoardSize()
        {
            System.Console.WriteLine("Please enter the board's size; Odd number between 3 to 9 only!");
            string userChoice = System.Console.ReadLine();
            bool isValidBoardSize = int.TryParse(userChoice, out int boardSize);

            while (!isValidBoardSize || !(boardSize >= 3 && boardSize <= 9) || !(boardSize % 2 == 1))
            {
                System.Console.WriteLine("Wrong input! Please choose an odd number between 3 to 9!");
                userChoice = System.Console.ReadLine();
                isValidBoardSize = int.TryParse(userChoice, out boardSize);
            }

            return boardSize;
        }

        public static void GetCellToInsertSymbol(GameBoard.eSymbols i_PlayerSymbol, ref GameBoard io_GameBoard, ref bool io_Quit, string i_PlayerName, ref int io_RowIndex, ref int io_ColumnIndex)
        {
            int boardSize = io_GameBoard.Rows;
            TurnMessage(i_PlayerName);
            UpdateIndexes(ref io_RowIndex, ref io_ColumnIndex, ref io_Quit);

            if (!io_Quit)
            {
                CheckValidRange(ref io_RowIndex, ref io_ColumnIndex, boardSize);

                while (!CheckIfCellAvailable(ref io_GameBoard, ref io_RowIndex, ref io_ColumnIndex) && !io_Quit)
                {
                    UpdateIndexes(ref io_RowIndex, ref io_ColumnIndex, ref io_Quit);
                    CheckValidRange(ref io_RowIndex, ref io_ColumnIndex, boardSize);
                }
            }
        }

        public static void CheckValidRange(ref int io_RowIndex, ref int io_ColumnIndex,  int i_BoardSize)
        {
            bool isQuit = false;

            while (io_RowIndex < 0 || io_RowIndex > i_BoardSize || io_ColumnIndex < 0 || io_ColumnIndex > i_BoardSize)
            {
                System.Console.WriteLine("Number out of board range:");
                UpdateIndexes(ref io_RowIndex, ref io_ColumnIndex, ref isQuit);
            }
        }

        public static bool CheckIfCellAvailable(ref GameBoard io_GameBoard, ref int io_Row, ref int io_Column)
        {
            bool isAvailable = true;

            if (!io_GameBoard.BoardCells[io_Row - 1, io_Column - 1].IsAvailable)
            {
                System.Console.WriteLine("Cell not available, choose another one please:");
                isAvailable = false;
            }

            return isAvailable;
        }

        public static void UpdateIndexes(ref int io_RowIndex, ref int io_ColumnIndex, ref bool io_IsQuit)
        {
            System.Console.Write("Please enter the row index: ");
            string indexRowChoice = System.Console.ReadLine();

            io_IsQuit = CheckIfQuit(indexRowChoice);

            if (!io_IsQuit)
            {
                System.Console.Write("Please enter the column index: ");
                string indexColumnChoice = System.Console.ReadLine();
                io_IsQuit = CheckIfQuit(indexColumnChoice);

                if (!io_IsQuit)
                {
                    bool isValidRow = int.TryParse(indexRowChoice, out io_RowIndex);
                    bool isValidColumn = int.TryParse(indexColumnChoice, out io_ColumnIndex);

                    while (!isValidRow || !isValidColumn)
                    {
                        System.Console.WriteLine("Wrong input! Please choose valid numbers: ");
                        System.Console.Write("Please enter the row index: ");
                        indexRowChoice = System.Console.ReadLine();

                        System.Console.Write("Please enter the column index: ");
                        indexColumnChoice = System.Console.ReadLine();

                        isValidRow = int.TryParse(indexRowChoice, out io_RowIndex);
                        isValidColumn = int.TryParse(indexColumnChoice, out io_ColumnIndex);
                    }
                }
            }
        }

        public static void TurnMessage(string i_PlayerName)
        {
            string message = string.Format(
@"{0} it's your turn!", 
            i_PlayerName);

            System.Console.WriteLine(message);
        }

        public static bool CheckIfQuit(string i_InputFromUser)
        {
            bool isQuit = false;

            if (i_InputFromUser.Equals("Q"))
            {
                isQuit = true;
            }

            return isQuit;
        }

        public static void DrawBoard(GameBoard i_GameBoard)
        {
            StringBuilder gameBoardPrint = new StringBuilder();
            int gameBoardSize = i_GameBoard.Rows;
            ManageUI.ClearScreen();

            gameBoardPrint.Append("  ");

            for (int i = 0; i < gameBoardSize; i++)
            {
                gameBoardPrint.Append(i + 1).Append("   ");
            }

            gameBoardPrint.AppendLine();

            for (int i = 0; i < gameBoardSize; i++)
            {
                gameBoardPrint.Append(i + 1);
                for (int j = 0; j < gameBoardSize; j++)
                {
                    if (i_GameBoard.BoardCells[i, j].IsAvailable)
                    {
                        gameBoardPrint.Append("|   ");
                    }
                    else
                    {
                        gameBoardPrint.Append("| ").Append((char)i_GameBoard.BoardCells[i, j].CurrentSymbol).Append(" ");
                    }
                }

                gameBoardPrint.Append('|').AppendLine();
                gameBoardPrint.Append(" ").Append('=', (gameBoardSize * 4) + 1).AppendLine();
            }

            System.Console.WriteLine(gameBoardPrint);
        }

        public static void DisplayResults(ManageGame.eGameState i_GameState, string i_FirstPlayerName, string i_SecondPlayerName, int i_FirstPlayerScore, int i_SecondPlayerScore)
        {
            int state = (int) i_GameState;
            StringBuilder stateMessage = new StringBuilder();

            switch (i_GameState)
            {
                case ManageGame.eGameState.PlayerWon:
                    stateMessage.Append("Player ").Append(i_FirstPlayerName).Append(" Won! Congratulations!").AppendLine();
                    break;

                case ManageGame.eGameState.ComputerWon:
                    stateMessage.Append("Computer won! better luck the next time!").AppendLine();
                    break;

                case ManageGame.eGameState.Tie:
                    stateMessage.Append("It's a TIE!!!").AppendLine();
                    break;
            }

            string displayMessage = string.Format(
@"{0}
The score board is:
{1} score: {2}
{3} score: {4}",
            stateMessage,
            i_FirstPlayerName,
            i_FirstPlayerScore.ToString(),
            i_SecondPlayerName,
            i_SecondPlayerScore.ToString());

            System.Console.WriteLine(displayMessage);
        }

        public static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static bool IsReMatch()
        {
            System.Console.Write("Do you want to play another round? y/n: ");
            string input = System.Console.ReadLine();
            bool isAnotherRound = false, isValidInput = false;

            while (!isValidInput)
            {
                if (!input.Equals("y") && !input.Equals("n") && !input.Equals("Q"))
                {
                    System.Console.WriteLine("Wrong input! Please enter only y/n or Q for retiere");
                    input = System.Console.ReadLine();
                }
                else
                {
                    isValidInput = true;
                }
            }

            if (input.Equals("y"))
            {
                isAnotherRound = true;
            }

            return isAnotherRound;
        }
    }
}
