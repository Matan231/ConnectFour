using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace GameSettingsForm
{
    public partial class GameForm : Form
    {
        int m_NumOfRows;
        int m_NumOfCols;
        string m_Player1Name;
        string m_Player2Name;
        Match m_Match;

        public GameForm(int i_NumOfRows, int i_NumOfCols, string i_Player1Name, string i_Player2Name, Match i_Match)
        {
            m_NumOfRows = i_NumOfRows;
            m_NumOfCols = i_NumOfCols;
            m_Player1Name = i_Player1Name;
            m_Player2Name = i_Player2Name;
            m_Match = i_Match;

            InitializeComponent(i_NumOfRows, i_NumOfCols, i_Player1Name, i_Player2Name);
            m_Match.GameBoard.TokenPlaced += drawToken_TokenPlaced;
            m_Match.ColumnFull += blockColumn_ColumnFull;
            m_Match.EndGame += endGame_EndGAme;
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            int chosenColumn;
            chosenColumn = int.Parse((sender as Button).Text) - 1;
            m_Match.PlaceToken(chosenColumn);
        }

        private void drawToken_TokenPlaced(int i_Row, int i_Col, int i_Token)
        {
            string token;

            if(i_Token == 1)
            {
                token = "X";
            }
            else
            {
                token = "O";
            }
            this.m_BoardButtonsMetrix[i_Row, i_Col].Text = token;
        }

        private void blockColumn_ColumnFull(int i_Column)
        {
            this.m_ChooseColButtons[i_Column].Enabled = false;
        }
        private void endGame_EndGAme(eEndGameCausation i_EndGameCausation, Player i_Player)
        {
            string message = "";
            string header = "";

            changeScore(i_Player);
            if(i_EndGameCausation  == eEndGameCausation.ForInARow)
            {
                message = string.Format(@"{0} Won!!
Another Round?", i_Player.Name);
                header = "A Win!!";
            }
            else
            {
                message = @"Tie!!
Another Round?";
                header = "Tie!";
            }

            DialogResult result = MessageBox.Show(message, header, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(result == DialogResult.No)
            {
                this.Close();
            }
            else
            {
                this.newGame();
            }

        }

        private void newGame()
        {
            m_Match.NewRound();
            foreach(Button button in m_BoardButtonsMetrix)
            {
                button.Text = "";
            }
            foreach(Button button in m_ChooseColButtons)
            {
                button.Enabled = true;
            }
        }
        private void changeScore(Player i_Player)
        {

            if (i_Player.ID == 1)
            {
                m_Player1Score = i_Player.Score;
                m_LabelPlayer1Name.Text = string.Format("{0} : {1}", m_Player1Name, m_Player1Score);
            }
            else
            {
                m_Player2Score = i_Player.Score;
                m_LabelPlayer2Name.Text = string.Format("{0} : {1}", m_Player2Name, m_Player2Score);
            }
        }
    }
}
