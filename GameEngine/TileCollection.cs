using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    public class TileCollection : IEnumerable<int>
    {
        private readonly int[] tiles = new int[16];

        //indexer-enables tileCollection[row,col] access to tiles
        public int this[int row, int col]
        {
            get => tiles[row * 4 + col];
            set => tiles[row * 4 + col] = value;
        }
        //indexer-enables tileCollection[i] access to tiles
        public int this[int i]
        {
            get => tiles[i];
            set => tiles[i] = value;
        }
        public static bool operator ==(TileCollection left, TileCollection right) => left.SequenceEqual(right);
        public static bool operator !=(TileCollection left, TileCollection right) => !left.SequenceEqual(right);
        public override bool Equals(object? obj) => Equals(obj as TileCollection);

        // http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
        public override int GetHashCode()
        {
            if (tiles == null) return 0;
            unchecked
            {
                int hash = 17;
                foreach (var n in tiles)
                {
                    hash = hash * 23 + n;
                }
                return hash;
            }
        }
        public void SetBoard(int[] values) => Array.Copy(values, tiles, tiles.Length);

        public TileCollection Clone()
        {
            var clone = new TileCollection();
            int i = 0;
            foreach (int n in tiles)
            {
                clone[i] = n;
                i++;
            }
            return clone;
        }
        public void Clear() => Array.Fill(tiles, 0);

        public bool IsGameWon
        {
            // a value of 11 gives a score of 2 to Power 11=2048
            get => tiles.Contains(11);
        }
        public bool IsCollectionFull
        {
            get => !tiles.Any((v) => v == 0);
        }
        //The positions  of blank tiles in the array
        //Needed because a new tile is placed randomly in one of the positions
        //after a move
        public IEnumerable<int> EmptyTileIndices
        {
            get => tiles.Select((v, index) => index).Where((i) => tiles[i] == 0);
        }
        public IEnumerable<(int value, int index)> AllTilesAndIndices()
        {
            return tiles.Select((v, index) => (v, index));
        }

        public bool GetIsGameOver()
        {
            return IsCollectionFull
            //&& no possible matches
            && !tiles.Where((v,
           // check row, i/4 and (i+1)/4 gives same value for tiles on the same row
           i) => (i < 15 && tiles[i] == tiles[i + 1] && i / 4 == (i + 1) / 4) ||
                 //check col, look ahead i+4 
                 (i < 12 && tiles[i] == tiles[i + 4])).Any();
        }

        //enables the use of foreach
        public IEnumerator<int> GetEnumerator()
        {
            //need to cast IEnumerator to IEnumerator<int>
            return tiles.Cast<int>().GetEnumerator();
        }
       
        //non generic enumerator-not used but needed for IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
