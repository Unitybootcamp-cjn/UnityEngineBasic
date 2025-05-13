using System;
using System.Collections;
using UnityEditor.TerrainTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public GameObject deadeffect; //죽을 때 이펙트 등록
    Renderer[] renderers;
    Renderer renderer;


    private float flickerDuration = 1.0f; //피격 시 반짝이는 시간
    private float flickerInterval = 0.1f; //반짝이는 시간 인터벌

    public float invincibleDuration = 1f;    // 무적 지속 시간
    private bool isInvincible = false;       // 무적 중인지 여부

    public int hp = 5; // 체력

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        transform.position = Vector3.zero;
        renderer = GetComponent<Renderer>();
        if (CharacterManager.instance != null )
            renderer.material = CharacterManager.instance.CharacterMaterial[CharacterManager.instance.materialIndex];
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = - Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        transform.Translate(dir * speed * Time.deltaTime);

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -3.4f, 3.4f);
        pos.y = Mathf.Clamp(pos.y, -4.0f, 6.0f);

        transform.position = pos;
        #region 필기
        //transform.Translate(Vector3 dir);
        //게임 오브젝트를 이동시키기 위한 용도
        //게임 오브젝트의 위치를 Vector3 방향으로 이동하게 됩니다.
        //(물리 엔진과 관련된 연산을 수행하지는 않고 단순한 이동 기능으로 구현)
        //--> 기본적인 움직임

        //transform.position을 통해 계산해둔 위치로 오브젝트의 position을 바꿀 수 있습니다.
        //-- > 주로 포탈 같은 형태의 움직임 구현 시 효과적
        //position을 직접 움직일 오브젝트에는 rigidbody를 쓰지 않는 게 좋습니다.
        //(콜라이더들이 rigidbody의 상대적인 위치를 재계산해야하는 경우가 발생합니다.)

        //Rigidbody는 게임 오브젝트에 물리 엔진을 적용해서 충돌, 힘, 중력 등의
        //물리적인 상호작용을 가능하게 해주는 컴포넌트입니다.

        //Rigidbody.AddForce(Vector3 dir, ForceMode mode);
        //물리적인 연산을 통해 움직임을 구현하고, 힘을 주는 설정에 따라 지속적으로 처리할 지,
        //순간적인 힘을 가할지를 처리할 수 있습니다.
        #endregion

    }

    // 충돌 시
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        int coinLayer = LayerMask.NameToLayer("Coin");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (other.layer == coinLayer)
        {
            ScoreManager.instance.Score += 200;
            ScoreManager.instance.coin += 1;
            ScoreManager.instance.SetScoreText();
        }
        else if (other.layer == enemyLayer)
        {
            if (isInvincible) return;

            hp -= 1;
            StartCoroutine(InvincibleCoroutine());
            StartCoroutine(FlickerAll());
            // 죽음. 게임오버
            if (hp <= 0)
            {
                Destroy(gameObject);
                var Deadexplosion = Instantiate(deadeffect);
                Deadexplosion.transform.position = transform.position;
                GameManager.instance.SetGameOver();
            }
        }
            
    }

    

    // 무적 코루틴
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }


    // 피격 시 몸 반짝이는 코루틴
    IEnumerator FlickerAll()
    {
        float elapsed = 0f;

        while (elapsed < flickerDuration)
        {
            SetRenderersVisible(false);
            yield return new WaitForSeconds(flickerInterval);
            SetRenderersVisible(true);
            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval * 2;
        }
    }

    //피격 시 몸 반짝이는 함수
    void SetRenderersVisible(bool isVisible)
    {
        foreach (var rend in renderers)
        {
            rend.enabled = isVisible;
        }
    }
}
