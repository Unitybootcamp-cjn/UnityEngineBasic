using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;   //�÷��̾��� ����ü��
    public int currentHealth = 100; //�÷��̾��� ����ü��
    public Slider healthSlider;     //ü�� UI�� ����
    public Image damageImage;       //������ ���� ��쿡 ���� �̹���
    public AudioClip deathClip;     //�÷��̾ �ǰ� �� �� �����

    Animator animator;              //�ִϸ�����
    AudioSource playerAudio;        //����� �ҽ�
    PlayerMovement playerMovement;  //�÷��̾� ������
    bool isDead;                    //���� Ȯ�ο� ����

    private void Awake()
    {
        //������Ʈ ����
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //ü�� ����
        currentHealth = startHealth;
    }


    //�÷��̾� �ǰ� �� ȣ���� �Լ�
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            animator.SetTrigger("Damage"); // TODO : �ǰ� ���� �ִϸ��̼� ���� �����س���
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die"); // TODO : �׾��� ���� �ִϸ��̼� ���� �����س��� 
        playerMovement.enabled = false; //PlayerMovement�� ���� ��Ȱ��ȭ
    }
}
