namespace Games.Core.ConnectFour
{
    public class ConnectFourPlayer : Player
    {
        public TileColor Color { get; }
        public ConnectFourPlayer(string name, TileColor color) : base(name)
        {
            Color = color;
        }
    }
}
