namespace ReversedTicTacToe
{
    public class Player
    {
        private readonly GameBoard.eSymbols r_PlayerSymbol;
        private readonly string r_PlayerName;
        private int m_PlayerScore;

        public Player(string i_PlayerName, GameBoard.eSymbols i_PlayerSymbol)
        {
            r_PlayerName = i_PlayerName;
            r_PlayerSymbol = i_PlayerSymbol;
            m_PlayerScore = 0;
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public GameBoard.eSymbols PlayerSymbol
        {
            get
            {
                return r_PlayerSymbol; 
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }

            set
            {
                m_PlayerScore = value;
            }
        }
    }
}