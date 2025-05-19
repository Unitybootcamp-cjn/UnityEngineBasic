using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    public static StageController Instance; //매니저 정적 변수

    public int StagePoint = 0; //점수

    public Text PointText;   //점수를 표현할 UI
    public Image fade_Image;
    public float duration = 5.0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (fade_Image != null)
        {
            fade_Image.gameObject.SetActive(true);
            Color c = fade_Image.color;
            fade_Image.color = new Color(c.r, c.g, c.b, 1.0f);
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0, 1));
    }


    IEnumerator Fade(float start, float end)
    {
        float time = 0.0f;
        Color s = new Color(0, 0, 0, start);
        Color e = new Color(0, 0, 0, end);

        while (time < duration)
        {
            fade_Image.color = Color.Lerp(s, e, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        fade_Image.color = e;
    }



    //점수가 증가하면, 텍스트 UI에 수치를 적용
    public void AddPoint(int Point)
    {
        StagePoint += Point;
        PointText.text = Point.ToString();
    }

    //씬에 대한 리로드
    public void FinishGame()
    {
        StartCoroutine(FadeOut());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}