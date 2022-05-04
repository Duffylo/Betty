using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBetty
{
    [TestClass]
    public class PlayerTest
    {
        [TestMethod]
        public void Player_Balance_Add10_Is10()
        {
            // Arrange
            var player = new CasinoPlayer(Guid.NewGuid(), "Martin", 0);

            // Act
            player.Balance += 10;

            // Assert
            Assert.IsTrue(player.Balance == 10);
        }
    }
}