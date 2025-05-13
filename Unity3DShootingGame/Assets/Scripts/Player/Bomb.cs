using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 5;

    public int damage = 3;

    void Update()
    {

        Vector3 dir = Vector3.up;

        //transform.Translate(dir * speed * Time.deltaTime);
        transform.position += dir * speed * Time.deltaTime;

    }
}
