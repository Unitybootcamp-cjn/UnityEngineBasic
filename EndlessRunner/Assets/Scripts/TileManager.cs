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

    private float spawnZ = -50.0f; // ����(Z��)
    private float fencespawnZ = 30.0f; // �潺 ����(Z��)

    private float tileLength = 6.0f; // Ÿ���� ����
    private float fenceLength = 15.0f; // �潺�� ����

    private float passZone = 60.0f; // Ÿ�� ���� �Ÿ� 

    private int tile_on_screen = 20; // ȭ�鿡 ��ġ�� Ÿ�� ����
    private int fence_on_screen = 5;

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
        for (int i = 0; i < 5; i++)
        {
            var go = Instantiate(fencePrefabs[0]);
            go.transform.SetParent(transform);
            int rand = Random.Range(0, 5);
            switch (rand)
            {
                case 0:
                    go.transform.position = new Vector3(-3, 0, fencespawnZ);
                    break;
                case 1:
                    go.transform.position = new Vector3(-1, 0, fencespawnZ);
                    break;
                case 2:
                    go.transform.position = new Vector3(1, 0, fencespawnZ);
                    break;
                case 3:
                    go.transform.position = new Vector3(3, 0, fencespawnZ);
                    break;
                case 4:
                    go.transform.position = new Vector3(5, 0, fencespawnZ);
                    break;
                default:
                    break;
            }
            Fences.Add(go);
        }
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
        for (int i = 0; i < 5; i++)
        {
            // ���� �տ� �ִ� Ÿ���� �����մϴ�.
            Destroy(Fences[0]);
            //Ÿ�� ����Ʈ�� �� ���� ���� �����մϴ�.
            Fences.RemoveAt(0);
        }
    }
}
