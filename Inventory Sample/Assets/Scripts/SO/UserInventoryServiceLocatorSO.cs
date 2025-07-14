using UnityEngine;

[CreateAssetMenu(fileName = "UserInventoryServiceLocatorSO", menuName = "Scriptable Objects/UserInventoryServiceLocatorSO")]
public class UserInventoryServiceLocatorSO : ScriptableObject
{
   public IUserInventoryService Service { get; private set; }

   public void Bootstrap(IUserInventoryRepository userInventoryRepository, IItemService itemService)
    {
        Service = new UserInventoryService(userInventoryRepository, itemService);
    }
}
