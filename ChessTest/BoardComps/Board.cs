using SnowChess.Pieces;

namespace SnowChess.BoardComps
{
    public static class Board
    {
        private static readonly Piece[,] Squares = new Piece[8, 8];
        private static readonly List<Piece> CapturedPieces = new();
        public static readonly Piece InvalidPiece = new('z', -1, -1);
        public static readonly Piece EmptyPiece = new('x', -1, -1);

        public static void Initialize()
        {
            // fill the board with it's 64 squares
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Squares[i,j] = EmptyPiece;
                }
            }

            // spawn pieces on the board in a standard chess starting position
            AddPiece('a', 1, 0, typeof(Rook));
        }

        private static void AddPiece(char letter, int number, int side, Type pieceType)
        {
            if (pieceType == typeof(Rook))
            {
                Squares[letter, number] = new Rook(letter, number, side);
            }
                        
        }

        public static Piece GetSquareContent(char letter, int number)
        {
            // translate the letter to a number to use as index
            int letterValue = 'a' - letter;

            // return invalid piece if the coords are invalid
            if (letterValue < 0 || letterValue > 7)
            {
                return InvalidPiece;
            }
            if (number < 0 || number > 7)
            {
                return InvalidPiece;
            }
            return Squares[letterValue, number];
        }

        public static void MovePiece(char sourceLetter, int sourceNumber, char destinationLetter, int destinationNumber)
        {
            // get source and destionation piece, check valid squares
            var sourcePiece = GetSquareContent(sourceLetter, sourceNumber);
            if (sourcePiece == InvalidPiece) return;
            var destinationPiece = GetSquareContent(destinationLetter, destinationNumber);
            if (destinationPiece == InvalidPiece) return;

            // move the pieces
            Squares[sourceLetter, sourceNumber] = EmptyPiece;
            Squares[destinationLetter, destinationNumber] = sourcePiece;

            sourcePiece.Letter = destinationLetter;
            sourcePiece.Number = destinationNumber;

            // if a piece was captured, add to the capturedpieces collection and mark it as so.
            if (destinationPiece != EmptyPiece)
            {
                CapturedPieces.Add(destinationPiece);
                destinationPiece.Captured = true;
            }

            // if a pawn reaches the 8th rank (7 in code because C# is 0-indexed), it should present a way to become
            // a knight, bishop, rook or queen.
            if (sourcePiece.Number == 7 /*&& sourcePiece is Pawn*/)
            {
                // not implemented
            }
        }
    }
}
