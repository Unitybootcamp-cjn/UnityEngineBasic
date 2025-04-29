using UnityEngine;

public class SoTester : MonoBehaviour
{
    public Item[] items;

    void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"아이템 이름 : {item.name} {item.description} 가격 : {item.value}");
        }
    }
}
