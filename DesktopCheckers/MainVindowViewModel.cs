﻿using Checkers.Enums;
using Checkers.Interfaces;
using Checkers.Managers;
using Checkers.Models;
using Checkers.Models.Player;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopCheckers
{
    public class MainVindowViewModel : IUserInterface, INotifyPropertyChanged
    {
        private static readonly Brush NormalBrush = new SolidColorBrush(Colors.Gray);
        private static readonly Brush SemiTargetBrush = new SolidColorBrush(Colors.LightGreen);
        private static readonly Brush TargetBrush = new SolidColorBrush(Colors.Green);
        private static readonly Brush KillBrush = new SolidColorBrush(Colors.Red);
        private static readonly Brush FromBrush = new SolidColorBrush(Colors.LightSlateGray);

        private const int Size = 8;

        private string message;
        private bool isPlayerMoving;
        private BoardField fromField;

        public event PropertyChangedEventHandler? PropertyChanged;

        public BoardField[] BoardFields { get; set; }

        public DesktopPlayer DesktopPlayer { get; set; } = new DesktopPlayer();

        public List<MoveBase> AvailableMoves { get; set; } = new List<MoveBase>();

        public string Message
        {
            get { return message; }
            set {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public MainVindowViewModel()
        {
            BoardFields = new BoardField[Size * Size];
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    BoardFields[row * 8 + column] = new BoardField() { FiledColor = NormalBrush };
                }
            }
        }

        public async Task StartGame()
        {
            var gameManager = new GameManager(this);
            var whitePlayer = DesktopPlayer;
            whitePlayer.Name = "Mikołaj";
            whitePlayer.ChooseStarted += (allMoves) =>
            {
                AvailableMoves.Clear();
                foreach (var move in allMoves)
                {
                    AvailableMoves.Add(move);
                }
                isPlayerMoving = true;
            };

            var blackPlayer = new ComputerPlayer();
            await Task.Run(() =>
            {
                gameManager.StartGame(whitePlayer, blackPlayer);
            });
        }

        public void OnFieldClicked(int row, int column, BoardField clickedField)
        {
            if (clickedField.FiledColor == TargetBrush)
            {
                var selectedMoves = AvailableMoves.Where(m => 
                    m.From.Row == fromField.Position.Row && m.From.Column == fromField.Position.Column
                    && m.Target.Row == row && m.Target.Column == column)
                    .ToList();

                if (selectedMoves.Count == 1)
                {
                    fromField = null;
                    isPlayerMoving = false;
                    DesktopPlayer.SetMoveChoice(selectedMoves[0]);
                }
                else
                {

                }

                foreach (var field in BoardFields)
                {
                    field.FiledColor = NormalBrush;
                }
            }
            else
            {
                foreach (var field in BoardFields)
                {
                    field.FiledColor = NormalBrush;
                }

                if (isPlayerMoving)
                {
                    fromField = clickedField;
                    fromField.FiledColor = FromBrush;
                    var figureMoves = AvailableMoves.Where(m => m.From.Row == row && m.From.Column == column).ToList();
                    if (figureMoves.Count == 1 && figureMoves[0] is KillMove move)
                    {
                        var innerMove = move;
                        while (innerMove != null)
                        {
                            int targetRow = innerMove.Target.Row;
                            int targetColumn = innerMove.Target.Column;
                            BoardFields[targetRow * Size + targetColumn].FiledColor = innerMove == move ? TargetBrush : SemiTargetBrush;

                            int killRow = innerMove.Killed.Row;
                            int killColumn = innerMove.Killed.Column;
                            BoardFields[killRow * Size + killColumn].FiledColor = KillBrush;

                            innerMove = innerMove.InnerMove;
                        }
                    }
                    else
                    {
                        foreach (var figureMove in figureMoves)
                        {
                            int targetRow = figureMove.Target.Row;
                            int targetColumn = figureMove.Target.Column;

                            BoardFields[targetRow * Size + targetColumn].FiledColor = TargetBrush;
                        }
                    }
                }
            }
        }

        public void ShowNextMove(IPlayer player)
        {
            var color = player.Color == FigureColor.White ? "White" : "Black";
            Message = $"Player {player.Name} ({color}) move: ";
        }

        public void ShowMessage(string message)
        {
            Message = message;
        }

        public void GameOver(IPlayer whitePlayer, IPlayer blackPlayer, GameResult result, int moveCount)
        {
            switch (result)
            {
                case GameResult.WhiteWin:
                    Message = $"Game over!!! Player {whitePlayer.Name} (White) won in {moveCount} moves";
                    break;
                case GameResult.BlackWin:
                    Message = $"Game over!!! Player {blackPlayer.Name} (Black) won in {moveCount} moves";
                    break;
                default:
                    Message = $"Game over!!! Draw after {moveCount} moves";
                    break;
            }
        }

        public void ShowBoard(Board board)
        {
            for (int row = 0; row < board.Size; row++)
            {
                for (int column = 0; column < board.Size; column++)
                {
                    if (board.IsPositionEnabled(row, column))
                    {
                        BoardFields[row * 8 + column].Position = board.Positions[row, column];
                    }
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
