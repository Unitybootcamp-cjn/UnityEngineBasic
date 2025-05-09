using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class ScreenFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashSpeed = 1f;

    private bool isFlashing = false;


    public void Flash()
    {
        if (!isFlashing)
            StartCoroutine(DoFlash());
    }

    IEnumerator DoFlash()
    {
        isFlashing = true;

        float alpha = 0f;

        // ���� ���ϰ�
        while (alpha < 0.5f)
        {
            alpha += Time.deltaTime * flashSpeed;
            flashImage.color = new Color(1, 0, 0, alpha);
            yield return null;
        }

        // ���� �������
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * flashSpeed;
            flashImage.color = new Color(1, 0, 0, alpha);
            yield return null;
        }

        isFlashing = false;
    }
}