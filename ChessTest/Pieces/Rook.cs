using SnowChess.BoardComps;

namespace SnowChess.Pieces
{
    public class Rook : Piece
    {
        public Rook(char letter, int number, int side) : base(letter, number, side)
        {
        }

        public new char GetSymbol()
        {
            if (Side == 0)
            {
                return '♖';
            }
            else
            {
                return '♜';
            }
        }

        public List<string> LegalMoves() {
            List<string> moves = new();

            // up
            for (int i = Number + 1; i < 8; i++)
            {
                bool result = CheckSquare(Letter, i, ref moves);

                if (!result) break;
            }
            
            // down
            for (int i = Number - 1; i >= 0; i--)
            {
                bool result = CheckSquare(Letter, i, ref moves);

                if (!result) break;
            }

            // right
            for (char c = (char)(Letter + 1); c < 'i'; c++)
            {
                bool result = CheckSquare(c, Number, ref moves);

                if (!result) break;
            }

            // left
            for (char c = (char)(Letter - 1); c >= 'a'; c--)
            {
                bool result = CheckSquare(c, Number, ref moves);

                if (!result) break;
            }

            return moves;
        }

        private bool CheckSquare(char letter, int number, ref List<string> moves)
        {
            // get the content of the square
            var currentSquare = Board.GetSquareContent(letter, number);

            // if the square is invalid, we've reached the end of the board.
            if (currentSquare == Board.InvalidPiece) return false;

            // if we find a friendly piece, we can't move here, nor forward.
            if (currentSquare.Side == Side) return false;

            moves.Add($"{letter}{number}");

            // if we find an enemy piece, we cannot move forward.
            if (Board.GetSquareContent(letter, number) != Board.EmptyPiece) return false;

            return true;
        }
    }
}
