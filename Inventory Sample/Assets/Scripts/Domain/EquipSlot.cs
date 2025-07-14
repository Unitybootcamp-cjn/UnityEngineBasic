using System;

public enum EquipSlotType
{
    Weapon,
    Shield,
    ChestArmor,
    Gloves,
    Boots,
    Accessary
}

public sealed class EquipSlot
{
    private EquipSlotType _type;
    private UserInventoryItem? _item;

    public EquipSlot(EquipSlotType type)
    {
        _type = type;
    }

    public UserInventoryItem? EquippedItem => _item;
   
    public bool IsEquipped => _item != null;

    public void Equip(UserInventoryItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), $"{_type} : 장착할 아이템이 null입니다.");
        }

        if (IsEquipped)
        {
            Unequip();
        }

        _item = item;
    }

    public void Unequip()
    {
        if (IsEquipped == false)
        {
            throw new InvalidOperationException($"{_type} : 장착된 장비가 없습니다.");
        }

        _item = null;
    }
}