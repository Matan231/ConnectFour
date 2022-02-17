using System.Windows.Forms;
namespace GameSettingsForm
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(int i_NumOfRows, int i_NumOfCols, string i_Player1Name, string i_Player2Name)
        {
            Button boardButton;
            this.m_BoardButtonsMetrix = new Button[i_NumOfRows, i_NumOfCols];
            this.m_ChooseColButtons = new Button[i_NumOfCols];
            const int k_BoardButtonHeight = 40;
            const int k_BoardButtonWidth = 40;
            const int k_BoardButtonsMargin = 6;
            const int k_ChooseColButtonHeight = 20;
            const int k_ChooseColButtonWidth = k_BoardButtonWidth;
            const int k_boardMargin = 20;
            const int k_LabelPlayerHeight = 20;
            const int k_LabelPlayerWidth = 100;

            this.SuspendLayout();
            //Board Buttons
            for (int col = 0; col < i_NumOfCols; col++)
            {
                boardButton = new Button();
                boardButton.Name = string.Format("buttonBoard{0}", col);
                boardButton.Size = new System.Drawing.Size(k_ChooseColButtonWidth, k_ChooseColButtonHeight);
                boardButton.Location = new System.Drawing.Point(k_boardMargin + col * (k_BoardButtonWidth + k_BoardButtonsMargin), k_boardMargin);
                boardButton.Text = string.Format("{0}", col + 1);
                boardButton.UseVisualStyleBackColor = true;
                boardButton.Click += new System.EventHandler(this.buttons_Click);
                this.Controls.Add(boardButton);
                m_ChooseColButtons[col] = boardButton;
            }

            for (int row = 0; row < i_NumOfRows; row++)
            {
                for (int col = 0; col < i_NumOfCols; col++)
                {
                    boardButton = new Button();
                    boardButton.Name = string.Format("buttonBoard{0}{1}", row, col);
                    boardButton.Size = new System.Drawing.Size(k_BoardButtonWidth, k_BoardButtonHeight);
                    boardButton.Location = new System.Drawing.Point(k_boardMargin + col * (k_BoardButtonWidth + k_BoardButtonsMargin), k_boardMargin + k_ChooseColButtonHeight + k_BoardButtonsMargin + row * (k_BoardButtonHeight + k_BoardButtonsMargin));
                    boardButton.Text = "";
                    boardButton.UseVisualStyleBackColor = true;
                    this.Controls.Add(boardButton);
                    m_BoardButtonsMetrix[row, col] = boardButton;
                }
            }
            // 
            // GameForm
            // 
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.SetClientSizeCore(2*k_boardMargin + i_NumOfCols * (k_BoardButtonWidth + k_BoardButtonsMargin) - k_BoardButtonsMargin, k_LabelPlayerHeight + 2*k_boardMargin + k_ChooseColButtonHeight + k_BoardButtonsMargin + i_NumOfRows * (k_BoardButtonHeight + k_BoardButtonsMargin) - k_BoardButtonsMargin);
            this.Name = "GameForm";
            this.Text = "4 in a Row!!";
            this.ResumeLayout(false);
            this.StartPosition = FormStartPosition.CenterScreen;
            //
            //Label Player1Name + Player1Score 
            //
            m_LabelPlayer1Name = new Label();
            m_LabelPlayer1Name.Text = string.Format("{0} : {1}", i_Player1Name , m_Player1Score);
            m_LabelPlayer1Name.Size = new System.Drawing.Size(k_LabelPlayerWidth, k_LabelPlayerHeight);
            m_LabelPlayer1Name.Left = this.Right/2 - k_LabelPlayerWidth - 12;
            m_LabelPlayer1Name.Top = this.Bottom - k_LabelPlayerHeight -39;
            this.Controls.Add(m_LabelPlayer1Name);
            //
            //Label Player2Name + Player2Score
            //
            m_LabelPlayer2Name = new Label();
            m_LabelPlayer2Name.Text = string.Format("{0} : {1}", i_Player2Name, m_Player2Score);
            m_LabelPlayer2Name.Size = new System.Drawing.Size(k_LabelPlayerWidth, k_LabelPlayerHeight);
            m_LabelPlayer2Name.Left = m_LabelPlayer1Name.Right + 12;
            m_LabelPlayer2Name.Top = this.Bottom - k_LabelPlayerHeight - 39;
            this.Controls.Add(m_LabelPlayer2Name);

        }

        #endregion

        private Button[,] m_BoardButtonsMetrix;
        private Button[] m_ChooseColButtons;
        private Label m_LabelPlayer1Name;
        private Label m_LabelPlayer2Name;
        private int m_Player1Score = 0;
        private int m_Player2Score = 0;
    }
}