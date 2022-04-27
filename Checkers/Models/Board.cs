﻿using Checkers.Enums;

namespace Checkers.Models
{
    // moving back improve performance a lot
    // introduce interfaces
    public class Board
    {
        public Position[,] Positions { get; private set; }
        public int Size { get; private set; }
        public int FiguresCount { get; private set; }
        public List<Figure> Killed { get; set; }
        public List<Figure> AliveFigures { get; set; }

        public void Reset(int size, int figuresCount)
        {
            Size = size;
            FiguresCount = figuresCount;
            Killed = new List<Figure>();
            AliveFigures = new List<Figure>();

            InitializePositions();

            var figuresRows = FiguresCount * 2 / Size;
            // initialize White
            InitializeFigures(0, figuresRows, FigureColor.White);

            // initialize Black
            InitializeFigures(Size - figuresRows, Size, FigureColor.Black);
        }

        public bool IsPositionEnabled(int row, int col) => (row + col) % 2 == 1;

        public Position FindFigure(Figure figure)
        {
            foreach (var position in Positions)
            {
                if (position?.Figure == figure)
                {
                    return position;
                }
            }

            return null;
        }

        public void MarkKilled(Position position)
        {
            Killed.Add(position.Figure);
            AliveFigures.Remove(position.Figure);

            position.Figure = null;
        }

        public List<MoveBase> GetAllAvailableMoves(FigureColor forColor)
        {
            var moves = new List<MoveBase>();
            foreach (var figure in AliveFigures.Where(f => f.Color == forColor))
            {
                moves.AddRange(figure.GetAvailableMoves(this));
            }

            if (moves.Any(m => m.IsKillMove))
            {
                moves = moves.Where(m => m.IsKillMove).ToList();
            }

            return moves;
        }

        public Board DeepCopy()
        {
            Board copy = new Board()
            {
                Killed = Killed.Select(f => f.Copy()).ToList(),
                AliveFigures = AliveFigures.Select(f => f.Copy()).ToList(),
            };

            copy.Reset(Size, FiguresCount);
            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    copy.Positions[row, column].Figure = Positions[row, column].Figure.Copy();
                }
            }

            return copy;
        }

        private void InitializePositions()
        {
            Positions = new Position[Size, Size];

            for (int row = 0; row < Size; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (IsPositionEnabled(row, column))
                    {
                        Positions[row, column] = new Position() { Row = row, Column = column };
                    }
                }
            }
        }

        private void InitializeFigures(int figureRowsFrom, int figureRowsEnd, FigureColor color)
        {
            for (int row = figureRowsFrom; row < figureRowsEnd; row++)
            {
                for (int column = 0; column < Size; column++)
                {
                    if (IsPositionEnabled(row, column))
                    {
                        Figure man = color == FigureColor.White ? new WhiteMan() : new BlackMan();
                        AliveFigures.Add(man);
                        Positions[row, column].Figure = man;
                    }
                }
            }
        }
    }
}
