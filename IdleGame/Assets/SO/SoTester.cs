using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] items;

    void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"������ �̸� : {item.name} {item.description} ���� : {item.value}");
        }
    }
}
