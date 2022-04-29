using Checkers.Models;
using System.ComponentModel;
using System.Windows;

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
                OnPropertyChanged(nameof(IsWhiteMan));
                OnPropertyChanged(nameof(IsBlackMan));
            }
        }

        public Visibility IsWhiteMan => Position?.Figure is WhiteMan ? Visibility.Visible : Visibility.Collapsed;
        public Visibility IsBlackMan => Position?.Figure is BlackMan ? Visibility.Visible : Visibility.Collapsed;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
