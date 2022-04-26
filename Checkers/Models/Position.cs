using System;
namespace Checkers.Models
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Figure Figure { get; set; }
    }
}
