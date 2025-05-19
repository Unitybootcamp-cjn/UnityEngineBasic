using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("������ ��ġ")]
    [Tooltip("�Ϲ� ���� ������")] public int NormalDamage = 10;
    [Tooltip("��ų ���� ������")] public int SkillDamage = 20;
    [Tooltip("��� ���� ������")] public int DashDamage = 30;

    [Header("Ÿ��")]
    public NormalTarget normalTarget;
    public SkillTarget skillTarget;

    public void NormalAttack()
    {
        var targetList = new List<Collider>(normalTarget.targetList);

        foreach (var target in targetList)
        {
            var enemy = target.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                StartCoroutine(enemy.StartDamage(NormalDamage, transform.position, 0.5f, 0.5f));
            }
        }

    }

    public void SkillAttack()
    {
        var targetList = new List<Collider>(skillTarget.targetList);

        foreach (var target in targetList)
        {
            var enemy = target.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                StartCoroutine(enemy.StartDamage(SkillDamage, transform.position, 1f, 2f));
            }
        }
    }

    public void DashAttack()
    {
        var targetList = new List<Collider>(skillTarget.targetList);

        foreach (var target in targetList)
        {
            var enemy = target.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                StartCoroutine(enemy.StartDamage(DashDamage, transform.position, 1f, 2f));
            }
        }
    }
}
