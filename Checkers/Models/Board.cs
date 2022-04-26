using Checkers.Enums;

namespace Checkers.Models
{
    public class Board
    {
        public Position[,] Positions { get; private set; }
        public int Size { get; private set; }
        public int FiguresCount { get; private set; }
        public List<Figure> Killed { get; set; }
        public List<Figure> AliveFigures { get; set; }

        #region Reset

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
                        Figure man = color == FigureColor.White ? new WhiteMan() : new BlackKing();
                        AliveFigures.Add(man);
                        Positions[row, column].Figure = man;
                    }
                }
            }
        }

        #endregion
        
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
    }
}
