using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target; // �÷��̾��� ��ġ
    Vector3 camera_offset; // ī�޶�� �÷��̾� ���� �Ÿ� ����
    Vector3 moveVector; // ī�޶� �� ������ �̵��� ��ġ

    float transition = 0.0f; // ���� ��(��ȯ��)
    public static float camera_animate_duration = 3.0f; // ī�޶� �̿��ؼ� �ִϸ��̼� ������ �� �� ���� �ð�
    public Vector3 animate_offset = new Vector3(0, 5, 5); // �ִϸ��̼��� ���� ���� ������

    public float shakeDuration = 0.5f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPos;
    private bool isShaking = false;
    Vector3 shakeOffset = Vector3.zero;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera_offset = transform.position - target.position; // ���� �� (ī�޶� ��ġ - Ÿ�� ��ġ)
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

        // ��鸲�� ������ ����
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
