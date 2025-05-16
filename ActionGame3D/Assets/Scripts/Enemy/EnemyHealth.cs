using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startHealth = 100;   //���� ü��
    public int currentHealth = 100; //���� ü��

    public float flashSpeed = 5.0f;                     //�� ���� ����
    public Color flashColor = new Color(1, 0, 0, 0.1f); //������
    public float sinkSpeed = 1.0f;                      //������ �Ʒ��� ������� �ӵ�

    bool isDead, isSinking, damaged;//�� ���¿� ���� bool ��

    private void Awake()
    {
        currentHealth = startHealth;
    }

    private void Update()
    {
        //������ ó���� ���� �������� ���� �����ϴ� �ڵ�
        if (damaged)
        {
            //transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", flashColor);
            transform.GetChild(0).GetComponent<Renderer>().material.color = flashColor;
        }
        else
        {
            transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;

            //transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.Lerp(
            //transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_OutlineColor"), Color.black, flashSpeed * Time.deltaTime));
        }
        damaged = false;

        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    //�������� ������ ������ �÷��̾�κ��� ƨ�ܳ����� ȿ�� ����
    public IEnumerator StartDamage(int damage, Vector3 playerPosition, float delay, float pushBack)
    {
        yield return new WaitForSeconds(delay);

        try
        {
            //������ �Լ� ����
            TakeDamage(damage);

            //�÷��̾�κ��� �־��� ���� ���
            Vector3 diff = (playerPosition - transform.position).normalized;

            //AddForce(��ġ, ���); �� ���� �������� ���� ���մϴ�.
            GetComponent<Rigidbody>().AddForce(diff * 50f * pushBack, ForceMode.Impulse);
        }
        catch(MissingReferenceException e) //��ü ������ ��ȿ���� ���� ��Ȳ�� ���� �ȳ���
        {
            Debug.Log(e.ToString()); //���� �޼����� ����׷� �����
        }
    }

    void Death()
    {
        isDead = true;
        //�׾��� ��, ���� �հ� �Ʒ��� ����ɴ� ó���� �����ϱ� ���� ó��
        transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        StartSinking();
    }

    void StartSinking()
    {
        //�׺�޽� ������ ���ڽ��ϴ�.
        //�ʰ� ��ֹ� ���� �Ǵ��Ͽ� �÷��̾� ���� ���� ������ �� ����� ������Ʈ
        GetComponent<NavMeshAgent>().enabled = false;
        //���� ���꿡 ���� ó���� ���� �ʰڽ��ϴ�.
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, 2.0f);
    }
}
