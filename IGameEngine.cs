namespace chess_game
{
    public interface IGameEngine
    {
        GameState GameState { get; }
        GameResult GameResult { get; }

        void ExecuteCommand(Command command);
        bool NextRound(out PlayerType nextPlayer);
    }
}