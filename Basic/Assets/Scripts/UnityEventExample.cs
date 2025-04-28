using UnityEngine;
using UnityEngine.Events;

// ����Ƽ���� ����� �� �ִ� �븮�� ��� �� �ϳ�

// Action�̳� Func�� C# ������ �븮��
// �� ������ �ν����Ϳ� ������� ����.

public class UnityEventExample : MonoBehaviour
{
    public UnityEvent onSample;

    private void Update()
    {
        // onSample�� ������ ����� ��ϵ� ���¿��� AŰ�� ������ ���
        if(Input.GetKeyDown(KeyCode.A) && onSample != null)
        {
            // �� ����� �����Ѵ�.
            onSample.Invoke();
        }
    }
}
