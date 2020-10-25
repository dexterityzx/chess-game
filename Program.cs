﻿using System;

namespace chess_game
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = InitializeGame();

            var player = PlayerType.White;

            while (game.GameResult == GameResult.None)
            {
                try
                {
                    //get player command
                    Console.WriteLine($"{player.ToString()}:");
                    var commandString = Console.ReadLine();
                    //
                    var command = GenerateCommand(commandString, player);
                    game.ExecuteCommand(command);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            }

            Console.WriteLine(player.ToString());

        }

        public static Game InitializeGame()
        {
            var board = new Board();
            for (char file = Position.MIN_FILE; file <= Position.MAX_FILE; file++)
            {
                //Console.Write(file);
                var whiteChessPosition = new Position(2, file);
                board.Set(whiteChessPosition.RankIndex, whiteChessPosition.FileIndex, new Chess(PlayerType.White, ChessType.Pawn, whiteChessPosition));

                var BlackChessPosition = new Position(7, file);
                board.Set(BlackChessPosition.RankIndex, BlackChessPosition.FileIndex, new Chess(PlayerType.Black, ChessType.Pawn, BlackChessPosition));
            }

            var gameState = new GameState();
            gameState.Board = board;
            gameState.Result = GameResult.None;

            return new Game(gameState);
        }

        public static Command GenerateCommand(string commandString, PlayerType player)
        {
            var positions = commandString.Split("->");

            var from = new Position(int.Parse(positions[0].Substring(0, 1)), positions[0][0]);
            var to = new Position(int.Parse(positions[1].Substring(0, 1)), positions[1][0]);

            return new Command(player, from, to, false);
        }

    }
}
