using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//�߻�뿡 ������ ��ũ��Ʈ
//�߻� ��ư�� ������ ��� �Ѿ� �߻�
//���� �Ѿ˿��� ���⿡ ���� �̵��ϴ� �ڵ尡 ������ �Ǿ�����.
//���� ��ư ������ �����Ǳ�޳� ������ָ� ���� �Ϸ�

public class PlayerFire : MonoBehaviour
{
    public GameObject[] bulletFactory; //�Ѿ� ����

    public GameObject[] bombFactory; //��ź ����

    public GameObject firePosition;

    public GameObject bombeffect; //���� �� ����Ʈ ���


    public float shootInterval = 0.2f;
    private bool isShooting = false;

    [Header("��ź ����")]
    public float explosionRadius = 10f;     // ���� �ݰ�
    public int explosionDamage = 3;       // ���� ������
    public LayerMask Enemy;     // ������ ��� ���̾� (��: Enemy)

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Bomb());

        }
        StartCoroutine(Fire());
    }

    IEnumerator Bomb()
    {
        var bomb = Instantiate(bombFactory[0], firePosition.transform.position, Quaternion.identity);
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
            Instantiate(bulletFactory[0], firePosition.transform.position, Quaternion.identity);
            isShooting = false;
        }
    }
}
