using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic
{
    public class GameBoard
    {
        private int m_ConnectSize;
        private readonly int m_Height;
        private readonly int m_Length;
        private readonly int m_NumOfTiles;
        private readonly int[,] m_Board;
        private int m_LastMoveRow;
        private int m_LastMoveCol;
        public event Action<int, int, int> TokenPlaced;

        // Public Properties
        public int ConnectSize
        {
            get { return m_ConnectSize; }
            set { m_ConnectSize = value; }
        }

        public int Height
        {
            get { return m_Height; }
        }

        public int Length
        {
            get { return m_Length; }
        }

        public int[,] Board
        {
            get { return m_Board; }
        }

        public int NumOfTiles
        {
            get { return m_NumOfTiles; }
        }

        public int LastMoveRow
        {
            get { return m_LastMoveRow; }
        }

        public int LastMoveCol
        {
            get { return m_LastMoveCol; }
        }

        // Constructor
        public GameBoard(int i_Height, int i_Length, int i_ConnectSize)
        {
            m_ConnectSize = i_ConnectSize;
            m_Height = i_Height;
            m_Length = i_Length;
            // $G$ DSN-001 (-5) Coins should be represented by either a struct, a class or an enum.
            m_Board = new int[m_Height, m_Length];
            m_NumOfTiles = m_Board.GetLength(0) * m_Board.GetLength(1);
            m_LastMoveRow = -1;
            m_LastMoveCol = -1;
        }

        // Public Methods
        public void PlaceToken(int i_chosenColumn, int i_ActivePlayer)
        {
            for (int i = m_Height - 1; i >= 0; i--)
            {
                if (m_Board[i, i_chosenColumn] == 0)
                {
                    m_LastMoveRow = i;
                    m_LastMoveCol = i_chosenColumn;
                    m_Board[m_LastMoveRow, m_LastMoveCol] = i_ActivePlayer;
                    break;
                }
            }

            On_TokenPlaced(i_ActivePlayer);
        }

        public bool IsColumnFull(int i_ChosenColumn)
        {
            bool isColumnFull = false;

            if (m_Board[0, i_ChosenColumn] != 0)
            {
                isColumnFull = true;
            }

            return isColumnFull;
        }

        public bool IsLegalColumn(int i_ChosenColumn)
        {
            bool isLegalColumn = false;
            if (i_ChosenColumn >= 0 && i_ChosenColumn < m_Length)
            {
                isLegalColumn = true;
            }

            return isLegalColumn;
        }

        public bool FourInARowExists(int i_PlayerID)
        {
            bool isFourInARowFound = checkHorizontals(i_PlayerID) || checkVetricals(i_PlayerID) || checkDiagonals(i_PlayerID);

            return isFourInARowFound;
        }

        public void ClearBoard()
        {
            for (int row = 0; row < m_Height; row++)
            {
                for (int col = 0; col < m_Length; col++)
                {
                    m_Board[row, col] = 0;
                }
            }
        }

        public void RemoveToken(int i_chosenColumn)
        {
            for (int i = 0; i < m_Height; i++)
            {
                if (m_Board[i, i_chosenColumn] != 0)
                {
                    m_Board[i, i_chosenColumn] = 0;
                    m_LastMoveRow = i;
                    m_LastMoveCol = i;
                    break;
                }
            }
        }

        public GameBoard CopyGameBoard()
        {
            GameBoard copyGameBoard = new GameBoard(m_Height, m_Length, m_ConnectSize);

            for (int i = 0; i < m_Height; i++)
            {
                for (int j = 0; j < m_Length; j++)
                {
                    copyGameBoard.Board[i, j] = m_Board[i, j];
                }
            }

            return copyGameBoard;
        }

        // Private Methods
        private bool checkHorizontals(int i_PlayerID)
        {
            bool isHorizontalFourFound = false;

            for (int col = 0; col < m_Length - (m_ConnectSize - 1); col++)
            {
                for (int row = m_Height - 1; row >= 0; row--)
                {
                    isHorizontalFourFound = true;
                    for (int connect = 0; connect < m_ConnectSize; connect++)
                    {
                        isHorizontalFourFound = isHorizontalFourFound && (m_Board[row, col + connect] == i_PlayerID);
                        if (!isHorizontalFourFound)
                        {
                            break;
                        }
                    }

                    if (isHorizontalFourFound)
                    {
                        break;
                    }
                }

                if (isHorizontalFourFound)
                {
                    break;
                }
            }

            return isHorizontalFourFound;
        }

        private bool checkVetricals(int i_PlayerID)
        {
            bool isVerticalFourFound = false;

            for (int col = 0; col < m_Length; col++)
            {
                for (int row = 0; row < m_Height - (m_ConnectSize - 1); row++)
                {
                    isVerticalFourFound = true;
                    for (int connect = 0; connect < m_ConnectSize; connect++)
                    {
                        isVerticalFourFound = isVerticalFourFound && (m_Board[row + connect, col] == i_PlayerID);
                        if (!isVerticalFourFound)
                        {
                            break;
                        }
                    }

                    if (isVerticalFourFound)
                    {
                        break;
                    }
                }

                if (isVerticalFourFound)
                {
                    break;
                }
            }

            return isVerticalFourFound;
        }

        private bool checkDiagonals(int i_PlayerID)
        {
            return checkDiagonalsUp(i_PlayerID) || checkDiagonalsDown(i_PlayerID);
        }

        private bool checkDiagonalsUp(int i_PlayerID)
        {
            bool isDiagonalUpFound = false;

            for (int col = 0; col < m_Length - (m_ConnectSize - 1); col++)
            {
                for (int row = m_Height - 1; row >= m_ConnectSize - 1; row--)
                {
                    isDiagonalUpFound = true;
                    for (int connect = 0; connect < m_ConnectSize; connect++)
                    {
                        isDiagonalUpFound = isDiagonalUpFound && (m_Board[row - connect, col + connect] == i_PlayerID);
                        if (!isDiagonalUpFound)
                        {
                            break;
                        }
                    }

                    if (isDiagonalUpFound)
                    {
                        break;
                    }
                }

                if (isDiagonalUpFound)
                {
                    break;
                }
            }

            return isDiagonalUpFound;
        }

        private bool checkDiagonalsDown(int i_PlayerID)
        {
            bool isDiagonalDownFound = false;

            for (int col = 0; col < m_Length - (m_ConnectSize - 1); col++)
            {
                for (int row = 0; row <= m_Height - m_ConnectSize; row++)
                {
                    isDiagonalDownFound = true;
                    for (int connect = 0; connect < m_ConnectSize; connect++)
                    {
                        isDiagonalDownFound = isDiagonalDownFound && (m_Board[row + connect, col + connect] == i_PlayerID);
                        if (!isDiagonalDownFound)
                        {
                            break;
                        }
                    }

                    if (isDiagonalDownFound)
                    {
                        break;
                    }
                }

                if (isDiagonalDownFound)
                {
                    break;
                }
            }

            return isDiagonalDownFound;
        }

        protected virtual void On_TokenPlaced(int i_ActivePlayer)
        {

            if (TokenPlaced != null)
            {
                TokenPlaced.Invoke(m_LastMoveRow, m_LastMoveCol, i_ActivePlayer);
            }
        }

    }

    public enum eTokens
    {
        Coin1 = 1,
        Coin2 = 2,
        Empty = 0
    }
}