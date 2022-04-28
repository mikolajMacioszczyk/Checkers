// See https://aka.ms/new-console-template for more information
using Checkers.Managers;
using Checkers.Models;
using Checkers.Models.Player;

var manager = new GameManager();
var player1 = new UserPlayer();
player1.Name = "Mikoo";
var player2 = new ComputerPlayer();

manager.StartGame(player1, player2);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();