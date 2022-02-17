using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player = GameLogic.Player;
using GameBoard = GameLogic.GameBoard;

namespace GameLogic
{
    public class Match
    {
        private GameBoard m_GameBoard;
        private Player m_ActivePlayer;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Player m_PlayerWon = null;
        private GameAi m_GameAi;
        int m_MoveCount = 0;
        public event Action<int> ColumnFull;
        public event Action<eEndGameCausation, Player> EndGame;

        // Public Properties
        public GameBoard GameBoard
        {
            get { return m_GameBoard; }
        }

        public Player ActivePlayer
        {
            get { return m_ActivePlayer; }
        }

        public Player PlayerOne
        {
            get { return m_PlayerOne; }
        }

        public Player PlayerTwo
        {
            get { return m_PlayerTwo; }
        }

        // Constructor
        public Match(GameBoard i_GameBoard, Player i_PlayerOne, Player i_PlayerTwo)
        {
            m_GameBoard = i_GameBoard;
            m_PlayerOne = i_PlayerOne;
            m_PlayerTwo = i_PlayerTwo;
            m_ActivePlayer = m_PlayerOne;
            m_GameAi = new GameAi(m_GameBoard);
        }

        // Public Methods
        public void PlaceToken(int i_ChosenColumn)
        {
            int chosenColumn = i_ChosenColumn;

            do
            {
                if (m_ActivePlayer.IsComputer)
                {
                    chosenColumn = m_GameAi.ChooseColumn();
                }

                m_GameBoard.PlaceToken(chosenColumn, m_ActivePlayer.ID);
                m_MoveCount++;
                if (m_GameBoard.FourInARowExists(m_ActivePlayer.ID))
                {
                    m_ActivePlayer.Score++;
                    m_PlayerWon = m_ActivePlayer;
                    OnEndGame(eEndGameCausation.ForInARow);
                }
                else if (m_MoveCount == GameBoard.NumOfTiles)
                {
                    m_PlayerWon = null;
                    OnEndGame(eEndGameCausation.Tie);
                }

                if (m_GameBoard.IsColumnFull(chosenColumn))
                {
                    OnColumnFull(chosenColumn);
                }

                updateActivePlayer();
            } while (m_ActivePlayer.IsComputer);
         
        }

        public void NewRound()
        {
            m_MoveCount = 0;
            m_GameBoard.ClearBoard();
        }
        
        protected virtual void OnColumnFull(int i_Column)
        {
            if(ColumnFull != null)
            {
                ColumnFull.Invoke(i_Column);
            }
        }

        protected virtual void OnEndGame(eEndGameCausation i_EndGameCaustion)
        {
            if(EndGame != null)
            {   
                EndGame.Invoke(i_EndGameCaustion, m_ActivePlayer);
            }
        }

        // Private Methods
        private void updateActivePlayer()
        {
            
            if (m_ActivePlayer.ID == 1)
            {
                m_ActivePlayer = m_PlayerTwo;
            }
            else
            {
                m_ActivePlayer = m_PlayerOne;
            }

            if (m_MoveCount == 0 && m_PlayerWon != null)
            {
                m_ActivePlayer = m_PlayerWon;
            }
        }
    }

    public enum eEndGameCausation
    {
        ForInARow,
        Tie
    }
}