namespace BlazorServer2048.Store
{
    public record DirectionSelectedActionResult(bool IsRunning, int Total);
    public record DashboardResetActionResult();
    public record OnFirstRenderActionResult();
    public record ToggleRunStopActionResult(bool IsRunning, int Total);
    public record UpdateAllTilesActionResultA(Dictionary<int, int>? ChangedTileValues);
    public record UpdateSingleTileActionResultA(int TileId, int TileValue);
    public record IsBoardLoadedActionResult();

}
