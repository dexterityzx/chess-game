namespace chess_game
{
    public class Chess : IClonable<Chess>
    {
        public PlayerType Player { get; private set; }
        public ChessType Type { get; private set; }
        public Position InitialPosition { get; private set; }
        public Chess(PlayerType player, ChessType type, Position initialLoacation)
        {
            Player = player;
            Type = type;
            InitialPosition = initialLoacation;
        }
        public Chess Clone()
        {
            return new Chess(Player, Type, InitialPosition.Clone());
        }
    }
}