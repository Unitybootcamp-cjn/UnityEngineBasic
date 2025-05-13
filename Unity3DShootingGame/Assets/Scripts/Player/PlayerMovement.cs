using System;
using System.Collections;
using UnityEditor.TerrainTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public GameObject deadeffect; //���� �� ����Ʈ ���
    Renderer[] renderers;
    Renderer renderer;


    private float flickerDuration = 1.0f; //�ǰ� �� ��¦�̴� �ð�
    private float flickerInterval = 0.1f; //��¦�̴� �ð� ���͹�

    public float invincibleDuration = 1f;    // ���� ���� �ð�
    private bool isInvincible = false;       // ���� ������ ����

    public int hp = 5; // ü��

    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        transform.position = Vector3.zero;
        renderer = GetComponent<Renderer>();
        if (CharacterManager.instance != null )
            renderer.material = CharacterManager.instance.CharacterMaterial[CharacterManager.instance.materialIndex];
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = - Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);
        transform.Translate(dir * speed * Time.deltaTime);

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, -3.4f, 3.4f);
        pos.y = Mathf.Clamp(pos.y, -4.0f, 6.0f);

        transform.position = pos;
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

    // �浹 ��
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        int coinLayer = LayerMask.NameToLayer("Coin");
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (other.layer == coinLayer)
        {
            ScoreManager.instance.Score += 200;
            ScoreManager.instance.coin += 1;
            ScoreManager.instance.SetScoreText();
        }
        else if (other.layer == enemyLayer)
        {
            if (isInvincible) return;

            hp -= 1;
            StartCoroutine(InvincibleCoroutine());
            StartCoroutine(FlickerAll());
            // ����. ���ӿ���
            if (hp <= 0)
            {
                Destroy(gameObject);
                var Deadexplosion = Instantiate(deadeffect);
                Deadexplosion.transform.position = transform.position;
                GameManager.instance.SetGameOver();
            }
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
