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

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

            var whiteEndChessPosition = new Position('a', 3);
            var blackEndChessPosition = new Position('a', 6);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            game.ExecuteCommand(whitePlayerCommand);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            game.ExecuteCommand(blackPlayerCommand);

            Assert.Equal(game.GameState.Board.GetChess(whiteStartChessPosition), null);
            Assert.Equal(game.GameState.Board.GetChess(whiteEndChessPosition).Player, PlayerType.White);
            Assert.Equal(game.GameState.Board.GetChess(whiteEndChessPosition).Type, ChessType.Pawn);


            Assert.Equal(game.GameState.Board.GetChess(blackStartChessPosition), null);
            Assert.Equal(game.GameState.Board.GetChess(blackEndChessPosition).Player, PlayerType.Black);
            Assert.Equal(game.GameState.Board.GetChess(blackEndChessPosition).Type, ChessType.Pawn);
        }

        [Fact]
        public void PawnCanMoveTwoStepForwardAtInitialPosition()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 2);
            var blackStartChessPosition = new Position('a', 7);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteStartChessPosition));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackStartChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

            var whiteEndChessPosition = new Position('a', 4);
            var blackEndChessPosition = new Position('a', 5);

            var whitePlayerCommand = new Command(PlayerType.White, whiteStartChessPosition, whiteEndChessPosition, false);
            game.ExecuteCommand(whitePlayerCommand);

            var blackPlayerCommand = new Command(PlayerType.Black, blackStartChessPosition, blackEndChessPosition, false);
            game.ExecuteCommand(blackPlayerCommand);

            Assert.Equal(game.GameState.Board.GetChess(whiteStartChessPosition), null);
            Assert.Equal(game.GameState.Board.GetChess(whiteEndChessPosition).Player, PlayerType.White);
            Assert.Equal(game.GameState.Board.GetChess(whiteEndChessPosition).Type, ChessType.Pawn);


            Assert.Equal(game.GameState.Board.GetChess(blackStartChessPosition), null);
            Assert.Equal(game.GameState.Board.GetChess(blackEndChessPosition).Player, PlayerType.Black);
            Assert.Equal(game.GameState.Board.GetChess(blackEndChessPosition).Type, ChessType.Pawn);
        }

        [Fact]
        public void PawnCanNotMoveTwoStepForwardWhenNotAtInitialPosition()
        {
            var board = new Board();
            var whiteStartChessPosition = new Position('a', 3);
            var blackStartChessPosition = new Position('a', 6);

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

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

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

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

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

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

            board.SetChess(whiteStartChessPosition, new Chess(PlayerType.White, ChessType.Pawn, new Position('a', 2)));
            board.SetChess(blackStartChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, new Position('a', 7)));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;

            var game = new GameEngine(gameState);

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
            board.SetChess(whiteChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.SetChess(blackChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.White, new Position('a', 4), new Position('a', 5), true);

            var game = new GameEngine(gameState);

            var command1 = new Command(PlayerType.Black, new Position('b', 7), new Position('b', 5), false);
            game.ExecuteCommand(command1);

            var command2 = new Command(PlayerType.White, new Position('a', 5), new Position('b', 6), false);
            game.ExecuteCommand(command2);

            var capturedPosition = new Position('b', 5);
            Assert.Equal(game.GameState.Board.GetChess(capturedPosition), null);

        }

        [Fact]
        public void PawnShouldNotCaptureChessWithOneMove()
        {

            var board = new Board();
            var whiteChessPosition = new Position('a', 5);
            var blackChessPosition = new Position('b', 5);
            board.SetChess(whiteChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.SetChess(blackChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.Black, new Position('b', 6), new Position('b', 5), true);

            var game = new GameEngine(gameState);

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
            board.SetChess(whiteChessPosition, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));
            board.SetChess(blackChessPosition, new Chess(PlayerType.Black, ChessType.Pawn, blackChessPosition));

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;
            gameState.Player = PlayerType.White;
            gameState.PlayerCommand = new Command(PlayerType.White, new Position('a', 4), new Position('a', 5), true);

            var game = new GameEngine(gameState);
            var command1 = new Command(PlayerType.Black, new Position('b', 6), new Position('a', 5), false);
            game.ExecuteCommand(command1);


            Assert.Equal(game.GameState.Board.GetChess(blackChessPosition), null);

            Assert.Equal(game.GameState.Board.GetChess(whiteChessPosition).Player, PlayerType.Black);

            Assert.Equal(game.GameState.Board.GetChess(whiteChessPosition).Type, ChessType.Pawn);
        }
    }
}
