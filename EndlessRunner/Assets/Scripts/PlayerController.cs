using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller; //������Ʈ
    public ScoreManager scoreManager; //���ھ� �Ŵ���
    Animator animator;

    private Vector3 moveVector; //���� ����
    private float vertical_velocity = 0.0f; // ������ ���� ���� �ӵ�
    private float gravity = 12.0f; // �߷� ��

    [SerializeField] private bool isDead = false; //�Ϲ������� ����ִ� ����
    [SerializeField] private float speed = 5.0f; // �÷��̾��� �̵� �ӵ�
    [SerializeField] private float jump = 10.0f; // �÷��̾��� ���� ��ġ


    public void SetSpeed(float level)
    {
        speed += level;
    }

    public float GetSpeed() => speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();    
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ī�޶� ��Ʈ�ѷ��� �̿��� �÷��̾� ������ ���� ī�޶� ������ �����غ��� �մϴ�.
        if(Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
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
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SetAnimator("isSlide");
            }
            else
            {
                SetAnimator("isRun");
            }
        }
        else
        {
            // �ƴ� ��� �߷�ġ��ŭ ����������
            vertical_velocity -= gravity * Time.deltaTime;
        }


        //1. �¿� �̵�
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //2. ���� ����
        moveVector.y = vertical_velocity;
        //3. ������ �̵�
        moveVector.z = speed;


        // ������ ������ �̵� ����
        controller.Move(moveVector * Time.deltaTime);
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
        if (hit.transform.tag == "Obstacle")
        {
            speed -= 1;
            Destroy(hit.gameObject);
            scoreManager.SetTMP_Text();
        }
    }


    private void OnDeath()
    {
        isDead = true;
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
        animator.SetBool("isRun", true);
    }

    bool isJumping()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("isJump");
    }
}
