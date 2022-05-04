using Betty;

public class BalanceManager : IBalanceManager
{
    public BalanceManager()
    {
    }

    public void Deposit(int amount, CasinoPlayer player)
    {
        if (amount > 0)
        {
            // transaction
            player.Balance += amount;
        }
        else
            throw new DepositException("Amount is negative or 0");
    }

    public Guid OpenBet(int amount, CasinoPlayer player, ICasinoGame game)
    {
        if(player.Balance - amount >= 0)
        {
            // transaction
            player.Balance -= amount;
            var betId = Guid.NewGuid();
            //logger.LogInformation($"Registering bet opening game: {game.GameId} player: {player.Id} amount: {amount}");
            return betId;
        }
        else
        {
            throw new PlacingBetException($"Insufficient balance cannot bet {amount}");
        }
    }

    public bool CloseBet(Guid betId, int betResult, CasinoPlayer player)
    {
        //transaction closing bet and increase balance
        player.Balance += betResult;
        return true;
    }

    public void Withdraw(int amount, CasinoPlayer player)
    {
        if (amount > 0)
        {
            if(player.Balance >= amount)
                // transaction
                player.Balance -= amount;
            else
                throw new WithdrawalException("Not enought balance to excecute withdrawal.");
        }
        else
            throw new WithdrawalException("Amount is negative or 0.");
    }
}
