using Checkers.Eveluation;
using Checkers.Managers;
using Checkers.Models.Player;
using ConsoleCheckers;

var consoleUserInterface = new ConsoleUserInterface();
var manager = new GameManager(consoleUserInterface);
//var player1 = new ConsolePlayer();
//player1.Name = "Mikoo";
var evaluation = new SimplePositionBasedEvaluation();
var player2 = new ComputerPlayerWithAlphaBeta(evaluation, 9);
player2.Name = "ComputerPlayerWithAlphaBeta";
var player1 = new ComputerPlayer(evaluation, 7);
player1.Name = "ComputerPlayer";

manager.StartGame(player1, player2);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();