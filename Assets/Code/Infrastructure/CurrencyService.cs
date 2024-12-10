namespace Code.Infrastructure
{
    public class CurrencyService : ICurrency, IBet
    {
        public decimal Currency { get; private set; }

        public decimal Bet { get; private set; }

        public CurrencyService(Config config)
        {
            Currency = config.StartCurrency;
        }

        public void AddCurrency(decimal currency)
        {
            Currency += currency;
        }

        public void TakeAwayCurrency(decimal currency)
        {
            Currency -= currency;
        }

        public void SetBet(decimal bet)
        {
            Bet = bet;
        }
    }
}
