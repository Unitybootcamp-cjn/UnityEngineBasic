using UnityEngine;

public class ItemDisplayDataFactory
{
    private IItemService _itemService;

    public ItemDisplayDataFactory(IItemService itemService)
    {
        _itemService = itemService;
    }

    public ItemDisplayData Create(int itemId)
    {
        string name = _itemService.GetName(itemId);
        ItemGrade grade = _itemService.GetGrade(itemId);
        ItemType type = _itemService.GetType(itemId);
        Sprite gradeSprite = SpriteCache.GetSprite(_itemService.GetGradePath(itemId));
        Sprite iconSprite = SpriteCache.GetSprite(_itemService.GetIconPath(itemId));

        return new ItemDisplayData(name, grade, type, gradeSprite, iconSprite);
    }
}