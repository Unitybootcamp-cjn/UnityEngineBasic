using System.Security.Cryptography;

public class UserInventoryItemModel
{
    public long SerialNumber { get; }
    public int ItemId { get; }

    public UserInventoryItemModel(UserInventoryItem item)
    {
        SerialNumber = item.SerialNumber;
        ItemId = item.ItemId;
    }
}