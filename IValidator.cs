namespace chess_game
{
    public interface IValidator
    {
        void ValidateAndThrowException(Command command, GameState currentState);
    }
}