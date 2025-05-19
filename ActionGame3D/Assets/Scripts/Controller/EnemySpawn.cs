using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public PlayerHealth playerHealth;   //�÷��̾� ü��(���� ���� Ȯ��)
    public GameObject enemy;            //��ȯ�� ����
    public float intervalTime = 10.0f;  //�ݺ� �ð�(�� Ÿ��)
    public Transform[] spawnPools;      //��ȯ ����

    private void Start()
    {
        InvokeRepeating("Spawn", intervalTime, intervalTime);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0f)
            return;

        //��� ������ ���� ���� �� �ϳ��� �������� ����
        int spawnPointIndex = Random.Range(0, spawnPools.Length);

        //���õ� ������ ��ġ�� ȸ�� ���� �޾Ƽ� ����
        Instantiate(enemy, spawnPools[spawnPointIndex].position, spawnPools[spawnPointIndex].rotation, spawnPools[spawnPointIndex]);
        
    }
}
