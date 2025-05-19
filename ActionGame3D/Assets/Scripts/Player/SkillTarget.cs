using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
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
    private void LateUpdate()
    {
        targetList.RemoveAll(target => target == null);
    }
}
