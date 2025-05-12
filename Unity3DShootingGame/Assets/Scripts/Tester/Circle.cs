using UnityEngine;

//오브젝트를 원의 형태로 배치하는 코드

public class Circle : MonoBehaviour
{
    public GameObject prefab;
    public int count = 20;
    public float radius = 5.0f;

    private void Start()
    {
        ////count만큼 실행
        //for (int i = 0; i < count; i++)
        //{
        //    float radian = i * Mathf.PI * 2 / count;
        //    float x = Mathf.Cos(radian) * radius;
        //    float z = Mathf.Sin(radian) * radius;
        //    Vector3 pos = transform.position + new Vector3(x, 0, z);
        //    float degree = -radian * Mathf.Rad2Deg; // 오브젝트가 중앙을 바라보도록
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
                float degree = -radian * Mathf.Rad2Deg; // 오브젝트가 중앙을 바라보도록
                Quaternion rotation = Quaternion.Euler(0, degree, 0);
                Instantiate(prefab, pos, rotation);
            }
        }
    }
}
