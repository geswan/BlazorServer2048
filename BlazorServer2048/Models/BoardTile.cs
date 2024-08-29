namespace BlazorServer2048.Models
{
    public record BoardTile
    {
        public BoardTile(int id,int tileValue) 
        {
            Id = id;
            TileValue= tileValue;          
        }
        public int Id { get; init; }
        public int TileValue { get; init; } 

        public string FaceValueText
        {
            get
            {
                int value = TileValue == 0 ? 0 : (1 << TileValue);//2 to the power of TileValue
                return value == 0 ? string.Empty : value.ToString();
            }

        }

    }
}
