using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public int id;
    public string name;
    [TextArea] public string description;
    public int value;
    public int count;
    public int price;

    public ItemData(int id, string name, string description, int value, int count, int price)
    {
        this.id = id;
        this.name = name;
        this.description = description;
        this.value = value;
        this.count = count;
        this.price = price;
    }
}
