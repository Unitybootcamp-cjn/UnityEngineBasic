using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform target; // �÷��̾��� ��ġ
    Vector3 camera_offset; // ī�޶�� �÷��̾� ���� �Ÿ� ����
    Vector3 moveVector; // ī�޶� �� ������ �̵��� ��ġ

    float transition = 0.0f; // ���� ��(��ȯ��)
    public static float camera_animate_duration = 3.0f; // ī�޶� �̿��ؼ� �ִϸ��̼� ������ �� �� ���� �ð�
    public Vector3 animate_offset = new Vector3(0, 5, 5); // �ִϸ��̼��� ���� ���� ������

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        camera_offset = transform.position - target.position; // ���� �� (ī�޶� ��ġ - Ÿ�� ��ġ)
    }

    void Update()
    {
        moveVector = target.position + camera_offset;   // Ÿ�� + ī�޶� ���������� ���� ����
        moveVector.x = 0;                               // ī�޶� X�� ����(�¿� �̵� ����)
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5); // ī�޶� Y�� ����(3 ~ 5 ������ ������ ����


        if(transition > 1.0f)
        {
            transform.position = moveVector;
        }
        else
        {
            // ��ȯ�� ����ǰ� �ִ� ������ �� ������ �۾�
            // Vector3.Lerp(Vector a, Vector b, flaot t);
            // a���� b���� t �������� ������ �̵��ϴ� ��ɹ�(���� ����)
            transform.position = Vector3.Lerp(moveVector + animate_offset, moveVector, transition);
            // ������ ���� ��ġ���� �÷��̾��� ������� ���� �̵��� ������.

            transition += Time.deltaTime / camera_animate_duration;
            // ��ȯ ���� ������ ���� ��ŵ�ϴ�.(������ �ð� / �ִϸ��̼� �ð�)

            transform.LookAt(target.position + Vector3.up);
            // ���� �Ĵٺ��� �����մϴ�.
        }
    }
}
