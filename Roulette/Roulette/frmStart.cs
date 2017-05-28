using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Roulette
{
    public partial class frmStart : Form
    {
        string FileName;
        public Game game;

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
            game = new Game(1000L);

            BetScreen betScreen = new BetScreen(game);
            this.Hide();
            DialogResult rsltScreen = betScreen.ShowDialog();
            if (rsltScreen == DialogResult.No)
            {
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Yes)
            {
                saveFile();
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
            openFile();
            BetScreen betScreen = new BetScreen(game);
            this.Hide();
            DialogResult rsltScreen = betScreen.ShowDialog();
            if (rsltScreen == DialogResult.No)
            {
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Yes)
            {
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Save", game.credits.ToString());
                // Add a function to save the credits
                Application.Exit();
            }
            else if (rsltScreen == DialogResult.Cancel)
            {
                this.Show();
            }
        }

        private void saveFile()
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Roulette game file (*.roul)|*.roul";
                saveFileDialog.Title = "Save Roulette game";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }
            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, game);
                }
            }
        }
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Roulette game (*.roul)|*.roul";
            openFileDialog.Title = "Open Roulette game file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        game = (Game)formater.Deserialize(fileStream) as Game;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
        }
    }
}
