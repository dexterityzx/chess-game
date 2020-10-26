using System;
using Xunit;
using NSubstitute;

namespace chess_game
{
    public class PawnTest
    {
        public PawnTest()
        {
        }

        public void Dispose()
        {
        }

        [Fact]
        public void PawnCanMoveOneStepForward()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 2);
            var blackStartChessPosition = new Position('a', 7);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var whiteEndChessPosition = new Position('a', 3);
            var blackEndChessPosition = new Position('a', 6);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            game.ExecuteCommand(whitePlayerCommand);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            game.ExecuteCommand(blackPlayerCommand);

            Assert.Equal(game.GameState.Board.Get(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex), null);
            Assert.Equal(game.GameState.Board.Get(whiteEndChessPosition.RankIndex, whiteEndChessPosition.FileIndex).Player, PlayerType.White);
            Assert.Equal(game.GameState.Board.Get(whiteEndChessPosition.RankIndex, whiteEndChessPosition.FileIndex).Type, ChessType.Pawn);


            Assert.Equal(game.GameState.Board.Get(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex), null);
            Assert.Equal(game.GameState.Board.Get(blackEndChessPosition.RankIndex, blackEndChessPosition.FileIndex).Player, PlayerType.Black);
            Assert.Equal(game.GameState.Board.Get(blackEndChessPosition.RankIndex, blackEndChessPosition.FileIndex).Type, ChessType.Pawn);
        }

        [Fact]
        public void PawnCanMoveTwoStepForwardAtInitialPosition()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 2);
            var blackStartChessPosition = new Position('a', 7);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var whiteEndChessPosition = new Position('a', 4);
            var blackEndChessPosition = new Position('a', 5);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            game.ExecuteCommand(whitePlayerCommand);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            game.ExecuteCommand(blackPlayerCommand);

            Assert.Equal(game.GameState.Board.Get(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex), null);
            Assert.Equal(game.GameState.Board.Get(whiteEndChessPosition.RankIndex, whiteEndChessPosition.FileIndex).Player, PlayerType.White);
            Assert.Equal(game.GameState.Board.Get(whiteEndChessPosition.RankIndex, whiteEndChessPosition.FileIndex).Type, ChessType.Pawn);


            Assert.Equal(game.GameState.Board.Get(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex), null);
            Assert.Equal(game.GameState.Board.Get(blackEndChessPosition.RankIndex, blackEndChessPosition.FileIndex).Player, PlayerType.Black);
            Assert.Equal(game.GameState.Board.Get(blackEndChessPosition.RankIndex, blackEndChessPosition.FileIndex).Type, ChessType.Pawn);
        }

        [Fact]
        public void PawnCanNotMoveTwoStepForwardWhenNotAtInitialPosition()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 3);
            var blackStartChessPosition = new Position('a', 6);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var whiteEndChessPosition = new Position('a', 5);
            var blackEndChessPosition = new Position('a', 4);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            var excption1 = Assert.Throws<Exception>(() => game.ExecuteCommand(whitePlayerCommand));
            Assert.Equal(excption1.Message, "Invalid Command.");

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            var excption2 = Assert.Throws<Exception>(() => game.ExecuteCommand(blackPlayerCommand));
            Assert.Equal(excption2.Message, "Invalid Command.");
        }

        [Fact]
        public void PawnCanNotMoveOneStepForwardWhenOtherChessesAreOnTheWay()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 3);
            var blackStartChessPosition = new Position('a', 4);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var whiteEndChessPosition = new Position('a', 4);
            var blackEndChessPosition = new Position('a', 3);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            var excption1 = Assert.Throws<Exception>(() => game.ExecuteCommand(whitePlayerCommand));
            Assert.Equal(excption1.Message, "Invalid Command.");

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            var excption2 = Assert.Throws<Exception>(() => game.ExecuteCommand(blackPlayerCommand));
            Assert.Equal(excption2.Message, "Invalid Command.");
        }

        [Fact]
        public void BlackPawnCanNotMoveTwoStepForwardWhenOtherChessesAreOnTheWay()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 5);
            var blackStartChessPosition = new Position('a', 7);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var blackEndChessPosition = new Position('a', 5);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            var excption2 = Assert.Throws<Exception>(() => game.ExecuteCommand(blackPlayerCommand));
            Assert.Equal(excption2.Message, "Invalid Command.");
        }

        [Fact]
        public void WhitePawnCanNotMoveTwotepForwardWhenOtherChessesAreOnTheWay()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 2);
            var blackStartChessPosition = new Position('a', 4);

            board.Set(whiteStartChessPosition.RankIndex, whiteStartChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.Set(blackStartChessPosition.RankIndex, blackStartChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new Game(gameState);

            var whiteEndChessPosition = new Position('a', 4);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            var excption1 = Assert.Throws<Exception>(() => game.ExecuteCommand(whitePlayerCommand));
            Assert.Equal(excption1.Message, "Invalid Command.");
        }

        [Fact]
        public void PawnCanCaptureChessWithInPassing()
        {

            var board = new Board();
            var whiteChessPosition = new Position('a', 5);
            var blackChessPosition = new Position('b', 7);
            board.Set(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.Set(blackChessPosition.RankIndex, blackChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.White, new Position('a', 4), new Position('a', 5), true);

            var game = new Game(gameState);

            var command1 = new Command(PlayerType.Black, new Position('b', 7), new Position('b', 5), false);
            game.ExecuteCommand(command1);

            var command2 = new Command(PlayerType.White, new Position('a', 5), new Position('b', 6), false);
            game.ExecuteCommand(command2);

            var capturedPosition = new Position('b', 5);
            Assert.Equal(game.GameState.Board.Get(capturedPosition.RankIndex, capturedPosition.FileIndex), null);

        }

        [Fact]
        public void PawnShouldNotCaptureChessWithOneMove()
        {

            var board = new Board();
            var whiteChessPosition = new Position('a', 5);
            var blackChessPosition = new Position('b', 5);
            board.Set(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.Set(blackChessPosition.RankIndex, blackChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.Black, new Position('b', 6), new Position('b', 5), true);

            var game = new Game(gameState);

            var command2 = new Command(PlayerType.White, new Position('a', 5), new Position('b', 6), false);

            var excption = Assert.Throws<Exception>(() => game.ExecuteCommand(command2));
            Assert.Equal(excption.Message, "Invalid Command.");

        }

        [Fact]
        public void PawnShouldCaptureChessInDiagonal()
        {

            var board = new Board();
            var whiteChessPosition = new Position('a', 5);
            var blackChessPosition = new Position('b', 6);
            board.Set(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.Set(blackChessPosition.RankIndex, blackChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.White, new Position('a', 4), new Position('a', 5), true);

            var game = new Game(gameState);
            var command1 = new Command(PlayerType.Black, new Position('b', 6), new Position('a', 5), false);
            game.ExecuteCommand(command1);


            Assert.Equal(game.GameState.Board.Get(blackChessPosition.RankIndex, blackChessPosition.FileIndex), null);

            Assert.Equal(game.GameState.Board.Get(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex).Player, PlayerType.Black);

            Assert.Equal(game.GameState.Board.Get(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex).Type, ChessType.Pawn);
        }
    }
}
