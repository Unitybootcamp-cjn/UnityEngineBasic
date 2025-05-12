using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    Vector3 dir; //������ ����

    private void Start()
    {
        int randValue = Random.Range(0, 10);
        
        //�÷��̾� �������� �̵�
        if(randValue < 3)
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

    //�浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
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
