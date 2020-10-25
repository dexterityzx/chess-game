namespace chess_game
{
    public class GameState : IClonable<GameState>
    {
        public IBoard Board { get; set; }
        public PlayerType Player { get; set; }
        public GameResult Result { get; set; }
        public Command PlayerCommand { get; set; }

        public GameState()
        {
        }
        public GameState(IBoard board, PlayerType player, GameResult gameResult, Command command)
        {
            Board = board;
            Player = player;
            Result = gameResult;
            PlayerCommand = command;
        }
        public GameState Clone()
        {
            return new GameState(Board.Clone(), Player, Result, PlayerCommand.Clone());
        }

    }
}