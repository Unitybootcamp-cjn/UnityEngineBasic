using UnityEngine;
using UnityEngine.UI; //text ����� ���� �߰�
using TMPro;          //textMeshPro ����� ���� �߰�

public class TextSample : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public Text text;

    void Start()
    {
        tmp.text = "���� ���� ��";
        text.text = "�ϳ� ����";
    }

    void Update()
    {
            
    }
}
