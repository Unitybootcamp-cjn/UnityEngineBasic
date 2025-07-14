using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Equipment
{
    private Dictionary<EquipSlotType, EquipSlot> _equipSlots;

    public Equipment()
    {
        _equipSlots = new Dictionary<EquipSlotType, EquipSlot>();
        _equipSlots[EquipSlotType.Weapon] = new EquipSlot(EquipSlotType.Weapon);
        _equipSlots[EquipSlotType.Shield] = new EquipSlot(EquipSlotType.Shield);
        _equipSlots[EquipSlotType.ChestArmor] = new EquipSlot(EquipSlotType.ChestArmor);
        _equipSlots[EquipSlotType.Gloves] = new EquipSlot(EquipSlotType.Gloves);
        _equipSlots[EquipSlotType.Boots] = new EquipSlot(EquipSlotType.Boots);
        _equipSlots[EquipSlotType.Accessary] = new EquipSlot(EquipSlotType.Accessary);
    }

    public IReadOnlyDictionary<EquipSlotType, UserInventoryItem> EquippedItems
    {
        get
        {
            return _equipSlots
                .Where(slot => slot.Value.IsEquipped)
                .ToDictionary(slot => slot.Key, slot => slot.Value.EquippedItem);
        }
    }


    public void Equip(EquipSlotType type, UserInventoryItem item)
    {
        _equipSlots[type].Equip(item);
    }

    public void Unequip(EquipSlotType type)
    {
        _equipSlots[type].Unequip();
    }

    public UserInventoryItem? GetEquippedItem(EquipSlotType type) => _equipSlots[type].EquippedItem;

    public bool IsEquipped(EquipSlotType type)
    {
        return _equipSlots[type].IsEquipped;
    }

    public bool IsEquipped(UserInventoryItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item), "장비 아이템이 null입니다.");
        }

        return _equipSlots.Values.Any(slot => item.Equals(slot.EquippedItem));
    }
}