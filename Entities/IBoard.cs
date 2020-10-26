namespace chess_game
{
    public interface IBoard : IClonable<IBoard>
    {
        Chess Get(int rank, int file);
        void Set(int rank, int file, Chess chess);
        Chess GetChess(Position position);
        void SetChess(Position position, Chess chess);
    }
}