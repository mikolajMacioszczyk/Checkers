using Checkers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

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

        public bool IsWhiteMan => Position.Figure is WhiteMan;
        public bool IsBlackMan => Position.Figure is BlackMan;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
