using Fluxor;

namespace BlazorServer2048.Store.DashboardUseCase
{


    [FeatureState]

    public record DashboardState(int Total, bool IsRunning, bool IsGameWon,bool IsInitialised)
    {
        public DashboardState() : this(0, false, false,false)//default constructor C#9
        {

        }
    }


}




