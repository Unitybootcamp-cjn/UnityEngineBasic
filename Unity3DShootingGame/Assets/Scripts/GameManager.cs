using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public PlayerMovement player;
    public Text hp_text;
    private float typingSpeed = 0.2f;

    public TextMeshProUGUI GOScoreUI;
    public TextMeshProUGUI GOHighScoreUI;

    [SerializeField]
    private GameObject gameOverPanel;

    [HideInInspector]
    public bool isGameOver = false;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
        ScoreManager.instance.SetScoreText();
    }
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hp_text.text = $"HP : {player.hp}";
    }



    public void SetGameOver()
    {
        Time.timeScale = 0f;
        isGameOver = true;
        GOHighScoreUI.text = "HighScore : " + ScoreManager.instance.highScore.ToString();
        GOScoreUI.text = "Score : " + ScoreManager.instance.currentScore.ToString();
        ShowGameOverPanel();
    }
    void SetScoreText()
    {
        StartCoroutine(ScoreSentence(GOScoreUI.text));
        StartCoroutine(HSSentence(GOHighScoreUI.text));
    }
    private IEnumerator ScoreSentence(string sentence)
    {
        GOScoreUI.text = "";
        foreach (char c in sentence)
        {
            Debug.Log(sentence);
            GOScoreUI.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
    private IEnumerator HSSentence(string sentence)
    {
        GOHighScoreUI.text = "";
        foreach (char c in sentence)
        {
            Debug.Log(sentence);
            GOHighScoreUI.text += c;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        SetScoreText();
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonEnter()
    {
#if UNITY_EDITOR // 유니티 에디터 쪽에서의 작업
        UnityEditor.EditorApplication.isPlaying = false;
        //누르면 바로 꺼지는 기능(모바일, 빌드용)
#else
        Application.Quit(); // 현재 비활성화되는 코드가 바로 적용
#endif
    }
}
