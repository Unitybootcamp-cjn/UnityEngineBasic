using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour
{
    //���� Ÿ�ٿ� ���� ����Ʈ
    public List<Collider> targetList;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    //���Ͱ� ���� �ݰ����� ������, ����Ʈ �߰�
    private void OnTriggerEnter(Collider other)
    {
        if (!targetList.Contains(other) && other.CompareTag("Enemy"))
            targetList.Add(other);
    }


    //���Ͱ� ���� �ݰ濡�� �����, ����Ʈ ����
    private void OnTriggerExit(Collider other)
    {
        if (targetList.Contains(other) && other.CompareTag("Enemy"))
            targetList.Remove(other);
    }

    //���� ����(Update) ���Ŀ� �߸��� ������ ���� ��ȿ�� �˻�
    //���� ������ ����. Ÿ�� �˻��� ���� ������, ����Ƽ ���� �� �ݶ��̴��� ������Ʈ ���� ������Ʈ����
    //Destroy�� �� �̺�Ʈ �Լ��� ȣ���� �ȵǴ� Ÿ�̹��� ������ �� ����.
    private void LateUpdate()
    {
        targetList.RemoveAll(target => target == null);
    }
}
