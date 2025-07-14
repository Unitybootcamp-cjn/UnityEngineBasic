using System.Collections.Generic;
using System.Linq;
using System;

public interface IUserInventoryService
{
    IReadOnlyDictionary<EquipSlotType, UserInventoryItemModel> EquippedItems { get; }
    IReadOnlyList<UserInventoryItemModel> UnequippedItems { get; }

    void EquipRandom();
    void AcquireRandomItem();
    void AcquireItem(int itemId);
    void Equip(long serialNumber);
    void Unequip(long serialNumber);
}

public class UserInventoryService : IUserInventoryService
{
    private readonly IUserInventoryRepository _userInventoryRepository;
    private readonly IItemService _itemService;
    private readonly Inventory _inventory;
    private readonly Equipment _equipment;
    private readonly Random _random;

    public UserInventoryService(IUserInventoryRepository userInventoryRepository, IItemService itemService)
    {
        _userInventoryRepository = userInventoryRepository ?? throw new System.ArgumentNullException(nameof(userInventoryRepository));
        _itemService = itemService ?? throw new System.ArgumentNullException(nameof(itemService));

        _inventory = _userInventoryRepository.Load();
        _equipment = new Equipment();
        _random = new Random();
    }

    public IReadOnlyList<UserInventoryItemModel> UnequippedItems
    {
        get
        {
            return _inventory.Items
                .Where(item => _equipment.IsEquipped(item) == false)
                .Select(item => new UserInventoryItemModel(item))
                .ToList();
        }
    }

    public IReadOnlyDictionary<EquipSlotType, UserInventoryItemModel> EquippedItems
    {
        get
        {
            return _equipment.EquippedItems
                .ToDictionary(kvp => kvp.Key, kvp => new UserInventoryItemModel(kvp.Value));
        }
    }

    public void AcquireItem(int itemId)
    {
        long serialNumber = long.Parse($"{DateTime.Now.ToString("yyyymmdd")}{_random.Next(10000):D4}");
        UserInventoryItem newItem = new UserInventoryItem(serialNumber, itemId);

        _inventory.AddItem(newItem);

        _userInventoryRepository.Save(_inventory);
    }

    public void AcquireRandomItem()
    {
        int randomItemId = _itemService.GetRandomItemId();
        AcquireItem(randomItemId);
    }

    public void Equip(long serialNumber)
    {
        var item = _inventory.GetItem(serialNumber);
        var itemType = _itemService.GetType(item.ItemId);
        var equipSlotType = itemType switch
        {
            ItemType.Weapon => EquipSlotType.Weapon,
            ItemType.Shield => EquipSlotType.Shield,
            ItemType.ChestArmor => EquipSlotType.ChestArmor,
            ItemType.Gloves => EquipSlotType.Gloves,
            ItemType.Boots => EquipSlotType.Boots,
            ItemType.Accessary => EquipSlotType.Accessary,
            _ => throw new ArgumentException(nameof(itemType)),
        };

        _equipment.Equip(equipSlotType, item);
    }

    public void EquipRandom()
    {
        var randomItems = UnequippedItems.OrderBy(_ => _random.Next()).Take(5).ToList();
        foreach (var item in randomItems)
        {
            Equip(item.SerialNumber);
        }
    }

    public void Unequip(long serialNumber)
    {
        var item = _inventory.GetItem(serialNumber);
        var itemType = _itemService.GetType(item.ItemId);
        var equipSlotType = itemType switch
        {
            ItemType.Weapon => EquipSlotType.Weapon,
            ItemType.Shield => EquipSlotType.Shield,
            ItemType.ChestArmor => EquipSlotType.ChestArmor,
            ItemType.Gloves => EquipSlotType.Gloves,
            ItemType.Boots => EquipSlotType.Boots,
            ItemType.Accessary => EquipSlotType.Accessary,
            _ => throw new ArgumentException(nameof(itemType)),
        };
        _equipment.Unequip(equipSlotType);
    }
}
