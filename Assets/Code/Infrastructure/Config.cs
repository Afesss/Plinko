using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Infrastructure
{
    [CreateAssetMenu(fileName = nameof(Config), menuName = "Configs/" + nameof(Config))]
    public class Config : ScriptableObject
    {
        [Header("Currency")] 
        public decimal StartCurrency = 3000;
        public decimal StartBet = 2;
        [Header("Green")]
        public float[] Green12Bets;
        public float[] Green14Bets;
        public float[] Green16Bets;
        [Header("Orange")]
        public float[] Orange12Bets;
        public float[] Orange14Bets;
        public float[] Orange16Bets;
        [Header("Red")] 
        public float[] Red12Bets;
        public float[] Red14Bets;
        public float[] Red16Bets;
    }
}