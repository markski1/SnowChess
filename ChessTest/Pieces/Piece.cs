using SnowChess.BoardComps;

namespace SnowChess.Pieces
{
    public class Piece
    {
        public char Letter;
        public int Number;
        public int Side; // 0 white, 1 black
        public bool Captured = false;

        public char GetSymbol()
        {
            if (Side == 0)
            {
                return ' ';
            }
            else
            {
                return ' ';
            }
        }

        public Piece(char letter, int number, int side)
        {
            Letter = letter;
            Number = number;
            Side = side;
        }
    }
}
