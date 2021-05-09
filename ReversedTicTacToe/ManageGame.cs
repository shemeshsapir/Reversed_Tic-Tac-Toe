namespace ReversedTicTacToe
{
    public class ManageGame
    {
        private GameBoard m_GameBoard;
        private eGameMode m_GameMode;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private ComputerAI m_Computer;
        private int m_CountMoves;
        private bool m_PlayerQuitGame;

        public enum eGameState
        {
            PlayerWon,
            ComputerWon,
            Tie
        }

        public enum eGameMode
        {
            AgainstPlayer,
            AgainstComputer
        }

        public ManageGame()
        {
            m_CountMoves = 0;
            m_PlayerQuitGame = false;
        }

        public void StartGame()
        {
            ManageUI.WelcomeMessage();
            bool isStillRounding = true;
            int boardSize = ManageUI.GetBoardSize();
            setNewBoard(boardSize, boardSize);

            m_PlayerOne = new Player(ManageUI.GetPlayerName(), GameBoard.eSymbols.SymbolX);
            
            if (ManageUI.ChooseGameMode() == 1)
            {
                m_PlayerTwo = new Player(ManageUI.GetPlayerName(), GameBoard.eSymbols.SymbolO);
                m_GameMode = eGameMode.AgainstPlayer;
                playAgainstPlayer(ref m_PlayerQuitGame);
            }
            else
            {
                m_Computer = new ComputerAI();
                m_GameMode = eGameMode.AgainstComputer;
                playAgainstComputer(ref m_PlayerQuitGame);
            }

            while (isStillRounding)
            {
                if (ManageUI.IsReMatch())
                {
                    setNewBoard(boardSize, boardSize);
                    newRound(m_GameMode);
                }
                else
                {
                    checkResults();
                    isStillRounding = false;
                } 
            }
        }

        private void checkResults()
        {
            ManageUI.ClearScreen();
            if (m_GameMode == eGameMode.AgainstPlayer)
            {
                if (m_PlayerOne.PlayerScore > m_PlayerTwo.PlayerScore)
                {
                    ManageUI.DisplayResults(eGameState.PlayerWon, m_PlayerOne.PlayerName, m_PlayerTwo.PlayerName, m_PlayerOne.PlayerScore, m_PlayerTwo.PlayerScore);
                }
                else if (m_PlayerOne.PlayerScore < m_PlayerTwo.PlayerScore)
                {
                    ManageUI.DisplayResults(eGameState.PlayerWon, m_PlayerTwo.PlayerName, m_PlayerOne.PlayerName, m_PlayerTwo.PlayerScore, m_PlayerOne.PlayerScore);
                }
                else
                {
                    ManageUI.DisplayResults(eGameState.Tie, m_PlayerTwo.PlayerName, m_PlayerOne.PlayerName, m_PlayerTwo.PlayerScore, m_PlayerOne.PlayerScore);
                }
            }
            else
            {
                if (m_PlayerOne.PlayerScore > m_Computer.ComputerScore)
                {
                    ManageUI.DisplayResults(eGameState.PlayerWon, m_PlayerOne.PlayerName, m_Computer.ComputerName, m_PlayerOne.PlayerScore, m_Computer.ComputerScore);
                }
                else if (m_PlayerOne.PlayerScore < m_Computer.ComputerScore)
                {
                    ManageUI.DisplayResults(eGameState.ComputerWon, m_Computer.ComputerName, m_PlayerOne.PlayerName, m_Computer.ComputerScore, m_PlayerOne.PlayerScore);
                }
                else
                {
                    ManageUI.DisplayResults(eGameState.Tie, m_Computer.ComputerName, m_PlayerOne.PlayerName, m_Computer.ComputerScore, m_PlayerOne.PlayerScore);
                }
            }
        }

        private void newRound(eGameMode i_GameMode)
        {
            m_CountMoves = 0;
            m_PlayerQuitGame = false;

            switch (i_GameMode)
            {
                case eGameMode.AgainstPlayer:
                    playAgainstPlayer(ref m_PlayerQuitGame);
                    break;
                case eGameMode.AgainstComputer:
                    playAgainstComputer(ref m_PlayerQuitGame);
                    break;
            }
        }

        private void setNewBoard(int i_Rows, int i_Columns)
        {
            m_GameBoard = new GameBoard(i_Rows, i_Columns);
        }

        private void playAgainstPlayer(ref bool io_IsQuit)
        {
            bool playerOneFlag = true, isLose = false;
            int rowIndex = -1, columnIndex = -1;

            while (!m_GameBoard.IsBoardFull(m_CountMoves) && !io_IsQuit)
            {
                if (m_CountMoves == 0)
                {
                    ManageUI.DrawBoard(m_GameBoard);
                }

                if (playerOneFlag)
                {
                    playerMakeMove(ref m_GameBoard, ref io_IsQuit, ref rowIndex, ref columnIndex, ref isLose, ref m_PlayerOne, ref m_PlayerTwo);
                    playerOneFlag = false;
                }
                else
                {
                    playerMakeMove(ref m_GameBoard, ref io_IsQuit, ref rowIndex, ref columnIndex, ref isLose, ref m_PlayerTwo, ref m_PlayerOne);
                    playerOneFlag = true;
                }

                m_CountMoves++;

                if (isLose)
                {
                    break;
                }
            }

            if (m_GameBoard.IsBoardFull(m_CountMoves) && !isLose)
            {
                ManageUI.DisplayResults(eGameState.Tie, m_PlayerOne.PlayerName, m_PlayerTwo.PlayerName, m_PlayerOne.PlayerScore, m_PlayerTwo.PlayerScore);
            }
        }

        private void playAgainstComputer(ref bool io_IsQuit)
        {
            int rowIndex = -1, columnIndex = -1;
            bool playerFlag = true, isLose = false;

            while (!m_GameBoard.IsBoardFull(m_CountMoves) && !io_IsQuit)
            {
                if (m_CountMoves == 0)
                {
                    ManageUI.DrawBoard(m_GameBoard);
                }

                if (playerFlag)
                {
                    ManageUI.GetCellToInsertSymbol(m_PlayerOne.PlayerSymbol, ref m_GameBoard, ref io_IsQuit, m_PlayerOne.PlayerName, ref rowIndex, ref columnIndex);
                    
                    if (!io_IsQuit)
                    {
                        m_GameBoard.SetCellToSymbol(rowIndex - 1, columnIndex - 1, m_PlayerOne.PlayerSymbol);
                    }
                    
                    ManageUI.DrawBoard(m_GameBoard);
                    if (m_GameBoard.IsFoundSeries(m_PlayerOne.PlayerSymbol) || io_IsQuit)
                    {
                        m_Computer.ComputerScore++;
                        ManageUI.DisplayResults(eGameState.ComputerWon, m_Computer.ComputerName, m_PlayerOne.PlayerName, m_Computer.ComputerScore, m_PlayerOne.PlayerScore);
                        isLose = true;
                    }

                    playerFlag = false;
                }
                else
                {
                    m_Computer.MakeSmartMove(ref m_GameBoard, m_Computer.ComputerSymbol, out rowIndex, out columnIndex);
                    ManageUI.DrawBoard(m_GameBoard);
                    if (m_GameBoard.IsFoundSeries(m_Computer.ComputerSymbol))
                    {
                        m_PlayerOne.PlayerScore++;
                        ManageUI.DisplayResults(eGameState.PlayerWon, m_PlayerOne.PlayerName, m_Computer.ComputerName, m_PlayerOne.PlayerScore, m_Computer.ComputerScore);
                        isLose = true;
                    }

                    playerFlag = true;
                }

                m_CountMoves++;

                if (isLose)
                {
                    break;
                }
            }

            if (!isLose)
            {
                ManageUI.DisplayResults(eGameState.Tie, m_PlayerOne.PlayerName, m_Computer.ComputerName, m_PlayerOne.PlayerScore, m_Computer.ComputerScore);
            }
        }

        private void playerMakeMove(ref GameBoard io_GameBoard, ref bool io_IsQuit, ref int io_RowIndex, ref int io_ColumnIndex, ref bool io_IsLose, ref Player io_CurrentPlayer, ref Player io_SecondPlayer)
        {
            ManageUI.GetCellToInsertSymbol(io_CurrentPlayer.PlayerSymbol, ref m_GameBoard, ref io_IsQuit, io_CurrentPlayer.PlayerName, ref io_RowIndex, ref io_ColumnIndex);

            if (!io_IsQuit)
            {
                m_GameBoard.SetCellToSymbol(io_RowIndex - 1, io_ColumnIndex - 1, io_CurrentPlayer.PlayerSymbol);
            }

            ManageUI.DrawBoard(m_GameBoard);
            if (m_GameBoard.IsFoundSeries(io_CurrentPlayer.PlayerSymbol) || io_IsQuit)
            {
                io_SecondPlayer.PlayerScore++;
                ManageUI.DisplayResults(eGameState.PlayerWon, io_SecondPlayer.PlayerName, io_CurrentPlayer.PlayerName, io_SecondPlayer.PlayerScore, io_CurrentPlayer.PlayerScore);
                io_IsLose = true;
            }
        }
    }
}