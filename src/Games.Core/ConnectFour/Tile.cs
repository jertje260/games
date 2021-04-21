namespace Games.Core.ConnectFour
{
    public class Tile
    {
        public TileColor Color { get; }
        public int X { get; }
        public int Y { get; }

        public Tile(int x, int y, TileColor color)
        {
            Color = color;
            X = x;
            Y = y;
        }
    }
}