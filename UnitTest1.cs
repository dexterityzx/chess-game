using System;
using Xunit;

namespace chess_game
{
    public class UnitTest1
    {
        [Fact]
        public void TestInPassing()
        {

            var board = new Board();
            var whiteChessPosition = new Position(5, 'a');
            var BlackChessPosition = new Position(7, 'b');
            board.Set(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.Set(BlackChessPosition.RankIndex, BlackChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, BlackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.White, new Position(4, 'a'), new Position(5, 'a'), true);

            var game = new Game(gameState);

            var command1 = new Command(PlayerType.Black, new Position(7, 'b'), new Position(5, 'b'), false);
            game.ExecuteCommand(command1);

            var command2 = new Command(PlayerType.White, new Position(5, 'a'), new Position(6, 'b'), false);
            game.ExecuteCommand(command2);

            var capturedPosition = new Position(5, 'b');
            Assert.Equal(game.GameState.Board.Get(capturedPosition.RankIndex, capturedPosition.FileIndex), null);

        }
    }
}
