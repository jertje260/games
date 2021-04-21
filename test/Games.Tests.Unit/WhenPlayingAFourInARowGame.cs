using FluentAssertions;
using Games.Core;
using Games.Core.ConnectFour;
using Xunit;

namespace Games.Tests.Unit
{
    public class WhenPlayingAFourInARowGame
    {
        private readonly ConnectFourPlayer _player1;
        private readonly ConnectFourPlayer _player2;

        public WhenPlayingAFourInARowGame()
        {
            _player1 = new ConnectFourPlayer("p1", TileColor.Red);
            _player2 = new ConnectFourPlayer("p2", TileColor.Yellow);
        }

        [Fact]
        public void GivenANewGame_ShouldAllowPlayer1_ToPlayStoneAt0_0()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
        }

        [Fact]
        public void GivenANewGame_ShouldntAllowPlayer1_ToPlayStoneAtMinus1_0()
        {
            var game = GivenANewGame();

            game.Invoking(c => c.PlayStone(_player1, -1))
                .Should()
                .Throw<PlayNotAllowedException>();
        }

        [Fact]
        public void GivenANewGame_ShouldntAllowPlayer1_ToPlayStoneAt7_0()
        {
            var game = GivenANewGame();

            game.Invoking(c => c.PlayStone(_player1, 7))
                .Should()
                .Throw<PlayNotAllowedException>();
        }

        [Fact]
        public void GivenANewGame_ShouldNotAllowPlayer2_ToPlayAnyStone()
        {
            var game = GivenANewGame();

            game.Invoking(c => c.PlayStone(_player2, 0))
                .Should()
                .Throw<NotPlayersTurnException>();
        }

        [Fact]
        public void GivenANewGame_ShouldAllowPlayer2_ToPlayAnyStoneAfterPlayer1()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 1);
        }

        [Fact]
        public void GivenANewGame_ShouldntAllowPlayer_ToPlayStoneAfter6StonesAtSameX()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 0);
            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 0);
            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 0);
            game.Invoking(c => c.PlayStone(_player1, 0))
                .Should()
                .Throw<PlayNotAllowedException>();
        }

        [Fact]
        public void GivenANewGame_ShouldBeFinishedWhenPlayerWins_Horizontal()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 0);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 3);

            game.Finished.Should().BeTrue();
        }

        [Fact]
        public void GivenANewGame_ShouldBeFinishedWhenPlayerWins_HorizontalDifferentOrder()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 4);
            game.PlayStone(_player2, 5);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 3);

            game.Finished.Should().BeTrue();
        }

        [Fact]
        public void GivenANewGame_ShouldBeFinishedWhenPlayerWins_Vertically()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 5);
            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 0);

            game.Finished.Should().BeTrue();
        }

        [Fact]
        public void GivenANewGame_ShouldBeFinishedWhenPlayerWins_DiagonallyRightUp()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 0);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 3);
            game.PlayStone(_player1, 5);
            game.PlayStone(_player2, 3);
            game.PlayStone(_player1, 3);
            game.PlayStone(_player2, 6);
            game.PlayStone(_player1, 3);
            game.PlayStone(_player2, 6);
            game.PlayStone(_player1, 2);

            game.Finished.Should().BeTrue();
        }

        [Fact]
        public void GivenANewGame_ShouldBeFinishedWhenPlayerWins_DiagonallyRightDown()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 4);
            game.PlayStone(_player2, 3);
            game.PlayStone(_player1, 3);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 5);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 4);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 6);
            game.PlayStone(_player1, 2);

            game.Finished.Should().BeTrue();
        }

        [Fact]
        public void GivenANewGame_ShouldHaveWinnerSetOnesItsWon()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 4);
            game.PlayStone(_player2, 3);
            game.PlayStone(_player1, 3);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 5);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 4);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 6);
            game.PlayStone(_player1, 2);

            game.Winner.Should().Be(_player1);
        }

        [Fact]
        public void GivenANewGame_ShouldThrowException_WhenTryingMoveOnFishedGame()
        {
            var game = GivenANewGame();

            game.PlayStone(_player1, 4);
            game.PlayStone(_player2, 3);
            game.PlayStone(_player1, 3);
            game.PlayStone(_player2, 2);
            game.PlayStone(_player1, 2);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 5);
            game.PlayStone(_player2, 1);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 4);
            game.PlayStone(_player1, 1);
            game.PlayStone(_player2, 6);
            game.PlayStone(_player1, 2);
            game.Invoking(c => c.PlayStone(_player2, 4))
                .Should()
                .Throw<GameAlreadyFinishedException>();
        }

        private Game GivenANewGame()
        {
            return new Game(_player1, _player2);
        }
    }
}
