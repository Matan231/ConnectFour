using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameBoardClass = GameLogic.GameBoard;

namespace GameLogic
{
    public class GameAi
    {
        private const int k_AiPlayer = 2;
        private const int k_HumanPlayer = 1;
        private readonly GameBoardClass m_GameBoard;

        // Constructor
        public GameAi(GameBoardClass i_GameBoard)
        {
            m_GameBoard = i_GameBoard;
        }

        // Public Methods
        public int ChooseColumn()
        {

            const int k_SearchDepth = 3;
            Move aiOneMove = new Move(0, 0);
            Move humanOneMove = new Move(0, 0);
            Move finalChoosenColumn = new Move(0, 0);

            aiOneMove.IsAOneMoveWin = false;
            humanOneMove.IsAOneMoveWin = false;
            aiOneMove = currentBestMove(m_GameBoard.CopyGameBoard(), k_AiPlayer);
            humanOneMove = currentBestMove(m_GameBoard.CopyGameBoard(), k_HumanPlayer);
            if (aiOneMove.IsAOneMoveWin) 
            {
                finalChoosenColumn = aiOneMove;
            }
            else if(humanOneMove.IsAOneMoveWin)
            {
                finalChoosenColumn = humanOneMove;
            }
            else
            {
                finalChoosenColumn = generateMove(k_SearchDepth, m_GameBoard.CopyGameBoard(), k_AiPlayer);
            }

            while (m_GameBoard.IsColumnFull(finalChoosenColumn.CounterMove))
            {
                finalChoosenColumn.CounterMove = (finalChoosenColumn.CounterMove + 1) % m_GameBoard.Length;
            }

            return finalChoosenColumn.CounterMove;
        }

        // Private Methods
        private Move generateMove(int i_RecDepth, GameBoardClass i_CurrentGameBoard, int i_Turn)
        {
            int score;
            int bestScore;
            int bestMove = 0;
            Move choosenMove;
            Move bestMoveS = new Move(0, 0);
            int opBestScore = int.MaxValue;
            int myBestScore = int.MinValue;

            if (i_RecDepth == 0 || i_CurrentGameBoard.FourInARowExists(k_AiPlayer) || i_CurrentGameBoard.FourInARowExists(k_HumanPlayer))
            {
                choosenMove = currentBestMove(i_CurrentGameBoard, i_Turn);
                return choosenMove;
            }

            for (int i_column = 0; i_column < i_CurrentGameBoard.Length; i_column++)
            {
                if (i_CurrentGameBoard.IsColumnFull(i_column))
                {
                    continue;
                }

                GameBoardClass nextBoard = i_CurrentGameBoard.CopyGameBoard();
                nextBoard.PlaceToken(i_column, i_Turn);

                choosenMove = generateMove(i_RecDepth - 1, nextBoard, changeTurn(i_Turn));
                score = choosenMove.Score;
                if (i_Turn == k_AiPlayer)
                {
                    if (score > myBestScore)
                    {
                        myBestScore = score;
                        bestMove = i_column;
                    }
                }
                else
                {
                    if (score < opBestScore)
                    {
                        opBestScore = score;
                        bestMove = i_column;
                    }
                }
            }

            if (i_Turn == k_AiPlayer)
            {
                bestScore = myBestScore;
            }
            else
            {
                bestScore = opBestScore;
            }

            bestMoveS.Score = bestScore;
            bestMoveS.CounterMove = bestMove;

            return bestMoveS;
        }

        private int changeTurn(int i_Turn)
        {
            return (i_Turn % 2) + 1;
        }

        private Move currentBestMove(GameBoardClass i_CurrentGameBoard, int i_Turn)
        {
            const int k_FourInARowWeight = 100000;
            const int k_ThreeInARowWeight = 25;
            const int k_TwoInARowWeight = 5;
            int[] scores = new int[i_CurrentGameBoard.Length];
            int bestScore;
            int bestMove = 0;
            Move choosenMove = new Move(0, 0);

            for (int i = 0; i < i_CurrentGameBoard.Length; i++)
            {
                if (i_CurrentGameBoard.IsColumnFull(i))
                {
                    continue;
                }

                i_CurrentGameBoard.PlaceToken(i, i_Turn);
                if (i_CurrentGameBoard.FourInARowExists(k_AiPlayer))
                {
                    scores[i] += k_FourInARowWeight;
                    choosenMove.IsAOneMoveWin = true;
                }

                scores[i] += k_ThreeInARowWeight * numOfConnectSizeExists(k_AiPlayer, 3, i_CurrentGameBoard);
                scores[i] += k_TwoInARowWeight * numOfConnectSizeExists(k_AiPlayer, 2, i_CurrentGameBoard);
                if (i_CurrentGameBoard.FourInARowExists(k_HumanPlayer))
                {
                    scores[i] += 10 *-k_FourInARowWeight;
                    choosenMove.IsAOneMoveWin = true;
                }

                scores[i] += -k_ThreeInARowWeight * numOfConnectSizeExists(k_HumanPlayer, 3, i_CurrentGameBoard);
                scores[i] += -k_TwoInARowWeight * numOfConnectSizeExists(k_HumanPlayer, 2, i_CurrentGameBoard);
                i_CurrentGameBoard.RemoveToken(i);
            }

            if (i_Turn == k_AiPlayer)
            {
                bestScore = scores.Max();
            }
            else
            {
                bestScore = scores.Min();
            }

            for (int i = 0; i < i_CurrentGameBoard.Length; i++)
            {
                if (scores[i] == bestScore)
                {
                    bestMove = i;
                    break;
                }
            }

            choosenMove.Score = bestScore;
            choosenMove.CounterMove = bestMove;

            return choosenMove;
        }

