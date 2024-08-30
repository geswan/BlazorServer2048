using Fluxor;


namespace BlazorServer2048.Store.DashboardUseCase
{
    public static class Reducers
    {
        [ReducerMethod]
        public static DashboardState ReduceToggleRunStopActionResult(DashboardState state, ToggleRunStopActionResult action)
        {
           
            return state with { IsRunning = action.IsRunning};
        }

        [ReducerMethod]
        public static DashboardState DirectionSelectedActionResult(DashboardState state, DirectionSelectedActionResult action)
        {

            return state with { IsRunning = action.IsRunning, Total =state.Total + action.Score };

        }

        [ReducerMethod(typeof(DashboardResetActionResult))]
        public static DashboardState HandleDashboardResetActionResult(DashboardState state)
        {
          
           return  state with { IsRunning = true,Total=0,IsGameWon=false };
        }

    }

}

