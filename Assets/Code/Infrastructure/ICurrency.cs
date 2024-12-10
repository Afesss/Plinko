namespace Code.Infrastructure
{
    public interface ICurrency
    {
        public decimal Currency { get; }

        public void AddCurrency(decimal currency);
        public void TakeAwayCurrency(decimal currency);
    }
}