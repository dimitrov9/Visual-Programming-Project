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
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult drlExit =  MessageBox.Show("Дали сакате да ја исклучите играта?", "Потврди излез", MessageBoxButtons.OKCancel);
            if (drlExit == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Game.SetInitialCredits(1000L);
            BetScreen betScreen = new BetScreen();
            this.Hide();
            DialogResult rsltScreen = betScreen.ShowDialog();
            if (rsltScreen == DialogResult.No)
            {
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Yes)
            {
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Save", Game.credits.ToString());
                // Add a function to save the credits
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Cancel)
            {
                this.Show();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string savedCredits = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Save");
            long credits = Convert.ToInt64(savedCredits);
            Game.SetInitialCredits(credits);
            BetScreen betScreen = new BetScreen();
            this.Hide();
            DialogResult rsltScreen = betScreen.ShowDialog();
            if (rsltScreen == DialogResult.No)
            {
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Yes)
            {
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Save", Game.credits.ToString());
                // Add a function to save the credits
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Cancel)
            {
                this.Show();
            }
        }
    }
}
