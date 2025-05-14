using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//발사대에 연결할 스크립트
//발사 버튼을 눌렀을 경우 총알 발사
//현재 총알에는 방향에 따라 이동하는 코드가 구현이 되어있음.
//따라서 버튼 누르면 생성되기메나 만들어주면 구현 완료

public class PlayerFire : MonoBehaviour
{
    public static PlayerFire Instance;

    public GameObject[] bulletFactory; //총알 공장
    public int bulletIndex = 0;
    public GameObject[] bombFactory; //폭탄 공장
    public Transform[] firePosition; //총알이 발사되는 위치
    public GameObject bombeffect; //죽을 때 이펙트

    //오브젝트 풀[Object Pool]
    [Header("오브젝트 풀")]
    public int poolSize = 150; // 1. 풀의 크기에 대한 설정(총알 개수)
    public GameObject[] bulletObjectPool; // 2. 오브젝트 풀(배열 / 리스트)

    public float shootInterval = 0.2f; //총알 자동발사 인터벌
    private bool isShooting = false; // 발사하고 있는가?
    public int powerLevel = 0;


    [Header("폭탄 설정")]
    public float explosionRadius = 10f;     // 폭발 반경
    public int explosionDamage = 3;       // 폭발 데미지
    public LayerMask Enemy;     // 데미지 대상 레이어 (예: Enemy)


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //아이템 등을 이용해 총알의 개수를 바꿀 수 있는 게임이라면
        //풀의 사이즈를 최대로 잡아두고 생성한 뒤
        //플레이어의 소유 총알 개수를 통해 총알을 발사할 수 있도록 설정합니다.
        bulletObjectPool = new GameObject[poolSize]; // 3. 시작 부분에서 풀에 대한 할당

        for (int i = 0; i < poolSize; i++) 
        {
            var bullet = Instantiate(bulletFactory[bulletIndex]); // 4. 풀의 크기만큼 생성을 진행

            bulletObjectPool[i] = bullet; // 5. 생성된 오브젝트를 풀에 등록
                                          // 배열일 경우 인덱스로, 리스트일 경우 Add로 추가

            bullet.SetActive(false);      // 생성된 총알을 비활성화. 발사할 때만 활성화
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

        // 폭발 범위 내 모든 콜라이더 검색
        Vector3 center = bomb.transform.position;
        Collider[] hits = Physics.OverlapSphere(center, explosionRadius, Enemy);
        Debug.Log($"[Bomb] center={center}, radius={explosionRadius}, hits={hits.Length}");

        // 데미지 적용
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
                //풀 사이즈만큼 반복
                for (int i = 0; i < poolSize; i++)
                {
                    //풀에 있는 총알 하나를 받아옴
                    var bullet = bulletObjectPool[i];

                    //비활성화일 경우 활성화 진행
                    if (bullet.activeSelf == false)
                    {
                        bullet.SetActive(true);
                        // 발사 위치 조정
                        bullet.transform.position = firePosition[j].position;
                        // 반복문 종료
                        break;
                    }
                }
            }


            //Instantiate(bulletFactory[bulletIndex], firePosition.transform.position, Quaternion.identity);
            isShooting = false;
        }
    }
}
