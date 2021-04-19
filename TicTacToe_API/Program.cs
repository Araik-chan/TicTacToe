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
            
            int count = -1;
            string Index = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Index.txt";
            string fileName = @"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game0.txt";
            List<string> Index_List = new List<string>();



            string[] Lines = System.IO.File.ReadAllLines(@"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Index.txt");
            foreach (string line in Lines) // Read all line from the text file  
            {
                Index_List.Add(line);
            }




            //Open file
            string s = "";
            using (StreamReader sr = File.OpenText(Index))
            {


                //Check if Game has been created 
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



            //Add file to index if it is not already in the index
            if (!Index_List.Contains(count.ToString()))
            {
                using (StreamWriter sw = File.AppendText(Index))
                {
                    sw.WriteLine(count);
                    sw.Close();
                }
            }


            // Create a new file or append to existing file
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
                int count_col = 1;
                int count_row = 0; 
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
                    Random rnd = new Random();
                    new_line = count_col.ToString() + "," + count_row.ToString();

                    count_col = rnd.Next(1, 3);
                    count_row = rnd.Next(0, 2);
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

        public static bool End_Game(string winner)
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

            var lines = System.IO.File.ReadAllLines(@"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt");
            System.IO.File.WriteAllLines(@"C:\\Users\\kiara\\source\\repos\\TicTacToe\\TicTacToe_API\\Databases\\Game" + count + ".txt", lines.Take(lines.Length - 1).ToArray());

            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(winner);
                sw.WriteLine("End");
                sw.Close();
            }
            return true;
        }
    }


    
}
