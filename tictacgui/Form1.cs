using board;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictacgui
{
    public partial class Form1 : Form
    {
        Tic game = new Tic();
        Button[] buttons = new Button[9];
        public Form1()
        {
            InitializeComponent();
            game = new Tic();
            buttons[0] = button1;
            buttons[1] = button2;
            buttons[2] = button3;
            buttons[3] = button4;
            buttons[4] = button5;
            buttons[5] = button6;
            buttons[6] = button7;
            buttons[7] = button8;
            buttons[8] = button9;

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Click += handleButtonclick;
                buttons[i].Tag = i;
            }

        }

        private void handleButtonclick(object sender, EventArgs e)
        {
            Button clickedButton = (Button) sender;
            int i_value = 0;
            int j_value = 0;
            if ((int)clickedButton.Tag <= 2)
            {
                i_value = 0;
                j_value = (int)clickedButton.Tag;
            }
            else if ((int)clickedButton.Tag <= 5)
            {
                i_value = 1;
                j_value = (int)clickedButton.Tag % 3;
            }
            else if ((int)clickedButton.Tag <= 8)
            {
                i_value = 2;
                j_value = (int)clickedButton.Tag % 3;
            }

            if (game.the_board[i_value, j_value] != "0")
            {
                MessageBox.Show("Pick another square silly!");
            }
            else
            {
                if (game.turn % 2 == 0)
                {
                    game.the_board[i_value, j_value] = "X";
                }
                else
                {
                    game.the_board[i_value, j_value] = "O";
                }
                game.turn += 1;
                game.entered_moves.Add((int)clickedButton.Tag + 1.ToString());
                game.check_ending(game.the_board);
                if (game.x_win == true || game.o_win == true || game.turn == 9)
                {
                    updateBoard();
                    if (game.x_win == true)
                    {
                        MessageBox.Show("Crosses won!");
                    }
                    else if (game.o_win == true)
                    {
                        MessageBox.Show("Naughts won!");
                    }
                    else if (game.turn == 9)
                    {
                        MessageBox.Show("It was a draw!");
                    }
                    disableAllButtons();
                }
                else
                {
                    updateBoard();
                    computerChoose();
                }
            }
        }

        private void disableAllButtons()
        {
            foreach (var item in buttons)
            {
                item.Enabled = false;
            }
        }

        private void computerChoose()
        {
            if (game.turn % 2 != 0)
            {
                (int, int) ai_minimax_move = game.ai_minimax(game, 10, false).Item1;
                game.make_ai_move(ai_minimax_move);
            }
            else
            {
                (int, int) ai_minimax_move = game.ai_minimax(game, 10, true).Item1;
                game.make_ai_move(ai_minimax_move);
            }
            updateBoard();
            game.check_ending(game.the_board);
            if (game.x_win == true || game.o_win == true || game.turn == 9)
            {
                if (game.x_win == true)
                {
                    MessageBox.Show("Crosses won!");
                }
                else if (game.o_win == true)
                {
                    MessageBox.Show("Naughts won!");
                }
                else if (game.turn == 9)
                {
                    MessageBox.Show("It was a draw!");
                }
                disableAllButtons();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateBoard();
        }

        private void updateBoard()
        {
            // assign x or o
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int button_value = 0;
                    if (i == 0)
                    {
                        button_value = j;
                    }
                    else if (i == 1)
                    {
                        button_value = j + 3;
                    }
                    else if (i == 2)
                    {
                        button_value = j + 6;
                    }
                    if (game.the_board[i, j] == "0")
                    {
                        buttons[button_value].Text = "";
                    }
                    else if (game.the_board[i, j] == "X")
                    {
                        buttons[button_value].Text = "X";
                        buttons[button_value].ForeColor = Color.MidnightBlue;
                    }
                    else if (game.the_board[i, j] == "O")
                    {
                        buttons[button_value].Text = "O";
                        buttons[button_value].ForeColor = Color.DarkRed;
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            game = new Tic();
            foreach (var item in buttons)
            {
                item.Enabled = true;
            }
            updateBoard();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            game = new Tic();
            foreach (var item in buttons)
            {
                item.Enabled = true;
            }
            updateBoard();
            Random rnd = new Random();
            int random_num = rnd.Next(1, 3);

            if (random_num == 1)
            {
                computerChoose();
            }
            else
            {
                game.the_board[1, 1] = "X";
                game.turn += 1;
                game.entered_moves.Add("5");
                updateBoard();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

    }
}
