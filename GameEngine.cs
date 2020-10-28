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
            _gameStates.Push(gameState.Clone());

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

        private bool HasPlayerWon(GameState gameState, PlayerType currentPlayer)
        {
            if (!HasPositionsToMove(gameState, GetOpponent(currentPlayer)))
            {
                return true;
            }

            var rankToCheck = currentPlayer == PlayerType.Black ? Position.MIN_RANK : Position.MAX_RANK;
            var board = gameState.Board;
            for (char file = Position.MIN_FILE; file <= Position.MAX_FILE; file++)
            {
                var position = new Position(file, rankToCheck);
                var chess = board.GetChess(new Position(file, rankToCheck));

                if (chess != null)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasNextRound(out PlayerType nextPlayer)
        {
            var lastPlayer = _gameStates.Peek().Player;
            var gameState = _gameStates.Peek();

            if (lastPlayer == PlayerType.None)
            {
                nextPlayer = PlayerType.White;
                return true;
            }

            if (HasPlayerWon(gameState, lastPlayer))
            {
                gameState.Result = GameResult.Checkmate;
                nextPlayer = PlayerType.None;
                return false;
            }

            nextPlayer = gameState.HasAnotherRound ? lastPlayer : GetOpponent(lastPlayer);
            return true;
        }
        private GameState NextState(Command command, GameState currentState)
        {
            var nextState = currentState.Clone();
            nextState.Player = command.Player;

            var chessToCapture = nextState.Board.GetChess(command.To);
            nextState.HasAnotherRound = chessToCapture != null ? true : false;

            var chessToMove = nextState.Board.GetChess(command.From);
            nextState.Board.SetChess(command.From, null);
            nextState.Board.SetChess(command.To, chessToMove);

            nextState.PlayerCommand = command;
            nextState.PlayerCommand.HasBeenExecuted = true;

            return nextState;
        }
        private bool HasPositionsToMove(GameState gameState, PlayerType currentPlayer)
        {
            var positionsToCapture = FindPositionsToCapture(gameState, currentPlayer);
            if (positionsToCapture.Count > 0)
            {
                return true;
            }

            var rankDirection = GetRankMoveDirection(currentPlayer);
            var board = gameState.Board;
            for (int rank = Position.MIN_RANK; rank <= Position.MAX_RANK; rank++)
            {
                for (char file = Position.MIN_FILE; file <= Position.MAX_FILE; file++)
                {
                    var position = new Position(file, rank);
                    var chess = board.GetChess(new Position(file, rank));

                    if (chess == null)
                    {
                        continue;
                    }

                    if (chess.Player != currentPlayer)
                    {
                        continue;
                    }

                    var chessOntheWay = board.GetChess(position.Offset(0, 1 * rankDirection));
                    if (chessOntheWay == null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        private Dictionary<string, Position> FindPositionsToCapture(GameState gameState, PlayerType currentPlayer)
        {
            var positionsToCapture = new Dictionary<string, Position>();
            var rankDirection = GetRankMoveDirection(currentPlayer);
            var board = gameState.Board;

            for (int rank = Position.MIN_RANK; rank <= Position.MAX_RANK; rank++)
            {
                for (char file = Position.MIN_FILE; file <= Position.MAX_FILE; file++)
                {
                    var position = new Position(file, rank);
                    var chess = board.GetChess(new Position(file, rank));

                    if (chess == null)
                    {
                        continue;
                    }

                    if (chess.Player != currentPlayer)
                    {
                        continue;
                    }

                    var positionToCapture = position.Offset(1, 1 * rankDirection);
                    if (positionToCapture != null)
                    {
                        var chessToCapture = gameState.Board.GetChess(positionToCapture);
                        if (chessToCapture != null && chessToCapture.Player == GetOpponent(currentPlayer))
                        {
                            positionsToCapture.Add(positionToCapture.ToHash(), positionToCapture);
                        }
                    }

                    positionToCapture = position.Offset(-1, 1 * rankDirection);
                    if (positionToCapture != null)
                    {
                        var chessToCapture = gameState.Board.GetChess(positionToCapture);
                        if (chessToCapture != null && chessToCapture.Player == GetOpponent(currentPlayer))
                        {
                            positionsToCapture.Add(positionToCapture.ToHash(), positionToCapture);
                        }
                    }
                }
            }

            return positionsToCapture;
        }
        private bool IsValidCommand(Command command, GameState gameState)
        {
            if (command.Player == PlayerType.None)
            {
                return false;
            }

            var chess = gameState.Board.GetChess(command.From);
            if (chess == null)
            {
                return false;
            }

            var rankMoveDirection = GetRankMoveDirection(command.Player);
            var rankMove = command.To.Rank - command.From.Rank;

            // we can only move in same direction
            if ((rankMove * rankMoveDirection) < 0)
            {
                return false;
            }

            rankMove = Math.Abs(rankMove);
            var fileMove = Math.Abs((int)command.To.File - (int)command.From.File);

            var positionsToCapture = FindPositionsToCapture(gameState, command.Player);

            //we can not move the same chess again
            if (gameState.HasAnotherRound)
            {
                if (command.From.Equals(gameState.PlayerCommand.To))
                {
                    return false;
                }
            }
            //we have to capture something
            if (positionsToCapture.Count > 0)
            {
                return positionsToCapture.ContainsKey(command.To.ToHash());
            }
            else
            {
                // we can not move diagonal if we were not capturing anything
                if (fileMove != 0)
                {
                    return false;
                }
                else
                {
                    var rankDirection = GetRankMoveDirection(command.Player);
                    if (rankMove != 1)
                    {
                        return false;
                    }

                    var chessOntheWay = gameState.Board.GetChess(command.From.Offset(0, rankMove * rankDirection));
                    if (chessOntheWay != null)
                    {
                        return false;
                    }

                    return true;
                }
            }
        }
        private int GetRankMoveDirection(PlayerType player)
        {
            return player == PlayerType.Black ? -1 : 1;
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