using System.Collections;
using UnityEngine;

//���� 1�� ������
public class EnemyManager : MonoBehaviour
{
    float currentTime; //���� �ð�

    public float step = 1; //�ð� ����

    public GameObject[] enemyFactory; //�� ����
    private int enemyIndex;

    private void Start()
    {
        StartCoroutine(NextRound());
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > step)
        {
            var enemy = Instantiate(enemyFactory[enemyIndex]);
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;
            currentTime = 0;
        }
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSeconds(10);
        enemyIndex++;
    }
}
