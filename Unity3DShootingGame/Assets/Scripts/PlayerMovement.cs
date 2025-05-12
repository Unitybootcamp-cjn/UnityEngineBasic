using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public GameObject deadeffect; //���� �� ����Ʈ ���
    Renderer[] renderers;

    private float flickerDuration = 1.0f; //�ǰ� �� ��¦�̴� �ð�
    private float flickerInterval = 0.1f; //��¦�̴� �ð� ���͹�

    public float invincibleDuration = 1f;    // ���� ���� �ð�
    private bool isInvincible = false;       // ���� ������ ����

    public int hp = 5; // ü��

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        transform.Translate(dir * speed * Time.deltaTime);

        #region �ʱ�
        //transform.Translate(Vector3 dir);
        //���� ������Ʈ�� �̵���Ű�� ���� �뵵
        //���� ������Ʈ�� ��ġ�� Vector3 �������� �̵��ϰ� �˴ϴ�.
        //(���� ������ ���õ� ������ ���������� �ʰ� �ܼ��� �̵� ������� ����)
        //--> �⺻���� ������

        //transform.position�� ���� ����ص� ��ġ�� ������Ʈ�� position�� �ٲ� �� �ֽ��ϴ�.
        //-- > �ַ� ��Ż ���� ������ ������ ���� �� ȿ����
        //position�� ���� ������ ������Ʈ���� rigidbody�� ���� �ʴ� �� �����ϴ�.
        //(�ݶ��̴����� rigidbody�� ������� ��ġ�� �����ؾ��ϴ� ��찡 �߻��մϴ�.)

        //Rigidbody�� ���� ������Ʈ�� ���� ������ �����ؼ� �浹, ��, �߷� ����
        //�������� ��ȣ�ۿ��� �����ϰ� ���ִ� ������Ʈ�Դϴ�.

        //Rigidbody.AddForce(Vector3 dir, ForceMode mode);
        //�������� ������ ���� �������� �����ϰ�, ���� �ִ� ������ ���� ���������� ó���� ��,
        //�������� ���� �������� ó���� �� �ֽ��ϴ�.
        #endregion

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isInvincible) return; 

        hp -= 1;
        StartCoroutine(InvincibleCoroutine());
        StartCoroutine(FlickerAll());
        if (hp <= 0)
        {
            Destroy(gameObject);
            var Deadexplosion = Instantiate(deadeffect);
            Deadexplosion.transform.position = transform.position;
        }
    }

    // ���� �ڷ�ƾ
    private IEnumerator InvincibleCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibleDuration);
        isInvincible = false;
    }


    // �ǰ� �� �� ��¦�̴� �ڷ�ƾ
    IEnumerator FlickerAll()
    {
        float elapsed = 0f;

        while (elapsed < flickerDuration)
        {
            SetRenderersVisible(false);
            yield return new WaitForSeconds(flickerInterval);
            SetRenderersVisible(true);
            yield return new WaitForSeconds(flickerInterval);
            elapsed += flickerInterval * 2;
        }
    }

    //�ǰ� �� �� ��¦�̴� �Լ�
    void SetRenderersVisible(bool isVisible)
    {
        foreach (var rend in renderers)
        {
            rend.enabled = isVisible;
        }
    }
}
