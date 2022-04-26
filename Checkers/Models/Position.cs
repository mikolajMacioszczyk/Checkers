﻿namespace Checkers.Models
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }

        private Figure _figure;

        public Figure Figure
        {
            get { return _figure; }
            set 
            { 
                if (_figure != null)
                {
                    _figure.CurrentPosition = null;
                }

                _figure = value; 

                if (_figure != null)
                {
                    _figure.CurrentPosition = this;
                }
            }
        }
    }
}
