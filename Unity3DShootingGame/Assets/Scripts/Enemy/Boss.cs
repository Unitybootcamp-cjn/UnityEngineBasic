using System;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Action onDead;

    public void Die()
    {
        onDead?.Invoke();
        gameObject.SetActive(false);
    }


}
