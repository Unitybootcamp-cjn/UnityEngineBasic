using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. Ÿ���� �����˴ϴ�.
//2. �÷��̾ �̵��մϴ�.
//3. ������ �ִ� (�̹� �ǳ�) Ÿ���� �����մϴ�.
//4. ���忡 Ÿ���� ������ �����ϰ� �����˴ϴ�.

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs; // ����� Ÿ��
    public GameObject[] fencePrefabs; // ����� ��ֹ�

    private List<GameObject> tiles; // Ÿ�� ����Ʈ
    private List<GameObject> Fences; // ��ֹ� ����Ʈ
    private Transform player_transform; // �÷��̾� ��ġ

    private float spawnZ = -100.0f; // ����(Z��)
    private float fencespawnZ = 30.0f; // �潺 ����(Z��)

    private float tileLength = 6.0f; // Ÿ���� ����
    private float fenceLength = 20.0f; // �潺�� ����

    private float passZone = 100.0f; // Ÿ�� ���� �Ÿ� 

    private int tile_on_screen = 20; // ȭ�鿡 ��ġ�� Ÿ�� ����
    private int fence_on_screen = 5;

    private Queue<GameObject> fenceQueue = new Queue<GameObject>();
    private int lastSpawnCount;


    void Start()
    {
        tiles = new List<GameObject>();
        Fences = new List<GameObject>();
        // Ÿ�� ����Ʈ ����
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        // �÷��̾� ������ �±� �˻��ؼ� Ʈ������ ����

        for (int i = 0; i < tile_on_screen; i++)
        {
            StartCoroutine(CSpawn());
        }
        for (int i = 0; i < fence_on_screen; i++)
        {
            StartCoroutine(FenceSpawn());
        }
    }


    void Update()
    {
        //�÷��̾ ���� �Ÿ� �̻� �̵��ϰ� �Ǹ� Ÿ���� �����ϰ�, �������� Ÿ���� �����մϴ�.
        if (player_transform.position.z - passZone > (spawnZ - tile_on_screen * tileLength))
        {
            StartCoroutine(CSpawn());
            Release();
        }
        if (player_transform.position.z - passZone > (fencespawnZ - tile_on_screen * tileLength))
        {
            StartCoroutine(FenceSpawn());
            FenceRelease();
        }
    }

    IEnumerator CSpawn()
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

        yield return null;
    }

    IEnumerator FenceSpawn()
    {
        // 1) �� ���� ������ ���� (1~5��)
        int spawnCount = Random.Range(1, 6); // 1 �̻� 6 �̸� �� 1~5
        lastSpawnCount = spawnCount;
        // 2) ����� �ε���(0���� 4����)�� �Ź� ���� ����
        List<int> available = new List<int> { 0, 1, 2, 3, 4 };

        // 3) spawnCount ��ŭ �������� �̾Ƽ� ����
        for (int i = 0; i < spawnCount && available.Count > 0; i++)
        {
            // available �� ���� ����
            int randListIdx = Random.Range(0, available.Count);
            int idx = available[randListIdx];

            // ��ġ ���:  
            // idx = 0 �� x = -3  
            // idx = 1 �� x = -1  
            // ��  
            // idx = 4 �� x = 5  
            float x = -3 + idx * 2f;
            Vector3 pos = new Vector3(x, 0, fencespawnZ);

            // �ν��Ͻ� ����
            var go = Instantiate(fencePrefabs[0], pos, Quaternion.identity, transform);
            Fences.Add(go);
            fenceQueue.Enqueue(go);
            // ���� idx ���� ����
            available.RemoveAt(randListIdx);
        }

        // Z���� ������ �̵�
        fencespawnZ += fenceLength;

        yield return null;
    }

    private void Release()
    {
        // ���� �տ� �ִ� Ÿ���� �����մϴ�.
        Destroy(tiles[0]);
        //Ÿ�� ����Ʈ�� �� ���� ���� �����մϴ�.
        tiles.RemoveAt(0);
    }

    private void FenceRelease()
    {
        for (int i = 0; i < lastSpawnCount && fenceQueue.Count > 0; i++)
        {
            var go = fenceQueue.Dequeue();
            Destroy(go);
        }
    }
}
