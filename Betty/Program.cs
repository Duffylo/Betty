// See https://aka.ms/new-console-template for more information
using Betty;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main()
    {
        //setup
        using (var provider = new ServiceCollection()
    .AddScoped<IBalanceManager, BalanceManager>()
    .AddScoped<ICasino, BettyCasino>()
    .AddScoped<ICasinoGame, SomeBasicCasinoGame>()
    .BuildServiceProvider())
        {
            var game = provider.GetRequiredService<ICasinoGame>();
            var casino = provider.GetRequiredService<ICasino>();

            Console.WriteLine("Wellcome to Betty! \n");
            Help();

            var player = new CasinoPlayer(Guid.NewGuid(), "Martin", 0);

            Splash(game, casino, player);

            var (action, amount) = ReadAction();

            while (action != "q")
            {
                switch (action)
                {
                    case "b":
                        HandleBetAction(player, game, casino, amount);
                        break;
                    case "d":
                        HandleDepositAction(player, casino, amount);
                        break;
                    case "w":
                        HandleWithdrawalAction(player, casino, amount);
                        break;
                    case "h":
                        Help();
                        break;
                    default:
                        Console.WriteLine($"Invalid action: {action}, please retry. \n");
                        break;
                }

                Splash(game, casino, player);
                (action, amount) = ReadAction();
            }
        }

        Console.WriteLine("The next bet was a jackpot but you just missed it. \nBye!");
        Console.ReadLine();
    }

    static void Splash(ICasinoGame game, ICasino casino, CasinoPlayer player)
    {
        var InfoText = $"\nPlaying game {game.GameId} at casino <<{casino.Name}>> as {player.Name} with current balance [{player.Balance}]";
        Console.WriteLine(InfoText);
    }

    static void Help()
    {
        const string HelpText = "Need help? Use this: \n b - Bet  \n d - Deposit \n w - Withdraw \n q - Quit \n h - Help \n eg: b 10";
        Console.WriteLine(HelpText);
    }

    static (string action, string amount) ReadAction()
    {
        var userInput = Console.ReadLine();
        var args = userInput?.ToLower().Split(' ').Take(2);
        if (args == null) return ("", "");
        return (args.First(), args.Last());
    }

    static void HandleBetAction(CasinoPlayer player, ICasinoGame game, ICasino casino, string? amount)
    {
        if (int.TryParse(amount, out var betAmount) && betAmount >= game.MinBet && betAmount <= game.MaxBet)
            try
            {
                var betResult = casino.Bet(betAmount, game, player);
                if (betResult > 0)
                    Console.WriteLine($"^_^ {betResult}");
                else
                    Console.WriteLine(" -_- ");
            }
            catch (PlacingBetException pbe)
            {
                Console.WriteLine($"Cannot place bet. {pbe.Message} \n"); ;
            }
        else
            Console.WriteLine($"Invalid bet amount: {amount}. Input whole number between {game.MinBet} and {game.MaxBet} \n");
    }

    static void HandleDepositAction(CasinoPlayer player, ICasino casino, string? amount)
    {
        if (int.TryParse(amount, out var depAmount) && depAmount > 0)
        {
            casino.Deposit(depAmount, player);
            Console.WriteLine($"Deposited successfully {depAmount}");
        }
        else
            Console.WriteLine($"Invalid deposit amount: {amount}, please retry. \n");
    }

    static void HandleWithdrawalAction(CasinoPlayer player, ICasino casino, string? amount)
    {
        if (int.TryParse(amount, out var wAmount) && wAmount > 0)
            try
            {
                casino.Withdraw(wAmount, player);
            }
            catch (WithdrawalException we)
            {
                Console.WriteLine($"Impossible operation. {we.Message} \n"); ;
            }
        else
            Console.WriteLine($"Invalid withdrawal amount: {amount}, please retry. \n");
    }
}