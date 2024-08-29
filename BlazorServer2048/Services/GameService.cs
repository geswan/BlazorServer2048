using BlazorServer2048.Models;
using GameEngine;

namespace BlazorServer2048.Services
{
    public class GameService : IGameService
    {
        public GameService()
        {
            BoardTiles = [];
            for (int i = 0; i < 16; i++)
            {
                BoardTiles.Add(new BoardTile(i, 0));
            }

        }

        public int Total { get; private set; }

        private readonly Engine gameEngine = new();

        public List<BoardTile> BoardTiles { get; set; }
        public bool IsGameWon
        {

            get => gameEngine.IsWinner;
        }

        public bool IsRunning { get; set; } = true;

        public IEnumerable<(int value, int id)> StartGameA()
        {

            gameEngine.Reset();
            gameEngine.AddNewTilesToCollection(2);
            Total = 0;
            IsRunning = true;
            return gameEngine.AllTilesAndIndices();

        }


        public (bool isRunning, int score, int newTileId) PlayMoveA(Direction direction)
        {
            if (direction == Direction.UnKnown) return (false, 0, -1);
            int score = gameEngine.SlideTiles(direction);
            (IsRunning, int newTileId) = gameEngine.CompleteMove();
            return (IsRunning, score, newTileId);
        }

        public int GetTileValue(int x, int y)
        {
            return gameEngine.GetTileValue(x, y);
        }

        public int GetTileValue(int x)
        {
            return gameEngine.GetTileValue(x);
        }

    }
}