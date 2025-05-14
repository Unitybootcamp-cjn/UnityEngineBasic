using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//�߻�뿡 ������ ��ũ��Ʈ
//�߻� ��ư�� ������ ��� �Ѿ� �߻�
//���� �Ѿ˿��� ���⿡ ���� �̵��ϴ� �ڵ尡 ������ �Ǿ�����.
//���� ��ư ������ �����Ǳ�޳� ������ָ� ���� �Ϸ�

public class PlayerFire : MonoBehaviour
{
    public static PlayerFire Instance;

    public GameObject[] bulletFactory; //�Ѿ� ����
    public int bulletIndex = 0;
    public GameObject[] bombFactory; //��ź ����
    public Transform[] firePosition; //�Ѿ��� �߻�Ǵ� ��ġ
    public GameObject bombeffect; //���� �� ����Ʈ

    //������Ʈ Ǯ[Object Pool]
    [Header("������Ʈ Ǯ")]
    public int poolSize = 150; // 1. Ǯ�� ũ�⿡ ���� ����(�Ѿ� ����)
    public GameObject[] bulletObjectPool; // 2. ������Ʈ Ǯ(�迭 / ����Ʈ)

    public float shootInterval = 0.2f; //�Ѿ� �ڵ��߻� ���͹�
    private bool isShooting = false; // �߻��ϰ� �ִ°�?
    public int powerLevel = 0;


    [Header("��ź ����")]
    public float explosionRadius = 10f;     // ���� �ݰ�
    public int explosionDamage = 3;       // ���� ������
    public LayerMask Enemy;     // ������ ��� ���̾� (��: Enemy)


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //������ ���� �̿��� �Ѿ��� ������ �ٲ� �� �ִ� �����̶��
        //Ǯ�� ����� �ִ�� ��Ƶΰ� ������ ��
        //�÷��̾��� ���� �Ѿ� ������ ���� �Ѿ��� �߻��� �� �ֵ��� �����մϴ�.
        bulletObjectPool = new GameObject[poolSize]; // 3. ���� �κп��� Ǯ�� ���� �Ҵ�

        for (int i = 0; i < poolSize; i++) 
        {
            var bullet = Instantiate(bulletFactory[bulletIndex]); // 4. Ǯ�� ũ�⸸ŭ ������ ����

            bulletObjectPool[i] = bullet; // 5. ������ ������Ʈ�� Ǯ�� ���
                                          // �迭�� ��� �ε�����, ����Ʈ�� ��� Add�� �߰�

            bullet.SetActive(false);      // ������ �Ѿ��� ��Ȱ��ȭ. �߻��� ���� Ȱ��ȭ
        }
    }



    private void Update()
    {
        if (GameManager.instance.isGameOver == true)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(ScoreManager.instance.bomb > 0)
            {
                ScoreManager.instance.bomb -= 1;
                ScoreManager.instance.SetScoreText();
                StartCoroutine(Bomb());
            }
        }
        
        StartCoroutine(Fire());

        //if (Input.GetButtonDown("Fire1"))
        //{
        //    StartCoroutine(Bomb());
        //}
    }

    IEnumerator Bomb()
    {
        var bomb = Instantiate(bombFactory[0], firePosition[0].position, Quaternion.identity);
        yield return new WaitForSeconds(1f);

        // ���� ���� �� ��� �ݶ��̴� �˻�
        Vector3 center = bomb.transform.position;
        Collider[] hits = Physics.OverlapSphere(center, explosionRadius, Enemy);
        Debug.Log($"[Bomb] center={center}, radius={explosionRadius}, hits={hits.Length}");

        // ������ ����
        foreach (var hit in hits)
        {
            var health = hit.GetComponent<Enemy>();
            if (health != null) health.ApplyDamage(explosionDamage);

        }
        var Deadexplosion = Instantiate(bombeffect);
        Deadexplosion.transform.position = bomb.transform.position;
        Destroy(bomb);
    }

    IEnumerator Fire()
    {
        if (isShooting)
            yield return null;
        else
        {
            isShooting = true;
            yield return new WaitForSeconds(shootInterval);
            for (int j = 0; j <= 4; j++)
            {
                //Ǯ �����ŭ �ݺ�
                for (int i = 0; i < poolSize; i++)
                {
                    //Ǯ�� �ִ� �Ѿ� �ϳ��� �޾ƿ�
                    var bullet = bulletObjectPool[i];

                    //��Ȱ��ȭ�� ��� Ȱ��ȭ ����
                    if (bullet.activeSelf == false)
                    {
                        bullet.SetActive(true);
                        // �߻� ��ġ ����
                        bullet.transform.position = firePosition[j].position;
                        // �ݺ��� ����
                        break;
                    }
                }
            }


            //Instantiate(bulletFactory[bulletIndex], firePosition.transform.position, Quaternion.identity);
            isShooting = false;
        }
    }
}
