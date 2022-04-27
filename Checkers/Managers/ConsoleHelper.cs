﻿using Checkers.Models;

namespace Checkers.Managers
{
    public class ConsoleHelper
    {
        public static void ShowBoard(Board board)
        {
            for (int row = board.Size - 1; row >= 0; row--)
            {
                for (int column = 0; column < board.Size; column++)
                {
                    if (board.IsPositionEnabled(row, column))
                    {
                        var figure = board.Positions[row, column].Figure;
                        if (figure != null)
                        {
                            Console.ForegroundColor = figure.Color == Enums.FigureColor.White ? ConsoleColor.Blue : ConsoleColor.Red;
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
                Console.WriteLine();
                Console.WriteLine(new string('-', board.Size * 4));
            }
        }
    }
}