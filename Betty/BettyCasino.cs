namespace Betty
{
    public class BettyCasino : ICasino
    {
        private readonly IBalanceManager balanceManager;

        public BettyCasino(IBalanceManager balanceManager)
        {
            this.balanceManager = balanceManager;
        }

        public string Name => "Betty";

        public int Bet(int amount, ICasinoGame game, CasinoPlayer player)
        {
            // {new balance} = {old balance} - {bet amount} + {win amount}
            var betId = balanceManager.OpenBet(amount, player, game);
            var betResult = game.Bet(amount);
            balanceManager.CloseBet(betId, betResult, player);
            return betResult;
        }

        public void Deposit(int amount, CasinoPlayer player)
        {
            balanceManager.Deposit(amount, player);
        }

        public void Withdraw(int amount, CasinoPlayer player)
        {
            balanceManager.Withdraw(amount, player);
        }
    }
}
