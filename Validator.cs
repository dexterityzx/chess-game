using System;

namespace chess_game
{

    public class Validator : IValidator
    {
        private IValidator _pawnValidator;
        public Validator(IValidator pawnValidator)
        {
            _pawnValidator = pawnValidator;
        }
        public void ValidateAndThrowException(Command command, GameState currentState)
        {
            var chess = currentState.Board.GetChess(command.From);
            if (chess == null)
            {
                throw new Exception("Invalid Command. From position has no chess.");
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
                    _pawnValidator.ValidateAndThrowException(command, currentState);
                    break;
                default:
                    throw new Exception("Invalid ChessType");
            }
        }
    }
}