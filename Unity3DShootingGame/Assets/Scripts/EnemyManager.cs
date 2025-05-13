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

    void StartEnemyRoutine()
    {
        StartCoroutine(EnemyRoutine());
    }
    public void StopEnemyRoutine()
    {
        StopCoroutine(EnemyRoutine());
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
                enemyIndex++;
            }

            var enemy = Instantiate(enemyFactory[enemyIndex]);
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;

            yield return new WaitForSeconds(enemyInterval);
        }

    }

}
