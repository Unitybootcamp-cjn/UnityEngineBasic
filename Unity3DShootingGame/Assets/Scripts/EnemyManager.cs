using UnityEngine;

//���� 1�� ������
public class EnemyManager : MonoBehaviour
{
    float currentTime; //���� �ð�

    public float step = 1; //�ð� ����

    public GameObject enemyFactory; //�� ����


    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime > step)
        {
            var enemy = Instantiate(enemyFactory);
            enemy.transform.position = transform.position;
            enemy.transform.parent = transform;
            currentTime = 0;
        }
    }
}
