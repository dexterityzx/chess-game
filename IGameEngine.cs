namespace chess_game
{
    public interface IGameEngine
    {
        GameState GameState { get; }
        GameResult GameResult { get; }

        void ExecuteCommand(Command command);
        bool HasNextRound(out PlayerType nextPlayer);
    }
}