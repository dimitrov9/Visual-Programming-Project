using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Roulette
{
    public partial class BetScreen : Form{


        private String[] btnTexts;
        private bool canRepeat = false;
        private int betAmount = 5;

        public BetScreen()
        {
            InitializeComponent();
            UpdateLabels();
            SaveButtonsText();
            
        }


        public void UpdateLabels()
        {
                lblCredits.Text = Game.credits.ToString();
                lblTotalBet.Text = Game.totalBet.ToString();       
        }

        private void BetScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Game.credits < 5)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                // DialogResult rsltClose = MessageBox.Show("Дали сакате да ги зачувате кредитите?","Потврдете зачувување и излез",MessageBoxButtons.YesNoCancel,
                SaveAndExit frmExit = new SaveAndExit();
                DialogResult dlgExit = frmExit.ShowDialog();
                if (dlgExit == DialogResult.No)
                {
                    this.DialogResult = DialogResult.No;
                }
                else if (dlgExit == DialogResult.Yes)
                {
                    // Function to save the credits to load them later
                    this.DialogResult = DialogResult.Yes;
                }
                else
                {
                    frmExit.Dispose();
                    e.Cancel = true;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(Game.totalBet<=0){
                MessageBox.Show("Ве молиме внесете влог", "Влогот е празен");
            }
            else
            {
                Random rnd = new Random();
                int nextGuess = rnd.Next(0, 36);
                
                lblNumber.Text = nextGuess.ToString();

                int profit = Game.ReturnProfitLoss(nextGuess);
                int realProfit = profit - (int)Game.totalBet;

                if (realProfit > 0)
                {
                    MessageBox.Show("Освоивте " + Math.Abs(realProfit) + " денари!!", "Добивте");
                }
                else if (realProfit < 0)
                {
                    MessageBox.Show("Изгубивте " + Math.Abs(realProfit) + " денари!!", "Изгубивте");
                }
                else
                {
                    MessageBox.Show("Не добивте ништо", "На 0 сте");
                }

                if (Game.credits < 5)
                {
                    MessageBox.Show("Изгубивте се!! Неможете повеќе да уплаќате!!");
                    this.DialogResult = DialogResult.Cancel;
                    this.Dispose();
                    this.Close();
                }
                ClearChips();
                Game.SavePrevBet();
                Game.ResetAllBets();
                UpdateLabels();
                canRepeat = true;
                btnRepeat.Enabled = true;
            }
        }

        private void btnDoubleAll_Click(object sender, EventArgs e)
        {
            if (2 * Game.totalBet > Game.credits)
            {
                MessageBox.Show("Неможете повеќе да дуплирате");
            }
            else
            {
                Game.DoubleAllBets();
                UpdateLabels();
                foreach (Button btn in pnlBetNumbers.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
                foreach (Button btn in pnlTwelves.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
                foreach (Button btn in pnlOtherBets.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
            }

        }

        private void btnRepeat_Click(object sender, EventArgs e)
        {
            if (canRepeat && Game.prevTotalBet<=Game.credits)
            {
                Game.BetPrev();
                UpdateLabels();
                foreach (Button btn in pnlBetNumbers.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
                foreach (Button btn in pnlTwelves.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
                foreach (Button btn in pnlOtherBets.Controls)
                {
                    PlaceChip(btn, btn.TabIndex);
                }
                canRepeat = false;
                btnRepeat.Enabled = false;
            }
        }

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            Game.CancelAllBets();
            ClearChips();
            UpdateLabels();
            canRepeat = true;
            btnRepeat.Enabled = true;
        }

        #region 0-36 Buttons Click

        private void button0_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(0, betAmount);
                UpdateLabels();
                PlaceChip(button0, 0);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(1, betAmount);
                UpdateLabels();
                PlaceChip(button1, 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(2, betAmount);
                UpdateLabels();
                PlaceChip(button2, 2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(3, betAmount);
                UpdateLabels();
                PlaceChip(button3, 3);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(4, betAmount);
                UpdateLabels();
                PlaceChip(button4, 4);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(5, betAmount);
                UpdateLabels();
                PlaceChip(button5, 5);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(6, betAmount);
                UpdateLabels();
                PlaceChip(button6, 6);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(7, betAmount);
                UpdateLabels();
                PlaceChip(button7, 7);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(8, betAmount);
                UpdateLabels();
                PlaceChip(button8, 8);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(9, betAmount);
                UpdateLabels();
                PlaceChip(button9, 9);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(10, betAmount);
                UpdateLabels();
                PlaceChip(button10, 10);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(11, betAmount);
                UpdateLabels();
                PlaceChip(button11, 11);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(12, betAmount);
                UpdateLabels();
                PlaceChip(button12, 12);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(13, betAmount);
                UpdateLabels();
                PlaceChip(button13, 13);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(14, betAmount);
                UpdateLabels();
                PlaceChip(button14, 14);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(15, betAmount);
                UpdateLabels();
                PlaceChip(button15, 15);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(16, betAmount);
                UpdateLabels();
                PlaceChip(button16, 16);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(17, betAmount);
                UpdateLabels();
                PlaceChip(button17, 17);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(18, betAmount);
                UpdateLabels();
                PlaceChip(button18, 18);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(19, betAmount);
                UpdateLabels();
                PlaceChip(button19, 19);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(20, betAmount);
                UpdateLabels();
                PlaceChip(button20, 20);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(21, betAmount);
                UpdateLabels();
                PlaceChip(button21, 21);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(22, betAmount);
                UpdateLabels();
                PlaceChip(button22, 22);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(23, betAmount);
                UpdateLabels();
                PlaceChip(button23, 23);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(24, betAmount);
                UpdateLabels();
                PlaceChip(button24, 24);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(25, betAmount);
                UpdateLabels();
                PlaceChip(button25, 25);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(26, betAmount);
                UpdateLabels();
                PlaceChip(button26, 26);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(27, betAmount);
                UpdateLabels();
                PlaceChip(button27, 27);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(28, betAmount);
                UpdateLabels();
                PlaceChip(button28, 28);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(29, betAmount);
                UpdateLabels();
                PlaceChip(button29, 29);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(30, betAmount);
                UpdateLabels();
                PlaceChip(button30, 30);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(31, betAmount);
                UpdateLabels();
                PlaceChip(button31, 31);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(32, betAmount);
                UpdateLabels();
                PlaceChip(button32, 32);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(33, betAmount);
                UpdateLabels();
                PlaceChip(button33, 33);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(34, betAmount);
                UpdateLabels();
                PlaceChip(button34, 34);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(35, betAmount);
                UpdateLabels();
                PlaceChip(button35, 35);
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InstertNumberBet(36, betAmount);
                UpdateLabels();
                PlaceChip(button36, 36);
            }
        }

        #endregion

        #region Twelves Buttons Click

        private void btnFirst12_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertTwelves(1, betAmount);
                UpdateLabels();
                PlaceChip(btnFirst12, btnFirst12.TabIndex);
            }
        }

        private void btnSecond12_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertTwelves(2, betAmount);
                UpdateLabels();
                PlaceChip(btnSecond12, btnSecond12.TabIndex);
            }
        }

        private void btnThird12_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertTwelves(3, betAmount);
                UpdateLabels();
                PlaceChip(btnThird12, btnThird12.TabIndex);
            }
        }

        #endregion

        #region Other Bets Buttons Click
        private void btn1to18_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(0, betAmount);
                UpdateLabels();
                PlaceChip(btn1to18, btn1to18.TabIndex);
            }
        }

        private void btn19to36_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(1, betAmount);
                UpdateLabels();
                PlaceChip(btn19to36, btn19to36.TabIndex);
            }
        }

        private void btnEVEN_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(2, betAmount);
                UpdateLabels();
                PlaceChip(btnEVEN, btnEVEN.TabIndex);
            }
        }

        private void btnODD_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(3, betAmount);
                UpdateLabels();
                PlaceChip(btnODD, btnODD.TabIndex);
            }
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(4, betAmount);
                UpdateLabels();
                PlaceChip(btnRed, btnRed.TabIndex);
            }
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            if (Game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                Game.InsertOtherBets(5, betAmount);
                UpdateLabels();
                PlaceChip(btnBlack, btnBlack.TabIndex);
            }
        }
        #endregion

        #region Change Bet Amount Buttons Click

        private void btnBet5_Click(object sender, EventArgs e)
        {
            betAmount = 5;
            SelectBetAmount();
        }

        private void btnBet10_Click(object sender, EventArgs e)
        {
            betAmount = 10;
            SelectBetAmount();
        }

        private void btnBet50_Click(object sender, EventArgs e)
        {
            betAmount = 50;
            SelectBetAmount();
        }

        #endregion

        public void PlaceChip(Button button,int btnIndex)
        {

            if (HasBet(button))
            {
                if (button.Image == null)
                {
                    Image redChip = new Bitmap(Properties.Resources.Red_Chip, new Size((int)(button1.Width / 1.5F), (int)(button1.Height / 1.5F)));
                    button.Image = redChip;
                    button.ImageAlign = ContentAlignment.MiddleCenter;
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.TextImageRelation = TextImageRelation.Overlay;
                }

                if (btnIndex < 37)
                {
                    button.Text = Game.numBets[btnIndex].ToString();
                }
                else if (btnIndex == 37)
                {
                    button.Text = Game.first12.ToString();
                }
                else if (btnIndex == 38)
                {
                    button.Text = Game.second12.ToString();
                }
                else if (btnIndex == 39)
                {
                    button.Text = Game.third12.ToString();
                }
                else if (btnIndex > 39 && btnIndex<46)
                {
                    button.Text = Game.otherBets[btnIndex-40].ToString();
                }

            }
        }

        public void ClearChips()
        {
            foreach (Button btn in pnlBetNumbers.Controls)
            {
                btn.Image = null;
                btn.Text = btnTexts[btn.TabIndex];
            }
            foreach (Button btn in pnlTwelves.Controls)
            {
                btn.Image = null;
                btn.Text = btnTexts[btn.TabIndex];
            }
            foreach (Button btn in pnlOtherBets.Controls)
            {
                btn.Image = null;
                btn.Text = btnTexts[btn.TabIndex];
            }
        }

        private void SaveButtonsText()
        {
            int totalBetBtns = pnlBetNumbers.Controls.Count + pnlTwelves.Controls.Count + pnlOtherBets.Controls.Count;
            btnTexts = new String[totalBetBtns];
            foreach (Button btn in pnlBetNumbers.Controls)
            {
                btnTexts[btn.TabIndex] = btn.Text;
            }
            foreach (Button btn in pnlTwelves.Controls)
            {
                btnTexts[btn.TabIndex] = btn.Text;
            }
            foreach (Button btn in pnlOtherBets.Controls)
            {
                btnTexts[btn.TabIndex] = btn.Text;
            }

        }

        private bool HasBet(Button button)
        {
            if (button.TabIndex < 37)
            {
                return Game.numBets[button.TabIndex] > 0;
            }
            else if (button.TabIndex == 37)
            {
                return Game.first12 > 0;
            }
            else if (button.TabIndex == 38)
            {
                return Game.second12 > 0;
            }
            else if (button.TabIndex == 39)
            {
                return Game.third12 > 0;
            }
            else if (button.TabIndex > 39 && button.TabIndex < 46)
            {
                return Game.otherBets[button.TabIndex-40] > 0;
            }
            return false;
        }

        private void SelectBetAmount()
        {
            Button[] btns = new Button[3]{btnBet5, btnBet10, btnBet50};
            for (int i = 0; i < 3; i++)
            {
                if (btns[i].Text.Equals(betAmount.ToString()))
                {
                    btns[i].FlatAppearance.BorderSize = 3;
                }
                else
                {
                    btns[i].FlatAppearance.BorderSize = 0;
                }
            }
        }

    }
}
