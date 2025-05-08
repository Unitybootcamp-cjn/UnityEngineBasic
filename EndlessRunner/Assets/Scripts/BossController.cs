using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    CharacterController controller; //������Ʈ

    private float vertical_velocity = 0.0f; // ������ ���� ���� �ӵ�
    private float gravity = 12.0f; // �߷� ��
    private Vector3 moveVector; //���� ����

    [SerializeField] private float speed = 5.0f; // ������ �̵� �ӵ�


    public void SetSpeed(float level)
    {
        speed += level-1;
    }

    public float GetSpeed() => speed;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            Destroy(hit.gameObject);
        }
    }

    void Update()
    {
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
    }
}
