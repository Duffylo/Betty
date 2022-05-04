namespace Betty
{
    public interface ICasinoGame
    {
        int MinBet { get; }
        int MaxBet { get; }
        int GameId { get; }
        int Bet(int amount);
    }
}