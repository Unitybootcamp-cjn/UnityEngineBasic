using UnityEngine;

//�߰��� ����� ��
//1. ������ ���� Boss Ŭ����
//2. ������ ������ �����ϴ� BossData Ŭ����(SO)

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    public EnemyManager enemyManager;



    public void Spawn()
    {
        Instantiate(boss, transform.position, Quaternion.identity);
        enemyManager.isBoss = true;
    }

    public void OnDead()
    {
        enemyManager.isBoss = false;
        enemyManager.StartEnemyRoutine();
    }
}
