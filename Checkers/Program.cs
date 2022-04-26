// See https://aka.ms/new-console-template for more information
using Checkers.Models;

Console.WriteLine("Hello, World!");

var board = new Board();
board.Reset(8, 16);

var move = new TransformMove();
move.MakeMove(board);