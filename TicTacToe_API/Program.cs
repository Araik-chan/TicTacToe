using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe_API;

namespace TicTacToe_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static List<Player_Move> Return_Position(string player, int row, int col,string pos)
        {
            
            int count = 0;
            string Index = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Index.txt";
            string fileName = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt";
            int linecount = 0;

            //Open file
            string s = "";
            using (StreamReader sr = File.OpenText(Index))
            {


                //Check if Game has been created 
                while ((s = sr.ReadLine()) != null)
                {
                    linecount++;
                    using (StreamReader ar = File.OpenText(fileName))
                    {
                        string a = "";
                        while ((a = ar.ReadLine()) != null)
                        {
                            if (a == "End")
                            {
                                count++;
                                fileName = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt";
                            }

                        }
                    }

                }

                //check if game is in Index
                linecount = linecount - 1;
                s = sr.ReadLine();

               
                

            }

            if (s == null)
            {
                using (StreamWriter sw = File.AppendText(Index))
                {
                    sw.WriteLine();
                    sw.Close();
                }
            }
            else
            {
                if (!s.Contains(linecount.ToString()))
                {
                    using (StreamWriter sw = File.AppendText(Index))
                    {
                        sw.WriteLine(linecount);
                        sw.Close();
                    }
                }
            }
            // Create a new file or append to exsising file
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("Player:" + player +  ",col:" + col + ",row:" + row);
                sw.Close();
            }

            List<Player_Move> play2 = new List<Player_Move>();
             Player_Move play2_move = new Player_Move();

            //Open file
            using (StreamReader sr = File.OpenText(fileName))
            {
                string b = "";
                int count_col = col + 1;
                int count_row = row;
                string new_line = "";
                List<string> check = new List<string>();

                //Get played moves from file
                while ((b = sr.ReadLine()) != null)
                {
                    string[] lines = b.Split(',');
                    string[] col_split = lines[1].Split(':');
                    string[] row_split = lines[2].Split(':');

                    string play = col_split[1] + "," + row_split[1];
                    check.Add(play);
                                    
                }

                //Play Os Turn
                do
                {
                    new_line = count_col.ToString() + "," + count_row.ToString();

                    if (count_col == 3)
                    {
                        count_col = 0;
                    }
                    else if(count_col == 0 && count_row == 0)
                    {
                        count_col = 1;
                        count_row = 1;
                    }
                    else if (count_col == 0 && count_row == 1)
                    {
                        count_col = 1;
                        count_row = 1;
                    }
                    else if (count_col == 0 && count_row == 2)
                    {
                        count_col = 1;
                        count_row = 1;
                    }
                    else if (count_col == 1 && count_row == 0)
                    {
                        count_col = 1;
                        count_row = 2;
                    }
                    else if (count_col == 1 && count_row == 1)
                    {
                        count_col = 1;
                        count_row = 1;
                    }
                    else if (count_col == 1 && count_row == 2)
                    {
                        count_col = 0;
                        count_row = 2;
                    }
                    else if (count_col == 2 && count_row == 0)
                    {
                        count_col = 2;
                        count_row = 1;
                    }
                    else if (count_col == 2 && count_row == 1)
                    {
                        count_col = 0;
                        count_row = 0;
                    }
                    else if (count_col == 2 && count_row == 2)
                    {
                        count_col = 1;
                        count_row = 2;
                    }
                    if (count_row == 3)
                    {
                        count_row = 0;
                    }
                }
                while (check.Contains(new_line));

                play2_move.col = count_col;
                play2_move.row = count_row;
                play2_move.player = "O";
                play2_move.pos = pos;
                play2.Add(play2_move);
            }

            //write player Os turn to file
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine("Player:" + play2_move.player + ",col:" + play2_move.col + ",row:" + play2_move.row);
                sw.Close();
            }
           
            //return player Os turn to board
            return play2;
        }

        public static bool End_Game(string winner, string player)
        {
            int count = 0;
            string Index = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Index.txt";
            string fileName = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt";



            using (StreamReader sr = File.OpenText(Index))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    using (StreamReader ar = File.OpenText(fileName))
                    {
                        string a = "";
                        while ((a = ar.ReadLine()) != null)
                        {
                            if (a == "End")
                            {
                                count++;
                                fileName = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt";
                            }

                        }
                    }

                }

            }
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(winner + " " + player);
                sw.WriteLine("End");
                sw.Close();
            }
            return true;
        }
    }


    
}
