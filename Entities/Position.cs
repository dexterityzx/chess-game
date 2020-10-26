using System;

namespace chess_game
{
    public class Position : IClonable<Position>
    {
        public const int MIN_RANK = 1;
        public const int MAX_RANK = 8;
        public const char MIN_FILE = 'a';
        public const char MAX_FILE = 'h';
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

        public Position Offset(int fileOffset, int rankOffset)
        {
            return new Position((char)((int)_file + fileOffset), _rank + rankOffset);
        }

        public Position(char file, int rank)
        {
            Rank = rank;
            File = file;
        }

        public Position(string position)
        {
            File = position[0];
            Rank = (int)position[1];
        }

        public Position Clone()
        {
            return new Position(_file, _rank);
        }

        public bool Equals(Position position)
        {
            return _file == position._file &&
                   _rank == position._rank;
        }
    }
}