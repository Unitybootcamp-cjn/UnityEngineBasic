using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject effect; //¿Ã∆Â∆Æ µÓ∑œ


    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime;
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
