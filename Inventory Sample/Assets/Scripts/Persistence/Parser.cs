using UnityEngine;

public interface IReadonlyStorage<T> where T : class
{
    T? LoadFrom(string origin);
}

public interface IStorage<T> : IReadonlyStorage<T> where T : class
{
    void SaveTo(string path, T data);
}

public class ResourcesJsonParser<T> : IReadonlyStorage<T> where T : class
{
    public T? LoadFrom(string path)
    {
        TextAsset json = Resources.Load<TextAsset>(path);
        if (json == null)
        {
            return null;
        }
        
        return JsonUtility.FromJson<T>(json.text);
    }
}