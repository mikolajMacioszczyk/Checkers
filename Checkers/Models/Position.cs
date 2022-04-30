namespace Checkers.Models
{
    public class Position
    {
        public int Row { get; set; }

        public int Column { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        private Figure _figure;
        
        [System.Text.Json.Serialization.JsonIgnore]
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
                    if (_figure.CurrentPosition != null)
                    {
                        _figure.CurrentPosition.Figure = null;
                    }

                    _figure.CurrentPosition = this;
                }
            }
        }

        public bool IsEmpty() => Figure == null;

        public override string ToString()
        {
            return $"[{Row}, {Column}]";
        }

        public Position Copy()
        {
            return new Position()
            {
                Row = Row,
                Column = Column,
                Figure = null
            };
        }

        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Column == position.Column;
        }
    }
}
