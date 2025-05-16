using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;   //플레이어의 시작체력
    public int currentHealth = 100; //플레이어의 현재체력
    public Slider healthSlider;     //체력 UI와 연결
    public Image damageImage;       //데미지 입을 경우에 대한 이미지
    public AudioClip deathClip;     //플레이어가 피격 시 쓸 오디오

    Animator animator;              //애니메이터
    AudioSource playerAudio;        //오디오 소스
    PlayerMovement playerMovement;  //플레이어 움직임
    bool isDead;                    //죽음 확인용 변수

    private void Awake()
    {
        //컴포넌트 연결
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //체력 설정
        currentHealth = startHealth;
    }


    //플레이어 피격 시 호출할 함수
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
            animator.SetTrigger("Damage"); // TODO : 피격 시의 애니메이션 만들어서 연결해놓기
        }
    }

    void Death()
    {
        isDead = true;
        animator.SetTrigger("Die"); // TODO : 죽었을 때의 애니메이션 만들어서 연결해놓기 
        playerMovement.enabled = false; //PlayerMovement에 대한 비활성화
    }
}
