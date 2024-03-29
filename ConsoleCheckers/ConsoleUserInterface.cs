﻿using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Models;

namespace ConsoleCheckers
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void GameOver(IPlayer whitePlayer, IPlayer blackPlayer, GameResult result, int moveCount)
        {
            Console.WriteLine("Game over !!!");
            switch (result)
            {
                case GameResult.WhiteWin:
                    Console.WriteLine($"Player {whitePlayer.Name} (White) won in {moveCount} moves");
                    break;
                case GameResult.BlackWin:
                    Console.WriteLine($"Player {blackPlayer.Name} (Black) won in {moveCount} moves");
                    break;
                default:
                    Console.WriteLine($"Draw after {moveCount} moves");
                    break;
            }
        }

        public void ShowBoard(Board board)
        {
            Console.WriteLine("  | " + string.Join(" | ", Enumerable.Range(0, board.Size)) + " |");
            Console.WriteLine(new string('-', board.Size * 4 + 5));
            for (int row = board.Size - 1; row >= 0; row--)
            {
                Console.Write($"{row} |");
                for (int column = 0; column < board.Size; column++)
                {
                    if (board.IsPositionEnabled(row, column))
                    {
                        var figure = board.Positions[row, column].Figure;
                        if (figure != null)
                        {
                            Console.ForegroundColor = figure.Color == FigureColor.White ? ConsoleColor.Blue : ConsoleColor.Red;
                            Console.Write(figure is Man ? " M " : " K ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                    else
                    {
                        Console.Write(" x ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine($" {row}");
                Console.WriteLine(new string('-', board.Size * 4 + 5));
            }
            Console.WriteLine("  | " + string.Join(" | ", Enumerable.Range(0, board.Size)) + " |");
            Console.WriteLine();
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowNextMove(IPlayer player)
        {
            var color = player.Color == FigureColor.White ? "White" : "Black";
            Console.WriteLine($"Player {player.Name} ({color}) move: ");
        }
    }
}
