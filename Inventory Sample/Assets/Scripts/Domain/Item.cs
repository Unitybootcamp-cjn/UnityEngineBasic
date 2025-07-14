using System;
using System.Text;
using UnityEngine;

public sealed class Item : Entity<int>
{
    public string Name { get; }

    private readonly int _atk;
    private readonly int _def;

    public Item(int id, string name, int atk, int def)
    {
        Id = id;
        Name = name;
        _atk = atk;
        _def = def;
    }
}
