namespace Betty
{
    public class SomeBasicCasinoGame : ICasinoGame
    {
        public int GameId => 1;
        public int MinBet => 1;
        public int MaxBet => 10;

        public int Bet(int amount)
        {
            var rand = new Random();
            var next = rand.Next(10);
            return next switch
            {
                < 5 => 0,
                >= 5 and < 9 => amount * rand.Next(1, 3),
                _ => amount * rand.Next(2, 11),
            };
        }
    }
}