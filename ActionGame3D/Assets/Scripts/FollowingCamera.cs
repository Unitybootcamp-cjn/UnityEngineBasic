using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public float distanceAway = 7.0f; //맵 범위
    public float distanceUp = 1.0f;   //y축

    public Transform target; //플레이어
    private void LateUpdate()
    {
        //카메라 위치 = 타겟 위치 + 위 - 앞
        if(target != null)
        {
            transform.position = target.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
        }
    }
}
