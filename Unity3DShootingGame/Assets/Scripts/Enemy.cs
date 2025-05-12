using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect; //����Ʈ ���
    public GameObject coin;

    [SerializeField] private int hp = 1;

    Bullet bullet;

    Vector3 dir; //������ ����

    private void Start()
    {
        int randValue = Random.Range(0, 10);
        bullet = new Bullet();

        //�÷��̾� �������� �̵�
        if (randValue < 3)
        {
            //���� ������ "Player"�� �˻��մϴ�.
            var target = GameObject.Find("Player");
            dir = target.transform.position - transform.position;
            //�Ϲ�ȭ�� ���� �����ϰ� �̵��ϵ��� ó��
            //������ ũ�⸦ 1�� ����
            dir.Normalize();
        }
        else //�Ʒ��� �̵�
        {
            dir = Vector3.down;
        }
    }
    private void Update()
    {
        //Vector3 dir = Vector3.down;

        transform.position += dir * speed * Time.deltaTime;
    }

    public void ApplyDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            var explosion = Instantiate(effect);
            explosion.transform.position = transform.position;
            //Destroy(collision.gameObject);
            Destroy(gameObject);
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

    //�浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(bullet.damage);
        
    }

    //�浹 ��
    private void OnCollisionExit(Collision collision)
    {
        
    }

    //�浹 ���� ��Ȳ
    private void OnCollisionStay(Collision collision)
    {
        
    }
}
