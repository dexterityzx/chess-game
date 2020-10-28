namespace chess_game
{
    public class GameState : IClonable<GameState>
    {
        public IBoard Board { get; set; }
        public PlayerType Player { get; set; }
        public GameResult Result { get; set; }
        public Command PlayerCommand { get; set; }

        public bool HasAnotherRound { get; set; }

        public GameState()
        {
        }
        public GameState(IBoard board, PlayerType player, GameResult gameResult, Command command, bool hasAnotherRound)
        {
            Board = board;
            Player = player;
            Result = gameResult;
            PlayerCommand = command;
            HasAnotherRound = hasAnotherRound;
        }
        public GameState Clone()
        {
            var borad = Board != null ? Board.Clone() : null;
            var playerCommand = PlayerCommand != null ? PlayerCommand.Clone() : null;
            return new GameState(Board.Clone(), Player, Result, playerCommand, HasAnotherRound);
        }

    }
}