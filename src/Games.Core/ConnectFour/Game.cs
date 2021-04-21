namespace Games.Core.ConnectFour
{
    public class Game
    {
        public readonly Tile?[,] PlayedStones;
        private readonly ConnectFourPlayer _player1;
        private readonly ConnectFourPlayer _player2;
        public  ConnectFourPlayer CurrentPlayer { get; private set; }
        private int _playedStonesCount;

        public Game(ConnectFourPlayer player1, ConnectFourPlayer player2)
        {
            _player1 = player1;
            CurrentPlayer = player1;
            _player2 = player2;
            PlayedStones = new Tile?[7, 6];
            _playedStonesCount = 0;
        }

        public bool Started { get; private set; } = false;
        public bool Finished { get; private set; }
        public ConnectFourPlayer? Winner { get; private set; }

        public void Start()
        {
            Started = true;
        }

        public void PlayStone(ConnectFourPlayer player, int x)
        {
            if (!Started)
            {
                throw new GameNotStartedException();
            }

            if (Finished)
            {
                throw new GameAlreadyFinishedException();
            }

            if (player != CurrentPlayer)
            {
                throw new NotPlayersTurnException();
            }

            if (x < 0 || x > 6)
            {
                throw new PlayNotAllowedException();
            }

            var y = GetYForX(x);
            if (y < 0 || y > 5)
            {
                throw new PlayNotAllowedException();
            }

            PlayedStones[x, y] = new Tile(x, y, player.Color);
            _playedStonesCount++;

            Finished = IsFinished(x, y, player);
            if (Finished)
            {
                Winner = CurrentPlayer;
            }

            if (IsBoardFull())
            {
                Finished = true;
            }

            CurrentPlayer = CurrentPlayer == _player1 ? _player2 : _player1;
        }

        private bool IsBoardFull()
        {
            return _playedStonesCount == 42;
        }

        private bool IsFinished(int x, int y, ConnectFourPlayer player)
        {
            var inARow = 1;
            var lastChecked = (x, y);

            // horizontal to the left
            while (lastChecked.x - 1 >= 0 && PlayedStones[lastChecked.x - 1, y]?.Color == player.Color)
            {
                inARow++;
                lastChecked.x--;
                if (inARow == 4)
                {
                    return true;
                }
            }

            // horizontal to the right
            lastChecked = (x, y);
            while (lastChecked.x + 1 <= 6 && PlayedStones[lastChecked.x + 1, y]?.Color == player.Color)
            {
                inARow++;
                lastChecked.x++;
                if (inARow == 4)
                {
                    return true;
                }
            }

            // reset for vertical
            inARow = 1;
            lastChecked = (x, y);
            while (lastChecked.y - 1 >= 0 && PlayedStones[x, lastChecked.y - 1]?.Color == player.Color)
            {
                inARow++;
                lastChecked.y--;
                if (inARow == 4)
                {
                    return true;
                }
            }

            // reset for diagonally right up
            inARow = 1;
            lastChecked = (x, y);
            while (lastChecked.y + 1 <= 5 && lastChecked.x + 1 <= 6 && PlayedStones[lastChecked.x + 1, lastChecked.y + 1]?.Color == player.Color)
            {
                inARow++;
                lastChecked.y++;
                lastChecked.x++;
                if (inARow == 4)
                {
                    return true;
                }
            }

            lastChecked = (x, y);
            while (lastChecked.y - 1 >= 0 && lastChecked.x - 1 >= 0 && PlayedStones[lastChecked.x - 1, lastChecked.y - 1]?.Color == player.Color)
            {
                inARow++;
                lastChecked.y--;
                lastChecked.x--;
                if (inARow == 4)
                {
                    return true;
                }
            }

            // reset for diagonally right down
            inARow = 1;
            lastChecked = (x, y);
            while (lastChecked.y - 1 >= 0 && lastChecked.x + 1 <= 6 && PlayedStones[lastChecked.x + 1, lastChecked.y - 1]?.Color == player.Color)
            {
                inARow++;
                lastChecked.y--;
                lastChecked.x++;
                if (inARow == 4)
                {
                    return true;
                }
            }

            lastChecked = (x, y);
            while (lastChecked.y + 1 <= 5 && lastChecked.x - 1 >= 0 && PlayedStones[lastChecked.x - 1, lastChecked.y + 1]?.Color == player.Color)
            {
                inARow++;
                lastChecked.y++;
                lastChecked.x--;
                if (inARow == 4)
                {
                    return true;
                }
            }

            return false;
        }

        private int GetYForX(int x)
        {
            var y = 0;

            while (y <= 5 && PlayedStones[x, y] != null)
            {
                y++;
            }

            return y;
        }
    }
}
