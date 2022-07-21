using SnowChess.BoardComps;

namespace SnowChess.Pieces
{
    public class Pawn : Piece
    {
        public Pawn(char letter, int number, int side) : base(letter, number, side)
        {
        }

        public new char GetSymbol()
        {
            if (Side == 0)
            {
                return '♙';
            }
            else
            {
                return '♟';
            }
        }

        public List<string> LegalMoves()
        {
            List<string> moves = new();

            int offset;
            if (Side == 0)
            {
                offset = 1;
            }
            else
            {
                offset = -1;
            }

            CheckMoveSquare(Letter, Number + offset, ref moves);

            CheckCaptureSquare((char)(Letter + 1), Number + offset, ref moves);

            CheckCaptureSquare((char)(Letter - 1), Number + offset, ref moves);




            return moves;
        }

        private void CheckMoveSquare(char letter, int number, ref List<string> moves)
        {
            var currentSquare = Board.GetSquareContent(letter, number);

            if (currentSquare == Board.EmptyPiece)
            {
                moves.Add($"{letter}{number}");
            }            
        }

        private void CheckCaptureSquare(char letter, int number, ref List<string> moves)
        {
            var currentSquare = Board.GetSquareContent(letter, number);

            if (currentSquare == Board.EmptyPiece || currentSquare == Board.InvalidPiece)
            {
                return;
            }

            if (currentSquare.Side != Side)
            {
                moves.Add($"{letter}{number}");
            }
        }
    }
}
