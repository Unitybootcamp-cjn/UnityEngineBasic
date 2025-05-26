using UnityEngine;
using Assets.Scripts.InventorySystem;
using System.Collections;
using Assets.Scripts.Items;

public class Player : MonoBehaviour
{
    public Inventory Inventory;
    private TileManager tileManager;

    private void Awake()
    {
        //기본 인벤토리 4개 제공
        Inventory = new Inventory(4);
        tileManager = GameManager.instance.TileManager;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if(tileManager != null)
            {
                var position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);

                if (GameManager.instance.TileManager.isInteractable(position))
                {
                    Debug.Log("check");
                    GameManager.instance.TileManager.SetInteract(position);
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //채집 기능
        }
    }

    public void Drop(Item item)
    {
        //위치 설정
        var spawn = transform.position;

        //던지는 범위
        float y = 1.0f;
        Vector3 offset = new Vector3(0, - y, 0);

        //오브젝트 생성
        var go = Instantiate(item, spawn + offset, Quaternion.identity);
        StartCoroutine(colliderOff(go,offset));
        //오브젝트에 대한 물리적인 힘 작용
    }

    IEnumerator colliderOff(Item go, Vector3 offset)
    {
        go.gameObject.GetComponent<Collider2D>().enabled = false;
        go.rbody.AddForce(offset * 2f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        go.gameObject.GetComponent<Collider2D>().enabled = true;
        go.rbody.AddForce(- offset * 2f, ForceMode2D.Impulse);


    }
}