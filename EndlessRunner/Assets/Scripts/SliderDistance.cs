using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderDistance : MonoBehaviour
{
    public Transform playerTransform;
    public Transform enemyTransform;
    public PlayerController playerController;

    public float maxDistance = 100f;
    public float minDistance = 1f;

    public Slider dangerBar;

    public float warningDistance = 20f;
    public TextMeshProUGUI distanceText;

    void Update()
    {
        if (playerTransform == null || enemyTransform == null || dangerBar == null)
            return;

        // 두 지점 사이의 실제 거리
        float dist = Vector3.Distance(playerTransform.position, enemyTransform.position);

        // minDistance ~ maxDistance 구간을 1~0 으로 보간
        float t = Mathf.InverseLerp(maxDistance, minDistance, dist);

        // Scrollbar.value 에 적용 (0 ~ 1)
        dangerBar.value = Mathf.Clamp01(t);
        distanceText.text = ((int)dist).ToString();

        if (dist <= warningDistance && !playerController.isDead)
        {
            FindObjectOfType<CameraController>().TriggerShake();
            FindObjectOfType<ScreenFlash>().Flash();
        }
    }
}