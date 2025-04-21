using DP.Contexts;
using DP.Repositories;
using UnityEngine;

namespace DP.Controllers
{
    public class ItemController : MonoBehaviour
    {
        [SerializeField] int _itemId;
        [SerializeField] int _itemNum;
        InventoryRepository _inventoryRepository;
        Camera _camera;

        private void Start()
        {
            _camera = Camera.main;

            _inventoryRepository = InventoryRepository.Singleton;
        }

        public void PickUp()
        {
            _inventoryRepository.AddItem(_itemId, _itemNum);
            _inventoryRepository.Save();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        PickUp();
                    }
                }
            }
        }
    }
}