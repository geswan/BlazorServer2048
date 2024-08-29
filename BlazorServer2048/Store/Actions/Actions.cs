using GameEngine;

namespace BlazorServer2048.Store
{
    public record BoardInitialiseAction();
    
    public record DirectionSelectedAction(Direction Direction);
    public record OnFirstRenderAction();
    public record ToggleRunStopAction();
    public record UpdateBoardAction(int NewTileId, bool IsRunning);

    public record GameOverAction(string Message);
}

