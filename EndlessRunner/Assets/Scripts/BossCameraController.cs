using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    Transform target; // 플레이어의 위치
    Vector3 camera_offset; // 카메라와 플레이어 간의 거리 간격
    Vector3 moveVector; // 카메라가 매 프레임 이동할 위치

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera_offset = transform.position - target.position; // 시작 값 (카메라 위치 - 타겟 위치)
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = target.position + camera_offset;   // 타겟 + 카메라 오프셋으로 방향 설정
        moveVector.x = 0;                               // 카메라 X축 고정(좌우 이동 안함)
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5); // 카메라 Y축 고정(3 ~ 5 사이의 값으로 제한

        transform.position = moveVector;
    }
}
