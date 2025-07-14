using UnityEngine;


[CreateAssetMenu(fileName = "ItemRepositorySO", menuName = "Scriptable Objects/ItemRepositorySO")]
public class ItemRepositoryLocatorSO : ScriptableObject
{
    public IItemRepository Repository { get; private set; }

    public void Bootstrap(IReadonlyStorage<ItemModelList> parser, string path)
    {
        Repository = new ItemRepository(parser, path);
    }
}
