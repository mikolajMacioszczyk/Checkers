﻿using Checkers.Eveluation;
using Checkers.Managers;
using Checkers.Models.Player;
using ConsoleCheckers;

var consoleUserInterface = new ConsoleUserInterface();
var manager = new GameManager(consoleUserInterface);
var player1 = new ConsolePlayer();
player1.Name = "Mikoo";
var evaluation = new TestEvaluation();
var player2 = new ComputerPlayer(evaluation, 4);

manager.StartGame(player1, player2);

Console.WriteLine("Press ENTER to continue");
Console.ReadLine();