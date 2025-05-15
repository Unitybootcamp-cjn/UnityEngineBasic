using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect; //이펙트 등록
    public GameObject hitEffect; //맞을 때 이펙트 등록

    public Action onDead;


    public int hp = 1000;

    public float invincibleDuration = 0.3f;    // 무적 지속 시간
    private bool isInvincible = false;       // 무적 중인지 여부

    Vector3 dir; //움직일 방향

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
    // 무적 코루틴

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
        // 죽음
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
