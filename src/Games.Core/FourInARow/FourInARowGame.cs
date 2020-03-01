namespace Games.Core.FourInARow
{
    public class FourInARowGame
    {
        private readonly Stone?[,] _playedStones;
        private readonly Player _player1;
        private readonly Player _player2;
        private Player _currentPlayer;
        private int _playedStonesCount;

        public FourInARowGame(Player player1, Player player2)
        {
            _player1 = player1;
            _currentPlayer = player1;
            _player2 = player2;
            _playedStones = new Stone?[7, 6];
            _playedStonesCount = 0;
        }

        public bool Finished { get; private set; }
        public Player? Winner { get; private set; }


        public void PlayStone(Player player, int x)
        {
            if (Finished)
            {
                throw new GameAlreadyFinishedException();
            }

            if (player != _currentPlayer)
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

            _playedStones[x, y] = new Stone(x, y, player);
            _playedStonesCount++;

            Finished = IsFinished(x, y, player);
            if (Finished)
            {
                Winner = _currentPlayer;
            }

            if (IsBoardFull())
            {
                Finished = true;
            }

            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        }

        private bool IsBoardFull()
        {
            return _playedStonesCount == 42;
        }

        private bool IsFinished(int x, int y, Player player)
        {
            var inARow = 1;
            var lastChecked = (x, y);

            // horizontal to the left
            while (lastChecked.x - 1 >= 0 && _playedStones[lastChecked.x - 1, y]?.Player == player)
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
            while (lastChecked.x + 1 <= 6 && _playedStones[lastChecked.x + 1, y]?.Player == player)
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
            while (lastChecked.y - 1 >= 0 && _playedStones[x, lastChecked.y - 1]?.Player == player)
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
            while (lastChecked.y + 1 <= 5 && lastChecked.x + 1 <= 6 && _playedStones[lastChecked.x + 1, lastChecked.y + 1]?.Player == player)
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
            while (lastChecked.y - 1 >= 0 && lastChecked.x - 1 >= 0 && _playedStones[lastChecked.x - 1, lastChecked.y - 1]?.Player == player)
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
            while (lastChecked.y - 1 >= 0 && lastChecked.x + 1 <= 6 && _playedStones[lastChecked.x + 1, lastChecked.y - 1]?.Player == player)
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
            while (lastChecked.y + 1 <= 5 && lastChecked.x - 1 >= 0 && _playedStones[lastChecked.x - 1, lastChecked.y + 1]?.Player == player)
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

            while (y <= 5 && _playedStones[x, y] != null)
            {
                y++;
            }

            return y;
        }
    }
}
