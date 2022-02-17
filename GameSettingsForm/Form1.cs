using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class SettingForm : Form
    {
        //Proprties
        public string Player1Name
        {
            get
            {
                return this.textBoxPlayer1.Text;
            }
        }

        public string Player2Name
        {
            get
            {
                return this.textBoxPlayer2.Text;
            }
        }

        public bool IsPlayer2Checked
        {
            get
            {
                return this.checkBoxPlayer2.Checked;
            }
        }

        public int NumOfRows
        {
            get
            {
                return (int)this.numericUpDownRows.Value;
            }
        }

        public int NumOfCols
        {
            get
            {
                return (int)this.numericUpDownCols.Value;
            }
        }

        public SettingForm()
        {
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked == true)
            {
                textBoxPlayer2.Text = "";
                textBoxPlayer2.Enabled = true;
            }
            else
            {
                textBoxPlayer2.Enabled = false;
                textBoxPlayer2.Text = "[Computer]";
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.TextLength == 0 || textBoxPlayer2.TextLength == 0)
            {
                MessageBox.Show("Name text is Empty, Please insert Name", "Name Error",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
