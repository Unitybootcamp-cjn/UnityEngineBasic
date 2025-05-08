using UnityEngine;

public class BossCameraController : MonoBehaviour
{
    Transform target; // �÷��̾��� ��ġ
    Vector3 camera_offset; // ī�޶�� �÷��̾� ���� �Ÿ� ����
    Vector3 moveVector; // ī�޶� �� ������ �̵��� ��ġ

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera_offset = transform.position - target.position; // ���� �� (ī�޶� ��ġ - Ÿ�� ��ġ)
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = target.position + camera_offset;   // Ÿ�� + ī�޶� ���������� ���� ����
        moveVector.x = 0;                               // ī�޶� X�� ����(�¿� �̵� ����)
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5); // ī�޶� Y�� ����(3 ~ 5 ������ ������ ����

        transform.position = moveVector;
    }
}
