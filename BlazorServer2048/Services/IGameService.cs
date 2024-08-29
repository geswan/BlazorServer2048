using BlazorServer2048.Models;
using GameEngine;

namespace BlazorServer2048.Services
{
    public interface IGameService
    {
        bool IsGameWon { get; }
        bool IsRunning { get; set; }
        List<BoardTile> BoardTiles { get; set; }
        int Total { get; }
        (bool isRunning, int score, int newTileId) PlayMove(Direction direction);
        IEnumerable<(int value, int id)> StartGame();
        int GetTileValue(int x, int y);
        int GetTileValue(int x);
    }
}