using UnityEngine;

public class DestroyZone : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        other.gameObject.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
