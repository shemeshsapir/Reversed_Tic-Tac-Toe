namespace ReversedTicTacToe
{
    public class Cell<T>
    {
        private bool m_IsAvailable;
        private T m_CurrentSymbol;

        public Cell(T i_Symbol)
        {
            m_IsAvailable = true;
            m_CurrentSymbol = i_Symbol;
        }

        public Cell()
        {
            m_IsAvailable = true;
            m_CurrentSymbol = default(T);
        }
        
        public bool IsAvailable
        {
            get
            {
                return m_IsAvailable;
            }

            set
            {
                m_IsAvailable = value;
            }
        }

        public T CurrentSymbol
        {
            get
            {
                return m_CurrentSymbol;
            }

            set
            {
                m_CurrentSymbol = value;
            }
        }
    }
}