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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult drlExit =  MessageBox.Show("Дали сакате да ја исклучите играта?", "Потврди излез", MessageBoxButtons.OKCancel);
            if (drlExit == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }
    }
}
