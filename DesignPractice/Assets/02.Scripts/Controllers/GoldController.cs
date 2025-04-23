using DP.Contexts;
using DP.Utilities.DI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DP.Controllers
{
    public class GoldController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] int _increaseAmount = 1234;
        [Inject] CurrencyContext _context;

        public void OnPointerClick(PointerEventData eventData)
        {
            _context.IncreaseGold(_increaseAmount);
            gameObject.SetActive(false);
        }
    }
}
