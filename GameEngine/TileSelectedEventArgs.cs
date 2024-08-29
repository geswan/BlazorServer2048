using System;
using System.Collections.Generic;

namespace GameEngine
{


    public class TileUpdatedEventArgs : EventArgs
    {
        public TileUpdatedEventArgs(int tileId, int tileValue)
        {
            TileID = tileId;
            TileValue = tileValue;

        }

        public int TileID { get; }
        public int TileValue { get; }

    }

    public class TilesUpdatedEventArgs : EventArgs
    {
        public TilesUpdatedEventArgs(IEnumerable<(int value, int index)> tileCollection)
        {
            TileCollection = tileCollection;

        }


        public IEnumerable<(int value, int index)> TileCollection { get; }

    }
}
