namespace chess_game
{
    public class Board : IBoard
    {
        public const int MIN = 0;
        public const int MAX = 7;
        private Chess[,] _board;

        public Board()
        {
            _board = new Chess[MAX + 1, MAX + 1];
        }

        public Board(Chess[,] board)
        {
            _board = board;
        }

        private bool IsValidPosition(int rank, int file)
        {
            return (MIN <= rank && rank <= MAX) &&
                (MIN <= file && file <= MAX);
        }
        public void Set(int rank, int file, Chess chess)
        {
            if (IsValidPosition(rank, file))
            {
                _board[rank, file] = chess;
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }

        }

        public Chess Get(int rank, int file)
        {
            if (IsValidPosition(rank, file))
            {
                return _board[rank, file] != null ? _board[rank, file].Clone() : null;
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        public IBoard Clone()
        {
            var clonedBoard = new Chess[MAX + 1, MAX + 1];
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    clonedBoard[i, j] = _board[i, j] != null ? _board[i, j].Clone() : null;
                }
            }

            return new Board(clonedBoard);
        }

        public Chess GetChess(Position position)
        {
            var file = FileToBoardIndex(position.File);
            var rank = RankToBoardIndex(position.Rank);

            return Get(rank, file);
        }

        public void SetChess(Position position, Chess chess)
        {
            var file = FileToBoardIndex(position.File);
            var rank = RankToBoardIndex(position.Rank);

            Set(rank, file, chess);
        }

        private int FileToBoardIndex(char positionFile)
        {
            return char.ToUpper(positionFile) - 65;
        }

        private int RankToBoardIndex(int positionRank)
        {
            return positionRank - 1;
        }
    }
}