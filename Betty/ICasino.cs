namespace Betty
{
    public interface ICasino
    {
        string Name { get; }
        int Bet(int amount, ICasinoGame game, CasinoPlayer player);
        void Deposit(int amount, CasinoPlayer player);
        void Withdraw(int amount, CasinoPlayer player);
    }
}
