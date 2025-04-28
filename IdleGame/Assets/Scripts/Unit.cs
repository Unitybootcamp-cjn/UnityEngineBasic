using UnityEngine;
// 2025-04-28
// 1. Animator, start, SetAnimator  Monster -> Unit

public class Unit : MonoBehaviour
{
    Animator animator;
    [Header("�÷��̾� �ɷ�ġ")]
    // �ɷ�ġ ���� �ڷ����� ��ġ�� �������δ� �����͸� �� ũ�� ��� ��. �׷��� float���� double��, int���� long�� ��ȣ��
    public double HP;
    public double ATK;
    public double ATK_SPEED;

    [Header("���� ����")]
    public float A_RANGE; // ��Ÿ�
    public float T_RANGE; // �߰� ����


    // virtual : �ش� Ű���尡 ���� �޼ҵ�� ���� �Լ��� ó���մϴ�.
    //           ���� �Լ��� �ٸ� �ʿ��� ������ ���ɼ��� �ִ� �Լ��� �޾��ݴϴ�.
    //           virtual�� �� ���, ��ӹ��� Ŭ���� �ʿ��� override Ű���带 ���� ��� �˻� ����
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        // �ڵ� ������ Animator�� �ν��ϰ�, Animator�� �ʵ峪 �޼ҵ带 ����� �� �ִ�.
    }

    protected void SetAnimator(string temp)
    {
        // �⺻ �Ķ���Ϳ� ���� reset(�ʱ�ȭ)
        // ����Ƽ Animator�� ������ parameter�� �̸��� ��Ȯ�ϰ� �����ؾ��Ѵ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        // ���ڷ� ���޹��� ���� true�� �����մϴ�.
        animator.SetBool(temp, true);
    }
}
