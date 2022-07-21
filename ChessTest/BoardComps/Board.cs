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
            for (int i = 0; i < 2; i++)
            {
                // remember, C# is 0 indexed, so rank 0 is actually 1 on the board, etc
                int rank, pawnRank;
                // where i is the team, 0 is white, 1 is black
                if (i == 0)
                {
                    rank = 0;
                    pawnRank = 1;
                }
                else
                {
                    rank = 7;
                    pawnRank = 6;
                }
                AddPiece('a', rank, i, typeof(Rook));
                AddPiece('b', rank, i, typeof(Knight));
                AddPiece('c', rank, i, typeof(Bishop));
                AddPiece('d', rank, i, typeof(Queen));
                AddPiece('e', rank, i, typeof(King));
                AddPiece('f', rank, i, typeof(Bishop));
                AddPiece('g', rank, i, typeof(Knight));
                AddPiece('h', rank, i, typeof(Rook));
                // sPAWNs
                for (char c = 'a'; c < 'i'; c++)
                {
                    AddPiece(c, pawnRank, i, typeof(Pawn));
                }
            }
            
        }

        private static void AddPiece(char letter, int number, int side, Type pieceType)
        {
            // We make a mess here, but it keeps board initialization clean!
            if (pieceType == typeof(Pawn))
            {
                Squares[letter, number] = new Pawn(letter, number, side);
            }
            else if (pieceType == typeof(Rook))
            {
                Squares[letter, number] = new Rook(letter, number, side);
            }
            else if (pieceType == typeof(Bishop))
            {
                Squares[letter, number] = new Bishop(letter, number, side);
            }
            else if (pieceType == typeof(Knight))
            {
                Squares[letter, number] = new Knight(letter, number, side);
            }
            else if (pieceType == typeof(Queen))
            {
                Squares[letter, number] = new Queen(letter, number, side);
            }
            else if (pieceType == typeof(King))
            {
                Squares[letter, number] = new King(letter, number, side);
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
