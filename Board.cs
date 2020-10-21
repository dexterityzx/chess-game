namespace chess_game
{
    public class Board : IBoard
    {
        const int MIN = 0;
        const int MAX = 8;
        private Chess[,] _board = new Chess[MAX, MAX];

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
                return _board[rank, file].Clone();
            }
            else
            {
                throw new System.ArgumentOutOfRangeException();
            }
        }

        public IBoard Clone()
        {
            var clonedBoard = new Chess[MAX, MAX];
            for (var i = 0; i < _board.GetLength(0); i++)
            {
                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    clonedBoard[i, j] = _board[i, j].Clone();
                }
            }

            return new Board(clonedBoard);
        }
    }
}