namespace GameEngine
{
    public class TileSlider : ITileSlider
    {
        public int SlideAllColumns(Direction direction, TileCollection tileCollection)
        {
            int moveScore = 0;
            for (int i = 0; i < 4; i++)
            {
                moveScore += SlideColumn(i, direction, tileCollection);
            }
            return moveScore;
        }

        private int SlideColumn(int col, Direction direction, TileCollection tileCollection)
        {
            var contiguousList = tileCollection.Select(v => v).Where((v, i) => i % 4 == col && v != 0).ToList();
            var (colAsList, score) = Slide(contiguousList, direction);
            int row = 0;
            //transfer to tileCollection           
            foreach (int n in colAsList)
            {
                tileCollection[row, col] = n;
                row++;
            }
            return score;
        }



        public int SlideAllRows(Direction direction, TileCollection tileCollection)
        {
            int moveScore = 0;
            for (int i = 0; i < 4; i++)
            {
                moveScore += SlideRow(i, direction, tileCollection);
            }
            return moveScore;
        }

        private int SlideRow(int row, Direction direction, TileCollection tileCollection)
        {
            //exclude blank tiles from list
            var contiguousList = tileCollection.Select(v => v).Where((v, i) => i / 4 == row && v != 0).ToList();
            var (rowAsList, score) = Slide(contiguousList, direction);
            int col = 0;
            //transfer to tileCollection
            foreach (int n in rowAsList)
            {
                tileCollection[row, col] = n;
                col++;
            }
            return score;
        }


        private (List<int> formattedList, int score) Slide(List<int> tileList, Direction direction)
        {
            //default sliding is left/up
            // so need to reverse the order for right/ down sliding
            bool isReversed = direction == Direction.Right || direction == Direction.Down;
            if (isReversed) tileList.Reverse();
            int slideScore = 0;
            //slide matching values together
            for (int index = 0; index < tileList.Count - 1; index++)
            {
                if (tileList[index] == tileList[index + 1])
                {
                    //mark tile for deletion
                    tileList[index + 1] = 0;
                    tileList[index] += 1;//promote tile
                    slideScore += 1 << tileList[index];//no need to use Math.Pow()
                    //slideScore += (int)Math.Pow(2, list[index]);
                    index++;//skip blank tile
                }
            }
            //No spaces allowed between values so remove the blanks created by combining values
            var formattedList = tileList.Select((i) => i).Where((i) => i != 0).ToList();
            //add zeros to pad the row up to the row length
            formattedList.AddRange(Enumerable.Repeat(0, 4 - formattedList.Count));
            if (isReversed) formattedList.Reverse();
            return (formattedList, slideScore);

        }
    }
}
