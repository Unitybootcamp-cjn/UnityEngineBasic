using UnityEngine;

//�߰��� ����� ��
//1. ������ ���� Boss Ŭ����
//2. ������ ������ �����ϴ� BossData Ŭ����(SO)

public class BossManager : MonoBehaviour
{
    public GameObject boss;
    public EnemyManager enemyManager;

    public static BossManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Spawn()
    {
        Debug.Log("���� ����");
        Instantiate(boss);
        enemyManager.isBoss = true;
    }

    public void OnDead()
    {
        Debug.Log("���� ����");
        GameManager.instance.SetGameOver();
        //enemyManager.isBoss = false;
        //enemyManager.StartEnemyRoutine();
    }
}
