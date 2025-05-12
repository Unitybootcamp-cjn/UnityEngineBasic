using System.Collections;
using UnityEngine;

//유닛 1개 생성기
public class EnemyManager : MonoBehaviour
{
    float currentTime; //현재 시간

    public float step = 1; //시간 간격

    public GameObject[] enemyFactory; //적 공장
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
