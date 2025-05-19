using System.Collections.Generic;
using UnityEngine;

public class NormalTarget : MonoBehaviour
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

    //로직 연산(Update) 이후에 잘못된 참조에 대한 유효성 검사
    //오류 방지를 위함. 타겟 검색에 대한 안전함, 유니티 구조 상 콜라이더나 오브젝트 같은 컴포넌트들이
    //Destroy될 때 이벤트 함수가 호출이 안되는 타이밍이 존재할 수 있음.
    private void LateUpdate()
    {
        targetList.RemoveAll(target => target == null);
    }
}
