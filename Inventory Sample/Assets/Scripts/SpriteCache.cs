using System.Collections.Generic;
using UnityEngine;
using System;

public static class SpriteCache
{
    private static readonly Dictionary<string, Sprite> _cache = new Dictionary<string, Sprite>();

    public static Sprite GetSprite(string path)
    {
        if (_cache.TryGetValue(path, out Sprite sprite))
        {
            return sprite;
        }

        sprite = Resources.Load<Sprite>(path);
        if (sprite == null)
        {
            throw new ArgumentException($"SpriteCache: Sprite not found at path: {path}");
        }

        _cache.Add(path, sprite);

        return sprite;
    }


    public static void RemoveSprite(string path)
    {
        if (_cache.ContainsKey(path))
        {
            _cache.Remove(path);
        }
    }

    public static void ClearCache()
    {
        _cache.Clear();
    }
}