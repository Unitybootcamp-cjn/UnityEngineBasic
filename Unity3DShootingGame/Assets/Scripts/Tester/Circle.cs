using UnityEngine;

//������Ʈ�� ���� ���·� ��ġ�ϴ� �ڵ�

public class Circle : MonoBehaviour
{
    public GameObject prefab;
    public int count = 20;
    public float radius = 5.0f;

    private void Start()
    {
        ////count��ŭ ����
        //for (int i = 0; i < count; i++)
        //{
        //    float radian = i * Mathf.PI * 2 / count;
        //    float x = Mathf.Cos(radian) * radius;
        //    float z = Mathf.Sin(radian) * radius;
        //    Vector3 pos = transform.position + new Vector3(x, 0, z);
        //    float degree = -radian * Mathf.Rad2Deg; // ������Ʈ�� �߾��� �ٶ󺸵���
        //    Quaternion rotation = Quaternion.Euler(0, degree, 0);
        //    Instantiate(prefab, pos, rotation);
        //}

        for (int i = 0;i < count; i++)
        {
            for (int j = 1; j < 6; j++)
            {
                float radian = i * Mathf.PI / count;
                float x = Mathf.Cos(radian) * radius;
                float z = Mathf.Sin(radian) * radius;
                Vector3 pos = transform.position + new Vector3(x * j, 0, z * j);
                float degree = -radian * Mathf.Rad2Deg; // ������Ʈ�� �߾��� �ٶ󺸵���
                Quaternion rotation = Quaternion.Euler(0, degree, 0);
                Instantiate(prefab, pos, rotation);
            }
        }
    }
}
