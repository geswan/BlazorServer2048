using BlazorServer2048.Services;
using Fluxor;

namespace BlazorServer2048.Store.GameboardUseCase
{
    public class Effects
    {
        private const int flashRate = 60;
        public IGameService GameService { get; set; }
        public IState<GameboardState> State { get; set; }
        public Effects(IGameService gameService, IState<GameboardState> state)
        {
            GameService = gameService;
            State = state;

        }

        [EffectMethod]
        public Task HandleStartGameA(BoardInitialiseAction action, IDispatcher dispatcher)
        {

            var changedValues = new Dictionary<int, int>();
            IEnumerable<(int value, int id)> tileValues = GameService.StartGameA();
            foreach ((int value, int id) in tileValues)
            {
                changedValues.Add(id, value);
            }

            dispatcher.Dispatch(new UpdateAllTilesActionResultA(changedValues));
            return Task.CompletedTask;
        }

        [EffectMethod]
        public async  Task HandleUpdateBoardA(UpdateBoardAction action, IDispatcher dispatcher)
        {
            var changedTileValues = new Dictionary<int, int>();
            for (int i = 0; i < 16; i++)
            {
                int value = GameService.GetTileValue(i);
                if (value != State.Value.BoardTiles[i].TileValue)
                {
                    changedTileValues.Add(i, value);
                }
            }         
            dispatcher.Dispatch(new UpdateAllTilesActionResultA(changedTileValues ));
            //flash the new tile
            //handled here and not in the BoardComponent as it updates the state
            int newTileValue = GameService.GetTileValue(action.NewTileId);
            await Task.Delay(flashRate);
            dispatcher!.Dispatch(new UpdateSingleTileActionResultA(action.NewTileId, 0));
            await Task.Delay(flashRate);
            dispatcher.Dispatch(new UpdateSingleTileActionResultA(action.NewTileId, newTileValue));
            if (action.IsRunning is false)
            {
                string msg = GameService.IsGameWon ? "Congratulations You Have Won!" : "Better Luck Next Time.";
                dispatcher.Dispatch(new GameOverAction(msg));
            }
          
        }

    }
}

