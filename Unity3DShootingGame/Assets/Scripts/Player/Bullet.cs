using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;

    public int damage = 1;

    void Update()
    {

        Vector3 dir = Vector3.up;

        //transform.Translate(dir * speed * Time.deltaTime);
        transform.position += dir * speed * Time.deltaTime;

    }
    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    
}
