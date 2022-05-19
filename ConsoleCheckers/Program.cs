using Checkers.Eveluation;
using Checkers.Managers;
using Checkers.Models.Player;
using ConsoleCheckers;

var consoleUserInterface = new ConsoleUserInterface();
var manager = new GameManager(consoleUserInterface);
//var player1 = new ConsolePlayer();
//player1.Name = "Mikoo";
var clusterEvaluation = new ClustersEvaluation();
var distanceEvaluation = new DistanceEvaluation();
var edgeEvaluation = new EdgePositionEvaluation();
var figureEvaluation = new FigureEvaluation();
var combinedEvaluation = new TestEvaluation();

int maxNormalDepth = 8;
int maxAlfaBetaDepth = 10;
var evaluation = figureEvaluation;

for (int depth = 3; depth <= maxNormalDepth; depth++)
{
    var first = new ComputerPlayer(evaluation, depth);
    first.Name = "PLayer 1";
    var second = new ComputerPlayer(evaluation, depth);
    second.Name = "PLayer 2";

    var sum = 0.0;
    for (int i = 0; i < 5; i++)
    {
        sum += manager.StartGame(first, second);
    }
    Console.WriteLine($"Normal! Depth: {depth}, Avg time: {Math.Round(sum / 5)}");
}

for (int depth = 3; depth <= maxAlfaBetaDepth; depth++)
{
    var first = new ComputerPlayerWithAlphaBeta(evaluation, depth);
    first.Name = "PLayer 1";
    var second = new ComputerPlayerWithAlphaBeta(evaluation, depth);
    second.Name = "PLayer 2";

    var sum = 0.0;
    for (int i = 0; i < 5; i++)
    {
        sum += manager.StartGame(first, second);
    }
    Console.WriteLine($"AlphaBeta! Depth: {depth}, Avg time: {Math.Round(sum / 5)}");
}

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();