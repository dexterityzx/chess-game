using System;
using Xunit;

namespace chess_game.UnitTests
{
    public class EverChessTest
    {
        [Fact]
        public void WhitePlayerHasNoMoveWillLose()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 4);
            var blackStartChessPosition = new Position('a', 5);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.Black;

            var gameEngine = new GameEngine(gameState);

            PlayerType nextPlayer;
            var hasNextRound = gameEngine.HasNextRound(out nextPlayer);
            Assert.False(hasNextRound);
            Assert.Equal(PlayerType.None, nextPlayer);
            Assert.Equal(GameResult.Checkmate, gameEngine.GameResult);
            Assert.Equal(gameEngine.GameState.Player, PlayerType.Black);
        }


        [Fact]
        public void BlackPlayerHasNoMoveWillLose()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 4);
            var blackStartChessPosition = new Position('a', 5);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var gameEngine = new GameEngine(gameState);

            PlayerType nextPlayer;
            var hasNextRound = gameEngine.HasNextRound(out nextPlayer);
            Assert.False(hasNextRound);
            Assert.Equal(PlayerType.None, nextPlayer);
            Assert.Equal(GameResult.Checkmate, gameEngine.GameResult);
            Assert.Equal(gameEngine.GameState.Player, PlayerType.White);
        }


        [Fact]
        public void WhitePlayerHasChessInTheOtherEndWillWin()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('b', 7);
            var blackStartChessPosition = new Position('a', 7);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.Black;

            var gameEngine = new GameEngine(gameState);

            var whiteEndChessPosition = new Position('b', 8);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            gameEngine.ExecuteCommand(whitePlayerCommand);

            PlayerType nextPlayer;
            var hasNextRound = gameEngine.HasNextRound(out nextPlayer);
            Assert.False(hasNextRound);
            Assert.Equal(PlayerType.None, nextPlayer);
            Assert.Equal(GameResult.Checkmate, gameEngine.GameResult);
            Assert.Equal(gameEngine.GameState.Player, PlayerType.White);
        }

        [Fact]
        public void BlackPlayerHasChessInTheOtherEndWillWin()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('b', 7);
            var blackStartChessPosition = new Position('a', 2);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var gameEngine = new GameEngine(gameState);

            var blackEndChessPosition = new Position('a', 1);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            gameEngine.ExecuteCommand(blackPlayerCommand);

            PlayerType nextPlayer;
            var hasNextRound = gameEngine.HasNextRound(out nextPlayer);
            Assert.False(hasNextRound);
            Assert.Equal(PlayerType.None, nextPlayer);
            Assert.Equal(GameResult.Checkmate, gameEngine.GameResult);
            Assert.Equal(gameEngine.GameState.Player, PlayerType.Black);
        }

    }
}