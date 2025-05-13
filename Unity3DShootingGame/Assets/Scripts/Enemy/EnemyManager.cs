using System.Collections;
using UnityEngine;

//유닛 1개 생성기
public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyFactory; //적 공장

    private void Start()
    {
        StartEnemyRoutine();
    }

    private void Update()
    {
        if(GameManager.instance.isGameOver == true)
        {
            StopEnemyRoutine();
        }
    }
    void StartEnemyRoutine()
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

        int spawnCount = 0;
        int enemyIndex = 0; // 적 단계
        float enemyInterval = 2f; // 스폰 간격
        

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

            var enemy = Instantiate(enemyFactory[enemyIndex]);
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;

            yield return new WaitForSeconds(enemyInterval);
        }

    }

}
