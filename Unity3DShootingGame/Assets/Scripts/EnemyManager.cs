using System.Collections;
using UnityEngine;

//���� 1�� ������
public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyFactory; //�� ����

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
        int enemyIndex = 0; // �� �ܰ�
        float enemyInterval = 2f; // ���� ����

        

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
