using Checkers.Managers;
using Checkers.Models.Player;
using ConsoleCheckers;

var consoleUserInterface = new ConsoleUserInterface();
var manager = new GameManager(consoleUserInterface);
var player1 = new ConsolePlayer();
player1.Name = "Mikoo";
var player2 = new ComputerPlayer();

manager.StartGame(player1, player2);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();