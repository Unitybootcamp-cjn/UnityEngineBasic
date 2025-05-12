using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

//발사대에 연결할 스크립트
//발사 버튼을 눌렀을 경우 총알 발사
//현재 총알에는 방향에 따라 이동하는 코드가 구현이 되어있음.
//따라서 버튼 누르면 생성되기메나 만들어주면 구현 완료

public class PlayerFire : MonoBehaviour
{
    public GameObject[] bulletFactory; //총알 공장

    public GameObject[] bombFactory; //폭탄 공장

    public GameObject firePosition;

    public GameObject bombeffect; //죽을 때 이펙트 등록


    public float shootInterval = 0.2f;
    private bool isShooting = false;

    [Header("폭탄 설정")]
    public float explosionRadius = 10f;     // 폭발 반경
    public int explosionDamage = 3;       // 폭발 데미지
    public LayerMask Enemy;     // 데미지 대상 레이어 (예: Enemy)

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
            Instantiate(bulletFactory[0], firePosition.transform.position, Quaternion.identity);
            isShooting = false;
        }
    }
}
