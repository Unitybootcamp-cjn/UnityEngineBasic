using System.Collections.Generic;
using UnityEngine;
//1. Ÿ���� �����˴ϴ�.
//2. �÷��̾ �̵��մϴ�.
//3. ������ �ִ� (�̹� �ǳ�) Ÿ���� �����մϴ�.
//4. ���忡 Ÿ���� ������ �����ϰ� �����˴ϴ�.

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs; // ����� Ÿ��
    private List<GameObject> tiles; // Ÿ�� ����Ʈ

    private Transform player_transform; // �÷��̾� ��ġ
    private float spawnZ = 0.0f; // ����(Z��)
    private float tileLength = 6.0f; // Ÿ���� ����
    private float passZone = 15.0f; // Ÿ�� ���� �Ÿ� 
    private int tile_on_screen = 10; // ȭ�鿡 ��ġ�� Ÿ�� ����

    void Start()
    {
        tiles = new List<GameObject>();
        // Ÿ�� ����Ʈ ����
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        // �÷��̾� ������ �±� �˻��ؼ� Ʈ������ ����

        for (int i = 0; i < tile_on_screen; i++)
        {
            Spawn();
        }

    }


    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ϰ� �Ǹ� Ÿ���� �����ϰ�, �������� Ÿ���� �����մϴ�.
        if (player_transform.position.z - passZone > (spawnZ - tile_on_screen * tileLength))
        {
            Spawn();
            Release();
        }
    }

    private void Spawn()
    {
        //������ ���� ���� (���Ŀ��� �����̳� ����ȭ�� ����)
        var go = Instantiate(tilePrefabs[0]);
        //������� Ÿ���� Ÿ�� �Ŵ����� �ڽ� ������Ʈ�� �˴ϴ�.
        go.transform.SetParent(transform);
        //������� Ÿ���� ��ġ�� �����մϴ�.
        go.transform.position = Vector3.forward * spawnZ;
        //���� ��ġ�� Ÿ�� ���� �������� ��� ����(ũ�⿡ �°� ����)
        spawnZ += tileLength;
        // Ÿ�� ����Ʈ�� ���
        tiles.Add(go);
    }
    private void Release()
    {
        // ���� �տ� �ִ� Ÿ���� �����մϴ�.
        Destroy(tiles[0]);
        //Ÿ�� ����Ʈ�� �� ���� ���� �����մϴ�.
        tiles.RemoveAt(0);
    }
}
