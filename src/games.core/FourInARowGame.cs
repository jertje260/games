using System;

namespace Games.Core
{
    public class FourInARowGame
    {
        private Stone[,] _playedStones;
        private Player _player1;
        private Player _player2;
        private Player _currentPlayer;

        public FourInARowGame(Player player1, Player player2)
        {
            _player1 = player1;
            _currentPlayer = player1;
            _player2 = player2;
            _playedStones = new Stone[7, 6];
        }

        public bool Finished { get; private set; }


        public void PlayStone(Player player, int x)
        {
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

            Finished = IsFinished(x, y, player);

            if (_currentPlayer == _player1)
            {
                _currentPlayer = _player2;
            }
            else
            {
                _currentPlayer = _player1;
            }
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

    public class PlayNotAllowedException : Exception
    {
    }

    public class NotPlayersTurnException : Exception
    {
    }

    public class Player
    {
        private string _name;
        public string Name => _name;

        public Player(string name)
        {
            _name = name;
        }
    }

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

        public bool SamePlayer(Player player)
        {
            return Player == player;
        }
    }
}
