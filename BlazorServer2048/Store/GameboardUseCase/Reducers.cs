
using BlazorServer2048.Models;
using Fluxor;
using System.Collections.Immutable;

namespace BlazorServer2048.Store.GameboardUseCase
{
    public static class Reducers
    {

        [ReducerMethod]
        public static GameboardState ReduceUpdateAllTilesActionResultA(GameboardState state, UpdateAllTilesActionResultA action)
        {
            List<BoardTile> updatedTiles = [];
            foreach (var boardTile in state!.BoardTiles)
            {
                if (action.ChangedTileValues!.TryGetValue(boardTile.Id, out int value))
                {
                    updatedTiles.Add(boardTile with { TileValue = value });
                }
                else
                {
                    updatedTiles.Add(boardTile);
                }
            }

            var newArray = ImmutableArray.Create(updatedTiles.ToArray());
            return state with { BoardTiles = newArray };
        }

        [ReducerMethod]
        public static GameboardState ReduceUpdateSingleTileActionResultA(GameboardState state, UpdateSingleTileActionResultA action)
        {
            var updatedTiles = state!.BoardTiles.SetItem(
                action.TileId, new BoardTile(action.TileId, action.TileValue));
            return state with { BoardTiles = updatedTiles };
        }

    }

}
