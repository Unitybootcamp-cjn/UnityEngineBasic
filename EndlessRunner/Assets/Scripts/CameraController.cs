using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target; // 플레이어의 위치
    Vector3 camera_offset; // 카메라와 플레이어 간의 거리 간격
    Vector3 moveVector; // 카메라가 매 프레임 이동할 위치

    float transition = 0.0f; // 보간 값(전환용)
    public static float camera_animate_duration = 3.0f; // 카메라를 이용해서 애니메이션 연출할 때 쓸 지속 시간
    public Vector3 animate_offset = new Vector3(0, 5, 5); // 애니메이션을 위한 시작 오프셋

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPos;
    private bool isShaking = false;
    Vector3 shakeOffset = Vector3.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera_offset = transform.position - target.position; // 시작 값 (카메라 위치 - 타겟 위치)
    }

    void Update()
    {
        moveVector = target.position + camera_offset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);

        Vector3 finalPosition;

        if (transition > 1.0f)
        {
            finalPosition = moveVector;
        }
        else
        {
            finalPosition = Vector3.Lerp(moveVector + animate_offset, moveVector, transition);
            transition += Time.deltaTime / camera_animate_duration;
            transform.LookAt(target.position + Vector3.up);
        }

        // 흔들림이 있으면 적용
        finalPosition += shakeOffset;

        transform.position = finalPosition;
    }


    public void TriggerShake()
    {
        if (!isShaking)
            StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            shakeOffset = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        shakeOffset = Vector3.zero;
        isShaking = false;
    }
}
