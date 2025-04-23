using DP.Contexts;
using DP.Models;
using TMPro;
using UnityEngine;

namespace DP.Views
{
    public class CurrencyView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] TextMeshProUGUI _gold;
        [SerializeField] TextMeshProUGUI _gem;

        public void RenderGold(Gold gold)
        {
            _gold.text = gold.Value.ToString();
        }

        public void RenderGem(Gem gem)
        {
            _gem.text = gem.Value.ToString();
        }
    }
}