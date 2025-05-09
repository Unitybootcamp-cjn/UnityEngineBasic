using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    CharacterController controller; //������Ʈ
    Animator animator;

    private float vertical_velocity = 0.0f; // ������ ���� ���� �ӵ�
    private float gravity = 12.0f; // �߷� ��
    private Vector3 moveVector; //���� ����

    private bool isOver = false; //������ ��������

    public float speed = 5.0f; // ������ �̵� �ӵ�


    public void SetSpeed(float level)
    {
        speed += level + 0.2f;
        // ���� �Ĺݺο� ���� �� ���̵� ���
        if (speed >= 14)
            speed += 1;
    }

    public float GetSpeed() => speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle" || hit.transform.tag == "Boost")
        {
            Destroy(hit.gameObject);
        }
        if(hit.transform.tag == "Player")
        {
            isOver = true;
            SetAnimator("isAttack");
        }
    }


    void Update()
    {
        if (isOver)
            return;
        moveVector = Vector3.zero; // ���� ���� �� ����

        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;
        }
        else
        {
            // �ƴ� ��� �߷�ġ��ŭ ����������
            vertical_velocity -= gravity * Time.deltaTime;
        }

        moveVector.y = vertical_velocity;

        moveVector.z = speed;

        // ������ ������ �̵� ����
        controller.Move(moveVector * Time.deltaTime);
        SetAnimator("isRun");
    }

    private void SetAnimator(string temp)
    {
        if (temp == "isAttack")
        {
            animator.SetTrigger("isAttack");
            return;
        }
        animator.SetBool("isRun", true);
    }
}
