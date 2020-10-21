using System;

namespace chess_game
{
    public class Position : IClonable<Position>
    {
        const int MIN_RANK = 1;
        const int MAX_RANK = 8;
        const char MIN_FILE = 'a';
        const char MAX_FILE = 'h';
        private char _file;
        private int _rank;
        public int Rank
        {
            get { return _rank; }
            set
            {
                if (MIN_RANK <= value && value <= MAX_RANK)
                {
                    _rank = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public char File
        {
            get { return _file; }
            set
            {
                if (MIN_FILE <= value && value <= MAX_FILE)
                {
                    _file = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
        public int FileIndex
        {
            get
            {
                return char.ToUpper(_file) - 65;
            }
        }

        public int RankIndex
        {
            get
            {
                return _rank - 1;
            }
        }
        public Position(int rank, char file)
        {
            Rank = rank;
            File = file;
        }

        public Position Clone()
        {
            return new Position(_rank, _file);
        }

        public bool Equals(Position position)
        {
            return _file == position._file &&
                   _rank == position._rank;
        }
    }
}