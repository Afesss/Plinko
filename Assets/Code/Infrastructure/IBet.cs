namespace Code.Infrastructure
{
    public interface IBet
    {
        public decimal Bet { get; }
        public void SetBet(decimal bet);
    }
}