using System.Collections;
using UnityEngine;

// ������Ʈ(Component)
// ����Ƽ ������Ʈ�� ����� ���
// �����Ǵ� ������Ʈ�� �ְ�,
// ��ũ��Ʈ�� ���� ����ڰ� ������ִ� ����� ���� ������Ʈ�ν� Ȱ���� �����ϴ�.(Mono ���)

// Monobehavior ���
// 1. ����Ƽ ������Ʈ�� �ش� Ŭ������ ������Ʈ�ν� ����� �� �ֽ��ϴ�.

// ���� Ŭ�������� ��Ȳ�� �°� �ִϸ��̼��� �����Ű�� �մϴ�.
// �̶� �ʿ��� �����ʹ� �����ϱ��? 2
// 1. Animation
// 2. Animator

public class Monster : MonoBehaviour
{
    // Range�� ����Ƽ �ν����Ϳ� �ش� �ʵ� ���� ���� ���� ����
    [Range(1,5)] public float speed;

    Animator animator;


    bool isSpawn = false; // ���� ����

    // ���Ͱ� �������� �� ������ �۾�(����)
    // ������ Ŀ���� ����
    IEnumerator OnSpawn()
    {
        float current = 0.0f; // ������ �� �����
        float percent = 0.0f; // �ִ� 1, �ݺ����� ���� ����
        float start = 0.0f; // ��ȭ ���� ��
        float end = transform.localScale.x; // ��ȭ ��������
        // localScale�� ���� ������Ʈ�� ������� ũ�⸦ �ǹ��մϴ�.
        // ����� ������Ʈ�� ũ��� ����մϴ�.

        while(percent < 1.0f)
        {
            current += Time.deltaTime;
            percent = current / 3.0f;

            // start���� end �������� percent �������� �̵��ض�.
            var pos = Mathf.Lerp(start, end, percent);
            // ����� ��ġ��ŭ ������(ũ��)�� �����մϴ�.
            transform.localScale = new Vector3(pos, pos, pos);
            // Ż���ߴٰ� ���ƿɴϴ�.
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        isSpawn = true;
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
        // �ڵ� ������ Animator�� �ν��ϰ�, Animator�� �ʵ峪 �޼ҵ带 ����� �� �ִ�.

        StartCoroutine(OnSpawn());
    }

    // ����Ƽ ������ ����Ŭ �Լ�
    private void Update()
    {
        if(isSpawn == false)
            return;

        transform.LookAt(Vector3.zero); // ������ �ٶ󺸱�

        var distance = Vector3.Distance(transform.position, Vector3.zero); // ���� ��ġ�� ���������� �Ÿ� ����
        // ������ ���غ��� ���� �Ÿ��� ������
        if(distance <= 0.5f)
        {
            SetAnimator("isIDLE"); // ����ϴ� �ִϸ��̼����� ����.
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, Time.deltaTime * speed); // ������ ���� �̵�
            SetAnimator("isMOVE"); // �����̴� �ִϸ��̼����� ����.
        }
        #region �ʱ�
        // 1. transform.position : ���� ������Ʈ�� ��ġ�� ��Ÿ����.
        // 2. Vector3 : 3D ȯ���� ��ǥ�� (X,Y,Z ��) ����
        // 3. MoveTowards(start, end, speed); start���� end�������� speed ��ġ��ŭ �̵��մϴ�.
        // 4. Time.deltaTime : ���� �������� �Ϸ�Ǳ���� �ɸ� �ð�
        //                     (��ǻ���� ������ �������� ���� Ŀ��)
        //                     �Ϲ������� �� 1��
        //                     ������Ʈ���� �۾��� �ϴµ��� �־� ������ ������ ��
        // Debug.Log(Time.deltaTime); // ���� 0.005 ������ ���ڰ� ��� ����
        // 5. transform.LookAt(Vector3 position) : Ư�� ������ �ٶ󺸰� �������ִ� ���


        // ���� ���� : �⺻������ �������ִ� Vector ��
        // Vector3.right == new Vector3(1,0,0);
        // Vector3.left == new Vector3(-1,0,0);
        // Vector3.up == new Vector3(0,1,0);
        // Vector3.down == new Vector3(0,-1,0);
        // Vector3.forward == new Vector3(0,0,1);
        // Vector3.back == new Vector3(0,0,-1);
        // Vector3.zero == new Vector3(0,0,0);
        // Vector3.one == new Vector3(1,1,1);
        #endregion
    }

    private void SetAnimator(string temp)
    {
        // �⺻ �Ķ���Ϳ� ���� reset(�ʱ�ȭ)
        // ����Ƽ Animator�� ������ parameter�� �̸��� ��Ȯ�ϰ� �����ؾ��Ѵ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMOVE", false);

        // ���ڷ� ���޹��� ���� true�� �����մϴ�.
        animator.SetBool(temp, true);
    }
}
