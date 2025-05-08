using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    CharacterController controller; //컴포넌트

    private float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    private float gravity = 12.0f; // 중력 값
    private Vector3 moveVector; //방향 벡터

    [SerializeField] private float speed = 5.0f; // 보스의 이동 속도


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
        moveVector = Vector3.zero; // 방향 벡터 값 리셋

        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;
        }
        else
        {
            // 아닐 경우 중력치만큼 떨어지도록
            vertical_velocity -= gravity * Time.deltaTime;
        }

        moveVector.y = vertical_velocity;

        moveVector.z = speed;

        // 설정한 방향대로 이동 진행
        controller.Move(moveVector * Time.deltaTime);
    }
}
