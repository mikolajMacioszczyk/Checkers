// See https://aka.ms/new-console-template for more information
using Checkers.Managers;
using Checkers.Models;

var board = new Board();
board.Reset(8, 12);

ConsoleHelper.ShowBoard(board);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();