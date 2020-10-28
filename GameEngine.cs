using System;
using System.Collections.Generic;

namespace chess_game
{
    public class GameEngine : IGameEngine
    {
        private Stack<GameState> _gameStates;
        public GameState GameState
        {
            get => _gameStates.Count > 0 ? _gameStates.Peek().Clone() : null;
        }

        public GameResult GameResult
        {
            get
            {
                if (_gameStates.Count > 0)
                {
                    return _gameStates.Peek().Result;
                };
                return GameResult.None;
            }
        }

        public GameEngine()
        {
            _gameStates = new Stack<GameState>();
        }

        public GameEngine(GameState gameState)
        {
            _gameStates = new Stack<GameState>();
            _gameStates.Push(gameState);

            if (gameState.PlayerCommand != null && !gameState.PlayerCommand.HasBeenExecuted)
            {
                ExecuteCommand(gameState.PlayerCommand);
            }
        }

        public void ExecuteCommand(Command command)
        {
            if (!IsValidCommand(command, GameState))
            {
                throw new Exception("Invalid Command.");
            }


            _gameStates.Push(NextState(command, GameState));

        }

        public bool HasNextRound(out PlayerType nextPlayer)
        {
            var lastPlayer = _gameStates.Peek().Player;

            switch (GameResult)
            {
                case GameResult.Checkmate:
                case GameResult.Stalemate:
                    nextPlayer = PlayerType.None;
                    return false;

                case GameResult.None:
                    nextPlayer = GetOpponent(lastPlayer);
                    return true;

                default:
                    throw new ArgumentException("Invalid GameResult");
            }
        }

        private GameState NextState(Command command, GameState currentState)
        {
            var nextState = currentState.Clone();
            nextState.Player = command.Player;

            var chess = nextState.Board.GetChess(command.From);
            nextState.Board.SetChess(command.From, null);
            nextState.Board.SetChess(command.To, chess);

            //check in passing
            if (chess.Type == ChessType.Pawn)
            {
                var capturedChessPosition = GetChessPositionCapturedByInPassing(currentState.Board, command.To, command.Player);
                if (capturedChessPosition != null)
                {
                    nextState.Board.SetChess(capturedChessPosition, null);
                }
            }

            nextState.Result = ValidateGameResult(nextState);

            nextState.PlayerCommand = command;
            nextState.PlayerCommand.HasBeenExecuted = true;

            return nextState;
        }

        private GameResult ValidateGameResult(GameState state)
        {
            //checkmate
            //return GameResult.Checkmate;

            //stalemate
            //return GameResult.Stalemate;

            return GameResult.None;
        }

        private bool IsValidCommand(Command command, GameState currentState)
        {
            var chess = currentState.Board.GetChess(command.From);
            if (chess == null)
            {
                return false;
            }
            // chess is not null

            // from and to position are not out of boundary

            // Game has no result yet

            // should not move opponent's chess

            // should not move the chess onto player's chess

            // should move what a chess can move
            switch (chess.Type)
            {
                case ChessType.Pawn:
                    var isValid = IsValidPawnMove(command, currentState);
                    if (!isValid) return false;
                    break;
                default:
                    return false;
            }
            return true;
        }
        private Position GetChessPositionCapturedByInPassing(IBoard board, Position To, PlayerType playerType)
        {
            var positionToCapture = To.Clone();
            positionToCapture.Rank += playerType == PlayerType.White ? -1 : 1;

            return board.GetChess(positionToCapture) == null ? null : positionToCapture;
        }
        private bool IsValidPawnMove(Command command, GameState gameState)
        {
            if (command.Player == PlayerType.None)
            {
                return false;
            }

            var chess = gameState.Board.GetChess(command.From);
            var rankMove = Math.Abs(command.To.Rank - command.From.Rank);
            var fileMove = Math.Abs((int)command.To.File - (int)command.From.File);

            if (fileMove == 0)
            {
                //can not move straight if some other chess were on the way
                //can move 2 steps from initial location
                var direction = command.Player == PlayerType.Black ? -1 : 1;
                for (var move = 1; move <= rankMove; move++)
                {
                    var chessOntheWay = gameState.Board.GetChess(command.From.Offset(0, move * direction));
                    if (chessOntheWay != null)
                    {
                        return false;
                    }
                }
                return (command.From.Equals(chess.InitialPosition) && rankMove == 2) || (rankMove == 1);
            }
            else
            {
                //normal capture
                var chessToCapture = gameState.Board.GetChess(command.To);
                if (chessToCapture != null)
                {
                    return true;
                }
                else
                {
                    //check in passing condition
                    var positionToCapture = GetChessPositionCapturedByInPassing(gameState.Board, command.To, command.Player);
                    chessToCapture = gameState.Board.GetChess(positionToCapture);

                    return positionToCapture != null
                        && IsInPassingCapturedAvailable(positionToCapture, gameState)
                        && chessToCapture.Player == GetOpponent(command.Player)
                        && fileMove == 1
                        && rankMove == 1;
                }
            }
        }
        private bool IsInPassingCapturedAvailable(Position PositionToCapture, GameState gameState)
        {
            var lastMovedToPosition = gameState.PlayerCommand.To;
            if (!lastMovedToPosition.Equals(PositionToCapture)) return false;

            var lastMovedChess = gameState.Board.GetChess(lastMovedToPosition);
            if (lastMovedChess.Type != ChessType.Pawn) return false;

            var lastMovedFileDistance = Math.Abs(gameState.PlayerCommand.To.Rank - gameState.PlayerCommand.From.Rank);
            if (lastMovedFileDistance == 1) return false;

            return true;
        }

        private PlayerType GetOpponent(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Black:
                case PlayerType.None:
                    return PlayerType.White;
                case PlayerType.White:
                    return PlayerType.Black;
                default:
                    throw new System.Exception("Invlaid PlayerTypes");
            }
        }
    }
}