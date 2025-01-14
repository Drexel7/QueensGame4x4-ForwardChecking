/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2016-2020, Florin Leon                               *
 *  E-mail:      florin.leon@academic.tuiasi.ro                           *
 *  Website:     http://florinleon.byethost24.com/lab_ia.html             *
 *  Description: Game playing. QueensGame 4x4 - Forward Checking          *
 *                                                                        *
 *  This code and information is provided "as is" without warranty of     *
 *  any kind, either expressed or implied, including but not limited      *
 *  to the implied warranties of merchantability or fitness for a         *
 *  particular purpose. You are free to use this source code in your      *
 *  applications as long as the original copyright notice is included.    *
 *                                                                        *
 **************************************************************************/
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QueensGame
{
    public partial class Form1 : Form
    {
        public int[,] pozOcupata = new int[4, 4];  
        public int QCount = 0;
        private int clickCount = 0; 
        private Game game;
        private Board board;
        private DrawBoard drawBoard;
        private bool gameEnded = false;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Problema Reginelor";
            this.Size = new System.Drawing.Size(620, 620);
            board = new Board();
            game = new Game();  
            drawBoard = new DrawBoard(game.Board);  

            // casuta debifata la pornire
            checkBox1.Checked = false;  
            game.ForwardCheckingEnabled = false;  
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // actualizează starea forward checking 
            game.ForwardCheckingEnabled = checkBox1.Checked;

            // redesenarea tablei
            this.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.ResetGame();
            clickCount = 0; // resetare contor de clicuri 
            gameEnded = false; // continuarea joc
            this.Invalidate();
        }

        private async void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (gameEnded) return; // daca jocul s-a terminat, asteapta

            if ((e.X >= 100) && (e.X < 500) && (e.Y >= 100) && (e.Y < 500))
            {
                int itempX = (e.X - 100) / 100;
                int itempY = (e.Y - 100) / 100;

                game.HandleMouseClick(itempX, itempY);
                this.Invalidate();

                clickCount++; // incrementare contor



                if (clickCount == 4)
                {
                    //delay 2 sec pt a primi rezultatul de win/loss
                    await Task.Delay(2000); 

                    game.ResetGame();  
                    clickCount = 0;    
                    gameEnded = false;
                    this.Invalidate(); 
                }    
            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            //reincarcare tabela daca e bifat FW-CK
            drawBoard.Draw(g, checkBox1.Checked);  

            g.DrawString("Sunt " + game.QueenCount + " regine pe tablă.", Font, Brushes.Black, 200, 50);

            //decizie joc Castigat/Pierdut
            string statusMessage = game.CheckGameStatus(clickCount);
            if (!string.IsNullOrEmpty(statusMessage))
            {
                g.DrawString(statusMessage, new Font("Arial", 24),
                             statusMessage == "Ai câștigat!" ? Brushes.Green : Brushes.Red,
                             200, 520);
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // incarcare tabela
        }

        private void iesireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void despreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string info =
                "Jocul celor 4 Regine utilizand Forward Checking\r\n"+
                "\r\n"+
                "Reguli joc:\r\n" +
                "Jucătorul plasează, prin apasarea click-ului, câte o regină în tabelă.\r\n" +
                "\r\n" +
                "Scopul jocului:\r\n" +
                "Plasarea simultană a 4 regine duce la câștigarea jocului.\r\n" +
                "În cazul unui număr limitat de click-uri (4) finalizat prin\r\n" +
                "neplasarea celor 4 regine, jocul este pierdut.\r\n" +
                "\r\n" +
                "(c)2025 Maftei-Gutui Robert";
            MessageBox.Show(info, "4 Queens Game");
        }

    }
}
