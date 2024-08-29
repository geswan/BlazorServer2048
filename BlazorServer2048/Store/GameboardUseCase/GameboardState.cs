using BlazorServer2048.Models;
using Fluxor;
using System.Collections.Immutable;
namespace BlazorServer2048.Store.GameboardUseCase
{
    [FeatureState]
    public record GameboardState
    {
        public GameboardState()
        {
            var boardTiles = new List<BoardTile>(16);

           for (int i = 0; i < 16; i++)
            {
                boardTiles.Add(new BoardTile(i, 0));
 
           }
          
            BoardTiles = ImmutableArray.Create(boardTiles.ToArray());
        }

        public ImmutableArray<BoardTile> BoardTiles { get; init; }
    }


}
