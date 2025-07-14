using System.Collections.Generic;
using System.Linq;
using System;

public class Inventory
{
    private SortedDictionary<long, UserInventoryItem> _items;

    public Inventory()
    {
        _items = new SortedDictionary<long, UserInventoryItem>();
    }

    public Inventory(List<UserInventoryItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        _items = new SortedDictionary<long, UserInventoryItem>();
        foreach (var item in items)
        {
            _items.Add(item.SerialNumber, item);
        }
    }

    public IReadOnlyList<UserInventoryItem> Items => _items.Values.ToList();

    public void AddItem(UserInventoryItem item)
    {
        if (_items.ContainsKey(item.SerialNumber))
        {
            throw new ArgumentException($"Item with serial number {item.SerialNumber} already exists in the inventory.");
        }

        _items.Add(item.SerialNumber, item);
    }

    public void RemoveItem(UserInventoryItem item) => _items.Remove(item.SerialNumber);

    public UserInventoryItem GetItem(long serialNumber)
    {
        if (_items.TryGetValue(serialNumber, out UserInventoryItem item))
        {
            return item;
        }

        throw new ArgumentException($"Item with serial number {serialNumber} does not exist in the inventory.");
    }
}
