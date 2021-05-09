using System;
using System.Collections.Generic;

namespace ReversedTicTacToe
{
    public class ComputerAI
    {
        private readonly GameBoard.eSymbols r_ComputerSymbol;
        private readonly string r_ComputerName;
        private readonly Random r_RandomMove;
        private int m_ComputerScore;

        public ComputerAI()
        {
            r_ComputerName = "Computer";
            r_ComputerSymbol = GameBoard.eSymbols.SymbolO;
            m_ComputerScore = 0;
            r_RandomMove = new Random();
        }

        public int ComputerScore
        {
            get
            {
                return m_ComputerScore;
            }

            set
            {
                m_ComputerScore = value;
            }
        }

        public string ComputerName
        {
            get
            {
                return r_ComputerName;
            }
        }

        public GameBoard.eSymbols ComputerSymbol
        {
            get
            {
                return r_ComputerSymbol;
            }
        }

        public void MakeSmartMove(ref GameBoard io_GameBoard, GameBoard.eSymbols i_ComputerSymbol, out int o_Row, out int o_Column)
        {
            Location calculateMove, currentMove;
            int boardSize = io_GameBoard.Rows;
            List<Location> availableMoves = new List<Location>();
            List<Location> goodMoves = new List<Location>();

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    currentMove = new Location(i, j);

                    if (io_GameBoard.BoardCells[i, j].IsAvailable)
                    {
                        availableMoves.Add(currentMove);

                        if (!IsEndTheGame(ref io_GameBoard, currentMove, i_ComputerSymbol))
                        {
                            goodMoves.Add(currentMove);
                        }
                    }
                }
            }

            if (goodMoves.Count > 0)
            {
                calculateMove = goodMoves[r_RandomMove.Next(goodMoves.Count)];
            }
            else
            {
                calculateMove = availableMoves[r_RandomMove.Next(availableMoves.Count)];
            }

            o_Row = calculateMove.RowIndex;
            o_Column = calculateMove.ColumnIndex;

            io_GameBoard.SetCellToSymbol(o_Row, o_Column, GameBoard.eSymbols.SymbolO);
        }

        public bool IsEndTheGame(ref GameBoard io_GameBoard, Location i_Location, GameBoard.eSymbols i_SymbolToCheck)
        {
            bool isEndGame = false;
            int row = i_Location.RowIndex, column = i_Location.ColumnIndex;

            if (io_GameBoard.BoardCells[row, column].IsAvailable)
            {
                io_GameBoard.SetCellToSymbol(row, column, i_SymbolToCheck);

                if (io_GameBoard.IsFoundSeries(i_SymbolToCheck))
                {
                    isEndGame = true;
                }

                io_GameBoard.SetCellToEmpty(row, column);
            }

            return isEndGame;
        }

        public struct Location
        {
            private int m_ColumnIndex;
            private int m_RowIndex;

            public Location(int i_Row, int i_Column)
            {
                m_RowIndex = i_Row;
                m_ColumnIndex = i_Column;
            }

            public int ColumnIndex
            {
                get
                {
                    return m_ColumnIndex;
                }

                set
                {
                    m_ColumnIndex = value;
                }
            }

            public int RowIndex
            {
                get
                {
                    return m_RowIndex;
                }

                set
                {
                    m_RowIndex = value;
                }
            }
        }
    }
}