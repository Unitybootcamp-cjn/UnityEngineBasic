using UnityEngine;

[CreateAssetMenu(fileName = "ItemServiceSO", menuName = "Scriptable Objects/ItemServiceSO")]
public class ItemServiceLocatorSO : ScriptableObject
{
    public IItemService Service { get; private set; }

    public void Bootstrap(IItemRepository itemRepository)
    {
        Service = new ItemService(itemRepository);
    }
}
