using System.Collections;
using UnityEngine;

//1. 해당 스크립트를 사용 시 애니메이터 컴포넌트를 요구합니다.
// -> 해당 속성이 포함되어있는 스크립트를 컴포넌트로 연결했을 경우
//    다음과 같이 처리됩니다.
//    1. 요구하고 있는 애니메이터 컴포넌트가 없을 경우 자동 연결해줍니다.
//    2. 이 스크립트가 컴포넌트로 사용되는 동안, 어떤 경우라도 요구되고있는 컴포넌트를 제거할 수 없습니다.(이 경우엔 애니메이터)
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    PlayerAttack playerAttack;

    //공격, 스킬, 대시에 관한 시간
    float lastAttackTime, lastSkillTime, lastDashTime;
    public bool attacking = false;
    public bool dashing = false;

    //UI에 컨트롤러를 배치해서 그 컨트롤러로 이동을 진행해볼 예정
    float h, v;

    //스틱의 위치를 전달받아 x, y축을 처리
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    //UI의 버튼 등을 이용해서 공격을 진행해야 하므로, 기능 별로 함수 구현을 진행하는 식으로 공격 구현
    //XXXDown : 눌렀을 때 1회
    //XXXUp : 누르고 뗐을 때 1회
    //XXX : 누르고 있는 동안 계속

    //OnAttackUp, OnSkillUp, OnDashUp 같이 동작을 제어하기 위한 값들을 조건에 따라 처리하는 함수 ==> 플래그 함수

    //연타 공격에 대한 코루틴 설계
    private IEnumerator Attack()
    {
        if(Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;
            while (attacking)
            {
                animator.SetTrigger("Attack");
                //애니메이터의 파라미터 중에서 SetTrigger는
                //설정하는 것으로 조건을 바로 만족하게 됩니다.
                //수행 끝나면 끝.
                playerAttack.NormalAttack();
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    public void OnAttackDown()
    {
        attacking = true;
        animator.SetBool("Combo", true);
        StartCoroutine(Attack());
    }

    public void OnAttackUp()
    {
        attacking = false;
        animator.SetBool("Combo", false);
        animator.ResetTrigger("Attack");
    }

    public void OnSkillDown()
    {
        if(Time.time - lastSkillTime > 1.0f)
        {
            animator.SetBool("Skill", true);
            lastSkillTime = Time.time;
            playerAttack.SkillAttack();
        }
    }

    public void OnSkillUp()
    {
        animator.SetBool("Skill", false);
    }

    public void OnDashDown()
    {
        if (Time.time - lastDashTime > 1.0f)
        {
            dashing = true;
            lastDashTime = Time.time;
            playerAttack.DashAttack();
        }
    }

    public void OnDashUp()
    {
        dashing = false;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float back = 1f;

        if (v < 0f)
            back = -1f;

        animator.SetFloat("Speed", new Vector2(h, v).magnitude);
        animator.SetFloat("Direction", back * (Mathf.Atan2(h, v) * Mathf.Rad2Deg));


        Vector3 dir = new Vector3(h, 0f, v);
        transform.Translate(dir * 5.0f * Time.deltaTime);

        Rigidbody rbody = GetComponent<Rigidbody>();

        if (rbody)
        {
            Vector3 speed = rbody.linearVelocity;
            speed.x = 4 * h;
            speed.z = 4 * v;
            rbody.linearVelocity = speed;

            //방향 전환
            if (h != 0f && v != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            }
        }

#else
        //애니메이터에 대한 연결이 진행되야 작동하도록 처리
        if (animator)
        {
            //이동 방향(조절)
            float back = 1f;
            
            if (v < 0f)
                back = -1f;

            animator.SetFloat("Speed", new Vector2(h, v).magnitude);
            animator.SetFloat("Direction", back * (Mathf.Atan2(h, v) * Mathf.Rad2Deg));
            //h, v 기준   back        position
            //0, 1          1           front
            //1, 0          1           right
            //0, -1         -1          back
            //-1, -1        -1          left

            //magnitude == 벡터의 길이, 크기

            Rigidbody rbody = GetComponent<Rigidbody>();

            //리지드바디가 연결되어 있을 때
            if (rbody)
            {
                Vector3 speed = rbody.linearVelocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rbody.linearVelocity = speed;

                //방향 전환
                if(h != 0f && v != 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }
#endif

    }
}
