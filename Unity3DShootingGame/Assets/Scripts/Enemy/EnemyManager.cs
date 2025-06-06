using System;
using System.Collections;
using UnityEngine;

//유닛 1개 생성기
public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyFactory; //적 공장
    public GameObject boss;
    public event Action onEnemySpawned; //적 생성 시의 콜백 기능 구현

    //오브젝트 풀
    [Header("오브젝트 풀")]
    private int poolSize = 50;
    public GameObject[] enemypool;
    public Transform[] spawnPoint; // 기존 생성 위치
    public BossManager bossManager;

    int spawnCount = 0;
    int enemyIndex = 0; // 적 단계
    float enemyInterval = 2f; // 스폰 간격

    [Header("보스 출현")]
    public bool isBoss = false;

    private void Awake()
    {
        CreatePool();
    }
    private void Start()
    {
        StartEnemyRoutine();
    }

    private void CreatePool()
    {
        enemypool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            var enemy = Instantiate(enemyFactory[enemyIndex]);
            enemypool[i] = enemy;
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        if (isBoss)
            StopEnemyRoutine();

        if (GameManager.instance.isGameOver == true)
        {
            StopEnemyRoutine();
        }
    }

    public void StartEnemyRoutine()
    {
        StartCoroutine("EnemyRoutine");
    }

    void StopEnemyRoutine()
    {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3f);

        while (true)
        {
            spawnCount++;

            if (spawnCount % 5 == 0)
            {
                if (enemyIndex < enemyFactory.Length - 1)
                {
                    enemyIndex++;
                    CreatePool();
                }
                else
                {
                    bossManager.Spawn();
                    //var go = Instantiate(boss);
                    //go.transform.position += Vector3.up * 2;
                    //isBoss = true;
                    break;
                }
            }
            foreach (Transform spawn in spawnPoint)
            {

                for (int i = 0; i < poolSize; i++)
                {
                    var enemy = enemypool[i];
                    if (enemy.activeSelf == false)
                    {
                        enemy.transform.parent = transform;
                        enemy.transform.position = spawn.position;
                        enemy.SetActive(true);
                        onEnemySpawned?.Invoke(); //이벤트에 대한 실행
                        break;
                    }
                }
            }
            //var enemy = Instantiate(enemyFactory[enemyIndex]);
            //enemy.transform.position = transform.position;
            //enemy.transform.parent = transform;

            yield return new WaitForSeconds(enemyInterval);
        }

    }

}
