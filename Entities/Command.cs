namespace chess_game
{
    public class Command : IClonable<Command>
    {
        public PlayerType Player;
        public Position From;
        public Position To;
        public bool HasBeenExecuted = false;
        public Command(PlayerType player, Position from, Position to, bool hasBeenExecuted)
        {
            Player = player;
            From = from;
            To = to;
            HasBeenExecuted = hasBeenExecuted;
        }
        public Command Clone()
        {
            return new Command(Player, From.Clone(), To.Clone(), HasBeenExecuted);
        }
    }
}