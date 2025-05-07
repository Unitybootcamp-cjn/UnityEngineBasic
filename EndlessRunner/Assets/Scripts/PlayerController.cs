using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller; //컴포넌트

    [SerializeField] private Vector3 moveVector; //방향 벡터
    [SerializeField] private float speed = 5.0f; //플레이어의 이동 속도
    [SerializeField] private float vertical_velocity = 0.0f; // 점프를 위한 수직 속도
    [SerializeField] private float gravity = 12.0f; // 중력 값

    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }

    void Update()
    {
        // 카메라 컨트롤러를 이용해 플레이어 움직임 전에 카메라 연출을 진행해보려 합니다.
        if(Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            controller.Move(Vector3.forward * speed * Time.deltaTime);
            return;
        }


        moveVector = Vector3.zero; // 방향 벡터 값 리셋

        // 땅에 닿아있을 경우 velocity 고정
        if (controller.isGrounded)
        {
            vertical_velocity = 0.0f;
        }
        else
        {
            // 아닐 경우 중력치만큼 떨어지도록
            vertical_velocity -= gravity * Time.deltaTime;
        }


        //1. 좌우 이동
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //2. 점프 관련
        moveVector.y = vertical_velocity;
        //3. 앞으로 이동
        moveVector.z = speed;


        // 설정한 방향대로 이동 진행
        controller.Move(moveVector * Time.deltaTime);
    }
}
