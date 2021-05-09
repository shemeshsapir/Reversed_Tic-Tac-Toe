namespace ReversedTicTacToe
{
    public class GameBoard
    {
        private readonly int r_Rows;
        private readonly int r_Columns;
        private readonly int r_BoardSize;
        private readonly Cell<eSymbols>[,] r_BoardCells = null;

        public enum eSymbols
        {
            SymbolX = 'X',
            SymbolO = 'O'
        }

        public GameBoard(int i_Rows, int i_Columns)
        {
            r_Rows = i_Rows;
            r_Columns = i_Columns;
            r_BoardSize = r_Columns * r_Rows;
            r_BoardCells = new Cell<eSymbols>[r_Rows, r_Columns];
            InitializeBoard();
        }

        public Cell<eSymbols>[,] BoardCells
        {
            get
            {
                return r_BoardCells;
            }
        }

        public int Rows
        {
            get
            {
                return r_Rows;
            }
        }

        public int Columns
        {
            get
            {
                return r_Columns;
            }
        }

        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < r_Rows; i++)
            {
                for (int j = 0; j < r_Columns; j++)
                {
                    r_BoardCells[i, j] = new Cell<eSymbols>();
                }
            }
        }

        public bool IsFullRow(eSymbols i_PlayerSymbol)
        {
            int sameSymbolInRow = 0;
            bool isFull = false;

            for (int i = 0; i < r_Rows; i++)
            {
                sameSymbolInRow = 0;

                for (int j = 0; j < r_Columns; j++)
                {
                    if (!r_BoardCells[i, j].IsAvailable && r_BoardCells[i, j].CurrentSymbol == i_PlayerSymbol)
                    {
                        sameSymbolInRow++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (sameSymbolInRow == r_Rows)
                {
                    isFull = true;
                    break;
                }
            }

            return isFull;
        }

        public bool IsFullColumn(eSymbols i_PlayerSymbol)
        {
            int sameSymbolInColumn = 0;
            bool isFull = false;

            for (int i = 0; i < r_Rows; i++)
            {
                sameSymbolInColumn = 0;

                for (int j = 0; j < r_Columns; j++)
                {
                    if (!r_BoardCells[j, i].IsAvailable && r_BoardCells[j, i].CurrentSymbol.Equals(i_PlayerSymbol))
                    {
                        sameSymbolInColumn++;
                    }
                }

                if (sameSymbolInColumn == r_Columns)
                {
                    isFull = true;
                    break;
                }
            }

            return isFull;
        }

        public bool IsFullLeftDiagnoal(eSymbols i_PlayerSymbol)
        {
            bool isFull = false;
            int sameSymbolInDiagnoal = 0;

            for (int i = 0; i < r_Rows; i++)
            {
                if (!r_BoardCells[i, i].IsAvailable && r_BoardCells[i, i].CurrentSymbol == i_PlayerSymbol)
                {
                    sameSymbolInDiagnoal++;
                }
                else
                {
                    break;
                }
            }

            if (sameSymbolInDiagnoal == r_Rows)
            {
                isFull = true;
            }

            return isFull;
        }

        public bool IsFullRightDiagnoal(eSymbols i_PlayerSymbol)
        {
            bool isFull = false;
            int colIndex = r_Columns - 1, sameSymbolInDiagnoal = 0;

            for (int i = 0; i < r_Rows; i++)
            {
                if (!r_BoardCells[i, colIndex].IsAvailable && r_BoardCells[i, colIndex].CurrentSymbol == i_PlayerSymbol)
                {
                    sameSymbolInDiagnoal++;
                }
                else
                {
                    break;
                }

                colIndex--;
            }

            if (sameSymbolInDiagnoal == r_Rows)
            {
                isFull = true;
            }

            return isFull;
        }

        public bool IsFoundSeries(GameBoard.eSymbols i_Symbol)
        {
            bool isFoundSeries = false;

            if (this.IsFullColumn(i_Symbol) || this.IsFullLeftDiagnoal(i_Symbol) || this.IsFullRightDiagnoal(i_Symbol) || this.IsFullRow(i_Symbol))
            {
                isFoundSeries = true;
            }

            return isFoundSeries;
        }

        public void SetCellToEmpty(int i_Row, int i_Column)
        {
            this.BoardCells[i_Row, i_Column].CurrentSymbol = default(eSymbols);
            this.BoardCells[i_Row, i_Column].IsAvailable = true;
        }

        public void SetCellToSymbol(int i_Row, int i_Column, eSymbols i_SymbolToInsert)
        {
            this.BoardCells[i_Row, i_Column].CurrentSymbol = i_SymbolToInsert;
            this.BoardCells[i_Row, i_Column].IsAvailable = false;
        }

        public bool IsBoardFull(int i_CountMoves)
        {
            return r_BoardSize == i_CountMoves;
        }
    }
}