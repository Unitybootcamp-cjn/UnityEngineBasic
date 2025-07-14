using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UserInventoryServiceLocatorSO _userInventoryServiceLocator;
    [SerializeField] private InventoryUI _inventoryUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _userInventoryServiceLocator.Service.AcquireRandomItem();
            _inventoryUI.Refresh();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            _userInventoryServiceLocator.Service.EquipRandom();
            _inventoryUI.Refresh();
        }
    }
}
