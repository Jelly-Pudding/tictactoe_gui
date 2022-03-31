using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    public class Tic
    {
        public string[,] the_board;
        public int turn;
        public bool x_win;
        public bool o_win;
        public HashSet<string> entered_moves;

        public Tic()
        {
            the_board = new string[3, 3] { { "0", "0", "0" }, { "0", "0", "0" }, { "0", "0", "0" } };
            turn = 0;
            x_win = false;
            o_win = false;
            entered_moves = new HashSet<string>();
        }
        public void printer(string[,] matrix)
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("+++++++");
                Console.WriteLine("|" + matrix[i, 0] + "|" + matrix[i, 1] + "|" + matrix[i, 2] + "|");
                if (i == 2)
                {
                    Console.WriteLine("+++++++\n");
                }
            }
        }

        public void make_move(string[,] matrix)
        {
            Console.WriteLine("Enter your move (1 to 9):");
            string string_move = Console.ReadLine();
            while (string_move != "1" && string_move != "2" && string_move != "3" &&
                   string_move != "4" && string_move != "5" && string_move != "6" &&
                   string_move != "7" && string_move != "8" && string_move != "9" ||
                   this.entered_moves.Contains(string_move))
            {
                Console.WriteLine("Enter a valid move (between 1-9 and on an empty square):");
                string_move = Console.ReadLine();
            }

            this.entered_moves.Add(string_move);
            int move = Int32.Parse(string_move);
            this.turn += 1;
            string sign = "X";
            if (this.turn % 2 == 0)
            {
                sign = "O";
            }
            if (move <= 3)
            {
                matrix[0, move - 1] = sign;
            }
            else if (move <= 6)
            {
                matrix[1, move - 4] = sign;
            }
            else if (move <= 9)
            {
                matrix[2, move - 7] = sign;
            }
        }

        public string make_ai_move((int, int) move_tuple)
        {
            this.turn += 1;
            string sign = "X";
            if (this.turn % 2 == 0)
            {
                sign = "O";
            }
            this.the_board[move_tuple.Item1, move_tuple.Item2] = sign;
            string num = "";
            if (move_tuple.Item1 == 0)
            {
                num = (move_tuple.Item2 + 1).ToString();
                this.entered_moves.Add(num);
            }
            else if (move_tuple.Item1 == 1)
            {
                num = (move_tuple.Item2 + 4).ToString();
                this.entered_moves.Add(num);
            }
            else
            {
                num = (move_tuple.Item2 + 7).ToString();
                this.entered_moves.Add(num);
            }

            return num;

        }
        public List<(int, int)> get_available_moves()
        {

            List<(int, int)> moves = new List<(int, int)>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (this.the_board[i, j] == "0")
                    {
                        moves.Add((i, j));
                    }
                }
            }
            return moves;
        }

        public void check_ending(string[,] matrix)
        {

            for (int i = 0; i < 3; i++)
            {

                // horizontal

                if (matrix[i, 0] == matrix[i, 1] && matrix[i, 0] == matrix[i, 2])
                {
                    if (matrix[i, 0] == "X")
                    {
                        this.x_win = true;
                        return;
                    }
                    else if (matrix[i, 0] == "O")
                    {
                        this.o_win = true;
                        return;
                    }
                }

                // vertical

                if (matrix[0, i] == matrix[1, i] && matrix[0, i] == matrix[2, i])
                {
                    if (matrix[0, i] == "X")
                    {
                        this.x_win = true;
                        return;
                    }
                    else if (matrix[0, i] == "O")
                    {
                        this.o_win = true;
                        return;
                    }
                }
            }

            // positive diagonal 
            if (matrix[0, 0] == matrix[1, 1] && matrix[0, 0] == matrix[2, 2])
            {
                if (matrix[0, 0] == "X")
                {
                    this.x_win = true;
                    return;
                }
                else if (matrix[0, 0] == "O")
                {
                    this.o_win = true;
                    return;
                }
            }

            // negative diagonal
            if (matrix[2, 0] == matrix[1, 1] && matrix[2, 0] == matrix[0, 2])
            {
                if (matrix[2, 0] == "X")
                {
                    this.x_win = true;
                    return;
                }
                else if (matrix[2, 0] == "O")
                {
                    this.o_win = true;
                    return;
                }
            }

        }
        public ((int, int), int) ai_minimax(Tic game, int depth, bool is_maximising)
        {
            game.check_ending(game.the_board);
            if (game.x_win == true)
            {
                game.x_win = false;
                game.o_win = false;
                return ((0, 0), 10000);
            }
            else if (game.o_win == true)
            {
                game.o_win = false;
                return ((0, 0), -10000);
            }
            else if (game.turn == 9)
            {
                return ((0, 0), 0);
            }
            else if (depth == 0)
            {
                return ((0, 0), 0);
            }
            int best_count = 0;
            (int, int) best_move = (0, 0);
            if (is_maximising == true)
            {
                best_count = -1000;
                List<(int, int)> children = game.get_available_moves();
                for (int i = 0; i < children.Count; i++)
                {
                    string move_entered = game.make_ai_move(children[i]);
                    int score = game.ai_minimax(game, depth - 1, false).Item2;
                    game.entered_moves.Remove(move_entered);
                    game.the_board[children[i].Item1, children[i].Item2] = "0";
                    game.turn -= 1;
                    if (score > best_count)
                    {
                        best_move = children[i];
                        best_count = score;
                    }
                }
                return (best_move, best_count);
            }

            else if (is_maximising == false)
            {
                best_count = 1000;
                List<(int, int)> children = game.get_available_moves();
                for (int i = 0; i < children.Count; i++)
                {
                    string move_entered = game.make_ai_move(children[i]);
                    int score = game.ai_minimax(game, depth - 1, true).Item2;
                    game.entered_moves.Remove(move_entered);
                    game.the_board[children[i].Item1, children[i].Item2] = "0";
                    game.turn -= 1;
                    if (score < best_count)
                    {
                        best_move = children[i];
                        best_count = score;
                    }
                }
                return (best_move, best_count);
            }
            return (best_move, best_count);
        }

    }
}


class Program
{
    static void Main(string[] args)
    {
        Tic game = new Tic();
        game.printer(game.the_board);
        while (game.turn < 9 && game.x_win == false && game.o_win == false)
        {
            game.make_move(game.the_board);
            game.printer(game.the_board);
            game.check_ending(game.the_board);
            if (game.x_win == false && game.o_win == false && game.turn != 9)
            {
                (int, int) ai_minimax_move = game.ai_minimax(game, 10, false).Item1;
                string num = "";
                if (ai_minimax_move.Item1 == 0)
                {
                    num = (ai_minimax_move.Item2 + 1).ToString();
                }
                else if (ai_minimax_move.Item1 == 1)
                {
                    num = (ai_minimax_move.Item2 + 4).ToString();
                }
                else
                {
                    num = (ai_minimax_move.Item2 + 7).ToString();
                }
                Console.WriteLine("The ai chose square " + num + ".");
                game.make_ai_move(ai_minimax_move);
                game.check_ending(game.the_board);
                game.printer(game.the_board);
            }
        }
        if (game.x_win == true)
        {
            Console.WriteLine("Crosses won!");
        }
        else if (game.o_win == true)
        {
            Console.WriteLine("Naughts won!");
        }
        else
        {
            Console.WriteLine("It was a draw!");
        }

    }

}