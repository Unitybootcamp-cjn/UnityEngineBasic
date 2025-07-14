using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor.UIElements;


public enum ItemType
{
    Weapon,
    Shield,
    ChestArmor,
    Gloves,
    Boots,
    Accessary
}

public enum ItemGrade
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}

public interface IItemService
{
    int GetRandomItemId();
    string GetGradePath(int itemId);
    string GetIconPath(int itemId);
    ItemType GetType(int itemId);
    ItemGrade GetGrade(int itemId);
    string GetName(int itemId);
}

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly Random _random;

    public ItemService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository), "초기화 순서가 잘못되었습니다.");
        _random = new Random();
    }

    public int GetRandomItemId()
    {
        IReadOnlyList<Item> items = _itemRepository.FindAll();
        int randomIndex = _random.Next(items.Count);
        Item randomItem = items[randomIndex];

        return randomItem.Id;
    }

    private const string k_SpritePath = "Textures";

    public string GetGradePath(int itemId)
    {
        ItemGrade grade = GetGrade(itemId);
        
        return Path.Combine(k_SpritePath, $"{grade}");
    }

    public string GetIconPath(int itemId)
    {
        StringBuilder sb = new StringBuilder(itemId.ToString());
        sb[1] = '1';

        return Path.Combine(k_SpritePath, sb.ToString());
    }

    public ItemType GetType(int itemId)
    {
        int typeDigit = itemId / 10000;

        return typeDigit switch
        {
            1 => ItemType.Weapon,
            2 => ItemType.Shield,
            3 => ItemType.ChestArmor,
            4 => ItemType.Gloves,
            5 => ItemType.Boots,
            6 => ItemType.Accessary,
            _ => throw new ArgumentException($"Invalid item id : {itemId}")
        };
    }

    public ItemGrade GetGrade(int itemId)
    {
        int gradeDigit = itemId / 1000 % 10;

        return gradeDigit switch
        {
            1 => ItemGrade.Common,
            2 => ItemGrade.Uncommon,
            3 => ItemGrade.Rare,
            4 => ItemGrade.Epic,
            5 => ItemGrade.Legendary,
            _ => throw new ArgumentException($"Invalid item id : {itemId}")
        };
    }

    public string GetName(int itemId)
    {
        var item = _itemRepository.FindById(itemId);
        if (item == null)
        {
            throw new ArgumentException($"Item with id {itemId} does not exist.");
        }

        return item.Name;
    }
}