        private int numOfConnectSizeExists(int i_PlayerID, int i_ConnectSize, GameBoardClass i_Gameboard)
        {
            int numOfConnects = checkHorizontals(i_PlayerID, i_ConnectSize, i_Gameboard) + checkVetricals(i_PlayerID, i_ConnectSize, i_Gameboard) + checkDiagonals(i_PlayerID, i_ConnectSize, i_Gameboard);
            int numbOfWindows = checkWindowHorizontals(i_PlayerID, i_ConnectSize, i_Gameboard);

            return numbOfWindows + numOfConnects;
        }

        private int checkHorizontals(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            int count;
            int numOfhorizontalSize = 0;

            for (int col = 0; col < i_GameBoard.Length - (i_ConnectSize - 1); col++)
            {
                for (int row = i_GameBoard.Height - 1; row >= 0; row--)
                {
                    count = 0;
                    for (int i = 0; i < i_ConnectSize; i++)
                    {
                        if (i_GameBoard.Board[row, col + i] == i_PlayerID)
                        {
                            count++;
                        }
                    }

                    if (count == i_ConnectSize)
                    {
                        numOfhorizontalSize++;
                    }
                }
            }

            return numOfhorizontalSize;
        }

        private int checkVetricals(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            int count;
            int numOfVerticalSize = 0;

            for (int col = 0; col < i_GameBoard.Length; col++)
            {
                for (int row = 0; row < i_GameBoard.Height - (i_ConnectSize - 1); row++)
                {
                    count = 0;
                    for (int i = 0; i < i_ConnectSize; i++)
                    {
                        if (i_GameBoard.Board[row + i, col] == i_PlayerID)
                        {
                            count++;
                        }
                    }

                    if (count == i_ConnectSize)
                    {
                        numOfVerticalSize++;
                    }
                }
            }

            return numOfVerticalSize;
        }

        private int checkWindowHorizontals(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            int count;
            int numOfhorizontalSize = 0;
            int windowSize = i_ConnectSize + 1;
            for (int col = 0; col < i_GameBoard.Length - (windowSize - 1); col++)
            {
                for (int row = i_GameBoard.Height - 1; row >= 0; row--)
                {
                    count = 0;
                    for (int i = 0; i < windowSize; i++)
                    {
                        if (i_GameBoard.Board[row, col + i] == i_PlayerID)
                        {
                            count++;
                        }
                    }

                    if (count == i_ConnectSize)
                    {
                        numOfhorizontalSize++;
                    }
                }
            }

            return numOfhorizontalSize;
        }

        private int checkDiagonals(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            return checkDiagonalsUp(i_PlayerID, i_ConnectSize, i_GameBoard) + checkDiagonalsDown(i_PlayerID, i_ConnectSize, i_GameBoard);
        }

        private int checkDiagonalsUp(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            int count;
            int numOfDiagonalUp = 0;
            for (int col = 0; col < i_GameBoard.Length - (i_ConnectSize - 1); col++)
            {
                for (int row = i_GameBoard.Height - 1; row >= i_ConnectSize - 1; row--)
                {
                    count = 0;
                    for (int i = 0; i < i_ConnectSize; i++)
                    {
                        if (i_GameBoard.Board[row - i, col + i] == i_PlayerID)
                        {
                            count++;
                        }
                    }

                    if (count == i_ConnectSize)
                    {
                        numOfDiagonalUp++;
                    }
                }
            }

            return numOfDiagonalUp;
        }

        private int checkDiagonalsDown(int i_PlayerID, int i_ConnectSize, GameBoardClass i_GameBoard)
        {
            int count;
            int numOfDiagonalDown = 0;

            for (int col = 0; col < i_GameBoard.Length - (i_ConnectSize - 1); col++)
            {
                for (int row = 0; row <= i_GameBoard.Height - i_ConnectSize; row++)
                {
                    count = 0;
                    for (int i = 0; i < i_ConnectSize; i++)
                    {
                        if (i_GameBoard.Board[row + i, col + i] == i_PlayerID)
                        {
                            count++;
                        }
                    }

                    if (count == i_ConnectSize)
                    {
                        numOfDiagonalDown++;
                    }
                }
            }

            return numOfDiagonalDown;
        }
    }

    // Move Struct
    public struct Move
    {
        private int m_Score;
        private int m_CounterMove;
        private bool m_IsAOneMoveWin;

        // Public Properties
        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public int CounterMove
        {
            get { return m_CounterMove; }
            set { m_CounterMove = value; }
        }

        public bool IsAOneMoveWin
        {
            get { return m_IsAOneMoveWin; }
            set { m_IsAOneMoveWin = value; }
        }

        // Constructor
        public Move(int i_Countermove, int i_Score)
        {
            m_Score = i_Score;
            m_CounterMove = i_Countermove;
            m_IsAOneMoveWin = false;
        }
    }
}