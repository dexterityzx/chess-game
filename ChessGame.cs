using System;

namespace chess_game
{
    class ChessGame
    {
        static void Main(string[] args)
        {
            var gameEngine = InitializeGameEngine();

            PlayerType player;

            while (gameEngine.NextRound(out player))
            {
                try
                {
                    //get player command
                    Console.WriteLine($"{player.ToString()}:");
                    var commandString = Console.ReadLine();
                    //
                    var command = GenerateCommand(commandString, player);
                    gameEngine.ExecuteCommand(command);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }

            Console.WriteLine(player.ToString());

        }

        public static GameEngine InitializeGameEngine()
        {
            var board = new Board();
            for (char file = Position.MIN_FILE; file <= Position.MAX_FILE; file++)
            {
                //Console.Write(file);
                var whiteChessPosition = new Position(file, 2);
                board.SetChess(whiteChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));

                var BlackChessPosition = new Position(file, 7);
                board.SetChess(BlackChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, BlackChessPosition));
            }

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.None;

            return new GameEngine(gameState);
        }

        public static Command GenerateCommand(string commandString, PlayerType player)
        {
            var positions = commandString.Split("->");

            var from = new Position(positions[0]);
            var to = new Position(positions[1]);

            return new Command(player, from, to, false);
        }

    }
}
