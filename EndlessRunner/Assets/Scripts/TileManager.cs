using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1. 타일이 생성됩니다.
//2. 플레이어가 이동합니다.
//3. 기존에 있던 (이미 건넌) 타일을 제거합니다.
//4. 월드에 타일의 개수가 균일하게 유지됩니다.

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs; // 등록할 타일
    public GameObject[] fencePrefabs; // 등록할 장애물

    private List<GameObject> tiles; // 타일 리스트
    private List<GameObject> Fences; // 장애물 리스트
    private Transform player_transform; // 플레이어 위치

    private float spawnZ = -100.0f; // 스폰(Z축)
    private float fencespawnZ = 30.0f; // 펜스 스폰(Z축)

    private float tileLength = 6.0f; // 타일의 길이
    private float fenceLength = 20.0f; // 펜스의 길이

    private float passZone = 100.0f; // 타일 유지 거리 

    private int tile_on_screen = 20; // 화면에 배치할 타일 개수
    private int fence_on_screen = 5;

    private Queue<GameObject> fenceQueue = new Queue<GameObject>();
    private int lastSpawnCount;


    void Start()
    {
        tiles = new List<GameObject>();
        Fences = new List<GameObject>();
        // 타일 리스트 생성
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        // 플레이어 씬에서 태그 검색해서 트랜스폼 적용

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
        //플레이어가 일정 거리 이상 이동하게 되면 타일을 생성하고, 지나갔던 타일을 제거합니다.
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
        //고정된 값을 생성 (이후에는 랜덤이나 패턴화로 변경)
        var go = Instantiate(tilePrefabs[0]);
        //만들어진 타일은 타일 매니저의 자식 오브젝트가 됩니다.
        go.transform.SetParent(transform);
        //만들어진 타일의 위치를 설정합니다.
        go.transform.position = Vector3.forward * spawnZ;
        //생성 위치가 타일 길이 기준으로 계속 증가(크기에 맞게 생성)
        spawnZ += tileLength;
        // 타일 리스트에 등록
        tiles.Add(go);

        yield return null;
    }

    IEnumerator FenceSpawn()
    {
        // 1) 몇 개를 뽑을지 결정 (1~5개)
        int spawnCount = Random.Range(1, 6); // 1 이상 6 미만 → 1~5
        lastSpawnCount = spawnCount;
        // 2) 사용할 인덱스(0부터 4까지)를 매번 새로 생성
        List<int> available = new List<int> { 0, 1, 2, 3, 4 };

        // 3) spawnCount 만큼 랜덤으로 뽑아서 스폰
        for (int i = 0; i < spawnCount && available.Count > 0; i++)
        {
            // available 중 랜덤 선택
            int randListIdx = Random.Range(0, available.Count);
            int idx = available[randListIdx];

            // 위치 계산:  
            // idx = 0 → x = -3  
            // idx = 1 → x = -1  
            // …  
            // idx = 4 → x = 5  
            float x = -3 + idx * 2f;
            Vector3 pos = new Vector3(x, 0, fencespawnZ);

            // 인스턴스 생성
            var go = Instantiate(fencePrefabs[0], pos, Quaternion.identity, transform);
            Fences.Add(go);
            fenceQueue.Enqueue(go);
            // 같은 idx 재사용 방지
            available.RemoveAt(randListIdx);
        }

        // Z축을 앞으로 이동
        fencespawnZ += fenceLength;

        yield return null;
    }

    private void Release()
    {
        // 가장 앞에 있는 타일을 제거합니다.
        Destroy(tiles[0]);
        //타일 리스트의 맨 앞의 값을 제거합니다.
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
