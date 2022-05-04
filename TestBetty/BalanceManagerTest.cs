using System;
using Betty;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBetty
{
    [TestClass]
    public class BalanceManagerTest
    {
        private readonly IServiceProvider serviceProvider = new ServiceCollection()
            .AddScoped<IBalanceManager, BalanceManager>()
            .AddScoped<ICasinoGame, SomeBasicCasinoGame>()
            .BuildServiceProvider();

        [TestMethod]
        public void PlaceBet_BalanceOperationFlow()
        {
            // Arrange
            const int amount = 10;
            var balanceManager = serviceProvider.GetRequiredService<IBalanceManager>();
            var game = serviceProvider.GetRequiredService<ICasinoGame>();
            var player = new CasinoPlayer(Guid.NewGuid(), "Martin", amount);

            // Act
            var betId = balanceManager.OpenBet(10, player, game);

            // Assert
            Assert.IsTrue(player.Balance == 0);

            // Act
            var betResult = game.Bet(amount);
            balanceManager.CloseBet(betId, betResult, player);

            // Assert 
            Assert.IsTrue(player.Balance == betResult);
        }
    }
}