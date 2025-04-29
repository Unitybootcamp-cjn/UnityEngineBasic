using UnityEngine;

// T Singleton, MonoSingleton
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                //������ Ÿ�Կ� �´� ������Ʈ�� ã�Ƽ� �� ���� return

                //ã�� ����� null�� ���
                if(_instance == null)
                {
                    GameObject go = new GameObject(); // �� ������Ʈ ����
                    go.name = typeof(T).Name; //�������� ���·� �̸� ����
                    go.AddComponent<T>(); //���� ������Ʈ�� ������Ʈ�� �߰��ϴ� ��ũ��Ʈ
                    DontDestroyOnLoad(go);
                }
            }

            return _instance;
        }
    }

    //����Ƽ���� Awake �� ��� �߰�
    public virtual void Awake()
    {
        if(_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
