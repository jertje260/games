namespace Games.Core.FourInARow
{
    public class Stone
    {
        public readonly Player Player;
        public int X { get; }
        public int Y { get; }

        public Stone(int x, int y, Player player)
        {
            Player = player;
            X = x;
            Y = y;
        }
    }
}