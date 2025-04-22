using DP.Repositories;
using DP.Contexts;
using UnityEngine;

namespace DP.Controllers
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] int _itemId;
        [SerializeField] int _itemNum;
        InventoryRepository _inventoryRepository;

        private void Start()
        {
            _inventoryRepository = InventoryRepository.Singleton;
        }

        public void PickUp()
        {
            _inventoryRepository.AddItem(_itemId, _itemNum);
            _inventoryRepository.Save();
        }
    }
}