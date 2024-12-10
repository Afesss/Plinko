using System;
using Code.Infrastructure;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyTMP;
        [SerializeField] private TMP_InputField _betInputField;
        [SerializeField] private TMP_InputField _topUpInputField;
        [SerializeField] private GameObject _topUpPopUp;
        private ICurrency _currency;
        private IBet _bet;
        
        [Inject]
        public void Construct(ICurrency currency, IBet bet, Config config)
        {
            _currency = currency;
            _bet = bet;
            _currencyTMP.text = $"{currency.Currency}$";
            _bet.SetBet(config.StartBet);
            _betInputField.text = config.StartBet.ToString();
            _betInputField.onValueChanged.AddListener(BetChanged);
        }

        public void Plus()
        {
            decimal.TryParse(_betInputField.text, out decimal bet);
            bet++;
            if (bet > _currency.Currency)
                bet = _currency.Currency;
            _betInputField.text = bet.ToString();
            _bet.SetBet(bet);
        }

        public void Minus()
        {
            decimal.TryParse(_betInputField.text, out decimal bet);
            bet--;
            if (bet < 0)
                bet = 0;
            _betInputField.text = bet.ToString();
            _bet.SetBet(bet);
        }

        public void TopUp()
        {
            decimal.TryParse(_topUpInputField.text, out decimal currency);
            if(currency <= 0)
                return;
            _currency.AddCurrency(currency);
        }

        private void BetChanged(string value)
        {
            decimal.TryParse(value, out decimal bet);
            if (bet < 0)
                bet = 0;
            if (bet > _currency.Currency)
                bet = _currency.Currency;
            _bet.SetBet(bet);
        }

        private void Update()
        {
            _currencyTMP.text = $"{_currency.Currency}$";
            _topUpPopUp.SetActive(_currency.Currency <= 0);
        }
    }
}
