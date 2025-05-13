using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public GameObject effect; //이펙트 등록
    public GameObject hitEffect; //맞을 때 이펙트 등록
    public GameObject coin;
    

    [SerializeField] private int hp = 1;

    Bullet bullet;

    Vector3 dir; //움직일 방향

    private void Start()
    {
        int randValue = Random.Range(0, 10);
        bullet = new Bullet();

        //플레이어 방향으로 이동
        if (randValue < 3)
        {
            //게임 씬에서 "Player"를 검색합니다.
            if(GameManager.instance.isGameOver == false)
            {
                var target = GameObject.Find("Player");
                dir = target.transform.position - transform.position;
            }
            //일반화를 통해 균일하게 이동하도록 처리
            //방향의 크기를 1로 설정
            dir.Normalize();
        }
        else //아래로 이동
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
        var hiteffect = Instantiate(hitEffect);
        hiteffect.transform.position = transform.position;
        // 죽음
        if (hp <= 0)
        {
            var explosion = Instantiate(effect);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
            ScoreManager.instance.Score += 100;
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

    //충돌 시작
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
            Destroy(gameObject);
            ScoreManager.instance.Score += 100;
            Instantiate(coin, transform.position, Quaternion.identity);
        }
    }

    //충돌 끝
    private void OnCollisionExit(Collision collision)
    {
        
    }

    //충돌 진행 상황
    private void OnCollisionStay(Collision collision)
    {
        
    }
}
