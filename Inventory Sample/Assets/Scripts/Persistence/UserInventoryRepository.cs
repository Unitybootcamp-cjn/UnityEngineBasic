using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;

public interface IUserInventoryRepository
{
    Inventory Load();
    void Save(Inventory inventory);
}

public sealed class UserInventoryRepository : IUserInventoryRepository
{
    private readonly string _filePath;

    public UserInventoryRepository(string filePath)
    {
        _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
    }

    public Inventory Load()
    {
        if (false == File.Exists(_filePath))
        {
            return new Inventory();
        }

        string jsonFile = File.ReadAllText(_filePath);
        UserInventoryModel model = JsonUtility.FromJson<UserInventoryModel>(jsonFile);

        List<UserInventoryItem> items = model.items
            .Select(itemModel => new UserInventoryItem(itemModel.serial_number, itemModel.item_id))
            .ToList();

        return new Inventory(items);
    }

    public void Save(Inventory inventory)
    {
        if (inventory == null)
        {
            throw new ArgumentNullException(nameof(inventory));
        }

        UserInventoryModel model = new UserInventoryModel
        {
            items = inventory.Items.Select(item => new UserInventoryItemData
            {
                serial_number = item.SerialNumber,
                item_id = item.ItemId
            }).ToArray()
        };
        
        string json = JsonUtility.ToJson(model, true);
        File.WriteAllText(_filePath, json);
    }
}