namespace BlazorServer2048.Models
{
    public static  class TileComponentBase
    {
       
        public  static readonly string TileBaseName = "d-flex  justify-content-center align-items-center tile ";

        public static readonly List<string> ClassNames = new()
        {
            string.Empty,
           "tile2",
           "tile4",
           "tile8",
           "tile16",
           "tile32",
           "tile64",
           "tile128",
           "tile256",
           "tile512",
           "tile1024",
           "tile2048",
        };
    }
}
