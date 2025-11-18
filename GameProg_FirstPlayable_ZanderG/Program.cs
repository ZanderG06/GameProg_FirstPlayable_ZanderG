using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProg_FirstPlayable_ZanderG
{
    internal class Program
    {
        static string map = "map.txt";
        static string[] mapInGame = System.IO.File.ReadAllLines(map);
        static int playerX = 0;
        static int playerY = 0;
        static int enemyX = 17;
        static int enemyY = 11;
        static bool isPlaying = true;
        static bool playerTurn = true;
        
        static void Main(string[] args)
        {
            while (isPlaying)
            {
                DisplayMap();
                PlayerMovement();
                DisplayMap();
                enemyMovement();
            }
        }

        static void DisplayMap()
        {
            Console.SetCursorPosition(0, 0);
            
            int length = mapInGame.Length;
            int height = mapInGame[0].Length;
            
            for (int i = 0; i < height + 2; i++) Console.Write('░');

            Console.Write("\n");

            for (int i = 0; i < length; i++)
            {
                Console.Write('░');
                for (int j = 0; j < height; j++)
                {
                    if (mapInGame[i][j] == '*')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (mapInGame[i][j] == '~')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }

                    Console.Write(mapInGame[i][j]);
                    Console.ResetColor();
                }
                Console.Write('░');
                Console.Write("\n");
            }

            for (int i = 0; i < height + 2; i++) Console.Write('░');

            Console.SetCursorPosition(playerX + 1, playerY + 1);
            Console.Write('@');

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(enemyX + 1, enemyY + 1);
            Console.Write('#');

            Console.ResetColor();
        }

        static void PlayerMovement()
        {
            if (playerTurn == true)
            {
                bool player1 = true;

                while (player1)
                {
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Player 1's turn");
                    Console.WriteLine("WASD/Arrow Keys to move");

                    ConsoleKey playerInput = Console.ReadKey(true).Key;

                    if (playerInput == ConsoleKey.W || playerInput == ConsoleKey.UpArrow)
                    {
                        if(playerY <= 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (mapInGame[playerY-1][playerX] == '*')
                            {
                                player1 = false;
                                playerY--;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (playerInput == ConsoleKey.A || playerInput == ConsoleKey.LeftArrow)
                    {
                        if (playerX <= 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (mapInGame[playerY][playerX-1] == '*')
                            {
                                player1 = false;
                                playerX--;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (playerInput == ConsoleKey.S || playerInput == ConsoleKey.DownArrow)
                    {
                        if (playerY >= mapInGame.GetLength(0)-1)
                        {
                            continue;
                        }
                        else
                        {
                            if (mapInGame[playerY+1][playerX] == '*')
                            {
                                player1 = false;
                                playerY++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (playerInput == ConsoleKey.D || playerInput == ConsoleKey.RightArrow)
                    {
                        if (playerX >= mapInGame[0].Length -1)
                        {
                            continue;
                        }
                        else
                        {
                            if (mapInGame[playerY][playerX+1] == '*')
                            {
                                player1 = false;
                                playerX++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        static void enemyMovement()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Computer's turn");
            Console.WriteLine("WASD/Arrow Keys to move");
            Thread.Sleep(1000);

            
        }
    }
}
