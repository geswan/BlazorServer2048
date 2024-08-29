namespace GameEngine
{
    [Flags]
    public enum Direction
    {
        Up = 1,
        Left = 2,
        Down = 4,
        Right = 8,
        UnKnown= 16
    }
    public class Engine
    {
        private readonly TileCollection tileCollection = new();
        private readonly ITileSlider tileSlider = new TileSlider();
        public bool IsWinner = false;
        private int[] lastBlankTilePattern= new int[16];
        private readonly Random random = new();

        public ITileSlider TileSlider => tileSlider;

        public void Reset()
        {
            tileCollection.Clear();
            IsWinner = false;
        }
        public int GetNewTileId()
        {
            int[] emptyTileIndices = tileCollection.EmptyTileIndices.ToArray();
            if (emptyTileIndices.Length == 0) return -1;//no spaces
            int index = random.Next(emptyTileIndices.Length);
            return emptyTileIndices[index];
        }

        //returns the score for the move
        public int SlideTiles(Direction direction) =>

          direction switch
          {
              Direction.Up => TileSlider.SlideAllColumns(Direction.Up,tileCollection),
              Direction.Down => TileSlider.SlideAllColumns(Direction.Down,tileCollection),
              Direction.Left => TileSlider.SlideAllRows(Direction.Left, tileCollection),
              Direction.Right => TileSlider.SlideAllRows(Direction.Right,tileCollection),
              _ => throw new ArgumentException("Invalid enum value", nameof(direction))
          };
        //returns true if another move is possible
        public (bool isContinue,int newTileId) CompleteMove()
        {
            var emptytileCollection = tileCollection.EmptyTileIndices;
            //if nothing has changed since the last move, return true and skip adding a new tile
            if (lastBlankTilePattern.SequenceEqual(emptytileCollection)) return (true,-1);
            if (tileCollection.IsGameWon)
            {
                IsWinner = true;
                return (false,-1);
            }
            int tileId = GetNewTileId();
            var newtileValue = GetRandomTileValue();
            tileCollection[tileId] = newtileValue;
            UpdateAllTiles(true);
            return (!tileCollection.GetIsGameOver(), tileId);
        }
        public void SetBoard(int[] values) => tileCollection.SetBoard(values);

        public void AddNewTilesToCollection(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int newTileId = GetNewTileId();
                var newtileValue = GetRandomTileValue();
                tileCollection[newTileId] = newtileValue;
            }
        }
        public bool UpdateAllTiles(bool isRaiseEvent)
        {
            lastBlankTilePattern = tileCollection.EmptyTileIndices.ToArray();
            if (!isRaiseEvent)
            {
                return true;
            }

            int i = 0;
            foreach (int v in tileCollection)
            {
                var args = new TilesUpdatedEventArgs(tileCollection.AllTilesAndIndices());
                RaiseTilesUpdatedEvent(args);
                i++;
            }          
          
            return true;
        }

        public int  GetTileValue(int x,int y)
        {
            return tileCollection[x,y];
        }

        public int GetTileValue(int i)
        {
            return tileCollection[i];
        }

        public IEnumerable<(int value, int index)> AllTilesAndIndices()
        {
          return  tileCollection.AllTilesAndIndices();
        }
        public IEnumerable<int> TileValues() => tileCollection;
         public event EventHandler<TilesUpdatedEventArgs>? TilesUpdatedEvent;
        private void RaiseTilesUpdatedEvent(TilesUpdatedEventArgs args)
        {
            TilesUpdatedEvent?.Invoke(this, args);
        }
        private int GetRandomTileValue()
        {
            double value = random.NextDouble();
            //10:90 selection ratio between 2 and 1 
            return value < 0.1 ? 2 : 1;
        }
    }
}
