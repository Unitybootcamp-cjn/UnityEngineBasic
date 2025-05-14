using System;
using System.Collections;
using UnityEngine;

//���� 1�� ������
public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyFactory; //�� ����

    public event Action onEnemySpawned; //�� ���� ���� �ݹ� ��� ����

    //������Ʈ Ǯ
    [Header("������Ʈ Ǯ")]
    private int poolSize = 30;
    public GameObject[] pool;
    public Transform[] spawnPoint; // ���� ���� ��ġ

    int spawnCount = 0;
    int enemyIndex = 0; // �� �ܰ�
    float enemyInterval = 2f; // ���� ����

    [Header("���� ����")]
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
        pool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            var enemy = Instantiate(enemyFactory[enemyIndex]);
            pool[i] = enemy;
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

            if (spawnCount % 10 == 0)
            {
                if (enemyIndex < enemyFactory.Length - 1)
                {
                    enemyIndex++;
                }
            }
            foreach (Transform spawn in spawnPoint)
            {

                for (int i = 0; i < poolSize; i++)
                {
                    var enemy = pool[i];
                    if (enemy.activeSelf == false)
                    {
                        enemy.transform.parent = transform;
                        enemy.transform.position = spawn.position;
                        enemy.SetActive(true);
                        onEnemySpawned?.Invoke(); //�̺�Ʈ�� ���� ����
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
