using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect; //����Ʈ ���
    public GameObject hitEffect; //���� �� ����Ʈ ���

    public Action onDead;


    public int hp = 1000;

    public float invincibleDuration = 0.3f;    // ���� ���� �ð�
    private bool isInvincible = false;       // ���� ������ ����

    Vector3 dir; //������ ����

    private void Awake()
    {
    }

    private void Start()
    {

    }
    private void Update()
    {
        if(transform.position.y > 6.0)
            transform.position += Vector3.down * speed * Time.deltaTime;
    }
    private void OnEnable()
    {
        StopAllCoroutines();
        isInvincible = true;
        StartCoroutine(InvincibleCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        isInvincible = false;
    }
    // ���� �ڷ�ƾ

    private IEnumerator InvincibleCoroutine()
    {
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
            ScoreManager.instance.Score += 10000;
        }
    }
    private void OnTriggerEnter(Collider collision)
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
        }
    }
}
