using UnityEngine;
//�̱��� ����(Singleton Pattern)
//���� �ѹ� ��ü�� '����'�ϰ�, �� �Ŀ��� �������� �ʰ� �� ��ü�� ����ϵ��� 
//���α׷� ��ü���� �������� ���� ������ ��ü�� ����� �ִ� ��

//[����]
// ���ʿ��ϰ� ���� ��ü�� ������ ���� �ʿ䰡 ������.(�޸� ���� ���� ��)

//[������]
//1. �������� �׽�Ʈ�� �����ϴµ����� �������մϴ�.
//2. �̱����� �ν��Ͻ��� ����� ���, �ش� �ν��Ͻ��� �����ϴ� ��� Ŭ������ ���� ������ ����� �� ����.
//3. ����� �� �����.

//��� : �̱������� ��Ծ �ɹ��� ���� �������.
//      ex) ����Ƽ�� �Ŵ��� �ڵ�


public class GameManager : MonoBehaviour
{
    //������Ƽ(Property)�� ������ �̱���

    //1. private static Ŭ������ instance
    private static GameManager instance = null;

    //2. ������Ƽ�� ����
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }

    //3. ����� �ʵ� / �޼ҵ� ������ֱ�
    public int value = 1;

    //���� ���� ����
    private void Awake()
    {
        //null�� ���� üũ�� �����ؼ� null�̸� ���, �ƴϸ� �ı�

        //�̹� �����ϰ� �ִ� ���
        if(Instance != null)
        {
            Destroy(gameObject); //�� �� �̻��� ��� ����
            return;
        }
        //�ν��Ͻ��� �ڽ��� �ʱ�ȭ
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}

public class Test
{
    void Use()
    {
        //�Ŵ����� ����� �����ͼ� ����� �����մϴ�.
        GameManager.Instance.value++;
    }
}


public class BasicSingleton
{
    private static BasicSingleton instance;

    private BasicSingleton()
    {
        // new�� ���� ������ ���� �ʰ� ���� ����
    }

    // ���� ���� �޼ҵ带 ���� �̱��� ���� �� return
    public static BasicSingleton GetInstance()
    {
        if(instance == null)
        {
            instance = new BasicSingleton();
        }
        return instance;
    }
}