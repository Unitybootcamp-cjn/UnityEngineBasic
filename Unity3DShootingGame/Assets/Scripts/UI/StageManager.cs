using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int kill; //�׿��� �ϴ� ��
    public int killed; //���� ��

    public static StageManager instance;
    //�� �Ŵ��� ����(�̱������� �ٲٸ� ���� x)
    public EnemyManager enemyManager;
    //���� �Ŵ��� ����
    public BossManager bossManager;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        //�� ���� �ÿ� ī��Ʈ�� ����(�̺�Ʈ)
        enemyManager.onEnemySpawned += EnemySpawned;

        //Ǯ ���� �ִ� ���͵鿡�� �̺�Ʈ ����
        foreach (var enemy in enemyManager.pool)
        {
            var go = enemy.GetComponent<Enemy>();
            go.onDead += EnemyKilled;
        }
    }

    private void Update()
    {
        //������ óġ�Ǿ��� ��� ���� �ٽ� �����Ѵ�.(�������� ���� �� ���� ó����)
    }

    void EnemySpawned() => kill++;

    void EnemyKilled()
    {
        killed++;
        Debug.Log($"������ : {killed} / {kill}");
        if (killed >= kill)
        {
            //�������� Ŭ����
            Debug.Log("Stage Clear");
        }
    }
}