using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect; //����Ʈ ���
    public GameObject hitEffect; //���� �� ����Ʈ ���
    public GameObject coin;
    public GameObject bombImage;
    public GameObject heal;
    public GameObject powerUp;

    public Action onDead;
    

    [SerializeField] private int hp = 1;

    public float invincibleDuration = 0.01f;    // ���� ���� �ð�
    private bool isInvincible = false;       // ���� ������ ����

    Vector3 dir; //������ ����

    private void Awake()
    {
        StartCoroutine(InvincibleCoroutine());
    }

    private void Start()
    {
        int randValue = UnityEngine.Random.Range(0, 100);

        //�÷��̾� �������� �̵�
        if (randValue < 3)
        {
            //���� ������ "Player"�� �˻��մϴ�.
            if(GameManager.instance.isGameOver == false)
            {
                var target = GameObject.Find("Player");
                dir = target.transform.position - transform.position;
            }
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

    // ���� �ڷ�ƾ
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }

    public void Die()
    {
        onDead?.Invoke();
        gameObject.SetActive(false);
    }

    public void ApplyDamage(int damage)
    {
        if (isInvincible) return;

        hp -= damage;
        var hiteffect = Instantiate(hitEffect);
        hiteffect.transform.position = transform.position;
        // ����
        if (hp <= 0)
        {
            var explosion = Instantiate(effect);
            explosion.transform.position = transform.position;
            //Destroy(gameObject);
            Die();
            ScoreManager.instance.Score += 100;

            int rand = UnityEngine.Random.Range(0, 40);
            if (rand == 0)
                Instantiate(bombImage, transform.position, Quaternion.identity);
            if (rand == 1)
                Instantiate(heal, transform.position, Quaternion.identity);
            if (rand == 2)
                Instantiate(powerUp, transform.position, Quaternion.identity);
            if (rand > 30)
                Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

    //�浹 ����
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        int playerLayer = LayerMask.NameToLayer("Player");

        if (other.layer == bulletLayer)
        {
            var bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                ApplyDamage(bullet.damage);
            }
        }
        else if (other.layer == playerLayer)
        {
            var explosion = Instantiate(effect);
            explosion.transform.position = transform.position;
            Die();
            ScoreManager.instance.Score += 100;

            int rand = UnityEngine.Random.Range(0, 5);
            if(rand == 0)
                Instantiate(coin, transform.position, Quaternion.identity);
        }
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
