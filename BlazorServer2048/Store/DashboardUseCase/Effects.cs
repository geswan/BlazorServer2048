using BlazorServer2048.Services;
using Fluxor;

namespace BlazorServer2048.Store.DashboardUseCase
{
    public class Effects
    {
        public IGameService GameService { get; set; }
        public IState<DashboardState> State { get; set; }
        public Effects(IGameService gameService, IState<DashboardState> state)
        {
            GameService = gameService;
            State = state;
        }

      

        [EffectMethod]
        public Task HandleToggleRunStopAction(ToggleRunStopAction action, IDispatcher dispatcher)
        {
            if (State.Value.IsRunning)
            {
                GameService.IsRunning = false;
                dispatcher.Dispatch(new ToggleRunStopActionResult(false,0));
            }
            else
            {
                dispatcher.Dispatch(new DashboardResetActionResult());
                dispatcher.Dispatch(new BoardInitialiseAction());//handled in TilesUseCase Effects
            }
          
            return Task.CompletedTask;
        }
        [EffectMethod]
        public Task HandleDirectionSelectedAction(DirectionSelectedAction action, IDispatcher dispatcher)
        {
            int score;
            if (GameService.IsRunning)
            {
                (bool isRunning, score, int newTileId) = GameService.PlayMoveA(action.Direction);
                dispatcher.Dispatch(new DirectionSelectedActionResult(isRunning, score + State.Value.Total));
                if (newTileId == -1) return Task.CompletedTask;//no new tile required so the board has not changed
                //separation of concerns, let the TilesUseCase Effects class update the board
                dispatcher.Dispatch(new UpdateBoardAction(newTileId,isRunning));
            }
            return Task.CompletedTask;
        }

        [EffectMethod(typeof(OnFirstRenderAction))]
        public Task HandleOnFirstRenderAction(IDispatcher dispatcher)
        {          
                dispatcher.Dispatch(new DashboardResetActionResult());
                dispatcher.Dispatch(new BoardInitialiseAction());
                return Task.CompletedTask;
        }
       
    }

}
