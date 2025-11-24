using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GameProg_FirstPlayable_ZanderG
{
    internal class Program
    {
        static string map = "map.txt"; //Gets path of map
        static string[] mapInGame = File.ReadAllLines(map); //Makes map an array
        static int playerX = 0;
        static int playerY = 0;
        static int enemyX = 17;
        static int enemyY = 11;
        static int playerHealth = 5;
        static int enemyHealth = 3;
        static int goldAmount = 0;
        static bool isPlaying = true;
        static List<(int, int)> gold = new List<(int, int)>();
        
        static void Main(string[] args)
        {
            gold.Add((0, 5));
            gold.Add((11, 0));
            gold.Add((9, 17));
            gold.Add((4, 6));
            gold.Add((5, 14));
            
            while (isPlaying)
            {
                DisplayMap();
                PlayerMovement();
                DisplayMap();
                enemyMovement();

                if (playerHealth <= 0)
                {
                    isPlaying = false;
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Computer Wins!        ");
                    Thread.Sleep(1000);
                    Console.ReadKey(true);
                }

                if (enemyHealth <= 0)
                {
                    isPlaying = false;
                    Console.SetCursorPosition(0, 15);
                    Console.WriteLine("Player 1 Wins!        ");
                    Thread.Sleep(1000);
                    Console.ReadKey(true);
                }
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
                    else if (mapInGame[i][j] == '░')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
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

            foreach((int x, int y) in gold)
            {
                Console.SetCursorPosition(y+1, x+1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write('&');
                Console.ResetColor();
            }

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
                Console.WriteLine($"Player Health: {playerHealth}         ");
                Console.WriteLine($"Enemy Health: {enemyHealth}         ");
                Console.WriteLine($"Gold: {goldAmount}    ");

                ConsoleKey playerInput = Console.ReadKey(true).Key;

                if (playerInput == ConsoleKey.W || playerInput == ConsoleKey.UpArrow)
                {
                    if (playerY <= 0)
                    {
                        continue;
                    }
                    else
                    {
                        if (mapInGame[playerY - 1][playerX] == '*' || mapInGame[playerY - 1][playerX] == '░')
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
                        if (mapInGame[playerY][playerX - 1] == '*' || mapInGame[playerY][playerX - 1] == '░')
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
                        if (mapInGame[playerY + 1][playerX] == '*' || mapInGame[playerY + 1][playerX] == '░')
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
                        if (mapInGame[playerY][playerX + 1] == '*' || mapInGame[playerY][playerX + 1] == '░')
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

            if (mapInGame[playerY][playerX] == '░')
            {
                playerHealth--;

                Console.SetCursorPosition(0, 15);
                Console.WriteLine("Player stepped on Lava!          ");
                Console.WriteLine("WASD/Arrow Keys to move");
                Console.WriteLine($"Player Health: {playerHealth}         ");
                Console.WriteLine($"Enemy Health: {enemyHealth}         ");
                Console.WriteLine($"Gold: {goldAmount}    ");
                Thread.Sleep(1000);

            }

            for(int i = 0; i < gold.Count; i++)
            {
                if (gold.Contains((playerY, playerX)))
                {
                    gold.Remove((playerY, playerX));
                    goldAmount++;

                    if (goldAmount >= 5)
                    {
                        Console.SetCursorPosition(0, 15);
                        Console.WriteLine("Player got gold!          ");
                        Console.WriteLine("Damage Doubled!        ");
                        Console.WriteLine($"Player Health: {playerHealth}         ");
                        Console.WriteLine($"Enemy Health: {enemyHealth}         ");
                        Console.WriteLine($"Gold: {goldAmount}    ");
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.SetCursorPosition(0, 15);
                        Console.WriteLine("Player got gold!          ");
                        Console.WriteLine("WASD/Arrow Keys to move");
                        Console.WriteLine($"Player Health: {playerHealth}         ");
                        Console.WriteLine($"Enemy Health: {enemyHealth}         ");
                        Console.WriteLine($"Gold: {goldAmount}    ");
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        static void enemyMovement()
        {
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Computer's turn          ");
            Console.WriteLine("WASD/Arrow Keys to move");
            Console.WriteLine($"Player Health: {playerHealth}         ");
            Console.WriteLine($"Enemy Health: {enemyHealth}         ");
            Console.WriteLine($"Gold: {goldAmount}    ");
            Thread.Sleep(1000);

            int targetX = playerX - enemyX;
            int targetY = playerY - enemyY;

            if (targetX < 0 && mapInGame[enemyY][enemyX - 1] == '*' || targetX < 0 && mapInGame[enemyY][enemyX - 1] == '░')
            {
                enemyX--;
            }
            else if (targetX > 0 && mapInGame[enemyY][enemyX + 1] == '*' || targetX > 0 && mapInGame[enemyY][enemyX + 1] == '░')
            {
                enemyX++;
            }
            else if (targetY < 0 && mapInGame[enemyY - 1][enemyX] == '*' || targetY < 0 && mapInGame[enemyY - 1][enemyX] == '░')
            {
                enemyY--;
            }
            else if (targetY > 0 && mapInGame[enemyY + 1][enemyX] == '*' || targetY > 0 && mapInGame[enemyY + 1][enemyX] == '░')
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
            if (goldAmount >= 5)
            {
                enemyHealth -= 2;
            }
            else
            {
                enemyHealth--;
            }




            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Player Attacked!!!");
            Console.WriteLine("Computer runs away!       ");
            Console.WriteLine($"Player Health: {playerHealth}         ");
            Console.WriteLine($"Enemy Health: {enemyHealth}         ");
            Console.WriteLine($"Gold: {goldAmount}    ");
            Thread.Sleep(1000);

            bool enemyEscape = false;

            if (enemyX + 6 <= mapInGame[0].Length)
            {
                if (mapInGame[enemyY][enemyX + 5] == '*' || mapInGame[enemyY][enemyX + 5] == '░')
                {
                    enemyX += 5;
                    enemyEscape = true;
                }
            }
            if (enemyY + 6 <= mapInGame[1].Length && enemyEscape == false)
            {
                if (mapInGame[enemyY + 5][enemyX] == '*' || mapInGame[enemyY + 5][enemyX] == '░')
                {
                    enemyY += 5;
                    enemyEscape = true;
                }
            }
            if (enemyX - 6 >= 0 && enemyEscape == false)
            {
                if (mapInGame[enemyY][enemyX - 5] == '*' || mapInGame[enemyY][enemyX - 5] == '░')
                {
                    enemyX -= 5;
                    enemyEscape = true;
                }
            }
            if (enemyY - 6 >= 0 && enemyEscape == false)
            {
                if (mapInGame[enemyY - 5][enemyX] == '*' || mapInGame[enemyY - 5][enemyX] == '░')
                {
                    enemyY -= 5;
                    enemyEscape = true;
                }
            }
            if (enemyEscape == false)
            {
                enemyX = 17;
                enemyY = 11;
            }
        }

        static void EnemyAttack()
        {
            playerHealth--;
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Computer Attacked!!!");
            Console.WriteLine("Player runs away!          ");
            Console.WriteLine($"Player Health: {playerHealth}         ");
            Console.WriteLine($"Enemy Health: {enemyHealth}         ");
            Console.WriteLine($"Gold: {goldAmount}    ");
            Thread.Sleep(1000);

            bool playerEscape = false;

            if (playerX + 5 <= mapInGame[0].Length)
            {
                if (mapInGame[playerY][playerX + 5] == '*' || mapInGame[playerY][playerX + 5] == '░')
                {
                    playerX += 5;
                    playerEscape = true;
                }
            }
            if (playerY + 5 <= mapInGame[1].Length && playerEscape == false)
            {
                if (mapInGame[playerY + 5][playerX] == '*' || mapInGame[playerY + 5][playerX] == '░')
                {
                    playerY += 5;
                    playerEscape = true;
                }
            }
            if (playerX - 5 >= 0 && playerEscape == false)
            {
                if (mapInGame[playerY][playerX - 5] == '*' || mapInGame[playerY][playerX - 5] == '░')
                {
                    playerY -= 5;
                    playerEscape = true;
                }
            }
            if (enemyY - 5 >= 0 && playerEscape == false)
            {
                if (mapInGame[playerY - 5][playerX] == '*' || mapInGame[playerY - 5][playerX] == '░')
                {
                    playerY -= 5;
                    playerEscape = true;
                }
            }
            if (playerEscape == false)
            {
                playerX = 0;
                playerY = 0;
            }
        }
    }
}