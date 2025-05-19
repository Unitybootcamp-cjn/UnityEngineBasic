using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    //공격 타겟에 대한 리스트
    public List<Collider> targetList;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    //몬스터가 공격 반경으로 들어오면, 리스트 추가
    private void OnTriggerEnter(Collider other)
    {
        if (!targetList.Contains(other) && other.CompareTag("Enemy"))
            targetList.Add(other);
    }


    //몬스터가 공격 반경에서 벗어나면, 리스트 제거
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
