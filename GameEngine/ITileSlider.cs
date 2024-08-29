namespace GameEngine
{
    public interface ITileSlider
    {
        int SlideAllColumns(Direction direction, TileCollection tileCollection);
        int SlideAllRows(Direction direction, TileCollection tileCollection);
    }
}