using System.Collections;
using UnityEngine;

//1. �ش� ��ũ��Ʈ�� ��� �� �ִϸ����� ������Ʈ�� �䱸�մϴ�.
// -> �ش� �Ӽ��� ���ԵǾ��ִ� ��ũ��Ʈ�� ������Ʈ�� �������� ���
//    ������ ���� ó���˴ϴ�.
//    1. �䱸�ϰ� �ִ� �ִϸ����� ������Ʈ�� ���� ��� �ڵ� �������ݴϴ�.
//    2. �� ��ũ��Ʈ�� ������Ʈ�� ���Ǵ� ����, � ���� �䱸�ǰ��ִ� ������Ʈ�� ������ �� �����ϴ�.(�� ��쿣 �ִϸ�����)
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    PlayerAttack playerAttack;

    //����, ��ų, ��ÿ� ���� �ð�
    float lastAttackTime, lastSkillTime, lastDashTime;
    public bool attacking = false;
    public bool dashing = false;

    //UI�� ��Ʈ�ѷ��� ��ġ�ؼ� �� ��Ʈ�ѷ��� �̵��� �����غ� ����
    float h, v;

    //��ƽ�� ��ġ�� ���޹޾� x, y���� ó��
    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    //UI�� ��ư ���� �̿��ؼ� ������ �����ؾ� �ϹǷ�, ��� ���� �Լ� ������ �����ϴ� ������ ���� ����
    //XXXDown : ������ �� 1ȸ
    //XXXUp : ������ ���� �� 1ȸ
    //XXX : ������ �ִ� ���� ���

    //OnAttackUp, OnSkillUp, OnDashUp ���� ������ �����ϱ� ���� ������ ���ǿ� ���� ó���ϴ� �Լ� ==> �÷��� �Լ�

    //��Ÿ ���ݿ� ���� �ڷ�ƾ ����
    private IEnumerator Attack()
    {
        if(Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;
            while (attacking)
            {
                animator.SetTrigger("Attack");
                //�ִϸ������� �Ķ���� �߿��� SetTrigger��
                //�����ϴ� ������ ������ �ٷ� �����ϰ� �˴ϴ�.
                //���� ������ ��.
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

            //���� ��ȯ
            if (h != 0f && v != 0f)
            {
                transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
            }
        }

#else
        //�ִϸ����Ϳ� ���� ������ ����Ǿ� �۵��ϵ��� ó��
        if (animator)
        {
            //�̵� ����(����)
            float back = 1f;
            
            if (v < 0f)
                back = -1f;

            animator.SetFloat("Speed", new Vector2(h, v).magnitude);
            animator.SetFloat("Direction", back * (Mathf.Atan2(h, v) * Mathf.Rad2Deg));
            //h, v ����   back        position
            //0, 1          1           front
            //1, 0          1           right
            //0, -1         -1          back
            //-1, -1        -1          left

            //magnitude == ������ ����, ũ��

            Rigidbody rbody = GetComponent<Rigidbody>();

            //������ٵ� ����Ǿ� ���� ��
            if (rbody)
            {
                Vector3 speed = rbody.linearVelocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
                rbody.linearVelocity = speed;

                //���� ��ȯ
                if(h != 0f && v != 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }
        }
#endif

    }
}
