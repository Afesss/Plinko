using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Code.Boards
{
    public class BetField : MonoBehaviour
    {
        [SerializeField] private TMP_Text _betTmp;
        public float BetCoeff { get; private set; }

        private Vector3 _originScale;
        public void Init(float betCoeff)
        {
            _betTmp.text = betCoeff.ToString();
            BetCoeff = betCoeff;
            _originScale = transform.localScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            transform.DOScale(_originScale * 0.7f, 0.3f).OnComplete(() =>
            {
                transform.DOScale(_originScale, 0.3f);
            });
        }
    }
}
