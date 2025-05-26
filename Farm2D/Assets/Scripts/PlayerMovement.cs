using System;
using UnityEngine;


[Serializable]
public class PlayerStat
{
    public float speed; //플레이어의 이동 속도
    //public int count_of_harvest; //현재 수확물의 갯수
}


public class PlayerMovement : MonoBehaviour
{
    public PlayerStat stat;
    Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private Vector2 last = Vector2.down;

    void SetAnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if (direction.magnitude > 0)
            {
                animator.SetBool("isMove", true);

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                {
                    animator.SetBool("isMove", false);
                }
            }
        }
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector2(h, v);
        SetAnimateMovement(dir);

        transform.position += dir * stat.speed * Time.deltaTime;
    }
}
