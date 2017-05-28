using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Roulette
{
    public partial class BetScreen : Form{

        
        private String[] btnTexts;
        private bool canRepeat = false;
        private int betAmount = 5;
        public Game game;

        private int[] prevNumbers = new int[31];
        //private int angle;
        //private Timer timer;
        //private Image wheelImage;

        //private Ball ball;

        public BetScreen(Game gamee)
        {
            InitializeComponent();
            this.game = gamee;
            UpdateLabels();
            SaveButtonsText();
            //angle = 0;
//this.DoubleBuffered = true;
            //timer = new Timer();
            //timer.Interval = 50;
            //timer.Tick += new EventHandler(timer1_Tick);
            //timer.Start();
            //pictureBox1.Width = 400;
            //pictureBox1.Height = 400;
            //wheelImage = pictureBox1.Image;
            // Define the border style of the form to a dialog box.
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            this.MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            this.MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            this.StartPosition = FormStartPosition.CenterScreen;

            //ball = new Ball(new Point((int)(panel1.Location.X + panel1.Width * 0.8f), (int)(panel1.Location.Y + panel1.Height / 2)), Color.White);


        }

        public void UpdateLabels()
        {
                lblCredits.Text = game.credits.ToString();
                lblTotalBet.Text = game.totalBet.ToString();       
        }

        private void BetScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (game.credits < 5)
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
           // MessageBox.Show("The pictureBox size is: " + pictureBox1.Size.ToString() + Environment.NewLine + "The Image size is: " + pictureBox1.Image.Size);


            if (game.totalBet <= 0)
            {
                MessageBox.Show("Ве молиме внесете влог", "Влогот е празен");
            }
            else
            {

                Random rnd = new Random();
                int nextGuess = rnd.Next(0, 36);
                
                lblNumber.Text = nextGuess.ToString();
                if (nextGuess == 0)
                {
                    lblNumber.BackColor = Color.Green;
                }
                else if (game.isRed(nextGuess))
                {
                    lblNumber.BackColor = Color.Red;
                }
                else if (game.isBlack(nextGuess))
                {
                    lblNumber.BackColor = Color.Black;
                }
                int profit = game.ReturnProfitLoss(nextGuess);
                int realProfit = profit - (int)game.totalBet;

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

                if (game.credits < 5)
                {
                    MessageBox.Show("Изгубивте се!! Неможете повеќе да уплаќате!!");
                    this.DialogResult = DialogResult.Cancel;
                    this.Dispose();
                    this.Close();
                }
                InsertLastNumber(nextGuess);
                ClearChips();
                game.SavePrevBet();
                game.ResetAllBets();
                UpdateLabels();
                canRepeat = true;
                btnRepeat.Enabled = true;
            }
        }

        private void btnDoubleAll_Click(object sender, EventArgs e)
        {
            if (2 * game.totalBet > game.credits)
            {
                MessageBox.Show("Неможете повеќе да дуплирате");
            }
            else
            {
                game.DoubleAllBets();
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
            if (canRepeat && game.prevTotalBet<=game.credits)
            {
                game.BetPrev();
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
            game.CancelAllBets();
            ClearChips();
            UpdateLabels();
            canRepeat = true;
            btnRepeat.Enabled = true;
        }

        #region 0-36 Buttons Click

        private void button0_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(0, betAmount);
                UpdateLabels();
                PlaceChip(button0, 0);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(1, betAmount);
                UpdateLabels();
                PlaceChip(button1, 1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(2, betAmount);
                UpdateLabels();
                PlaceChip(button2, 2);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(3, betAmount);
                UpdateLabels();
                PlaceChip(button3, 3);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(4, betAmount);
                UpdateLabels();
                PlaceChip(button4, 4);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(5, betAmount);
                UpdateLabels();
                PlaceChip(button5, 5);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(6, betAmount);
                UpdateLabels();
                PlaceChip(button6, 6);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(7, betAmount);
                UpdateLabels();
                PlaceChip(button7, 7);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(8, betAmount);
                UpdateLabels();
                PlaceChip(button8, 8);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(9, betAmount);
                UpdateLabels();
                PlaceChip(button9, 9);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(10, betAmount);
                UpdateLabels();
                PlaceChip(button10, 10);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(11, betAmount);
                UpdateLabels();
                PlaceChip(button11, 11);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(12, betAmount);
                UpdateLabels();
                PlaceChip(button12, 12);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(13, betAmount);
                UpdateLabels();
                PlaceChip(button13, 13);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(14, betAmount);
                UpdateLabels();
                PlaceChip(button14, 14);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(15, betAmount);
                UpdateLabels();
                PlaceChip(button15, 15);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(16, betAmount);
                UpdateLabels();
                PlaceChip(button16, 16);
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(17, betAmount);
                UpdateLabels();
                PlaceChip(button17, 17);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(18, betAmount);
                UpdateLabels();
                PlaceChip(button18, 18);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(19, betAmount);
                UpdateLabels();
                PlaceChip(button19, 19);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(20, betAmount);
                UpdateLabels();
                PlaceChip(button20, 20);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(21, betAmount);
                UpdateLabels();
                PlaceChip(button21, 21);
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(22, betAmount);
                UpdateLabels();
                PlaceChip(button22, 22);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(23, betAmount);
                UpdateLabels();
                PlaceChip(button23, 23);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(24, betAmount);
                UpdateLabels();
                PlaceChip(button24, 24);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(25, betAmount);
                UpdateLabels();
                PlaceChip(button25, 25);
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(26, betAmount);
                UpdateLabels();
                PlaceChip(button26, 26);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(27, betAmount);
                UpdateLabels();
                PlaceChip(button27, 27);
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(28, betAmount);
                UpdateLabels();
                PlaceChip(button28, 28);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(29, betAmount);
                UpdateLabels();
                PlaceChip(button29, 29);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(30, betAmount);
                UpdateLabels();
                PlaceChip(button30, 30);
            }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(31, betAmount);
                UpdateLabels();
                PlaceChip(button31, 31);
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(32, betAmount);
                UpdateLabels();
                PlaceChip(button32, 32);
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(33, betAmount);
                UpdateLabels();
                PlaceChip(button33, 33);
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(34, betAmount);
                UpdateLabels();
                PlaceChip(button34, 34);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(35, betAmount);
                UpdateLabels();
                PlaceChip(button35, 35);
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InstertNumberBet(36, betAmount);
                UpdateLabels();
                PlaceChip(button36, 36);
            }
        }

        #endregion

        #region Twelves Buttons Click

        private void btnFirst12_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertTwelves(1, betAmount);
                UpdateLabels();
                PlaceChip(btnFirst12, btnFirst12.TabIndex);
            }
        }

        private void btnSecond12_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertTwelves(2, betAmount);
                UpdateLabels();
                PlaceChip(btnSecond12, btnSecond12.TabIndex);
            }
        }

        private void btnThird12_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertTwelves(3, betAmount);
                UpdateLabels();
                PlaceChip(btnThird12, btnThird12.TabIndex);
            }
        }

        #endregion

        #region Other Bets Buttons Click
        private void btn1to18_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(0, betAmount);
                UpdateLabels();
                PlaceChip(btn1to18, btn1to18.TabIndex);
            }
        }

        private void btn19to36_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(1, betAmount);
                UpdateLabels();
                PlaceChip(btn19to36, btn19to36.TabIndex);
            }
        }

        private void btnEVEN_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(2, betAmount);
                UpdateLabels();
                PlaceChip(btnEVEN, btnEVEN.TabIndex);
            }
        }

        private void btnODD_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(3, betAmount);
                UpdateLabels();
                PlaceChip(btnODD, btnODD.TabIndex);
            }
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(4, betAmount);
                UpdateLabels();
                PlaceChip(btnRed, btnRed.TabIndex);
            }
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            if (game.credits < betAmount)
            {
                MessageBox.Show("Неможете да ја уплатите таа сума");
            }
            else
            {
                game.InsertOtherBets(5, betAmount);
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
                    button.Text = game.numBets[btnIndex].ToString();
                }
                else if (btnIndex == 37)
                {
                    button.Text = game.first12.ToString();
                }
                else if (btnIndex == 38)
                {
                    button.Text = game.second12.ToString();
                }
                else if (btnIndex == 39)
                {
                    button.Text = game.third12.ToString();
                }
                else if (btnIndex > 39 && btnIndex<46)
                {
                    button.Text = game.otherBets[btnIndex - 40].ToString();
                }
                ResizeChipFont(button);
            }
        }

        private static void ResizeChipFont(Button button)
        {
            long tempBet = Convert.ToInt64(button.Text);
            if (tempBet > 99)
            {
                button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
            }
            else
            {
                button.Font = new Font(button.Font.FontFamily, 15, FontStyle.Bold);
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
                return game.numBets[button.TabIndex] > 0;
            }
            else if (button.TabIndex == 37)
            {
                return game.first12 > 0;
            }
            else if (button.TabIndex == 38)
            {
                return game.second12 > 0;
            }
            else if (button.TabIndex == 39)
            {
                return game.third12 > 0;
            }
            else if (button.TabIndex > 39 && button.TabIndex < 46)
            {
                return game.otherBets[button.TabIndex-40] > 0;
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

        private void InsertLastNumber(int number)
        {
            int[] copyArray = new int[prevNumbers.Length];
            Array.Copy(prevNumbers, 0, copyArray, 1, prevNumbers.Length-1);
            copyArray[0] = number;
            Array.Copy(copyArray, prevNumbers, prevNumbers.Length);

            for (int i = 0; i < prevNumbers.Length; i++)
            {
                foreach (Label lbl in pnlLastNums.Controls)
                {
                    if (lbl.TabIndex == i)
                    {
                        lbl.Text = prevNumbers[i].ToString();
                        if (prevNumbers[i] == 0)
                        {
                            lbl.BackColor = Color.Green;
                        }
                        else if(game.isRed(prevNumbers[i]))
                        {
                            lbl.BackColor = Color.Red;
                        }
                        else if (game.isBlack(prevNumbers[i]))
                        {
                            lbl.BackColor = Color.Black;
                        }
                    }
                }

            }
        }



        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    if (angle > 360)
        //    {
        //        angle %= 360;
        //    }
        //    angle+=2;
        //    //pictureBox1.Image = RotateImage(wheelImage, angle, false, true, Color.Transparent);
        //    //this.Invalidate();
        //    //panel1.Invalidate(true);
        //}

        ///// <summary>
        ///// Method to rotate an Image object. The result can be one of three cases:
        ///// - upsizeOk = true: output image will be larger than the input, and no clipping occurs 
        ///// - upsizeOk = false & clipOk = true: output same size as input, clipping occurs
        ///// - upsizeOk = false & clipOk = false: output same size as input, image reduced, no clipping
        ///// 
        ///// A background color must be specified, and this color will fill the edges that are not 
        ///// occupied by the rotated image. If color = transparent the output image will be 32-bit, 
        ///// otherwise the output image will be 24-bit.
        ///// 
        ///// Note that this method always returns a new Bitmap object, even if rotation is zero - in 
        ///// which case the returned object is a clone of the input object. 
        ///// </summary>
        ///// <param name="inputImage">input Image object, is not modified</param>
        ///// <param name="angleDegrees">angle of rotation, in degrees</param>
        ///// <param name="upsizeOk">see comments above</param>
        ///// <param name="clipOk">see comments above, not used if upsizeOk = true</param>
        ///// <param name="backgroundColor">color to fill exposed parts of the background</param>
        ///// <returns>new Bitmap object, may be larger than input image</returns>
        //public static Bitmap RotateImage(Image inputImage, float angleDegrees, bool upsizeOk,bool clipOk, Color backgroundColor)
        //{

        //    // Test for zero rotation and return a clone of the input image
        //    if (angleDegrees == 0f)
        //        return (Bitmap)inputImage.Clone();

        //    // Set up old and new image dimensions, assuming upsizing not wanted and clipping OK
        //    int oldWidth = inputImage.Width;
        //    int oldHeight = inputImage.Height;
        //    int newWidth = oldWidth;
        //    int newHeight = oldHeight;
        //    float scaleFactor = 1f;

        //    // If upsizing wanted or clipping not OK calculate the size of the resulting bitmap
        //    if (upsizeOk || !clipOk)
        //    {
        //        double angleRadians = angleDegrees * Math.PI / 180d;

        //        double cos = Math.Abs(Math.Cos(angleRadians));
        //        double sin = Math.Abs(Math.Sin(angleRadians));
        //        newWidth = (int)Math.Round(oldWidth * cos + oldHeight * sin);
        //        newHeight = (int)Math.Round(oldWidth * sin + oldHeight * cos);
        //    }

        //    // If upsizing not wanted and clipping not OK need a scaling factor
        //    if (!upsizeOk && !clipOk)
        //    {
        //        scaleFactor = Math.Min((float)oldWidth / newWidth, (float)oldHeight / newHeight);
        //        newWidth = oldWidth;
        //        newHeight = oldHeight;
        //    }

        //    // Create the new bitmap object. If background color is transparent it must be 32-bit, 
        //    //  otherwise 24-bit is good enough.
        //    Bitmap newBitmap = new Bitmap(newWidth, newHeight, backgroundColor == Color.Transparent ?
        //                                     PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
        //    newBitmap.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

        //    // Create the Graphics object that does the work
        //    using (Graphics graphicsObject = Graphics.FromImage(newBitmap))
        //    {
        //        graphicsObject.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphicsObject.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //        graphicsObject.SmoothingMode = SmoothingMode.HighQuality;

        //        // Fill in the specified background color if necessary
        //        if (backgroundColor != Color.Transparent)
        //            graphicsObject.Clear(backgroundColor);

        //        // Set up the built-in transformation matrix to do the rotation and maybe scaling
        //        graphicsObject.TranslateTransform(newWidth / 2f, newHeight / 2f);

        //        if (scaleFactor != 1f)
        //            graphicsObject.ScaleTransform(scaleFactor, scaleFactor);

        //        graphicsObject.RotateTransform(angleDegrees);
        //        graphicsObject.TranslateTransform(-oldWidth / 2f, -oldHeight / 2f);

        //        // Draw the result 
        //        //graphicsObject.DrawImage(inputImage, 0, 0);
        //    }

        //    return newBitmap;
        //}
        
        ///// <summary>
        ///// Resize the image to the specified width and height.
        ///// </summary>
        ///// <param name="image">The image to resize.</param>
        ///// <param name="width">The width to resize to.</param>
        ///// <param name="height">The height to resize to.</param>
        ///// <returns>The resized image.</returns>
        //public static Bitmap ResizeImage(Image image, int width, int height)
        //{
        //    var destRect = new Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        using (var wrapMode = new ImageAttributes())
        //        {
        //            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //        }
        //    }

        //    return destImage;
        //}


        //private void pictureBox1_Paint(object sender, PaintEventArgs e)
        //{
        //   // ball.Draw(e.Graphics);
        //}

       // private void panel1_Paint(object sender, PaintEventArgs e)
        //{
            //e.Graphics.DrawImage(wheelImage,new Rectangle(panel1.Location,panel1.Size));
            //panel1.BackgroundImage = wheelImage;
            //ball.Draw(e.Graphics);
        //}
    
    }
}
