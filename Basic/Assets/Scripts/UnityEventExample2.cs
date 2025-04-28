using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnityEventExample2 : MonoBehaviour
{
    public UnityEvent Onquest;
    public Text clear_count_text; // Ŭ���� ����
    public Text current_count_text; // ���� ����
    public Text message; // �޼���

    public int clear_count = 10;
    public int current_count = 0;

    void countPlus() => current_count++; // ���� ����(1��)

    void countText()
    {
        current_count_text.text = $"{current_count} ��";
        message.text = $"[����Ʈ ���� ��...] ���� ���� {current_count} / {clear_count} ��";
    }

    void QuestClear()
    {
        if(current_count == clear_count)
        {
            clear_count_text.text = "��";
            current_count_text.text = "��";
            message.text = "����Ʈ �Ϸ�";
            clear_count_text.color = Color.gray;
            current_count_text.color = Color.gray;
            message.color = Color.cyan;
            Onquest.RemoveAllListeners(); // ��ϵ� ��� ���� ����
            Onquest = null;
        }
        else
        {
            countText();
        }
    }

    void Start()
    {
        // AddListener�� �߰��� �̺�Ʈ�� �ν����Ϳ����� Ȯ���� �Ұ��մϴ�.
        Onquest.AddListener(countPlus);
        // ����Ƽ �̺�Ʈ�� countPlus�� ����մϴ�.
        Onquest.AddListener(QuestClear);
    }

    void Update()
    {
        if(Onquest != null)
        {

            if (Input.GetKeyDown(KeyCode.S))
            {
                Onquest.Invoke();
            }
        }
        else
        {
            Debug.Log("��ϵ� �����ʰ� �����ϴ�.");
            //�񿵱� ������ : ��ũ��Ʈ�� ���ؼ� (AddListener)�� ���ؼ� �߰��� ������
            //                RemoveListener�� ���ؼ� ���Ű� �����մϴ�.
            //���� ������   : �ν����͸� ���ؼ� �߰��� ������
            //                �̰� �ν����͸� ���� ���� ������ �մϴ�.
        }
    }
}
