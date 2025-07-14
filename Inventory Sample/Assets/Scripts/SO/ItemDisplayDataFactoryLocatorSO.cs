using UnityEngine;

[CreateAssetMenu(fileName = "ItemDisplayDataFactoryLocatorSO", menuName = "Scriptable Objects/ItemDisplayDataFactoryLocatorSO")]
public class ItemDisplayDataFactoryLocatorSO : ScriptableObject
{
    public ItemDisplayDataFactory Factory { get; private set; }

    public void Bootstrap(IItemService itemService)
    {
        Factory = new ItemDisplayDataFactory(itemService);
    }
}
