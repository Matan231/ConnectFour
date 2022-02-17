using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameAi = GameLogic.GameAi;
using GameBoardm = GameLogic.GameBoard;

namespace GameLogic
{
    public class Player
    {
        // $G$ CSS-999 (-3) this members should be readonly (first two).
        private readonly int m_ID;
        private readonly string m_PlayerName;
        private int m_PlayerScore;
        private bool m_IsQuiting;
        private bool m_IsComputer = false;

        // Public Properties
        public string Name
        {
            get { return m_PlayerName; }
        }

        public int Score
        {
            get { return m_PlayerScore; }
            set { m_PlayerScore = value; }
        }

        public int ID
        {
            get { return m_ID; }
        }

        public bool IsQuiting
        {
            get { return m_IsQuiting; }
            set { m_IsQuiting = value; }
        }

        public bool IsComputer
        {
            get { return m_IsComputer; }
        }

        // Constructors
        public Player(string i_PlayerName, int i_id)
        {
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
            m_ID = i_id;
            m_IsQuiting = false;
        }

        public Player(string i_PlayerName, int i_id, bool i_IsComputer)
        {
            m_PlayerName = i_PlayerName;
            m_PlayerScore = 0;
            m_ID = i_id;
            m_IsQuiting = false;
            m_IsComputer = i_IsComputer;
        }
    }
}