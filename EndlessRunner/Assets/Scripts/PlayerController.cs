using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller; //������Ʈ
    public ScoreManager scoreManager; //���ھ� �Ŵ���
    Animator animator;
    Renderer[] renderers;

    private Vector3 moveVector; //���� ����
    private float vertical_velocity = 0.0f; // ������ ���� ���� �ӵ�
    private float gravity = 12.0f; // �߷� ��

    public bool isDead = false; //�Ϲ������� ����ִ� ����
    [SerializeField] private float speed = 5.0f; // �÷��̾��� �̵� �ӵ�
    [SerializeField] private float jump = 10.0f; // �÷��̾��� ���� ��ġ

    private bool isStopped = false;

    private float flickerDuration = 1.0f;
    private float flickerInterval = 0.1f;

    public void SetSpeed(float level)
    {
        speed += level;
    }

    public float GetSpeed() => speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();    
        animator = GetComponent<Animator>();
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        // ī�޶� ��Ʈ�ѷ��� �̿��� �÷��̾� ������ ���� ī�޶� ������ �����غ��� �մϴ�.
        if(Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            //controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        // ���� ������ ��� Update �۾� x
        if (isDead)
            return;

        moveVector = Vector3.zero; // ���� ���� �� ����

        // ���� ������� ��� velocity ����
        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;
            // ���� ��� �߰�
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping())
            {
                vertical_velocity = jump;
                SetAnimator("isJump");
            }
            //else if (Input.GetKeyDown(KeyCode.S))
            //{
            //    SetAnimator("isSlide");
            //}
        }
        else
        {
            // �ƴ� ��� �߷�ġ��ŭ ����������
            vertical_velocity -= gravity * Time.deltaTime;

            // ����
            if (vertical_velocity < -10f)
            {
                OnDeath();
                scoreManager.OnDead();
                SetAnimator("isDead");
            }
        }

        //1. �¿� �̵�
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //2. ���� ����
        moveVector.y = vertical_velocity;
        //3. ������ �̵�
        moveVector.z = speed;

        

        if (!isStopped)
        {
            // ������ ������ �̵� ����
            controller.Move(moveVector * Time.deltaTime);
            SetAnimator("isRun");
        }

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Boss")
        {
            //�浹�ϸ� �ٷ� �״� �̺�Ʈ�� ����
            OnDeath();
            scoreManager.OnDead();
            SetAnimator("isDead");
        }
        if (hit.transform.tag == "Obstacle" && !isStopped)
        {
            Destroy(hit.gameObject);
            scoreManager.SetTMP_Text();
            StartCoroutine(StopAndResume());
            StartCoroutine(FlickerAll());

        }
        if (hit.transform.tag == "Boost" && !isStopped)
        {
            Destroy(hit.gameObject);
            StartCoroutine(Boost());
        }
    }
    private IEnumerator StopAndResume()
    {
        isStopped = true;
        SetAnimator("isDamaged");

        // 1�ʰ� ���
        yield return new WaitForSeconds(1f);

        isStopped = false;
    }
    private IEnumerator Boost()
    {
        speed += 5;
        // 3�� �ν���
        scoreManager.SetTMP_Text();

        yield return new WaitForSeconds(3f);

        speed -= 5;
        scoreManager.SetTMP_Text();
    }

    private void OnDeath()
    {
        isDead = true;
    }
    

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

    void SetRenderersVisible(bool isVisible)
    {
        foreach (var rend in renderers)
        {
            rend.enabled = isVisible;
        }
    }


    private void SetAnimator(string temp)
    {
        if(temp == "isJump")
        {
            animator.SetTrigger("isJump");
            return;
        }
        if(temp == "isSlide")
        {
            animator.SetTrigger("isSlide");
            return;
        }
        if (temp == "isDead")
        {
            animator.SetTrigger("isDead");
            return;
        }
        if (temp == "isDamaged")
        {
            animator.SetTrigger("isDamaged");
            return;
        }
        animator.SetBool("isRun", true);
    }

    bool isJumping()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("isJump");
    }
}
