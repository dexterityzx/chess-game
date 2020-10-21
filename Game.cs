using System;
using System.Collections.Generic;

namespace chess_game
{
    public class Game
    {
        private Stack<GameState> _gameStates;
        public GameState GameState
        {
            get => _gameStates.Count > 0 ? _gameStates.Peek().Clone() : null;
        }

        public Game()
        {
            _gameStates = new Stack<GameState>();
        }

        public void ExecuteCommand(Command command)
        {
            if (!IsValidCommand(command, GameState))
            {
                throw new Exception("Invalid Command.");
            }

            _gameStates.Push(NextState(command, GameState));

        }

        public bool NextPlayer(GameResult result)
        {

            switch (result)
            {
                case GameResult.Checkmate:
                case GameResult.Stalemate:
                    return false;

                case GameResult.None:
                    return true;

                default:
                    throw new ArgumentException("Invalid GameResult");
            }
        }

        private bool IsValidCommand(Command command, GameState currentState)
        {
            var chess = currentState.Board.Get(command.From.RankIndex, command.From.FileIndex);
            // chess is not null

            // from and to position are not out of boundary

            // Game has no result yet

            // should not move opponent's chess

            // should not move the chess onto player's chess

            // should move what a chess can move
            switch (chess.Type)
            {
                case ChessType.Pawn:
                    if (!IsValidPawnMove(command, currentState)) return false;
                    break;
                default:
                    throw new Exception("Invalid ChessType");
            }
            return true;
        }
        private Position GetInPassingCapturedChessPosition(IBoard board, Position To, PlayerType playerType)
        {
            var offset = playerType == PlayerType.White ? -1 : 1;
            var chess = board.Get(To.RankIndex + offset, To.FileIndex);
            return chess == null ? null : new Position(To.RankIndex + offset, To.File);
        }
        private bool IsValidPawnMove(Command command, GameState gameState)
        {
            var chess = gameState.Board.Get(command.From.RankIndex, command.From.FileIndex);
            var rankMove = command.To.RankIndex - command.From.RankIndex;
            var fileMove = command.To.FileIndex - command.From.FileIndex;

            if (command.Player == PlayerType.Black)
            {
                rankMove *= -1;
            }

            if (fileMove == 0)
            {
                //rank can only be ascsending
                //can move 2 steps from initial location
                return (command.From.Equals(chess.InitialPosition) && rankMove == 2) || (rankMove == 1);
            }
            else
            {
                //check in passing condition
                var positionToCapture = GetInPassingCapturedChessPosition(gameState.Board, command.To, command.Player);
                var chessToCapture = gameState.Board.Get(positionToCapture.RankIndex, positionToCapture.FileIndex);

                return positionToCapture != null
                    && IsInPassingCapturedAvailable(positionToCapture, gameState)
                    && chessToCapture.Player == GetOpponent(command.Player)
                    && (Math.Abs(fileMove) == 1)
                    && (rankMove == 1);

            }
        }
        private bool IsInPassingCapturedAvailable(Position PositionToCapture, GameState gameState)
        {
            var lastMovedToPosition = gameState.PlayerCommand.To;
            if (!lastMovedToPosition.Equals(PositionToCapture)) return false;

            var lastMovedChess = gameState.Board.Get(lastMovedToPosition.RankIndex, lastMovedToPosition.FileIndex);
            if (lastMovedChess.Type != ChessType.Pawn) return false;

            var lastMovedFileDistance = Math.Abs(gameState.PlayerCommand.To.RankIndex - gameState.PlayerCommand.From.RankIndex);
            if (lastMovedFileDistance == 1) return false;

            return true;
        }
        private GameState NextState(Command command, GameState currentState)
        {
            var nextState = currentState.Clone();
            nextState.Player = command.Player;

            var chess = nextState.Board.Get(command.From.RankIndex, command.From.FileIndex);
            nextState.Board.Set(command.From.RankIndex, command.From.FileIndex, null);
            nextState.Board.Set(command.To.RankIndex, command.To.FileIndex, chess);

            nextState.Result = ValidateGameResult(nextState);
            //check in passing
            if (chess.Type == ChessType.Pawn)
            {
                var capturedChessPosition = GetInPassingCapturedChessPosition(currentState.Board, command.To, command.Player);
                if (capturedChessPosition != null)
                {
                    nextState.Board.Set(capturedChessPosition.RankIndex, capturedChessPosition.FileIndex, null);
                }
            }

            return nextState;
        }
        private PlayerType GetOpponent(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Black:
                    return PlayerType.White;
                case PlayerType.White:
                    return PlayerType.Black;
                default:
                    throw new System.Exception("Invlaid PlayerTypes");
            }
        }
        private GameResult ValidateGameResult(GameState state)
        {
            //checkmate
            //return GameResult.Checkmate;

            //stalemate
            //return GameResult.Stalemate;

            return GameResult.None;
        }

    }
}