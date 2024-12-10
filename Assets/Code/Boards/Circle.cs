using Code.Infrastructure;
using UnityEngine;

namespace Code.Boards
{
    public class Circle : MonoBehaviour
    {
        private Vector3 _originPos;
        private decimal _bet;
        private ICurrency _currency;
        public void Init(IBet bet, ICurrency currency)
        {
            _originPos = transform.position;
            _bet = bet.Bet;
            currency.TakeAwayCurrency(_bet);
            _currency = currency;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            BetField betField = other.GetComponent<BetField>();
            if(betField != null)
                _currency.AddCurrency(_bet * (decimal)betField.BetCoeff);
            Destroy(gameObject);
        }
    }
}
