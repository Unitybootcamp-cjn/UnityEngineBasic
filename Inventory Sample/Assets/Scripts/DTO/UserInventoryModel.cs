
using JetBrains.Annotations;
using System;

[Serializable]
public class UserInventoryItemData
{
    public long serial_number;
    public int item_id;
}

[Serializable]
public class UserInventoryModel
{
    public UserInventoryItemData[] items;
}