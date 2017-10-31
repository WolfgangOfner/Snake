// ----------------------------------------------------------------------- 
// <copyright file="Program.cs" company="FHWN"> 
// Copyright (c) FHWN. All rights reserved. 
// </copyright> 
// <summary>This program works with geometric objects.</summary> 
// <author>Wolfgang Ofner</author> 
// -----------------------------------------------------------------------

namespace Snake
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        private static List<Snake> liste = new List<Snake>();

        static int window_width = 0;
        static int window_height = 0;

        static Random row = new Random();
        static Random col = new Random();
        static Random row_special = new Random();
        static Random col_special = new Random();

        static int apple_row = 0;
        static int apple_col = 0;
        static int special_row = 0;
        static int special_col = 0;

        static int direction = 0;
        static int starting_time = 0;
        static int time = 0;
        static int score = 0;
        static int difficulty = 0;
        static int mode = 0;

        static void Main(string[] args)
        {
            Console.BufferHeight = Console.LargestWindowHeight - 10;
            Console.BufferWidth = Console.LargestWindowWidth - 10;
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.LargestWindowWidth - 10;
            window_height = Console.WindowHeight;
            window_width = Console.WindowWidth;

            apple_row = row.Next(0, window_height);
            apple_col = col.Next(0, window_width);
            special_row = row.Next(0, window_height);
            special_col = col.Next(0, window_width);

            Snake head = new Snake(window_width / 2, window_height / 2);

            liste.Add(head);

            bool exit = false;

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please enter the difficulty:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1: Easy");
                Console.WriteLine("2: Medium");
                Console.WriteLine("3: Hard");
                Console.WriteLine("4: Ultra");
                Console.WriteLine("5: You will die");
                Console.ResetColor();
                string input = Console.ReadLine();

                try
                {
                    difficulty = Convert.ToInt32(input);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou haven't entered a number!\n");
                    Console.ResetColor();
                    continue;
                }

                switch (difficulty)
                {
                    case 1:
                        starting_time = 120;
                        exit = true;
                        break;

                    case 2:
                        starting_time = 100;
                        exit = true;

                        break;

                    case 3:
                        starting_time = 80;
                        exit = true;

                        break;

                    case 4:
                        starting_time = 60;
                        exit = true;

                        break;

                    case 5:
                        starting_time = 40;
                        exit = true;

                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou have insert a wrong number!\n");
                        Console.ResetColor();
                        continue;

                }
            } while (exit == false);

            time = starting_time;

            do
            {
                exit = false;
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Which kind of snake do you want to play?");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1: Snake");
                Console.WriteLine("2: Snake 2");
                Console.ResetColor();
                string input = Console.ReadLine();

                try
                {
                    mode = Convert.ToInt32(input);
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nYou haven't entered a number!\n");
                    Console.ResetColor();
                    continue;
                }

                switch (mode)
                {

                    case 1:
                        exit = true;
                        break;

                    case 2:
                        exit = true;
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nYou have insert a wrong number!\n");
                        Console.ResetColor();
                        continue;
                }
            }
            while (exit == false);

            ConsoleKeyInfo k = new ConsoleKeyInfo('k', ConsoleKey.K, false, false, false);

            do
            {
                draw();

                check_crash();
                check_apple();
                System.Threading.Thread.Sleep(starting_time);

                if (score % (difficulty * 5) == 0)
                {
                    check_special();
                }

                if (Console.KeyAvailable)
                {
                    k = Console.ReadKey(true);
                }
                else
                {
                    if (direction != 3 && k.Key == ConsoleKey.DownArrow)
                    {
                        for (int i = 0; i < liste.Count; i++)
                        {
                            if (i == liste.Count - 1)
                            {
                                liste[i].Row += 1;
                                break;
                            }
                            liste[i].Col = liste[i + 1].Col;
                            liste[i].Row = liste[i + 1].Row;
                        }

                        direction = 1;
                    }

                    if (direction != 4 && k.Key == ConsoleKey.LeftArrow)
                    {
                        for (int i = 0; i < liste.Count; i++)
                        {
                            if (i == liste.Count - 1)
                            {
                                liste[i].Col -= 1;
                                break;
                            }
                            liste[i].Col = liste[i + 1].Col;
                            liste[i].Row = liste[i + 1].Row;
                        }

                        direction = 2;

                    }

                    if (direction != 1 && k.Key == ConsoleKey.UpArrow)
                    {
                        for (int i = 0; i < liste.Count; i++)
                        {
                            if (i == liste.Count - 1)
                            {
                                liste[i].Row -= 1;
                                break;
                            }
                            liste[i].Col = liste[i + 1].Col;
                            liste[i].Row = liste[i + 1].Row;
                        }

                        direction = 3;
                    }

                    if (direction != 2 && k.Key == ConsoleKey.RightArrow)
                    {
                        for (int i = 0; i < liste.Count; i++)
                        {
                            if (i == liste.Count - 1)
                            {
                                liste[i].Col += 1;
                                break;
                            }
                            liste[i].Col = liste[i + 1].Col;
                            liste[i].Row = liste[i + 1].Row;
                        }

                        direction = 4;
                    }
                }
            }
            while (true);

        }

        private static void draw()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(Console.WindowWidth - 12, 0);
            Console.WriteLine("Score: {0}", score);

            for (int i = 0; i < liste.Count; i++)
            {
                if (mode == 1)
                {                                                       // snake 1
                    if (liste[i].Row < 0 || liste[i].Col < 0 || liste[i].Row == window_height || liste[i].Col == window_width)
                    {
                        int middle_of_window_width = (Console.LargestWindowWidth - 30) / 2;
                        Console.SetCursorPosition(middle_of_window_width, 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("You crashed into the wall.");
                        Console.SetCursorPosition(middle_of_window_width, 2);
                        Console.WriteLine("         GAME OVER!");
                        Console.ResetColor();
                        System.Threading.Thread.Sleep(5000);
                        Environment.Exit(1);
                    }
                }
                else
                {                                                           // snake 2
                    if (liste[i].Row == window_height)
                    {
                        liste[i].Row = 0;
                    }

                    if (liste[i].Col == window_width)
                    {
                        liste[i].Col = 0;
                    }

                    if (liste[i].Row < 0)
                    {
                        liste[i].Row = window_height - 1;
                    }

                    if (liste[i].Col < 0)
                    {
                        liste[i].Col = window_width - 1;
                    }

                    if (score % (difficulty * 5) == 0 && score != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.SetCursorPosition(special_col, special_row);
                        Console.WriteLine("A");
                    }
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(liste[i].Col, liste[i].Row);
                Console.WriteLine("X");
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(apple_col, apple_row);
            Console.WriteLine("O");
            Console.ResetColor();
        }

        private static void check_crash()
        {
            for (int i = 0; i < liste.Count - 1; i++)
            {
                if (liste[liste.Count - 1].Col == liste[i].Col && liste[liste.Count - 1].Row == liste[i].Row)
                {
                    int middle_of_window_width = (Console.LargestWindowWidth - 30) / 2;
                    Console.SetCursorPosition(middle_of_window_width, (Console.LargestWindowHeight / 2) - 7);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You crashed into the snake.");
                    Console.SetCursorPosition(middle_of_window_width, (Console.LargestWindowHeight / 2) - 6);
                    Console.WriteLine("         GAME OVER!");
                    Console.ResetColor();
                    System.Threading.Thread.Sleep(5000);
                    Environment.Exit(1);
                }
            }
        }

        private static void check_apple()
        {
            if (apple_row == liste[liste.Count - 1].Row && apple_col == liste[liste.Count - 1].Col)
            {
                bool exit = false;

                do
                {
                    apple_row = row.Next(0, window_height);
                    apple_col = col.Next(0, window_width);

                    for (int i = 0; i < liste.Count; i++)
                    {
                        if (apple_col == liste[i].Col)
                        {
                            continue;
                        }

                        if (apple_row == liste[i].Row)
                        {
                            continue;
                        }
                    }
                    exit = true;

                } while (exit == false);

                score += difficulty;

                if (time > starting_time / 2)
                {
                    time -= 2;
                }

                switch (direction)
                {
                    case 1:         //down
                        Snake down = new Snake(liste[0].Col, liste[0].Row - 1);
                        liste.Insert(0, down);
                        break;

                    case 2:         //left
                        Snake left = new Snake(liste[0].Col + 1, liste[0].Row);
                        liste.Insert(0, left);
                        break;

                    case 3:             //top
                        Snake top = new Snake(liste[0].Col, liste[0].Row + (0 + 1));
                        liste.Insert(0, top);
                        break;

                    case 4:             //right
                        Snake right = new Snake(liste[0].Col + (0 - 1), liste[0].Row);
                        liste.Insert(0, right);
                        break;

                    default:

                        break;
                }
            }
        }

        private static void check_special()
        {
            if (special_row == liste[liste.Count - 1].Row && special_col == liste[liste.Count - 1].Col)
            {
                bool exit = false;

                do
                {
                    special_row = row_special.Next(0, window_height);
                    special_col = col_special.Next(0, window_width);

                    for (int i = 0; i < liste.Count; i++)
                    {
                        if (special_col == liste[i].Col)
                        {
                            continue;
                        }

                        if (special_row == liste[i].Row)
                        {
                            continue;
                        }
                    }

                    exit = true;

                }
                while (exit == false);

                score += difficulty * 6;
            }
        }
    }
}
