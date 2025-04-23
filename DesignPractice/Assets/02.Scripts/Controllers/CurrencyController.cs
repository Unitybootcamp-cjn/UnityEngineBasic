using DP.Contexts;
using DP.Utilities.DI;
using DP.Views;
using UnityEngine;
using UnityEngine.UI;

namespace DP.Controllers
{
    public class CurrencyController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] Button _increaseGold;
        [SerializeField] Button _decreaseGold;
        [SerializeField] Button _increaseGem;
        [SerializeField] Button _decreaseGem;

        [Header("View")]
        [SerializeField] CurrencyView _view;


        [Inject] CurrencyContext _context;


        private void OnEnable()
        {
            _view.RenderGold(_context.Gold);
            _view.RenderGem(_context.Gem);
            _context.OnGoldChanged += _view.RenderGold;
            _context.OnGemChanged += _view.RenderGem;

            _increaseGold.onClick.AddListener(Increase100Gold);
            _decreaseGold.onClick.AddListener(Decrease100Gold);
            _increaseGem.onClick.AddListener(Increase100Gem);
            _decreaseGem.onClick.AddListener(Decrease100Gem);
        }

        private void OnDisable()
        {
            _context.OnGoldChanged -= _view.RenderGold;
            _context.OnGemChanged -= _view.RenderGem;

            _increaseGold.onClick.RemoveListener(Increase100Gold);
            _decreaseGold.onClick.RemoveListener(Decrease100Gold);
            _increaseGem.onClick.RemoveListener(Increase100Gem);
            _decreaseGem.onClick.RemoveListener(Decrease100Gem);
        }

        public void Increase100Gold()
            => _context.IncreaseGold(100);

        public void Decrease100Gold()
            => _context.DecreaseGold(100);

        public void Increase100Gem()
            => _context.IncreaseGem(100);

        public void Decrease100Gem()
            => _context.DecreaseGem(100);

        public void IncreaseGold(int amount)
        {
            _context.IncreaseGold(amount);
        }
    }
}