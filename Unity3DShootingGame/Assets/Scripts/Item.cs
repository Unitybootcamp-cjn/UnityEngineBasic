using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject effect; //����Ʈ ���

    float speed = 1f;
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        int playerLayer = LayerMask.NameToLayer("Player");

        if (other.layer == playerLayer)
        {
            var explosion = Instantiate(effect);
            explosion.transform.position = transform.position;
            Destroy(gameObject);
        }

    }
}
