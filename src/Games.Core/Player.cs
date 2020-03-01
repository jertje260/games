namespace Games.Core
{
    public class Player
    {
        private string _name;
        public string Name => _name;

        public Player(string name)
        {
            _name = name;
        }
    }
}