namespace BlazorServer2048.Store
{
    public record DirectionSelectedActionResult(bool IsRunning, int Score);
    public record DashboardResetActionResult();
    public record OnFirstRenderActionResult();
    public record ToggleRunStopActionResult(bool IsRunning, int Total);
    public record UpdateAllTilesActionResult(Dictionary<int, int>? ChangedTileValues);
    public record UpdateSingleTileActionResult(int TileId, int TileValue);
    public record IsBoardLoadedActionResult();

}
