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
        static string map = "map.txt"; //Gets path of map
        static string[] mapInGame = System.IO.File.ReadAllLines(map); //Makes map an array
        static int playerX = 0;
        static int playerY = 0;
        static int enemyX = 17;
        static int enemyY = 11;
        static int playerHealth;
        static int enemyHealth;
        static bool isPlaying = true;
        
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
            bool player1 = true;

            while (player1)
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("Player 1's turn        ");
                Console.WriteLine("WASD/Arrow Keys to move");

                ConsoleKey playerInput = Console.ReadKey(true).Key;

                if (playerInput == ConsoleKey.W || playerInput == ConsoleKey.UpArrow)
                {
                    if (playerY <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (mapInGame[playerY - 1][playerX] == '*')
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
                        if (mapInGame[playerY][playerX - 1] == '*')
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
                    if (playerY >= mapInGame.GetLength(0) - 1)
                    {
                        continue;
                    }
                    else
                    {
                        if (mapInGame[playerY + 1][playerX] == '*')
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
                    if (playerX >= mapInGame[0].Length - 1)
                    {
                        continue;
                    }
                    else
                    {
                        if (mapInGame[playerY][playerX + 1] == '*')
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

            if (playerX == enemyX && playerY == enemyY)
            {
                PlayerAttack();
            }
        }

        static void enemyMovement()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Computer's turn          ");
            Console.WriteLine("WASD/Arrow Keys to move");
            Thread.Sleep(1000);

            int targetX = playerX - enemyX;
            int targetY = playerY - enemyY;

            if (targetX < 0 && mapInGame[enemyY][enemyX - 1] == '*')
            {
                enemyX--;
            }
            else if (targetX > 0 && mapInGame[enemyY][enemyX + 1] == '*')
            {
                enemyX++;
            }
            else if (targetY < 0 && mapInGame[enemyY - 1][enemyX] == '*')
            {
                enemyY--;
            }
            else if (targetY > 0 && mapInGame[enemyY + 1][enemyX] == '*')
            {
                enemyY++;
            }

            if (playerX == enemyX && playerY == enemyY)
            {
                EnemyAttack();
            }
        }

        static void PlayerAttack()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Player Attacked!!!");
            Console.WriteLine("WASD/Arrow Keys to move");
            Thread.Sleep(1000);

            

            if (mapInGame[enemyY][enemyX + 5] == '*')
            {
                enemyX += 5;
            }
            else if (mapInGame[enemyY + 5][enemyX] == '*')
            {
                enemyY += 5;
            }
            else if (mapInGame[enemyY][enemyX - 5] == '*')
            {
                enemyY -= 5;
            }
            else if (mapInGame[enemyY - 5][enemyX] == '*')
            {
                enemyY -= 5;
            }
            else
            {
                enemyX = 17;
                enemyY = 11;
            }
        }

        static void EnemyAttack()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Computer Attacked!!!");
            Console.WriteLine("WASD/Arrow Keys to move");
            Thread.Sleep(1000);

            if (mapInGame[playerY][playerX + 5] == '*')
            {
                playerX += 5;
            }
            else if (mapInGame[playerY + 5][playerX] == '*')
            {
                playerY += 5;
            }
            else if (mapInGame[playerY][playerX - 5] == '*')
            {
                playerY -= 5;
            }
            else if (mapInGame[playerY - 5][playerX] == '*')
            {
                playerY -= 5;
            }
            else
            {
                playerX = 0;
                playerY = 0;
            }
        }
    }
}
