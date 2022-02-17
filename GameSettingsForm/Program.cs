using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms;
using GameLogic;

namespace GameSettingsForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        public static void Main()
        {
            //Application.EnableVisualStyles();
            startGame();
        }

        private static void startGame()
        {
            SettingForm gameSetting = new SettingForm();
            Player player1;
            Player player2;
            GameBoard gameBoard;
            Match match;

            gameSetting.ShowDialog();
            if (gameSetting.DialogResult == DialogResult.OK)
            {
                gameBoard = new GameBoard(gameSetting.NumOfRows, gameSetting.NumOfCols, 4);
                player1 = new Player(gameSetting.Player1Name, 1, false);
                if (!gameSetting.IsPlayer2Checked)
                {
                    player2 = new Player(gameSetting.Player2Name, 2, true) ;
                }
                else
                {
                    player2 = new Player(gameSetting.Player2Name, 2, false);
                }

                match = new Match(gameBoard, player1, player2);
                GameForm gameForm = new GameForm(gameSetting.NumOfRows, gameSetting.NumOfCols, gameSetting.Player1Name, gameSetting.Player2Name, match);
                gameForm.ShowDialog();

            }

        }
    }
}
