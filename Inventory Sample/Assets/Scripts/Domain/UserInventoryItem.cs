using System;
using Unity.VisualScripting;

public sealed class UserInventoryItem : Entity<long>
{
    public long SerialNumber { get; }
    public int ItemId { get; }

    public UserInventoryItem(long serialNumber, int itemId)
    {
        SerialNumber = serialNumber;
        ItemId = itemId;
    }
}
