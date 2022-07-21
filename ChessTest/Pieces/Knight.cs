using SnowChess.BoardComps;

namespace SnowChess.Pieces
{
    public class Knight : Piece
    {
        public Knight(char letter, int number, int side) : base(letter, number, side)
        {
        }

        public new char GetSymbol()
        {
            if (Side == 0)
            {
                return '♘';
            }
            else
            {
                return '♞';
            }
        }

        public List<string> LegalMoves()
        {
            List<string> moves = new();

            CheckSquare((char)(Letter + 2), Number + 1, ref moves);

            CheckSquare((char)(Letter + 2), Number - 1, ref moves);

            CheckSquare((char)(Letter - 2), Number + 1, ref moves);

            CheckSquare((char)(Letter - 2), Number - 1, ref moves);

            CheckSquare((char)(Letter + 1), Number + 2, ref moves);

            CheckSquare((char)(Letter + 1), Number - 2, ref moves);

            CheckSquare((char)(Letter - 1), Number + 2, ref moves);

            CheckSquare((char)(Letter - 1), Number - 2, ref moves);

            return moves;
        }

        private void CheckSquare(char letter, int number, ref List<string> moves)
        {
            // get the content of the square
            var currentSquare = Board.GetSquareContent(letter, number);

            // if the square is invalid, we've reached the end of the board.
            if (currentSquare == Board.InvalidPiece) return;

            // if we find a friendly piece, we can't move here, nor forward.
            if (currentSquare.Side == Side) return;

            moves.Add($"{letter}{number}");
        }
    }
}
