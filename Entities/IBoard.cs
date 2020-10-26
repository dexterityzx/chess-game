namespace chess_game
{
    public interface IBoard : IClonable<IBoard>
    {
        Chess GetChess(Position position);
        void SetChess(Position position, Chess chess);
    }
}