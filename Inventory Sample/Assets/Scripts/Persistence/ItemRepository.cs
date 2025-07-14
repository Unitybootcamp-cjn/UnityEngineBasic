using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;

public interface IItemRepository
{
    Item FindById(int id);
    IReadOnlyList<Item> FindAll();
}

public class ItemRepository : IItemRepository
{
    private List<Item> _items;

    public ItemRepository(IReadonlyStorage<ItemModelList> parser, string path)
    {
        if (parser == null)
        {
            throw new System.ArgumentNullException(nameof(parser), "Parser cannot be null");
        }

        ItemModelList modelList = parser.LoadFrom(path);
        _items = modelList.data.Select(model => ToItem(model)).ToList();
    }

    public IReadOnlyList<Item> FindAll()
    {
        return _items;
    }

    public Item FindById(int id)
    {
        return _items.Find(item => item.Id == id);
    }
    
    private Item ToItem(ItemModel model)
    {
        return new Item(model.item_id, model.item_name, model.attack_power, model.defense);
    }
}