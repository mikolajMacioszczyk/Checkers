using Checkers.Eveluation;
using Checkers.Managers;
using Checkers.Models.Player;
using ConsoleCheckers;

var consoleUserInterface = new ConsoleUserInterface();
var manager = new GameManager(consoleUserInterface);
//var player1 = new ConsolePlayer();
//player1.Name = "Mikoo";
var evaluation = new TestEvaluation();
var aplhaBethaPlayer = new ComputerPlayerWithAlphaBeta(evaluation, 7);
aplhaBethaPlayer.Name = "ComputerPlayerWithAlphaBeta";
var ordinaryPlayer = new ComputerPlayer(evaluation, 7);
ordinaryPlayer.Name = "ComputerPlayer";

manager.StartGame(ordinaryPlayer, aplhaBethaPlayer);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();