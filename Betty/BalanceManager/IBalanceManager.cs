namespace Betty
{
    public interface IBalanceManager
    {
        void Withdraw(int amount, CasinoPlayer player);
        void Deposit(int amount, CasinoPlayer player);
        Guid OpenBet(int amount, CasinoPlayer player, ICasinoGame game);
        bool CloseBet(Guid betId, int betResult, CasinoPlayer player);
    }
}