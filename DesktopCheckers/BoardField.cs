using Checkers.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace DesktopCheckers
{
    public class BoardField : INotifyPropertyChanged
    {
        private Position position;
        public Position Position
        {
            get { return position; }
            set 
            { 
                position = value;
                OnPropertyChanged(nameof(IsWhite));
                OnPropertyChanged(nameof(IsBlack));
                OnPropertyChanged(nameof(IsKing));
            }
        }

        private Brush filedColor;

        public Brush FiledColor
        {
            get { return filedColor; }
            set 
            { 
                filedColor = value; 
                OnPropertyChanged(nameof(FiledColor));
            }
        }


        public Visibility IsWhite => Position?.Figure is WhiteMan || Position?.Figure is WhiteKing ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsBlack => Position?.Figure is BlackMan || Position?.Figure is BlackKing ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsKing => Position?.Figure is WhiteKing || Position?.Figure is BlackKing ? Visibility.Visible : Visibility.Collapsed;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
