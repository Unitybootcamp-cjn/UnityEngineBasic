using System;

[Serializable]
public struct ItemModel
{
    public int item_id;
    public string item_name;
    public int attack_power;
    public int defense;
}

[Serializable]
public class ItemModelList
{
    public ItemModel[] data;
}