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
    

        public BetScreen()
        {
            InitializeComponent();
            UpdateLabels();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(Game.totalBet<=0){
                MessageBox.Show("Ве молиме внесете влог", "Влогот е празен");
            }
            else
            {
                Random rnd = new Random();
                int nextGuess = rnd.Next(0, 2);

                lblNumber.Text = nextGuess.ToString();

                int profit = Game.ReturnProfitLoss(nextGuess);

                if (profit > 0)
                {
                    MessageBox.Show("Освоивте " + profit + " денари!!", "Добивте");
                }
                else if (profit < 0)
                {
                    MessageBox.Show("Изгубивте " + profit + " денари!!", "Изгубивте");
                }
                else
                {
                    MessageBox.Show("Не добивте ништо", "На 0 сте");
                }
                Game.ResetAllBets();
                UpdateLabels();

            }
        }

        public void UpdateLabels()
        {
            lblCredits.Text = Game.credits.ToString();
            lblTotalBet.Text = Game.totalBet.ToString();
        }
        
        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            Game.InstertNumberBet(1);
            UpdateLabels();
        }


    }
}
