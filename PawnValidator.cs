using System;

namespace chess_game
{
    public class PawnValidator : IValidator
    {
        public void ValidateAndThrowException(Command command, GameState gameState)
        {
            if (command.Player == PlayerType.None)
            {
                throw new Exception("Player is None.");
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
                        throw new Exception("Invalid move.");
                    }
                }

                if (rankMove == 2 && !command.From.Equals(chess.InitialPosition))
                {
                    throw new Exception("Invalid move.");
                }

                if (rankMove != 1)
                {
                    throw new Exception("Invalid move.");
                }
            }
            else
            {
                //normal capture
                var chessToCapture = gameState.Board.GetChess(command.To);
                if (chessToCapture == null)
                {
                    //check in passing condition
                    var positionToCapture = GetChessPositionCapturedByInPassing(gameState.Board, command.To, command.Player);
                    chessToCapture = gameState.Board.GetChess(positionToCapture);

                    if (positionToCapture == null)
                    {
                        throw new Exception("Invalid move.");
                    }

                    if (!IsInPassingCapturedAvailable(positionToCapture, gameState))
                    {
                        throw new Exception("Invalid move.");
                    }

                    if (chessToCapture.Player != GetOpponent(command.Player))
                    {
                        throw new Exception("Invalid move.");
                    }

                    if (fileMove != 1 || rankMove != 1)
                    {
                        throw new Exception("Invalid move.");
                    }

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
        private Position GetChessPositionCapturedByInPassing(IBoard board, Position To, PlayerType playerType)
        {
            var positionToCapture = To.Clone();
            positionToCapture.Rank += playerType == PlayerType.White ? -1 : 1;

            return board.GetChess(positionToCapture) == null ? null : positionToCapture;
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