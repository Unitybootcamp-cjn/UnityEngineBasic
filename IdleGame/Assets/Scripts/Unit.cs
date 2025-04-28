using UnityEngine;
// 2025-04-28
// 1. Animator, start, SetAnimator  Monster -> Unit

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("플레이어 능력치")]
    // 능력치 관련 자료형은 방치형 기준으로는 데이터를 좀 크게 잡는 편. 그래서 float보다 double을, int보다 long을 선호함
    public double HP;
    public double ATK;
    public double ATK_SPEED;

    [Header("공격 범위")]
    public float A_RANGE; // 사거리
    public float T_RANGE; // 추격 범위


    // virtual : 해당 키워드가 붙은 메소드는 가상 함수로 처리합니다.
    //           가상 함수는 다른 쪽에서 수정할 가능성이 있는 함수에 달아줍니다.
    //           virtual을 쓸 경우, 상속받은 클래스 쪽에서 override 키워드를 통해 기능 검색 가능
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        // 코드 내에서 Animator로 인식하고, Animator의 필드나 메소드를 사용할 수 있다.
    }

    protected void SetAnimator(string temp)
    {
        // 기본 파라미터에 대한 reset(초기화)
        // 유니티 Animator에 만들어둔 parameter의 이름을 정확하게 기재해야한다.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        // 인자로 전달받은 값을 true로 설정합니다.
        animator.SetBool(temp, true);
    }
}
