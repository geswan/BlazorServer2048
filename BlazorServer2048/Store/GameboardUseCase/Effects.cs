using BlazorServer2048.Services;
using Fluxor;


namespace BlazorServer2048.Store.GameboardUseCase
{
    public class Effects(IGameService gameService,  IState<GameboardState> gameboardState)
    {
        private const int flashRate = 60;
        public IGameService GameService { get; set; } = gameService;
        public IState<GameboardState> GameboardState { get; set; } = gameboardState;

        [EffectMethod(typeof(BoardInitialiseAction))]
        public Task HandleStartGame( IDispatcher dispatcher)
        {

            var changedValues = new Dictionary<int, int>();
            IEnumerable<(int value, int id)> tileValues = GameService.StartGame();
            foreach ((int value, int id) in tileValues)
            {
                changedValues.Add(id, value);
            }

            dispatcher.Dispatch(new UpdateAllTilesActionResult(changedValues));
            return Task.CompletedTask;
        }

  
        [EffectMethod]
        public async Task HandleDirectionSelectedAction(DirectionSelectedAction action, IDispatcher dispatcher)
        {
            int score;
            if (GameService.IsRunning)
            {
                (bool isRunning, score, int newTileId) = GameService.PlayMove(action.Direction);
                //update Dashboard state
                dispatcher.Dispatch(new DirectionSelectedActionResult(isRunning, score ));
                if (newTileId == -1) return ;//no new tile required so the board has not changed
                await UpdateBoard(newTileId, isRunning,dispatcher);
            }

        }

        public async Task UpdateBoard(int newTileId, bool isRunning, IDispatcher dispatcher)
        {
            var changedTileValues = new Dictionary<int, int>();
            for (int i = 0; i < 16; i++)
            {
                int value = GameService.GetTileValue(i);
                if (value != GameboardState.Value.BoardTiles[i].TileValue)
                {
                    changedTileValues.Add(i, value);
                }
            }
            dispatcher.Dispatch(new UpdateAllTilesActionResult(changedTileValues));
            //flash the new tile
            //handled here and not in the BoardComponent as it updates the state
            int newTileValue = GameService.GetTileValue(newTileId);
            await Task.Delay(flashRate);
            dispatcher!.Dispatch(new UpdateSingleTileActionResult(newTileId, 0));
            await Task.Delay(flashRate);
            dispatcher.Dispatch(new UpdateSingleTileActionResult(newTileId, newTileValue));
            if (isRunning is false)
            {
                string msg = GameService.IsGameWon ? "Congratulations You Have Won!" : "Better Luck Next Time.";
                dispatcher.Dispatch(new GameOverAction(msg));
            }

        }
    }
}

